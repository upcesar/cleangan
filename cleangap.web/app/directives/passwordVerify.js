'use strict';
app.directive("passwordVerify", function () {
    return {
        require: "ngModel",
        scope: {
            passwordVerify: '='
        },
        link: function (scope, element, attrs, ctrl) {
            scope.$watch(function () {
                var combined;

                if (scope.passwordVerify || ctrl.$viewValue) {
                    combined = scope.passwordVerify + '_' + ctrl.$viewValue;
                }
                return combined;
            }, function (value) {
                if (value) {
                    ctrl.$parsers.unshift(function (viewValue) {
                        var origin = scope.passwordVerify;
                        if (origin !== viewValue) {
                            ctrl.$setValidity("passwordVerify", false);
                            return undefined;
                        } else {
                            ctrl.$setValidity("passwordVerify", true);
                            return viewValue;
                        }
                    });
                }
            });
        }
    };
});

// 'use strict';
// app.directive("passwordVerify", function () {
//   function passwordVerify() {
//       return {
//         restrict: 'A', // only activate on element attribute
//         require: '?ngModel', // get a hold of NgModelController
//         link: function(scope, elem, attrs, ngModel) {
//           if (!ngModel) return; // do nothing if no ng-model
//
//           // watch own value and re-validate on change
//           scope.$watch(attrs.ngModel, function() {
//             validate();
//           });
//
//           // observe the other value and re-validate on change
//           attrs.$observe('passwordVerify', function(val) {
//             validate();
//           });
//
//           var validate = function() {
//             // values
//             var val1 = ngModel.$viewValue;
//             var val2 = attrs.passwordVerify;
//
//             // set validity
//             ngModel.$setValidity('passwordVerify', val1 === val2);
//           };
//         }
//       }
//     }
//   };
