'use strict';
app.controller('surveyController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.user = JSON.parse(window.localStorage.getItem("ls.authorizationData"));
    
    $scope.logOut = function () {
       authService.logOut();
       $location.path('/');
    }

    $scope.authentication = authService.authentication;

}]);
