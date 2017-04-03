
app.factory('httpRequestInterceptor', httpRequestInterceptor);

httpRequestInterceptor.$inject = ["$location", "$q", "localStorageService"];

function httpRequestInterceptor($location, $q, localStorageService) {
    return {
        request: function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get("authorizationData");
            if (authData) {
                config.headers.Authorization = "Bearer " + authData.token;
            }

            return config;

        },
        responseError: function (rejection) {
            $location.path("/");

            return $q.reject(rejection);
        }
    };
}