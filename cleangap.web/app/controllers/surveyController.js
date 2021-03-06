'use strict';
app.controller('surveyController', surveyController);

surveyController.$inject = ['$scope', '$q', '$http', '$filter', '$location', 'authService', '$routeParams', 'questionService'];

function surveyController($scope, $q, $http, $filter, $location, authService, $routeParams, questionService) {

    $scope.user = JSON.parse(window.localStorage.getItem("ls.authorizationData"));

    $scope.loadingData = true;

    $scope.showData = function () {
        $scope.loadingData = false;
    };

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
    };

    /*********************************************************************** 
     *  Get route whether it is coming from summary to edit the answer     * 
     ***********************************************************************/

    $scope.comingFromSummary = function () {
        var url = $location.url();

        return url.toLowerCase().indexOf("/survey/edit/") > -1;
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
        $scope.prevPage = parseInt($scope.survey.page, 10) - 1;
        $scope.nextPage = parseInt($scope.survey.page, 10) + 1;

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
            return value !== undefined && value !== null;
        }).length;

        question.childrenQuestion.forEach(function (childQuestion) {

            $scope.childrenQuestionUI[childQuestion.id] = currentAnswers[question.id] !== null &&
                                                          currentAnswers.length > 0 &&                                                          
                                                          childQuestion !== null &&
                                                          childQuestion.parentAnswerValue === currentAnswers[question.id].optionText;     

        });

        var qtyChildrenHidden = $scope.childrenQuestionUI.filter(function (value) {
            return value !== undefined && (value === null || !value);
        }).length;

        $scope.qtyChildrenHidden = qtyChildrenHidden;

    };

    $scope.dropdownChanged = function (optionId) {
        $scope.currentAnswer[optionId] = $scope.dropDownElement[optionId] !== null ? $scope.dropDownElement[optionId].id : null;
        $scope.checkForm();
    };

    $scope.questionWithCheckBox = [];
    $scope.countCheckBox = 0;
    $scope.checkBoxOptionId = [];
    
    $scope.templateRepeaterList = [];
    $scope.countRepeatersCtrl = 0;      //Qtd Individual control within a repeater
    $scope.countRepeatersGroup = 0;     //Qtd grouped / repeater control

    $scope.setRepeaterValues = function (option) {
        var templateRepeater = {
            "optionIdBase"      : option.optionId,
            "optionId"          : option.optionId,
            "optionText"        : option.optionText,
            "optionType"        : option.optionType,
            "questionChoices"   : option.questionChoices,
            "hasMultipleAnswer" : false,
            "uniqueAnswer"      : "",
            "multipleAnswers"   : [],
            "repeaterIndex"     : 0 
        };

        option.repeaterIndex = 0;
        $scope.templateRepeaterList.push(templateRepeater);
    };

    var convertAnswerToCurrent = function (serverAnswer) {

        var currentAnswers = [];

        $scope.qtyChildrenHidden = 0;

        serverAnswer.subsection.forEach(function (currentSubSection) {

            currentSubSection.questions.forEach(function (question) {

                question.qtyRepeaters = 0;

                question.questionOption.forEach(function (option) {

                    option.optionIdBase = null;

                    if (question.hasRepeater) {
                        $scope.setRepeaterValues(option);
                        $scope.countRepeatersCtrl++;
                    }

                    switch (option.optionType) {
                        case 'radio':
                            if (option.optionText === option.uniqueAnswer) {
                                currentAnswers[question.id] = option;
                            }
                            currentAnswers[question.id] = currentAnswers[question.id] === undefined ? null : currentAnswers[question.id];
                            break;
                        case 'checkbox':

                            if ($scope.checkBoxOptionId[question.id] === undefined) {
                                $scope.checkBoxOptionId[question.id] = [];
                            }

                            $scope.checkBoxOptionId[question.id].push(option.optionId);
                            $scope.countCheckBox++;
                            
                            // Check if question has any checkbox value
                            if ($scope.questionWithCheckBox[question.id] !== true) {
                                $scope.questionWithCheckBox[question.id] = option.uniqueAnswer === "true";
                            }

                            currentAnswers[option.optionId] = option.uniqueAnswer;
                            break;
                        case 'drop-down':

                            currentAnswers[option.optionId] = option.uniqueAnswer;

                            option.questionChoices.forEach(function (dropdownList) {
                                if (option.uniqueAnswer === dropdownList.id) {
                                    $scope.dropDownElement[option.optionId] = dropdownList;
                                    return;
                                }
                            });
                            break;

                        case 'input-date':
                            currentAnswers[option.optionId] = new Date(option.uniqueAnswer);
                            if (option.uniqueAnswer === null || !angular.isDate(currentAnswers[option.optionId])) {
                                currentAnswers[option.optionId] = null;
                            }
                            $scope.currentValidationMsg[option.optionId] = "Invalid Date";
                            $scope.currentValidationType[option.optionId] = "date";
                            break;

                        /*case 'input-file':
                            currentAnswers[option.optionId] = option.uniqueAnswer.name;
                            $scope.currentValidationMsg[option.optionId] = "File cannot be empty";
                            $scope.currentValidationType[option.optionId] = "file";
                            break;
                            */
                        default:
                            currentAnswers[option.optionId] = option.uniqueAnswer;
                            $scope.currentValidationMsg[option.optionId] = option.optionText === "E-Mail" ? "Invalid E-Mail" : "This answer cannot be empty";
                            $scope.currentValidationType[option.optionId] = option.optionText === "E-Mail" ? "email" : "text";
                            break;
                    }

                });

                showChildrenQuestionMap(question, currentAnswers);

            });

        });

        $scope.currentAnswer = currentAnswers;
    };

    getQuestions($scope.questionID);

    /**
     * Radio changed event.
     * @param {question} Question object.
     * @param {currentAnswer} The current Answer.
     */

    $scope.radioChanged = function (question, currentAnswer) {
        showChildrenQuestionMap(question, currentAnswer);
        $scope.checkForm();
    };

    $scope.checkBoxChanged = function (question, currentAnswer) {

        $scope.questionWithCheckBox[question.id] = false;
        $scope.checkBoxOptionId[question.id].forEach(function (optionId, index) {
            if (currentAnswer[optionId] === true) {
                $scope.questionWithCheckBox[question.id] = true;
            }
        });
        
        $scope.checkForm();
    }

    $scope.fileChanged = function (question, currentAnswer) {
        /*
        var filename = event.target.files[0].name;
        alert('file was selected: ' + filename);
        */
        debugger;
        $scope.checkForm();
    }

    /******************************
     *  Events - End
     ******************************/


    /******************************
     *  Fields Validations
     ******************************/

    $scope.validateUniqueAnswer = function (optionObj, isEmail) {
        isEmail = isEmail !== undefined ? isEmail : false;
        var answerText = $scope.currentAnswer[optionObj.optionId];
        return answerText !== null && answerText !== "";
    };

    $scope.sizeCurrentAnswer = 0;
    $scope.sizeAllAnswer = 0;

    $scope.checkForm = function () {
        var sizeCheckBoxes = $scope.questionWithCheckBox.filter(function (value) {
            return value !== undefined && value !== null;
        }).length;

        var sizeAllAnswer = $scope.currentAnswer.filter(function (value) {
            return value !== undefined;
        }).length - $scope.qtyChildrenHidden - $scope.countCheckBox + sizeCheckBoxes;


        var sizeCurrentAnswer = $scope.currentAnswer.filter(function (value) {
            return value !== undefined && value !== null && value !== "" && value !== false;
        }).length;

        $scope.sizeCurrentAnswer = sizeCurrentAnswer;
        $scope.sizeAllAnswer = sizeAllAnswer;


        $scope.isValidForm = sizeAllAnswer <= sizeCurrentAnswer;

        $scope.isValidated = true;
    };

    /******************************
     *  Fields Validations - End
     ******************************/

    /******************************
     *  Repeater sections
     ******************************/

    /**
     * Add reapeater.
     * @param {key} key Index.
     */

    $scope.listAllRepeaters = [];

    $scope.addRepeater = function (key) {

        var copyRepeater = [];
        angular.copy($scope.templateRepeaterList, copyRepeater);

        copyRepeater.forEach(function (item, index) {
            item.optionId += $scope.templateRepeaterList.length - 1;
            item.repeaterIndex++;
            $scope.survey.subsection[0].questions[key].questionOption.push(item);

            $scope.survey.subsection[0].questions[key].qtyRepeaters++;
            $scope.templateRepeaterList[index] = item;
            $scope.currentAnswer[item.optionId] = null;
        });

        $scope.countRepeatersGroup++;
        $scope.countRepeatersCtrl = $scope.countRepeatersGroup * copyRepeater.length;

        $scope.listAllRepeaters = $scope.survey.subsection[0].questions[key].questionOption.filter(function (value) {
            return value.repeaterIndex !== undefined;
        });

    };

    $scope.removeRepeater = function (pQuestionId, indexOptionRepeater) {

        //Find Question ID
        var objQuestion = $scope.survey.subsection[0].questions.find(function (obj) {
            return obj.id === pQuestionId;
        });

        //Remove the element
        var questionOptionAfterRemoving = objQuestion.questionOption.filter(function (obj) {
            //Delete answer value before removing the option
            if (obj.repeaterIndex === indexOptionRepeater) {
                delete $scope.currentAnswer[obj.optionId];
            }

            return obj.repeaterIndex !== indexOptionRepeater;
        });

        for (var i = 0; i < $scope.survey.subsection[0].questions.length; i++) {
            if ($scope.survey.subsection[0].questions[i] === objQuestion) {
                $scope.survey.subsection[0].questions[i].questionOption = questionOptionAfterRemoving;
                
                break;
            }
        }
        
        $scope.countRepeatersGroup--;
        
    };


    /******************************
     *  END: Repeater sections
     ******************************/

    $scope.saveAnswer = function (logOut) {

        logOut = logOut === undefined ? false : logOut;

        var response = $scope.currentAnswer.map(function (answer, index) {
            var currentAnswerReturn = {
                questionOptionId: index,
                hasMultipleAnswer: false,
                indexRepeater : null
            };

            if (answer !== undefined && answer !== null) {
                currentAnswerReturn.hasMultipleAnswer = !!answer['pop'];
                var prop = currentAnswerReturn.hasMultipleAnswer ? 'multipleValues' : 'uniqueValue';
                // For input radio, set {optionId, optionText}. Otherwise, {index, answer}
                currentAnswerReturn[prop] = answer.optionId ? answer.optionText : answer;
                currentAnswerReturn.questionOptionId = answer.optionId ? answer.optionId : index;

                //Check questionOptionId for its respective questionOptionIdBase (Repeater).
                var filteredRepeater = $filter("filter")($scope.templateRepeaterList, { optionId: currentAnswerReturn.questionOptionId });
                if (filteredRepeater.length > 0) {
                    currentAnswerReturn.questionOptionId = filteredRepeater[0].optionIdBase;
                    currentAnswerReturn.indexRepeater = filteredRepeater[0].repeaterIndex;
                }

            } else {
                currentAnswerReturn.uniqueValue = null;
            }

            return currentAnswerReturn;
        });

        var deferred = $q.defer();
        var allPromises = [];

        var getOptionIdRepeater = function (response, questionOptionId) {
            
        };

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
        var nextPage = $scope.survey.page === $scope.survey.pageTotal ? 'summary' : $scope.nextPage;
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
    };

}
