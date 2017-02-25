'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', 'datePickerOptionService',
    function ($scope, $window, $timeout, authService, datePickerOptionService) {

        $scope.dateOptions = datePickerOptionService;

        $scope.date = {};

        $scope.signingUp = false;
        $scope.doneSignUp = false;
        $scope.savedSuccessfully = false;

        $scope.message = "";
        $scope.alertType = "alert-danger";


        $scope.student = {};

        $scope.signUp = function () {

            $scope.signingUp = true;
            $scope.savedSuccessfully = false;
            $scope.doneSignUp = false;

            //TEMP
            $scope.student.dataNascimento = $("#studentDateOfBirth").val();

            $scope.registration = $scope.student;

            authService.saveRegistration($scope.registration).then(function (response) {

                $scope.savedSuccessfully = response.data.isSuccess;

                if (response.data.isSuccess) {
                    $scope.alertType = "alert-success";
                    $scope.message = response.data.message + ". Encaminhado para o Login.";
                    startTimer();
                } else {
                    $scope.alertType = "alert-danger";
                    $scope.message = response.data.message;
                }

                $scope.signingUp = false;
                $scope.doneSignUp = true;

            },
             function (response) {
                 /*
                 var errors = [];
                 for (var key in response.data.modelState) {
                     for (var i = 0; i < response.data.modelState[key].length; i++) {
                         errors.push(response.data.modelState[key][i]);
                     }
                 }
                 */
                 $scope.savedSuccessfully = false;
                 $scope.signingUp = false;
                 $scope.doneSignUp = true;
                 $scope.alertType = "alert-danger";

                 //$scope.message = "Houve falhas no processo de cadastro, devido a: " + errors.join(' ');
                 $scope.message = "Houve falhas no processo de cadastro.";

             });

        };

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                window.location = '/account/login/';


            }, 2000);
        }

    }
]);