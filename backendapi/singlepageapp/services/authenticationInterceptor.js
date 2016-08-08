'use strict';
app.factory('authenticationInterceptor', ['$q', '$location', 'localStorageService', function ($q, $location, localStorageService) {

    var authenticationInterceptorService = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authenticationInterceptorService.request = _request;
    authenticationInterceptorService.responseError = _responseError;

    return authenticationInterceptorService;
}]);