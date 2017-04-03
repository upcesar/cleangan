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

    $scope.answeredQuestions = [];

    $scope.currentAnswer = [];


    //authService.refreshToken()
    //    .then(function (response) {
    //        console.log('teste');
    //    // TODO: Put survey service here
    //    $http.get('http://cleangap.westcentralus.cloudapp.azure.com:8080/api/surveys/questions/' + $scope.questionID || '')
    //        .then(function (surveys) {
    //            $scope.surveys = surveys.data.questions;
    //            $scope.index = 0;
    //            update();
    //        });


    //}, function (error) {
    //    // TODO: LogOut and redirect to login
    //    $location.path('/');
    //});

    var getQuestions = function (questionID) {
        return questionService.Get(questionID)
            .then(function (surveys) {
                $scope.surveys = surveys.data.questions;
                $scope.index = 0;
                update();
             });
    }
    getQuestions($scope.questionID);

   

    $scope.next = function () {
        $scope.index++;
        update();
    };

    $scope.back = function () {
        $scope.index--;
        update();
    };

    $scope.saveAnswer = function () {
        $scope.currentAnswer.currentQuestionID = $scope.currentSurvey.id;
        $scope.currentAnswer.surveyID = $scope.questionID;
        $scope.currentAnswer.questionIndex = $scope.index;
        
        if ($scope.answeredQuestions.length > 0) {
            for (var x = 0; x < $scope.answeredQuestions.length; x++) {
                if ($scope.answeredQuestions[x].currentQuestionID === $scope.currentAnswer.currentQuestionID) {
                    $scope.answeredQuestions[x] = $scope.currentAnswer;
                }
                else {
                    $scope.answeredQuestions.push($scope.currentAnswer);
                }
            }
        }
        else {
            $scope.answeredQuestions.push($scope.currentAnswer);
        }

        console.log($scope.answeredQuestions);
    };

    function update() {
        $scope.currentSurvey = $scope.surveys[$scope.index];
        
    };
}
