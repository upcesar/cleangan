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

                convertAnswerToCurrent(surveys.data);
            });
    };

    var convertAnswerToCurrent = function (serverAnswer) {
        var currentAnswers = [];

        serverAnswer.questions.forEach(function (question) {
            question.questionOption.forEach(function (option) {
                if (option.optionType === 'radio') {
                    currentAnswers[question.id] = option;
                } else {
                    currentAnswers[option.optionId] = option.uniqueAnswer;
                }

            });
        });

        $scope.currentAnswer = currentAnswers;
    };

    getQuestions($scope.questionID);



    $scope.next = function () {
        $scope.saveAnswer();
        $location.path('/survey/' + $scope.nextPage);

    };

    $scope.back = function () {
        $location.path('/survey/' + $scope.prevPage);
    };

    $scope.saveAnswer = function () {
        var response = $scope.currentAnswer.map(function (answer, index) {
            var currentAnswerReturn = {
                questionOptionId: index,
                hasMultipleAnswer: !!answer['pop']
            };
            var prop = currentAnswerReturn.hasMultipleAnswer ? 'multipleValues' : 'uniqueValue';
            currentAnswerReturn[prop] = answer.id ? answer.id : answer;
            return currentAnswerReturn;
        });

        response.forEach(function (obj) {
            questionService.Post(obj);
        });
    };

    $scope.saveAndExit = function () {
        $scope.saveAnswer();
        //authService.logOut();
    };
}
