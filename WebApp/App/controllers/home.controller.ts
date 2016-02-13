'use strict';
class HomeController {

    public rentData = [];
    public houseTypes = [];
    public selectedHouseType = [];

    /* @ngInject */
    constructor(private $http) {
        
        $http.get('/api/rent').then((res) => {
            this.rentData = res.data;
        });

        $http.get('/api/rent/houseTypes').then((res) => {
            this.houseTypes = res.data;
        });
    }

    public changeFilters() {
        //http://localhost:4404/api/rent?houseTypeId=2&houseTypeId=4
        
        if (this.selectedHouseType && this.selectedHouseType.length > 0) {
            var url = [];
            this.selectedHouseType.forEach(x => url.push('houseTypeId=' + x));
            this.$http.get('/api/rent?' + url.join('&')).then((res) => {
                this.rentData = res.data;
            });
        }
    }
}


