'use strict';
app.controller('signinController', ['$scope', '$location', '$timeout', 'authService'  , function ($scope, $location, $timeout, authService, vcRecaptchaService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.response = null;
    $scope.widgetId = null;
    $scope.model = {
        key: '6LcWpRUUAAAAACbBSGWE_6tSKEkHcstmbTdDNklb'
    };
    $scope.setResponse = function (response) {
        $scope.response = response;
    };
    $scope.setWidgetId = function (widgetId) {
        $scope.widgetId = widgetId;
    };
    // $scope.cbExpiration = function() {
    //     console.info('Captcha expired. Resetting response object');
    //     vcRecaptchaService.reload($scope.widgetId);
    //     $scope.response = null;
    //  };
    // $scope.submit = function () {
    //     var valid;
    //     /**
    //      * SERVER SIDE VALIDATION
    //      *
    //      * You need to implement your server side validation here.
    //      * Send the reCaptcha response to the server and use some of the server side APIs to validate it
    //      * See https://developers.google.com/recaptcha/docs/verify
    //      */
    //     console.log('sending the captcha response to the server', $scope.response);
    //     if (valid) {
    //         console.log('Success');
    //     } else {
    //         console.log('Failed validation');
    //         // In case of a failed validation you need to reload the captcha
    //         // because each response can be checked just once
    //         vcRecaptchaService.reload($scope.widgetId);
    //     }
    // };

    $scope.registration = {
        name: "",
        email: "",
        password: "",
        // confirmPassword: "",
        // recaptcha: ""
    };

    $scope.signIn = function () {
        $scope.registration.recaptcha =  $scope.response;
        authService.saveRegistration($scope.registration).then(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
            startTimer();

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}]);
