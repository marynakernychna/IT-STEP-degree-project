import instance from './configurations/configurations';
import { CATEGORIES_URLS } from './../constants/api/urls';

export default class categoriesService {

    static getAll(paginationFilterModel) {
        return instance.get(CATEGORIES_URLS.GET_ALL +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static updateCategory(model) {
        return instance.put(CATEGORIES_URLS.UPDATE, model);
    }

    static deleteCategory(model) {
        return instance.delete(CATEGORIES_URLS.DELETE + `?title=${model}`);
    }
}
