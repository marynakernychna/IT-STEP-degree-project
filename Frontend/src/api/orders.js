import instance from './configurations/configurations';
import { ORDERS_URLS } from '../constants/api/urls';

export default class ordersService {

    static create(model) {
        return instance.post(ORDERS_URLS.CREATE, model);
    }
    
    static getAvailable(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_AVAILABLE +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }
    
    static assign(orderId) {
        return instance.get(ORDERS_URLS.ASSIGN +
            `?OrderId=${orderId}`);
    }
}
