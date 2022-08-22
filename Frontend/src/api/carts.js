import instance from './configurations/configurations';
import { CARTS_URLS } from '../constants/api/urls';

export default class cartsService {

    static getByUser(paginationFilterModel) {
        return instance.get(CARTS_URLS.GET_BY_USER +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }
}
