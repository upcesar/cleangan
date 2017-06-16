﻿'use strict';
app.controller('summaryController', summaryController);

summaryController.$inject = ['$scope', '$q', '$http', '$location', 'authService', '$routeParams', 'questionService'];

function summaryController($scope, $q, $http, $location, authService, $routeParams, questionService) {

    $scope.user = JSON.parse(window.localStorage.getItem("ls.authorizationData"));

    $scope.summaryData = [];

    var getSummaryData = function () {
        return questionService.GetSummary()
                .then(function (surveySummary) {
                    $scope.summaryData = surveySummary.data;
                });
    };

    $scope.finishSignature = {
        agree: false,
        fullName: "",
        signDate: new Date(),
        digitalSingature: "",
        valid: false
    };

    $scope.finishingSurvey = false;

    $scope.confirmFinishOptions = {
        title: "Terms & Conditions",
        templateUrl: 'app/views/term-conditions.html',
        scope: $scope,
        buttons: {
            back: {
                label: "Back",
                className: "btn-back btn-default"
            },
            confirm: {
                label: "Confirm",
                className: "btn-confirm btn-green disabled",
                callback: function () {
                    var x = $scope.$apply(function () {
                        $scope.doFinish($scope.finishSignature);
                    });

                    return true;
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
    }

    getSummaryData();

}