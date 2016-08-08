'use strict';
app.controller('IndexController', ['$scope', '$location', 'authenticationService', function ($scope, $location, authenticationService) {

    $scope.logOut = function () {
        authenticationService.logOut();
        $location.path('/home');
    }

    $scope.authentication = authenticationService.authentication;

}]);