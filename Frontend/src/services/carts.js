import cartsService from '../api/carts';
import { statusCodes } from './../constants/statusCodes';
import { generalMessages } from './../constants/messages/general';
import { errorMessage } from './alerts';

export function getCartByUser(paginationFilterModel) {

    return cartsService
        .getByUser(paginationFilterModel)
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
