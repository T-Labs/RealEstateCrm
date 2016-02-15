///<reference path="./typings/angularjs/angular.d.ts"/>

var app = angular.module('RealEstateMVCApp', ['ui.router']);

/** @ngInject */
function routerConfig($stateProvider: angular.ui.IStateProvider, $urlRouterProvider: angular.ui.IUrlRouterProvider,
    $locationProvider: any) {
 //   $locationProvider.html5Mode(true);
    $stateProvider
        .state('home', {
            url: '/?city&district&houseType&priceFrom&priceTo&page',
         
            reloadOnSearch: false
        });
}

app.controller('HomeController', HomeController).config(routerConfig);