import { errorMessage, successMessage } from './alerts';
import { generalMessages } from '../constants/messages/general';
import ordersService from './../api/orders';
import { ordersMessages } from './../constants/messages/orders';
import { statusCodes } from './../constants/statusCodes';

export function create(model) {

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

export function getClientOrders(paginationFilterModel) {

    return ordersService
        .getPageByClient(paginationFilterModel)
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

export function getAvailableOrders(paginationFilterModel) {

    return ordersService
        .getPageOfAvailable(paginationFilterModel)
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

export function assignToOrder(id) {

    return ordersService
        .assign({ id })
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

export function getOrdersByCourier(paginationFilterModel) {

    return ordersService
        .getPageOfAssignedByCourier(paginationFilterModel)
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

export function rejectSelectedOrder(id) {

    return ordersService
        .reject({ id })
        .then(
            () => {
                successMessage(
                    ordersMessages.ORDER_SUCCESSFULLY_REJECT
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
                ordersMessages.FAILED_REJECT_ORDER,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function deleteOrder(goodId) {

    return ordersService
        .delete(goodId)
        .then(
            () => {
                successMessage(
                    ordersMessages.ORDER_SUCCESSFULLY_DELETED
                );

                return true;
            },
            (err) => {
                errorMessage(
                    err.response.data,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                ordersMessages.DELETE_ORDER_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function confirmOrderDelivery(id) {

    return ordersService
        .confirmDelivery({ id })
        .then(
            () => {
                successMessage(
                    ordersMessages.DELIVERY_SUCCESSFULLY_CONFIRMED
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
                ordersMessages.FAILED_CONFIRM_DELIVERY,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function rejectDeliveryConfirmation(id) {

    return ordersService
        .rejectDeliveryConfirmation({ id })
        .then(
            () => {
                successMessage(
                    ordersMessages.DELIVERY_CONFIRMATION_REJECTED_SUCCESSFULLY
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
                ordersMessages.FAILED_REJECT_DELIVERY_CONFIRMATION,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function getDeliveredOrders(paginationFilterModel) {

    return ordersService
        .getPageDeliveredOrders(paginationFilterModel)
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

export async function changeOrderInfo(orderInfo) {

    const model = {
        orderInfo: {
            address: orderInfo.address,
            city: orderInfo.city,
            country: orderInfo.country
        },
        orderId: orderInfo.orderId
    };

    return await ordersService
        .update(model)
        .then(
            () => {
                successMessage(ordersMessages.EDIT_ORDER_INFO_SUCCESS);

                return true;
            },
            () => {
                errorMessage(
                    ordersMessages.EDIT_ORDER_INFO_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                generalMessages.SOMETHING_WENT_WRONG,
                ""
            );
        });
}
