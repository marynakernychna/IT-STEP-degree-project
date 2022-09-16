import cartsService from '../api/carts';
import { statusCodes } from './../constants/statusCodes';
import { generalMessages } from './../constants/messages/general';
import { errorMessage, successMessage } from './alerts';
import { cartsMessages } from './../constants/messages/carts';

export function getCartByUser(paginationFilterModel) {

    return cartsService
        .getPageByClient(paginationFilterModel)
        .then(
            (response) => {
                if (response.status === statusCodes.NO_CONTENT) {
                    return null;
                }

                return response.data;
            },
            (err) => {
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

export function deleteWareFromCartByUser(goodId) {

    return cartsService
        .deleteWareByClient(goodId)
        .then(
            () => {
                successMessage(
                    cartsMessages.GOOD_SUCCESSFULLY_DELETED
                );

                return true;
            },
            (err) => {
                errorMessage(
                    cartsMessages.DELETE_GOOD_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                cartsMessages.DELETE_GOOD_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function addWareToCartByUser(goodId) {

    return cartsService
        .addWareByClient({ id: goodId })
        .then(
            () => {
                successMessage(
                    cartsMessages.GOOD_SUCCESSFULLY_ADDED
                );

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
                        cartsMessages.ADD_GOOD_FAILED,
                        generalMessages.SOMETHING_WENT_WRONG
                    );
            }
        )
        .catch(() => {
            errorMessage(
                cartsMessages.ADD_GOOD_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function getCartByUserAdmin(paginationFilterModel) {

    return cartsService
        .getPageByClientAdmin(paginationFilterModel)
        .then(
            (response) => {
                if (response.status === statusCodes.NO_CONTENT) {
                    return null;
                }

                return response.data;
            },
            (err) => {
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
