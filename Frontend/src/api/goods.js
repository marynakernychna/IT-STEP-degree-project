import instance from './configurations/configurations';
import { GOODS_URLS } from './../constants/api/urls';

export default class goodsService {

    static create(model) {
        return instance.post(GOODS_URLS.CREATE, model);
    }
}
