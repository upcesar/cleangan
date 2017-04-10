'use strict';
app.controller('surveyController', surveyController);

surveyController.$inject = ['$scope', '$http', '$location', 'authService', '$routeParams', 'questionService'];

function surveyController($scope, $http, $location, authService, $routeParams, questionService) {

    $scope.user = JSON.parse(window.localStorage.getItem("ls.authorizationData"));

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
    }

    $scope.questionID = $routeParams.questionID;    

    $scope.authentication = authService.authentication;

    $scope.currentAnswer = [];

 
    var getQuestions = function (questionID) {
        return questionService.Get(questionID)
            .then(function (surveys) {
                $scope.survey = surveys.data
                $scope.surveys = $scope.survey.questions;
                $scope.index = 0;
                $scope.prevPage = (parseInt($scope.survey.page, 10) - 1);
                $scope.nextPage = (parseInt($scope.survey.page, 10) + 1);
                update();
             });
    }
    getQuestions($scope.questionID);

   

    $scope.next = function () {
        var response = $scope.currentAnswer.map(function (obj, index) {
            var currentAnswerReturn = {
                questionOptionId: index,
                hasMultipleAnswer: !!obj['pop']
            };
            var prop = currentAnswerReturn.hasMultipleAnswer ? 'multipleValues' : 'uniqueValue';
            currentAnswerReturn[prop] = obj.id ? obj.id : obj;
            return currentAnswerReturn;
        })

        response.forEach(function (obj) {
           questionService.Post(obj);     
        });


        $location.path('/survey/' + $scope.nextPage);
    
        
    };

    $scope.back = function () {
        $location.path('/survey/' + $scope.prevPage);
    };

    $scope.saveAnswer = function () {
        
    };

    function update() {
        $scope.currentSurvey = $scope.surveys[$scope.index];
    };
}
