
var app = angular.module('cleangap', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

//var serviceBase = 'http://localhost:26264/';
// var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';

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
        controller: "forgotPasswordController",
    });
    //
    // $routeProvider.when("/dashboard", {
    //     controller: "dashboardController",
    //     templateUrl: "views/dashboard.html"
    // });
    //
    // $routeProvider.when("/create-staff", {
    //     controller: "createStaffController",
    //     templateUrl: "views/create-staff.html"
    // });
    //
    // $routeProvider.when("/create-user", {
    //     controller: "createUserController",
    //     templateUrl: "views/create-user.html"
    // });
    //
    //
    //
    // $routeProvider.when("/password-reset", {
    //     controller: "passwordResetController",
    //     templateUrl: "views/password-reset.html"
    // });
    //
    // $routeProvider.when("/staff", {
    //     controller: "staffController",
    //     templateUrl: "views/staff.html"
    // });
    //
    // $routeProvider.when("/survey", {
    //     controller: "surveyController",
    //     templateUrl: "views/survey.html"
    // });
    //
    // $routeProvider.when("/tokens", {
    //     controller: "tokensManagerController",
    //     templateUrl: "views/tokens.html"
    // });
    //
    // $routeProvider.when("/refresh", {
    //     controller: "refreshController",
    //     templateUrl: "views/refresh.html"
    // });

    $routeProvider.otherwise({ redirectTo: "/" });

}]);

var serviceBase = 'http://localhost:26264/';
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
