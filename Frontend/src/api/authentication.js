import instance from './configurations/configurations';
import { AUTHENTICATION_URLS } from './../constants/api/urls';

export default class authenticationService {
    static clientRegister(data) {
        return instance.post(AUTHENTICATION_URLS.CLIENT_REGISTER, data);
    }

    static login(data) {
        return instance.post(AUTHENTICATION_URLS.LOGIN, data);
    }

    static logout(model) {
        return instance.post(AUTHENTICATION_URLS.LOGOUT, model);
    }

    static changePassword(model) {
        return instance.put(AUTHENTICATION_URLS.CHANGE_PASSWORD, model);
    }

    static sendResetPasswordRequest(model) {
        return instance.post(AUTHENTICATION_URLS.RESET_PASSWORD_REQUEST, model);
    }

    static resetPassword(model) {
        return instance.put(AUTHENTICATION_URLS.RESET_PASSWORD, model);
    }

    static courierRegister(data) {
        return instance.post(AUTHENTICATION_URLS.COURIER_REGISTER, data);
    }

    static confirmEmail(model) {
        return instance.post(AUTHENTICATION_URLS.CONFIRM_EMAIL, model);
    }
}
