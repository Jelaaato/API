'use strict';
app.controller('SignInController', ['$scope', '$location', '$timeout', 'authenticationService', function ($scope, $location, $timeout, authenticationService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        UserName: "",
        Password: "",
        ConfirmPassword: ""
    };

    $scope.register = function () {

        authenticationService.saveRegistration($scope.registration).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "Registration is successful";
            startTimer();

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Registration failed due to " + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/home');
        }, 1000);
    }

}]);