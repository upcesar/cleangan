'use strict';
app.controller('loginController', ['$scope', '$window', '$location', 'authService', 'questionService', 'ngAuthSettings', function ($scope, $window, $location, authService, questionService, ngAuthSettings) {
    
    $window.document.getElementById("inputEmail").focus();

    $scope.loginData = {
        username: "",
        password: "",
        useRefreshTokens: false
    };
    $scope.sendingData = false;
    $scope.message = "";

    $scope.showMessage = function (message) {
        $scope.message = message;
        $scope.sendingData = false;
        $scope.loginData.password = "";
        $window.document.getElementById("inputPassword").focus();
    };

    $scope.login = function () {
        $scope.message = "";
        authService.logOut();
        $scope.sendingData = true;
        authService.login($scope.loginData).then(function (response) {
            //Check for open questions
            questionService.Status().then(function (response) {
                var data = response.data;
                if (data.isSucess) {
                    $location.path('/survey/');
                } else {
                    authService.logOut();
                    $scope.showMessage(data.message);
                }
            });
        },
         function (err) {
             $scope.showMessage(err !== null ? err.error_description : "System error. Contact support for assistance.");             
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

                $location.path('/associate');

            }
            else {
                //Obtain access token and redirect to SURVEY
                var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                authService.obtainAccessToken(externalData).then(function (response) {

                    $location.path('/survey');

                },
             function (err) {
                 $scope.message = err.error_description;
             });
            }

        });
    }
}]);
