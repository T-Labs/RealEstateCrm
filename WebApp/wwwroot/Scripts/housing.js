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
                window.alert('success!');
                var topMenu = $('topMenu');
                topMenu.popup({
                    position: 'right center',
                    title: 'My favorite dog',
                    content: 'My favorite dog would like other dogs as much as themselves'
                });
            },
            error: function (response) {
                alert('error');
            }
        });
    };
    return Housiong;
})();
//# sourceMappingURL=housing.js.map