
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signin", {
        controller: "signinController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/dashboard", {
        controller: "dashboardController",
        templateUrl: "/app/views/dashboard.html"
    });

    $routeProvider.when("/create-staff", {
        controller: "createStaffController",
        templateUrl: "/app/views/create-staff.html"
    });

    $routeProvider.when("/create-user", {
        controller: "createUserController",
        templateUrl: "/app/views/create-user.html"
    });

    $routeProvider.when("/forgot-password", {
        controller: "forgotPasswordController",
        templateUrl: "/app/views/forgot-password.html"
    });

    $routeProvider.when("/password-reset", {
        controller: "passwordResetController",
        templateUrl: "/app/views/password-reset.html"
    });

    $routeProvider.when("/staff", {
        controller: "staffController",
        templateUrl: "/app/views/staff.html"
    });

    $routeProvider.when("/survey", {
        controller: "surveyController",
        templateUrl: "/app/views/survey.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

//var serviceBase = 'http://localhost:26264/';
var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
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


