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
}
