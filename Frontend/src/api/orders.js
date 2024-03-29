import instance from './configurations/configurations';
import { ORDERS_URLS } from '../constants/api/urls';

export default class ordersService {

    static create(model) {
        return instance.post(ORDERS_URLS.CREATE, model);
    }

    static getPageByClient(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_PAGE_BY_CLIENT +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static getPageOfAvailable(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_PAGE_OF_AVAILABLE +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static getPageOfAssignedByCourier(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_PAGE_OF_ASSIGNED_BY_COURIER +
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
        console.log(orderId)
        return instance.put(ORDERS_URLS.CONFIRM_DELIVERY, orderId);
    }

    static rejectDeliveryConfirmation(orderId) {
        return instance.put(ORDERS_URLS.REJECT_DELIVERY_CONFIRMATION, orderId);
    }

    static getPageDeliveredOrders(paginationFilterModel) {
        return instance.get(ORDERS_URLS.GET_PAGE_OF_DELIVERED_ORDERS +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static update(model) {
        return instance.put(ORDERS_URLS.UPDATE, model);
    }
}
