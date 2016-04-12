///<reference path="./typings/angularjs/angular.d.ts"/>
var app = angular.module('RealEstateMVCApp', ['ui.router']);
/** @ngInject */
function routerConfig($stateProvider, $urlRouterProvider, $locationProvider) {
    //   $locationProvider.html5Mode(true);
    $stateProvider
        .state('home', {
        url: '/?city&district&houseType&priceFrom&priceTo&page',
        reloadOnSearch: false
    });
}
app
    .controller('HomeController', HomeController)
    .config(routerConfig);
//# sourceMappingURL=App.js.map