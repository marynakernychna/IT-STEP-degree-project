import instance from './configurations/configurations';
import { ORDERS_URLS } from '../constants/api/urls';

export default class ordersService {

    static create(model) {
        return instance.post(ORDERS_URLS.CREATE, model);
    }
}
