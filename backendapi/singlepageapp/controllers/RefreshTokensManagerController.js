'use strict';
app.controller('RefreshTokensManagerController', ['$scope', 'RefreshTokensManagerService', function ($scope, RefreshTokensManagerService) {

    $scope.refreshTokens = [];

    RefreshTokensManagerService.getAllRefreshTokens().then(function (results) {

        $scope.refreshTokens = results.data;

    }, function (error) {
        alert(error.data.message);
    });

    $scope.deleteRefreshTokens = function (index, tokenid) {

        tokenid = window.encodeURIComponent(tokenid);

        RefreshTokensManagerService.deleteRefreshTokens(tokenid).then(function (results) {

            $scope.refreshTokens.splice(index, 1);

        }, function (error) {
            alert(error.data.message);
        });
    }

}]);