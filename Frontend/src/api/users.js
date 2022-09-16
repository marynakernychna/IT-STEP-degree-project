import instance from './configurations/configurations';
import { USERS_URLS } from './../constants/api/urls';

export default class usersService {

    static getPageOfClients(paginationFilterModel) {
        return instance.get(USERS_URLS.VIEW_PAGE_OF_CLIENTS +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static getUserProfile() {
        return instance.get(USERS_URLS.PROFILE);
    }

    static updateClientsProfile(model, userEmail) {
        return instance.put(USERS_URLS.UPDATE_CLIENT_PROFILE +
            `?email=${userEmail}`, model);
    }

    static updateProfile(model) {
        return instance.put(USERS_URLS.UPDATE_PROFILE, model);
    }

    static getPageOfCouiers(paginationFilterModel) {
        return instance.get(USERS_URLS.VIEW_PAGE_OF_COURIERS +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }
}
