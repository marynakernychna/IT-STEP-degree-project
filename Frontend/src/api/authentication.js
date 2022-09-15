import instance from './configurations/configurations';
import { AUTHENTICATION_URLS } from './../constants/api/urls';

export default class authenticationService {
    static registerUser(data) {
        return instance.post(AUTHENTICATION_URLS.REGISTER_USER, data);
    }

    static loginUser(data) {
        return instance.post(AUTHENTICATION_URLS.LOGIN, data);
    }

    static logout(model) {
        return instance.post(AUTHENTICATION_URLS.LOGOUT, model);
    }

    static changePassword(model) {
        return instance.put(AUTHENTICATION_URLS.CHANGE_PASSWORD, model);
    }

    static requestPasswordReset(model) {
        return instance.post(AUTHENTICATION_URLS.REQUES_PASSWORD_RESET, model);
    }

    static resetPassword(model) {
        return instance.put(AUTHENTICATION_URLS.RESET_PASSWORD, model);
    }

    static registerCourier(data) {
        return instance.post(AUTHENTICATION_URLS.REGISTER_COURIER, data);
    }
}
