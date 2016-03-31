'use strict';

class Housiong {
    
    static addCall(id: number, status: string) {
        $.ajax({
                type: "POST",
                url: '/Housing/AddCall',
                data: {
                    housingId: id,
                    status: status
                },

                success: (response) => {
                    window.alert('success!');
                    var topMenu: any = $('topMenu');
                     topMenu.popup({
                        position : 'top right',
                        title    : 'My favorite dog',
                        content  : '<div class="ui message"><i class="close icon"></i><div class="header">Welcome back!</div><p>This is a special notification which you can dismiss if you're bored with it.</p></div>'
                      }).popup('show');
                },
                error: (response) => {
                    alert('error');
                }
            });
    }
}