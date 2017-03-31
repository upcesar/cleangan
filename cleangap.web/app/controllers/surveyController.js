'use strict';
app.controller('surveyController', surveyController);

surveyController.$inject = ['$scope', '$http', '$location', 'authService', '$routeParams'];

function surveyController($scope, $http, $location, authService, $routeParams) {

    $scope.user = JSON.parse(window.localStorage.getItem("ls.authorizationData"));

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
    }

    $scope.questionID = $routeParams.questionID;

    $scope.authentication = authService.authentication;

    $http.get('http://cleangap.westcentralus.cloudapp.azure.com:8080/api/surveys/questions/' + $scope.questionID || '')
        .then(function (surveys) {
            $scope.surveys = surveys.data.questions;
            $scope.index = 0;
            update();
        });

    $scope.next = function () {
        $scope.index++;
        update();
    };

    $scope.back = function () {
        $scope.index--;
        update();
    }

    function update() {
        $scope.currentSurvey = $scope.surveys[$scope.index];
        console.log($scope.currentSurvey);
    }
}
