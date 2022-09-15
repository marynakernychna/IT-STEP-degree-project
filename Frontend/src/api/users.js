import instance from './configurations/configurations';
import { USERS_URLS } from './../constants/api/urls';

export default class usersService {

    static getBriefUsersInfo(paginationFilterModel) {
        return instance.get(USERS_URLS.BRIEF_USERS_INFO +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static getUserInfo() {
        return instance.get(USERS_URLS.VIEW_PROFILE_INFO);
    }

    static editClientInfo(model, userEmail) {
        return instance.post(USERS_URLS.EDIT_CLIENT_INFO +
            `?email=${userEmail}`, model);
    }

    static editUserInfo(model) {
        return instance.post(USERS_URLS.EDIT_USER_INFO, model);
    }

    static getCouriersInfo(paginationFilterModel) {
        return instance.get(USERS_URLS.VIEW_COURIERS_INFO +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }
}
