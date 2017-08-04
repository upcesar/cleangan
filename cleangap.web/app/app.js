
'use strict';
var app = angular.module('cleangap', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'vcRecaptcha', 'ui.bootstrap', 'ngBootbox', '720kb.datepicker', 'infinite-scroll']);

//var serviceBase = 'http://localhost:51563/';
var serviceBase = 'http://cleangap.westcentralus.cloudapp.azure.com:8080/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('httpRequestInterceptor');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
