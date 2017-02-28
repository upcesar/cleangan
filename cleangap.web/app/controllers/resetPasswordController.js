'use strict';
app.controller('resetPasswordController', ['$scope', '$window', '$timeout', '$location', 'authService', 'ngAuthSettings', function ($scope, $window, $timeout, $location, authService, ngAuthSettings) {

    $window.document.getElementById("newPassword").focus();

    var qString = $location.search();
    
    //Redirect to login when hash isn't passed to the URL querystring.
    if (qString.q === undefined)
        $window.location = '/login';

    $scope.resetPasswordData = {};
    $scope.savedSuccessfully = false;

    if (qString.q !== undefined)
        $scope.resetPasswordData.token = qString.q;

    $scope.message = "";

    $scope.alertType = "alert-danger";

    $scope.sendingData = false;
    $scope.sentData = false;

    $scope.hideMsg = function () {

        if (!$scope.sendingData)
            $scope.sentData = false;
    };

    $scope.resetPassword = function () {

        var customer = $scope.resetPasswordData;

        $scope.sendingData = true;
        $scope.sentData = false;

        authService.resetPassword(customer).then(function (response) {

            $scope.message = response.message;

            $scope.form.$setUntouched();

            $scope.savedSuccessfully = response.isSuccess;

            if (response.isSuccess) {
                $scope.alertType = "alert-success";
                $scope.message += ". Now you are redirecting to Login.";
                startTimer();
            } else {
                $scope.resetPasswordData.newPassword = "";
                $scope.resetPasswordData.confirmNewPassword = "";
                $window.document.getElementById("newPassword").focus();
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
            window.location = '/login';

        }, 2000);
    }

}]);
