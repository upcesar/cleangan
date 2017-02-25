'use strict';
app.controller('loginController', ['$scope', '$window', 'authService', 'studentService', 'ngAuthSettings',
    function ($scope, $window, authService, studentService, ngAuthSettings) {

        $scope.msgAuthenticating = "Autenticando...";

        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.message = "";
        $scope.authErr = false;
        $scope.sendingData = false;
        $scope.authenticated = false;



        $scope.hideErrMsg = function () {
            $scope.authErr = false;
        };

        $scope.login = function () {

            $scope.sendingData = true;
            authService.login($scope.loginData).then(function (response) {

                $scope.authenticated = true;

                // For first users, redirect to courses.
                $scope.msgAuthenticating = "Autenticado. Verificando histórico do aluno";
                studentService.checkFirstCourse().then(function (response) {

                    if (response.data) {
                        window.location = "/courses/select";
                    } else {
                        window.location = "/dashboard";
                    }
                    
                },
                function (err) {

                    $scope.authErr = true;
                    $scope.sendingData = false;

                    $scope.message = err.error_description;

                    $scope.frmLogin.$setPristine();
                    $scope.frmLogin.$setUntouched();

                    $scope.loginData.userName = "";
                    $scope.loginData.password = "";
                });
                

            },
             function (err) {

                 $scope.authErr = true;
                 $scope.sendingData = false;

                 $scope.message = err.error_description;

                 $scope.frmLogin.$setPristine();
                 $scope.frmLogin.$setUntouched();

                 $scope.loginData.userName = "";
                 $scope.loginData.password = "";
             });
        };

        $scope.authExternalProvider = function (provider) {

            var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

            var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                        + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                        + "&redirect_uri=" + redirectUri;
            window.$windowScope = $scope;

            var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
        };

        $scope.authCompletedCB = function (fragment) {

            $scope.$apply(function () {

                if (fragment.haslocalaccount == 'False') {

                    authService.logOut();

                    authService.externalAuthData = {
                        provider: fragment.provider,
                        userName: fragment.external_user_name,
                        externalAccessToken: fragment.external_access_token
                    };

                    //$location.path('/associate');

                }
                else {
                    //Obtain access token and redirect to orders
                    var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                    authService.obtainAccessToken(externalData).then(function (response) {

                        //$location.path('/orders');

                    },
                 function (err) {
                     $scope.message = err.error_description;
                 });
                }

            });
        }
    }
]);
