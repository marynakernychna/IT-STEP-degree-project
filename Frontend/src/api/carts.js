import instance from './configurations/configurations';
import { CARTS_URLS } from '../constants/api/urls';

export default class cartsService {

    static getPageByClient(paginationFilterModel) {
        return instance.get(CARTS_URLS.GET_PAGE_BY_CLIENT +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static deleteWareByClient(goodId) {
        return instance.delete(CARTS_URLS.DELETE_WARE_BY_CLIENT +
            `?Id=${goodId}`);
    }

    static addWareByClient(model) {
        return instance.put(CARTS_URLS.ADD_WARE_BY_CLIENT, model);
    }

    static getPageByClientAdmin(paginationFilterModel) {
        return instance.get(CARTS_URLS.GET_PAGE_BY_CLIENT_ADMIN +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}
             &UserEmail=${paginationFilterModel.userEmail}`);
    }
}
