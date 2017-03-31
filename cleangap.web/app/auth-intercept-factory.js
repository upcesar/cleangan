/* global angular */

(function (angular) {
    var app = angular.module("cleangap");

  app.factory("authInterceptorFactory", ["$location", "$q", "localStorageService", "$rootScope", "$injector", "APP",
    function ($location, $q, localStorageService, $rootScope, $injector, APP) {

      var _request = function (config) {
        config.headers = config.headers || {};

        var authData = localStorageService.get("authorizationData");
        if (authData) {
          config.headers.Authorization = "Bearer " + authData.token;
        }

        return config;
      };

      var _responseError = function (rejection) {
        $rootScope.erro = rejection;
        $location.path("/");

        return $q.reject(rejection);
      };

      var _realizarLogin = function () {
        var serviceBase = APP.Server;
        var data = "grant_type=password&UserName=juansaintclair@gmail.com&Password=123456";

        var deferred = $q.defer();

        var $http = $injector.get("$http");
        $http.post(serviceBase + "token", data, {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded"
          }
        })
          .success(function (response) {
            localStorageService.set("authorizationData", {
              token: response.access_token, Password: loginData.UserName
            });

            deferred.resolve(response);

          }).error(function (err) {
            deferred.reject(err);
          });

        return deferred.promise;
      };

      var authInterceptorServiceFactory = {};
      authInterceptorServiceFactory.request = _request;
      authInterceptorServiceFactory.responseError = _responseError;
      authInterceptorServiceFactory.realizarLogin = _realizarLogin;

      return authInterceptorServiceFactory;
    }]);
})(angular);