'use strict';

app.factory('RefreshTokensManagerService', ['$http', function ($http) {
    var BaseUrl = 'https://itworkssys:446/';

    var RefreshTokensServiceFactory = {};

    var _getAllRefreshTokens = function () {

        return $http.get(BaseUrl + 'api/RefreshTokens').then(function (res) {
            return res;
        });
    };

    var _deleteRefreshTokens = function () {

        return $http.delete(BaseUrl + 'api/RefreshTokens?tokenid=' + tokenid).then(function (res) {
            return res;
        });
    };

    RefreshTokensServiceFactory.deleteRefreshTokens = _deleteRefreshTokens;
    RefreshTokensServiceFactory.getAllRefreshTokens = _getAllRefreshTokens;

    return RefreshTokensServiceFactory;

}]);