'use strict';
app.controller('summaryController', summaryController);

summaryController.$inject = ['$scope', '$q', '$http', '$location', 'authService', '$routeParams', 'questionService'];

function summaryController($scope, $q, $http, $location, authService, $routeParams, questionService) {

    $scope.summaryData = {
        currentAnswer : "Sap",
        newAnswer : ""
    };

    $scope.setNewValues = function (newValue) {
        $scope.summaryData.currentAnswer = newValue;
    };

    $scope.getNewValue = function () {
        $scope.summaryData.newAnswer = $scope.summaryData.currentAnswer;
    }

    $scope.editAnswerDialog = {
        title: "Edit Answer",
        templateUrl: 'app/views/edit-answer.html',
        scope: $scope,
        buttons: {
            cancel: {
                label: "Cancel",
                className: "btn-back btn-default"
            },
            ok: {
                label: "Ok",
                className: "btn-confirm btn-green",
                callback: function () {
                    var x = $scope.$apply(function () {
                        $scope.setNewValues($scope.summaryData.newAnswer);                        
                    });

                    return true;
                }
            }
        }
    };

}