'use strict';
app.controller('locationController', ['$scope', '$window', '$location', 'ngAuthSettings', function ($scope, $window, $location, ngAuthSettings) {

    $scope.redirectToUrl = function ($url, $blank) {        
        if (typeof $blank === "undefined" || $blank !== true) {
            window.location = $url;            
        } else {
            window.open($url, '_blank');
        }
    };

}]);