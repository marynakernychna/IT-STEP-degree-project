import instance from './configurations/configurations';
import { AUTHENTICATION_URLS } from './../constants/api/urls';

export default class authenticationService {
    static registerUser(model) {
        return instance.post(AUTHENTICATION_URLS.REGISTER_USER, model);
    }
}
