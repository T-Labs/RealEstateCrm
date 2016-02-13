///<reference path="../../node_modules/angular-typescript/ts/definitely-typed/angularjs/angular.d.ts"/>
var app = angular.module('RealEstateMVCApp', ['ui-router', 'ui.bootstrap']);
var configFunction = function ($stateProvider, $httpProvider, $locationProvider) {
    $locationProvider.hashPrefix('!').html5Mode(true);
    $stateProvider
        .state('home', {
        url: '/',
        controller: function () {
        }
    });
};
configFunction.$inject = ['$stateProvider', '$httpProvider', '$locationProvider'];
app.config(configFunction);
