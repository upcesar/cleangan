'use strict';
app.controller('finishSurveyController', finishSurveyController);

finishSurveyController.$inject = ['$scope', '$q', '$http', '$location', 'authService', '$routeParams', 'questionService'];

function finishSurveyController($scope, $q, $http, $location, authService, $routeParams, questionService) {

    $scope.validateName = function () {
        return ($scope.finishSignature.fullName !== undefined && $scope.finishSignature.fullName !== null && $scope.finishSignature.fullName !== "");
    };

    $scope.validateDate = function () {
        return ($scope.finishSignature.signDate !== undefined && $scope.finishSignature.signDate !== null && $scope.finishSignature.signDate !== "");
    };

    $scope.validateSignature = function () {
        return ($scope.finishSignature.digitalSingature !== undefined && $scope.finishSignature.digitalSingature !== null && $scope.finishSignature.digitalSingature !== "");
    };

    $scope.validateTermCondition = function () {

        $scope.finishSignature.valid = $scope.finishSignature.agree
                                 && $scope.validateName()
                                 && $scope.validateDate()
                                 && $scope.validateSignature();

        if ($scope.finishSignature.valid) {
            $(".btn-confirm").removeClass("disabled");
        } else {
            $(".btn-confirm").addClass("disabled");
        }
    };

    $scope.validateTermCondition();

}
