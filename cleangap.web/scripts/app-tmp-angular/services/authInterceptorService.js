'use strict';
app.factory('authInterceptorService', ['$q', '$injector', '$window', 'localStorageService', function ($q, $injector, $window, localStorageService) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};
       
        var authData = localStorageService.get('icbeuAuthData');
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.accessToken;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            var authService = $injector.get('authService');
            var authData = localStorageService.get('icbeuAuthData');

            if (authData) {
                //$location.path('/refresh');
                return $q.reject(rejection);
                
            }
            authService.logOut();
            window.location = "/account/login";            
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);