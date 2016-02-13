'use strict';
class HomeController {

    public rentData = [];
    /* @ngInject */
    constructor($http) {
        var temp = 1;
        $http.get('/api/rent').then((res) => {
            this.rentData = res.data;
        });
    }
}


