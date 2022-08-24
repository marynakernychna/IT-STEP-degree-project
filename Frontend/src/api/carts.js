import instance from './configurations/configurations';
import { CARTS_URLS } from '../constants/api/urls';

export default class cartsService {

    static getByUser(paginationFilterModel) {
        return instance.get(CARTS_URLS.GET_BY_USER +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static deleteWareByUser(goodId) {
        return instance.delete(CARTS_URLS.DELETE_WARE_BY_USER +
            `?Id=${goodId}`);
    }

    static addWareByUser(model) {
        return instance.post(CARTS_URLS.ADD_WARE_BY_USER, model);
    }

    static getByUserAdmin(paginationFilterModel) {
        return instance.get(CARTS_URLS.GET_BY_USER_ADMIN +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}
             &UserEmail=${paginationFilterModel.userEmail}`);
    }
}
