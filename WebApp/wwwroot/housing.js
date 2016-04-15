'use strict';
var Housiong = (function () {
    function Housiong() {
    }
    Housiong.addCall = function (id, status) {
        $.ajax({
            type: "POST",
            url: '/Housing/AddCall',
            data: {
                housingId: id,
                status: status
            },
            success: function (response) {
                Notify.success('Прозвон успешно добавлен');
            },
            error: function (response) {
                Notify.error('Ошибка при добавления прозвонов');
                console.error(response);
            }
        });
    };
    return Housiong;
}());
//# sourceMappingURL=housing.js.map