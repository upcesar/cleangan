
'use strict';
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

    $routeProvider.when("/password-reset", {
        templateUrl: "app/views/password-reset.html",
        controller: "resetPasswordController",
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

    $routeProvider.when("/survey/summary", {
        templateUrl: "app/views/summary.html",
        controller: "summaryController",
    });

    $routeProvider.when("/survey/edit/:questionID", {
        templateUrl: "app/views/survey.html",
        controller: "surveyController",
    });

    $routeProvider.when("/survey/:questionID", {
        templateUrl: "app/views/survey.html",
        controller: "surveyController",
    });


    $routeProvider.when("/survey", {
        templateUrl: "app/views/survey.html",
        controller: "surveyController",
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