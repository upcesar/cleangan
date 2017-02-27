
var app = angular.module('cleangap', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'vcRecaptcha']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    $routeProvider.when("/", {
        templateUrl: "app/views/login.html",
        controller: "loginController",
    });

    $routeProvider.when("/signin", {
        templateUrl: "app/views/signin.html",
        controller: "signinController",
    });

    $routeProvider.when("/forgot-password", {
        templateUrl: "app/views/forgot-password.html",
        controller: "passwordRecoveryController",
    });

    $routeProvider.when("/account/password-reset", {
        templateUrl: "app/views/password-reset.html",
    });

    $routeProvider.when("/dashboard", {
        templateUrl: "app/views/dashboard.html",
    });

    $routeProvider.when("/create-staff", {
        templateUrl: "app/views/create-staff.html",
    });

    $routeProvider.when("/create-user", {
        templateUrl: "app/views/create-user.html",
    });

    $routeProvider.when("/staff", {
        templateUrl: "app/views/staff.html",
    });

    $routeProvider.when("/survey", {
        templateUrl: "app/views/survey.html",
    });

    $routeProvider.when("/tokens", {
        templateUrl: "app/views/tokens.html",
        controller: "tokensManagerController"
    });

    $routeProvider.when("/refresh", {
        templateUrl: "app/views/refresh.html",
        controller: "refreshController"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
    $locationProvider.html5Mode(true);
}]);

// var serviceBase = 'http://localhost:51563/';
var serviceBase = 'http://cleangap.westcentralus.cloudapp.azure.com:8080/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
