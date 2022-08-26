import { errorMessage, successMessage } from './alerts';
import { generalMessages } from '../constants/messages/general';
import ordersService from './../api/orders';
import { ordersMessages } from './../constants/messages/orders';

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
