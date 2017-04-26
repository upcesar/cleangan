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
    $scope.childrenQuestionUI = [];

    $scope.dropDownElement = [];

    $scope.isValidForm = false;
    $scope.isValidated = false;
    $scope.qtyChildrenHidden = 0;

    var getQuestions = function (questionID) {
        return questionService.Get(questionID)
            .then(function (surveys) {
                $scope.survey = surveys.data
                $scope.surveys = $scope.survey.questions;
                $scope.index = 0;
                $scope.prevPage = (parseInt($scope.survey.page, 10) - 1);
                $scope.nextPage = (parseInt($scope.survey.page, 10) + 1);

                convertAnswerToCurrent(surveys.data);
                $scope.checkForm();
                $scope.isValidated = false;
            });
    };

    //Set dependent question map for showing / hiding in HTML
    var showChildrenQuestionMap = function (question, currentAnswers) {

        var sizeChildrenQuestionUI = $scope.childrenQuestionUI.filter(function (value) {
            return value !== undefined && value != null;
        }).length;

        question.childrenQuestion.forEach(function (childQuestion) {

            $scope.childrenQuestionUI[childQuestion.id] = currentAnswers[question.id] != null &&
                                                          currentAnswers.length > 0 &&                                                          
                                                          childQuestion != null &&
                                                          childQuestion.parentAnswerValue === currentAnswers[question.id].optionText;     

        });

        var qtyChildrenHidden = $scope.childrenQuestionUI.filter(function (value) {
            return value !== undefined && (value == null || !value);
        }).length;

        $scope.qtyChildrenHidden = qtyChildrenHidden;

    };

    $scope.dropdownChanged = function (optionId) {
        $scope.currentAnswer[optionId] = $scope.dropDownElement[optionId].id;
        $scope.checkForm();
    };


    var convertAnswerToCurrent = function (serverAnswer) {
        var currentAnswers = [];

        $scope.qtyChildrenHidden = 0;

        serverAnswer.questions.forEach(function (question) {

            question.questionOption.forEach(function (option) {

                switch (option.optionType) {
                    case 'radio':
                        if (option.optionText == option.uniqueAnswer) {
                            currentAnswers[question.id] = option;
                        }
                        currentAnswers[question.id] = currentAnswers[question.id] === undefined ? null : currentAnswers[question.id];
                        break;
                    case 'drop-down':

                        currentAnswers[option.optionId] = option.uniqueAnswer;

                        option.questionChoices.forEach(function (dropdownList) {
                            if (option.uniqueAnswer == dropdownList.id) {
                                $scope.dropDownElement[option.optionId] = dropdownList;
                                return;
                            }
                        });
                        break;

                    default:
                        currentAnswers[option.optionId] = option.uniqueAnswer;
                        break;
                }

            });

            showChildrenQuestionMap(question, currentAnswers);

        });

        $scope.currentAnswer = currentAnswers;
    };

    getQuestions($scope.questionID);

    /******************************
     *  Events
     ******************************/
    $scope.radioChanged = function (question, currentAnswer) {
        showChildrenQuestionMap(question, currentAnswer);
        $scope.checkForm();
    };

    /******************************
     *  Events - End
     ******************************/


    /******************************
     *  Fields Validations
     ******************************/

    $scope.validateUniqueAnswer = function (optionObj) {
        var answerText = $scope.currentAnswer[optionObj.optionId];
        return answerText != null && answerText != "";
    };

    $scope.sizeCurrentAnswer = 0;
    $scope.sizeAllAnswer = 0;


    $scope.checkForm = function () {
        var sizeCurrentAnswer = $scope.currentAnswer.filter(function (value) {
            return value !== undefined && value != null && value != "";
        }).length;

        var sizeAllAnswer = $scope.currentAnswer.filter(function (value) {
            return value !== undefined;
        }).length - $scope.qtyChildrenHidden;

        $scope.sizeCurrentAnswer = sizeCurrentAnswer;
        $scope.sizeAllAnswer = sizeAllAnswer;


        $scope.isValidForm = (sizeAllAnswer <= sizeCurrentAnswer);
        
        $scope.isValidated = true;
    }

    /******************************
     *  Fields Validations - End
     ******************************/

    $scope.saveAnswer = function () {
        var response = $scope.currentAnswer.map(function (answer, index) {
            var currentAnswerReturn = {
                questionOptionId: index,
                hasMultipleAnswer: false
            };

            if (answer != null) {
                currentAnswerReturn.hasMultipleAnswer = !!answer['pop'];
                var prop = currentAnswerReturn.hasMultipleAnswer ? 'multipleValues' : 'uniqueValue';
                // For input radio, set {optionId, optionText}. Otherwise, {index, answer}
                currentAnswerReturn[prop] = answer.optionId ? answer.optionText : answer;
                currentAnswerReturn.questionOptionId = answer.optionId ? answer.optionId : index;
            } else {
                currentAnswerReturn.uniqueValue = null;
            }

            return currentAnswerReturn;
        });

        response.forEach(function (obj) {
            questionService.Post(obj);
        });
    };

    $scope.next = function () {
        $scope.saveAnswer();
        $location.path('/survey/' + $scope.nextPage);

    };

    $scope.back = function () {
        $location.path('/survey/' + $scope.prevPage);
    };

    $scope.saveAndExit = function () {
        $scope.saveAnswer();
        authService.logOut();
    };
}
