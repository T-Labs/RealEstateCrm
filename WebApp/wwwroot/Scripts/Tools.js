'use strict';
var Notify = (function () {
    function Notify() {
    }
    Notify.success = function (message) {
        $.notify(message, 'success');
    };
    Notify.error = function (message) {
        $.notify(message, 'error');
    };
    Notify.info = function (message) {
        $.notify(message, 'info');
    };
    return Notify;
})();
//# sourceMappingURL=Tools.js.map