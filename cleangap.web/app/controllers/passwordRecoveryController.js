'use strict';
app.controller('passwordRecoveryController', ['$scope', '$window', '$timeout', '$location', 'authService', 'ngAuthSettings', function ($scope, $window, $timeout, $location, authService, ngAuthSettings) {

    $window.document.getElementById("inputEmail").focus();

    $scope.email = "";
    $scope.message = "";

    $scope.alertType = "alert-danger";

    $scope.sendingData = false;
    $scope.sentData = false;

    $scope.hideMsg = function () {

        if (!$scope.sendingData)
            $scope.sentData = false;
    };

    $scope.sendRecoveryInstruction = function () {

        authService.logOut();

        $scope.sendingData = true;
        $scope.sentData = false;
        $scope.alertType = "alert-danger";

        authService.sendPasswordRecovery($scope.email).then(function (response) {

            $scope.message = response.message;
            $scope.savedSuccessfully = response.isSuccess;

            if (response.isSuccess)
                $scope.alertType = "alert-success";

            $scope.sentData = true;
            $scope.sendingData = false;

        },
         function (err) {

             $cope.message = err.error_description;

             $scope.sendingData = false;
             $scope.sentData = true;

         });
    };

}]);
