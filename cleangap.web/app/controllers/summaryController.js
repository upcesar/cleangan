'use strict';
app.controller('summaryController', summaryController);

summaryController.$inject = ['$scope', '$q', '$http', '$location', 'authService', '$routeParams', 'questionService'];

function summaryController($scope, $q, $http, $location, authService, $routeParams, questionService) {

    $scope.user = JSON.parse(window.localStorage.getItem("ls.authorizationData"));

    $scope.loadingData = true;

    $scope.showData = function () {
        $scope.loadingData = false;
    };

    $scope.summaryData = [];

    $scope.objSummary = {
        previousPage : 1,
        nextPage : 1,
        numPages: 0,
        isBusy : false,
        surveysItems: []
    };

    var getSummaryData = function () {

        var page = $scope.objSummary.nextPage;
        

        if (page !== null && !$scope.objSummary.isBusy) {
            $scope.objSummary.isBusy = true;
            return questionService.GetSummary(page, $scope.objSummary.isBusy)
                    .then(function (surveySummary) {
                        var summaryData = surveySummary.data;

                        if (summaryData.surverysItems !== null) {
                            $scope.objSummary.previousPage = summaryData.previousPage;
                            $scope.objSummary.nextPage = summaryData.nextPage;
                            $scope.objSummary.numPages = summaryData.numPages;

                            angular.forEach(summaryData.surveysItems, function (value, key) {
                                $scope.objSummary.surveysItems.push(value);
                            });

                            $scope.objSummary.isBusy = false;
                        }
                    });
        }
            
    };

    $scope.initSignature = function () {
        $scope.finishSignature = {
            agree: false,
            fullName: "",
            signDate: new Date(),
            digitalSingature: "",
            valid: false
        };
    }

    $scope.initSignature();
    

    $scope.finishingSurvey = false;

    $scope.confirmFinishOptions = {
        title: "Terms & Conditions",
        templateUrl: 'app/views/term-conditions.html',
        controller: 'finishSurveyController',
        scope: $scope,
        buttons: {
            cancel: {
                label: "Cancel",
                className: "btn-back btn-default",
                callback: function () {
                    $scope.$apply(function () {
                        $scope.initSignature();
                    });

                    return true;
                }
            },
            confirm: {
                label: "Confirm",
                className: "btn-confirm btn-green disabled",
                callback: function () {
                    $scope.$apply(function () {
                        $scope.doFinish($scope.finishSignature);
                    });

                    return !$(".btn-confirm").hasClass("disabled");
                }
            }
        }
    };

    $scope.doFinish = function (obj) {
        var isDisabledBtn = $(".btn-confirm").hasClass("disabled");

        if (!($scope.finishingSurvey || isDisabledBtn)) {
            $scope.finishingSurvey = true;

            var result = questionService.Finish(obj).then(function (response) {
                var data = response.data;
                if (response.data.isSuccess) {
                    $scope.finishingSurvey = false;
                    $scope.logOut();
                }
            }, function (err) {
                $scope.finishingSurvey = false;
            });
        }
    };

    $scope.back = function () {
        var lastPage = $scope.summaryData.length > 0 ? $scope.summaryData[0].pageTotal : 1;
        $location.path('/survey/' + lastPage);
    };

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
    };

    $scope.loadMore = function () {
        getSummaryData();
    };

    getSummaryData();

}