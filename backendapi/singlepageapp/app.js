var app = angular.module('AuthApp', ['ngRoute', 'LocalStorageModule']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "HomePageController",
        templateUrl: "singlepageapp/views/home.html"
    });

    $routeProvider.when("/docu", {
        controller: "DocController",
        templateUrl: "singlepageapp/views/docu.html"
    });

    $routeProvider.when("/get", {
        controller: "DocController",
        templateUrl: "singlepageapp/views/getmethods.html"
    });

    $routeProvider.when("/post", {
        controller: "DocController",
        templateUrl: "singlepageapp/views/postmethods.html"
    });

    $routeProvider.when("/put", {
        controller: "DocController",
        templateUrl: "singlepageapp/views/putmethods.html"
    });

    $routeProvider.when("/del", {
        controller: "DocController",
        templateUrl: "singlepageapp/views/delmethods.html"
    });

    $routeProvider.when("/addAPIClient", {
        controller: "SignInController",
        templateUrl: "singlepageapp/views/signin.html"
    });

    //$routeProvider.when("/RevokeTokens", {
    //    controller: "RefreshTokensManagerController",
    //    templateUrl: "singlepageapp/views/RefreshTokens.html"
    //});

    $routeProvider.when("/auth", {
        controller: "DocController",
        templateUrl: "singlepageapp/views/Authentication.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

app.run(['authenticationService', function (authenticationService) {
    authenticationService.fillAuthData();
}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authenticationInterceptor');
});