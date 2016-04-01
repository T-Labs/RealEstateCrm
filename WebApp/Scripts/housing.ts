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
                    Notify.success('Прозвон успешно добавлен');
                   
                },
                error: (response) => {
                    Notify.error('Ошибка при добавления прозвонов');
                    console.error(response);
                }
            });
    }
}