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

    static delete(goodId) {
        return instance.delete(ORDERS_URLS.DELETE +
            `?Id=${goodId}`);
    }

    static confirmDelivery(orderId) {
        return instance.put(ORDERS_URLS.CONFIRM_DELIVERY, orderId);
    }

    static rejectDeliveryConfirmation(orderId) {
        return instance.put(ORDERS_URLS.REJECT_DELIVERY_CONFIRMATION, orderId);
    }

    static getDeliveredOrders(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_DELIVERED_ORDERS +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }
}
