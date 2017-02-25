'use strict';
app.controller('homeController', ['$scope', '$window', 'authService', 'ngAuthSettings', function ($scope, $window, authService, ngAuthSettings) {

    var _checkAuth = function () {
        authService.refreshToken().then(null, function (error) {
            authService.logOut();
        });
    };

    _checkAuth();

}]);