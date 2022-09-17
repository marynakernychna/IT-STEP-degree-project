import instance from './configurations/configurations';
import { CATEGORIES_URLS } from './../constants/api/urls';

export default class categoriesService {

    static getPagedAll(paginationFilterModel) {
        return instance.get(CATEGORIES_URLS.GET_PAGE +
            `?PageNumber=${paginationFilterModel.pageNumber}
             &PageSize=${paginationFilterModel.pageSize}`);
    }

    static getAll() {
        return instance.get(CATEGORIES_URLS.GET_ALL);
    }

    static createCategory(model) {
        return instance.post(CATEGORIES_URLS.CREATE, model);
    }

    static updateCategory(model) {
        return instance.put(CATEGORIES_URLS.UPDATE, model);
    }

    static deleteCategory(model) {
        return instance.delete(CATEGORIES_URLS.DELETE + `?title=${model}`);
    }
}
