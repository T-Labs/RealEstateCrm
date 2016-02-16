'use strict';
class HomeController {

    public rentData = [];
    public houseTypes = [];
    public selectedHouseType = [];
    public cityList = [];
    public city;
    public districtList = [];
    public disctrict;
    public priceFrom;
    public priceTo;

    public citySelected = [{}];
    public isEdit = false;
    loadingdata = true;
    /* @ngInject */
    constructor(private $http, private $location: ng.ILocationService) {

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

        $http.get('/api/rent/houseTypes').then((res) => {
            this.houseTypes = res.data;
        });

        $http.get('/api/rent/cityList').then((res) => {
            this.cityList = res.data;
        });

        $http.get('/api/rent/districtByCity').then((res) => {
            this.districtList = res.data;
        });


        this.priceFrom = urlParams['priceFrom'];
        this.priceTo = urlParams['priceTo'];
    }

    public changeFilters() {
        //http://localhost:4404/api/rent?houseTypeId=2&houseTypeId=4
        var url = [];
        if (this.selectedHouseType && this.selectedHouseType.length > 0) {
            this.selectedHouseType.forEach(x => url.push('houseTypeId=' + x));
        }

        if (this.city) {
           
            url.push('cityId=' + this.city);
            this.disctrict = undefined;
            this.districtList = [];
            this.$http.get('/api/rent/districtByCity?cityId=' + this.city).then((res) => {
                this.districtList = res.data;
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
            this.$http.get('/api/rent?' + url.join('&')).then((res) => {
                this.rentData = res.data;
                this.loadingdata = false;
            });
        } else {
            this.$http.get('/api/rent').then((res) => {
                this.rentData = res.data;
                this.loadingdata = false;
            });
        }

        this.$location.search({ city: this.city, district: this.disctrict, houseType: this.selectedHouseType, priceFrom: this.priceFrom, priceTo: this.priceTo });
    }
}


