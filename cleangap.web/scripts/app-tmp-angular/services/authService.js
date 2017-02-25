'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userID: "",
        userName: "",
        refreshToken: "",
        accessToken: ""
    };

    var _externalAuthData = {
        provider: "",
        userName: "",
        externalAccessToken: ""
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(serviceBase + 'account/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + ngAuthSettings.clientId;
        }

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            _authentication.isAuth = true;
            _authentication.userID = response.userID;
            _authentication.userName = response.userName;
            _authentication.refreshToken = response.refresh_token;
            _authentication.accessToken = response.access_token;

            localStorageService.set('icbeuAuthData', _authentication);

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('icbeuAuthData');

        _authentication.isAuth = false;
        _authentication.userID = "";
        _authentication.userName = "";
        _authentication.refreshToken = "";
        _authentication.accessToken = "";

    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('icbeuAuthData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userID = authData.userID;
            _authentication.userName = authData.userName;
            _authentication.refreshToken = authData.refreshToken;
            _authentication.accessToken = authData.accessToken;
        }

    };

    var _refreshToken = function () {
        var deferred = $q.defer();

        var authData = localStorageService.get('icbeuAuthData');

        if (authData) {

            var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;

            localStorageService.remove('icbeuAuthData');

            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                _authentication.refreshToken = response.refresh_token;
                _authentication.accessToken = response.access_token;

                localStorageService.set('icbeuAuthData', _authentication);

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });
            
        } else {
            deferred.reject(null);
        }

        return deferred.promise;
    };

    var _registerExternal = function (registerExternalData) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/account/registerexternal', registerExternalData).success(function (response) {

            localStorageService.set('icbeuAuthData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

            _authentication.isAuth = true;
            _authentication.userName = response.userName;
            _authentication.useRefreshTokens = false;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _sendPasswordRecovery = function (email) {

        _logOut();

        var deferred = $q.defer();

        var data = {
            email: email
        };

        $http.post(serviceBase + 'account/send-password-recovery', data).success(function (response) {

            deferred.resolve(response);

        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _resetPassword = function (data) {

        _logOut();

        debugger;
        var deferred = $q.defer();
        
        $http.post(serviceBase + 'account/reset-password', data).success(function (response) {

            deferred.resolve(response);

        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.refreshToken = _refreshToken;

    authServiceFactory.externalAuthData = _externalAuthData;
    authServiceFactory.registerExternal = _registerExternal;

    authServiceFactory.sendPasswordRecovery = _sendPasswordRecovery;
    authServiceFactory.resetPassword = _resetPassword;

    return authServiceFactory;
}]);