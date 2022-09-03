import instance from './configurations/configurations';
import { ORDERS_URLS } from '../constants/api/urls';

export default class ordersService {

    static create(model) {
        return instance.post(ORDERS_URLS.CREATE, model);
    }

    static getByUser(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_BY_USER +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static getAvailable(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_AVAILABLE +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static getByCourier(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_BY_COURIER +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static assign(orderId) {
        return instance.put(ORDERS_URLS.ASSIGN, orderId);
    }

    static reject(orderId) {
        return instance.put(ORDERS_URLS.REJECT, orderId);
    }
}
