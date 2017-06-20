'use strict';
app.controller('surveyController', surveyController);

surveyController.$inject = ['$scope', '$q', '$http', '$location', 'authService', '$routeParams', 'questionService'];

function surveyController($scope, $q, $http, $location, authService, $routeParams, questionService) {

    $scope.user = JSON.parse(window.localStorage.getItem("ls.authorizationData"));

    $scope.loadingData = true;

    $scope.showData = function () {
        $scope.loadingData = false;
    };

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
    }

    /*********************************************************************** 
     *  Get route whether it is coming from summary to edit the answer     * 
     ***********************************************************************/

    $scope.comingFromSummary = function () {
        var url = $location.url();

        return (url.toLowerCase().indexOf("/survey/edit/") > -1);
    };

    $scope.questionID = $routeParams.questionID;

    $scope.authentication = authService.authentication;

    $scope.currentAnswer = [];    
    $scope.childrenQuestionUI = [];

    $scope.currentValidationMsg = [];
    $scope.currentValidationType = [];

    $scope.dropDownElement = [];

    $scope.isValidForm = false;
    $scope.isValidated = false;
    $scope.qtyChildrenHidden = 0;

    var populateSurveyData = function () {
        
        $scope.surveys = $scope.survey.questions;
        $scope.index = 0;
        $scope.prevPage = (parseInt($scope.survey.page, 10) - 1);
        $scope.nextPage = (parseInt($scope.survey.page, 10) + 1);

        convertAnswerToCurrent($scope.survey);
        $scope.checkForm();
        $scope.isValidated = false;
    };

    var checkSteps = function (surveys) {
        $scope.survey = surveys.data;

        if ($scope.survey.redirectSummary) {
            $location.path('/survey/summary/');
        } else {
            populateSurveyData();
        }

    };


    var getQuestions = function (questionID) {

        if (questionID === undefined) {
            return questionService.GetLast()
                .then(function (surveys) {                    
                    checkSteps(surveys);
                });
        } else {
            return questionService.Get(questionID)
                .then(function (surveys) {
                    checkSteps(surveys);
                });
        }
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
        $scope.currentAnswer[optionId] = $scope.dropDownElement[optionId] !== null ? $scope.dropDownElement[optionId].id : null;
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
                    case 'checkbox':
                        currentAnswers[option.optionId] = option.uniqueAnswer;
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
                        $scope.currentValidationMsg[option.optionId] = option.optionText === "E-Mail" ? "Invalid E-Mail" : "This answer cannot be empty";
                        $scope.currentValidationType[option.optionId] = option.optionText === "E-Mail" ? "email" : "text";
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

    $scope.validateUniqueAnswer = function (optionObj, isEmail) {
        isEmail = isEmail !== undefined ? isEmail : false;
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

    $scope.saveAnswer = function (logOut) {

        logOut = logOut === undefined ? false : logOut;

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

        var deferred = $q.defer();
        var allPromises = [];

        response.forEach(function (obj) {

            var currentPromise = questionService.Post(obj).then(function (result) {
                return deferred.resolve(result);
            }, function (err) {
                return deferred.reject(err);
            });

            allPromises.push(currentPromise);
        });

        
        return $q.all(allPromises);

    };

    $scope.next = function () {
        $scope.saveAnswer();
        var nextPage = $scope.survey.page == $scope.survey.pageTotal ? 'summary' : $scope.nextPage;
        $location.path('/survey/' + nextPage);
    };

    $scope.back = function () {
        var prevPage = $scope.comingFromSummary() ? 'summary' : $scope.prevPage;
        $location.path('/survey/' + prevPage);
    };

    $scope.saveAndExit = function () {
        $scope.saveAnswer().then(function (result) {
            $scope.logOut();
        });
    };

    $scope.saveGoSummary = function () {
        $scope.saveAnswer();
        $location.path('/survey/summary');
    }

}
