/* global angular */

angular.module("cleangap")
    .service("questionService", ["$http", questionService]);

function questionService($http) {

    return {
        Get: (function (questionID, boundToMax) {
            boundToMax = boundToMax ? '?boundToMax=true' : '';
            return $http.get(serviceBase + '/api/surveys/questions/' + questionID + boundToMax || '').then(function (response) {
                return response;
            });
        }),

        GetLast: (function () {
            return $http.get(serviceBase + '/api/surveys/resume' || '').then(function (response) {
                return response;
            });
        }),

        Post: (function (answeredQuestion) {
            return $http.post(serviceBase + '/api/surveys/answers/', answeredQuestion).then(function (response) {
                return response;
            });
        }),

        Finish: (function (signature) {
            return $http.post(serviceBase + '/api/surveys/close/', signature).then(function (response) {
                return response;
            });
        })

    }

   
};

