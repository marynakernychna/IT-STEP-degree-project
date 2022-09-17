import { statusCodes } from '../constants/statusCodes';
import { errorMessage } from './alerts';
import { generalMessages } from '../constants/messages/general';
import categoriesService from './../api/categories';

export function getPagedCategories(paginationFilterModel) {

    return categoriesService
        .getPagedAll(paginationFilterModel)
        .then(
            (response) => {
                if (response.status === statusCodes.NO_CONTENT) {
                    return null;
                }

                return response.data;
            },
            () => {
                errorMessage(
                    generalMessages.GET_DATA_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                generalMessages.GET_DATA_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function getCategories() {

    return categoriesService
        .getAll()
        .then(
            (response) => {
                if (response.status === statusCodes.NO_CONTENT) {
                    return null;
                }

                return response.data;
            },
            () => {
                errorMessage(
                    generalMessages.GET_DATA_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                generalMessages.GET_DATA_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function updateCategory(model) {

    return categoriesService
        .updateCategory(model)
        .then(
            () => { 
                return true;
            },
            () => {
                errorMessage(
                    generalMessages.CHANGE_DATA_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                generalMessages.CHANGE_DATA_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function deleteCategory(title) {

    return categoriesService
        .deleteCategory(title)
        .then(
            () => { 
                return true;
            },
            () => {
                errorMessage(
                    generalMessages.DELETE_DATA_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                generalMessages.DELETE_DATA_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function createCategory(title) {

    return categoriesService
        .createCategory({ title })
        .then(
            () => {
                return true;
            },
            (err) => {
                err.response.status === statusCodes.BAD_REQUEST && err.response.data !== undefined ?
                    errorMessage(
                        err.response.data,
                        generalMessages.SOMETHING_WENT_WRONG
                    )
                    :
                    errorMessage(
                        generalMessages.CREATE_FAILED,
                        generalMessages.SOMETHING_WENT_WRONG
                    );
            }
        )
        .catch(() => {
            errorMessage(
                generalMessages.CREATE_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}
