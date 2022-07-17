import instance from './configurations/configurations';
import { USERS_URLS } from './../constants/api/urls';

export default class usersService {

    static getBriefUsersInfo(paginationFilterModel) {
        return instance.get(USERS_URLS.BRIEF_USERS_INFO +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }
}
