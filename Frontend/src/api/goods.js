import instance from './configurations/configurations';
import { GOODS_URLS } from './../constants/api/urls';

export default class goodsService {

    static create(model) {
        return instance.post(GOODS_URLS.CREATE, model);
    }

    static getAll(paginationFilterModel) {
        return instance.get(GOODS_URLS.GET_PAGE +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static getByCategory(paginationFilterModel) {
        return instance.get(GOODS_URLS.GET_PAGE_BY_CATEGORY +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}
             &CategoryTitle=${paginationFilterModel.categoryTitle}`);
    }

    static getById(model) {
        return instance.get(GOODS_URLS.GET_BY_ID +
            `?Id=${model}`);
    }

    static getCreatedByClient(paginationFilterModel) {
        return instance.get(GOODS_URLS.GET_CREATED_BY_CLIENT +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }
}
