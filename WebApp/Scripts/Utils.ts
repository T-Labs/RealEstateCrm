'use strict';


class Utils {

    public static loadDistrictSelectByCityId(cityId, districtControlId) {
        $.ajax({
            type: "GET",
            url: "/api/rent/districtByCity?cityId=" + cityId,
            data: {},
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success:  (msg) => {
                var district = <any>$(districtControlId);
                district.dropdown('clear');
                var options = district.get(0).options;
                options.length = 0;

                $.each(msg, (index, item) => {
                    options.add(new Option(item.Text, item.Value));
                });
            },
            error: () => {
                alert("Ошибка при загрузке списка районов");
            }
        });
    }

    public static inputNumbersOnly(sender, event) {
            var key;
            if (window.event) {
                key = (<KeyboardEvent>window.event).keyCode;
            }
            else {
                key = event.charCode;
            }

            if (!window.event && key === 0) {
                return;
            }

            var keyStr = String.fromCharCode(key);
            if (keyStr === '.' || keyStr === ',') {
                if (sender) {
                    if ($(sender).val().indexOf('.') != -1 || $(sender).val().indexOf(',') != -1) {
                        if (window.event) {
                            window.event.returnValue = false;
                        }
                        else {
                            event.preventDefault();
                        }
                        return;
                    }
                }
                if (window.event) {
                    window.event.returnValue = true;
                }
            }
            else if (key < 48 || key > 57) {
              
                if (window.event) {
                    window.event.returnValue = false;
                }
                else {
                    event.preventDefault();
                }
            }
        }
}



class Notify {
    public static success(message: string) {
        (<any>$).notify(message, 'success');
    }

    public static error(message: string) {
        (<any>$).notify(message, 'error');
    }

    public static info(message: string) {
        (<any>$).notify(message, 'info');
    }
}