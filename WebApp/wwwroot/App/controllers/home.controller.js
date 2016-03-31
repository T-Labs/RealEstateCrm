'use strict';
var HomeController = (function () {
    /* @ngInject */
    function HomeController($http, $location) {
        var _this = this;
        this.$http = $http;
        this.$location = $location;
        this.rentData = [];
        this.houseTypes = [];
        this.selectedHouseType = [];
        this.cityList = [];
        this.districtList = [];
        this.citySelected = [{}];
        this.isEdit = false;
        this.loadingdata = true;
        var urlParams = $location.search();
        this.city = $location.search().city;
        if (this.city) {
            this.citySelected[this.city] = { selected: true };
        }
        this.changeFilters();
        /*
        $http.get('/api/rent').then((res) => {
            this.rentData = res.data;
            this.loadingdata = false;
        });*/
        $http.get('/api/rent/houseTypes').then(function (res) {
            _this.houseTypes = res.data;
        });
        $http.get('/api/rent/cityList').then(function (res) {
            _this.cityList = res.data;
        });
        $http.get('/api/rent/districtByCity').then(function (res) {
            _this.districtList = res.data;
        });
        this.priceFrom = urlParams['priceFrom'];
        this.priceTo = urlParams['priceTo'];
    }
    HomeController.prototype.changeFilters = function () {
        var _this = this;
        //http://localhost:4404/api/rent?houseTypeId=2&houseTypeId=4
        var url = [];
        if (this.selectedHouseType && this.selectedHouseType.length > 0) {
            this.selectedHouseType.forEach(function (x) { return url.push('houseTypeId=' + x); });
        }
        if (this.city) {
            url.push('cityId=' + this.city);
            this.disctrict = undefined;
            this.districtList = [];
            this.$http.get('/api/rent/districtByCity?cityId=' + this.city).then(function (res) {
                _this.districtList = res.data;
            });
        }
        if (this.disctrict) {
            url.push('district=' + this.disctrict);
        }
        if (this.priceFrom) {
            url.push('priceFrom=' + this.priceFrom);
        }
        if (this.priceTo) {
            url.push('priceTo=' + this.priceTo);
        }
        if (url.length > 0) {
            this.loadingdata = true;
            this.rentData = [];
            this.$http.get('/api/rent?' + url.join('&')).then(function (res) {
                _this.rentData = res.data;
                _this.loadingdata = false;
            });
        }
        else {
            this.$http.get('/api/rent').then(function (res) {
                _this.rentData = res.data;
                _this.loadingdata = false;
            });
        }
        this.$location.search({ city: this.city, district: this.disctrict, houseType: this.selectedHouseType, priceFrom: this.priceFrom, priceTo: this.priceTo });
    };
    return HomeController;
})();
//# sourceMappingURL=home.controller.js.map