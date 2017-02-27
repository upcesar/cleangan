'use strict';
app.controller('passwordRecoveryController', ['$scope', '$window', '$timeout', '$location', 'authService', 'ngAuthSettings', function ($scope, $window, $timeout, $location, authService, ngAuthSettings) {

    
    var qString = $location.search();

    //Redirect to login when hash isn't passed to the URL querystring.
    if (qString.q === undefined)
        window.location = '/login';
        
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

    $scope.resetPassword = function () {

        var student = this.student;

        $scope.sendingData = true;
        $scope.sentData = false;

        authService.resetPassword(student).then(function (response) {

            $scope.message = response.message;

            $scope.frmPasswordReset.$setUntouched();

            if (response.isSuccess) {
                $scope.alertType = "alert-success";
                $scope.message += ". Em instantes será redirecionado para o Login.";
                startTimer();
            }

            $scope.sentData = true;
            $scope.sendingData = false;

        },
         function (err) {

             $cope.message = err.error_description;

             $scope.sendingData = false;
             $scope.sentData = true;

         });

    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            window.location = '/login/';

        }, 2000);
    }

}]);
