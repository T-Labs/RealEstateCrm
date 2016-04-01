'use strict';

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