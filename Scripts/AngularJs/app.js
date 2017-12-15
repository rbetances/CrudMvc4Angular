var app = angular.module("app", ["ngRoute"]);
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
    .when('/',

    {
        redirectTo: function () {
            return "/list";
        }
    })
    .when('/list',
    {
        templateUrl: "User/List.html",
        controller: "ListController"
    })
    .when('/Create',
    {
        templateUrl: "User/Edit.html",
        controller: "EditController"
    })
    .when('/Edit/:id',
    {
        templateUrl: "User/Edit.html",
        controller: "EditController"
    })
    .when('/Delete/:id',
    {
        templateUrl: "User/Delete.html",
        controller: "DeleteController"
    });;
}]);