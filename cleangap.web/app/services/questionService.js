/* global angular */

angular.module("cleangap")
    .service("questionService", ["$http", questionService]);

function questionService($http) {

    return {
        Get: (function (questionID) {
            return $http.get(serviceBase+'/api/surveys/questions/' + questionID || '');
        })
    }

   
};

