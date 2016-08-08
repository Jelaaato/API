'use strict';
app.factory('authenticationService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var BaseUrl = 'https://itworkssys:446/';
    var authenticationServiceFactory = {};

    var _auth = {
        isAuth: false,
        UserName: ""
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(BaseUrl + 'api/Account/Register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.UserName + "&password=" + loginData.Password;

        var deferred = $q.defer();

        $http.post(BaseUrl + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, UserName: loginData.UserName });

            _auth.isAuth = true;
            _auth.UserName = loginData.UserName;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _auth.isAuth = false;
        _auth.UserName = "";

    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _auth.isAuth = true;
            _auth.UserName = authData.UserName;
        }

    }

    authenticationServiceFactory.saveRegistration = _saveRegistration;
    authenticationServiceFactory.login = _login;
    authenticationServiceFactory.logOut = _logOut;
    authenticationServiceFactory.fillAuthData = _fillAuthData;
    authenticationServiceFactory.authentication = _auth;

    return authenticationServiceFactory;
}]);