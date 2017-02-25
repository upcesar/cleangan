'use strict';
var app = angular.module("cleangap", ['ngRoute', 'ngAnimate', 'LocalStorageModule', 'ui.mask', 'ui.date', 'ngLocale']);

var serviceBase = 'http://cleangap.westcentralus.cloudapp.azure.com:8080/api/';  //Set Base URL for API
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngapp'
});

app.config(['$httpProvider', '$locationProvider', function ($httpProvider, $locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
    $httpProvider.interceptors.push('authInterceptorService');
}]);

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);