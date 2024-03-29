import { statusCodes } from '../constants/statusCodes';
import { errorMessage, successMessage } from './alerts';
import { generalMessages } from '../constants/messages/general';
import goodsService from './../api/goods';
import { pageUrls } from './../constants/pageUrls';

export function createGood(model, history) {

    return goodsService
        .create(model)
        .then(
            () => {
                successMessage(
                    generalMessages.CREATION_SUCCESSFUL
                );
                history.push(pageUrls.VIEW_MY_GOODS);
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

export function getAllGoods(paginationFilterModel) {

    return goodsService
        .getAll(paginationFilterModel)
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

export function getGoodsByCategory(paginationFilterModel) {

    return goodsService
        .getByCategory(paginationFilterModel)
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

export function getFullGoodInfo(id) {

    return goodsService
        .getById(id)
        .then(
            (response) => {
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

export function getCreatedByUserGoods(paginationFilterModel) {

    return goodsService
        .getCreatedByClient(paginationFilterModel)
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
