'use strict';

app.controller('MethodsController', ['$scope', 'MethodsService', function ($scope, MethodsService) {

    $scope.samples = [];
    $scope.patients = [];

    MethodsService.getSamples().then(function (results) {
        $scope.samples = results.data;
    }, function (error) {
       
    }
    );

    MethodsService.getPatientbyPage().then(function (results) {
        $scope.patients = results.data;
    }, function (error) {

    }
    );

}]);