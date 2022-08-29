import { errorMessage, successMessage } from './alerts';
import { generalMessages } from '../constants/messages/general';
import ordersService from './../api/orders';
import { ordersMessages } from './../constants/messages/orders';
import { statusCodes } from './../constants/statusCodes';

export function orderAll(model) {

    return ordersService
        .create(model)
        .then(
            () => {
                successMessage(
                    ordersMessages.SUCCESSFULLY_ORDERED
                );

                return true;
            },
            () => {
                errorMessage(
                    ordersMessages.FAILED_TO_ORDER,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                ordersMessages.FAILED_TO_ORDER,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function getAvailableOrders(paginationFilterModel) {

    return ordersService
        .getAvailable(paginationFilterModel)
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

export function assignToOrder(orderId) {

    return ordersService
        .assign(orderId)
        .then(
            () => {
                successMessage(
                    ordersMessages.ORDER_SUCCESSFULLY_PICKED
                );

                return true;
            },
            (err) => {
                errorMessage(
                    err.response.data,
                    generalMessages.SOMETHING_WENT_WRONG
                )
            }
        )
        .catch(() => {
            errorMessage(
                ordersMessages.FAILED_PICKED_ORDER,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}
