'use strict';
app.controller('LoginController', ['$scope', '$location', 'authenticationService', function ($scope, $location, authenticationService) {

    $scope.loginData = {
        UserName: "",
        Password: ""
    };

    $scope.message = "";

    $scope.login = function () {

        authenticationService.login($scope.loginData).then(function (response) {

            $location.path('/docu');

        },
         function (err) {
             $scope.message = err.error_description;
         });
    };

}]);