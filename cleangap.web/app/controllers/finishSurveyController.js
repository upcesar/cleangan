'use strict';
app.controller('finishSurveyController', finishSurveyController);

finishSurveyController.$inject = ['$scope', '$q', '$http', '$location', 'authService', '$routeParams', 'questionService'];

function finishSurveyController($scope, $q, $http, $location, authService, $routeParams, questionService) {

    $scope.finishSurvey = {
        agree : false,
        name: "",
        date: "",
        signature: "",
        valid : false
    };

    $scope.finishing = false;

    $scope.validateName = function () {
        return ($scope.finishSurvey.name !== undefined && $scope.finishSurvey.name !== null && $scope.finishSurvey.name !== "");
    };

    $scope.validateDate = function () {
        return ($scope.finishSurvey.date !== undefined && $scope.finishSurvey.date !== null && $scope.finishSurvey.date !== "");
    };

    $scope.validateSignature = function () {
        return ($scope.finishSurvey.signature !== undefined && $scope.finishSurvey.signature !== null && $scope.finishSurvey.signature !== "");
    };

    $scope.validateTermCondition = function () {

        $scope.finishSurvey.valid = $scope.finishSurvey.agree
                                 && $scope.validateName()
                                 && $scope.validateDate()
                                 && $scope.validateSignature();

        if ($scope.finishSurvey.valid) {
            $(".btn-confirm").removeClass("disabled");
        } else {
            $(".btn-confirm").addClass("disabled");
        }
    };
}
