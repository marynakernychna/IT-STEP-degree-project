import { statusCodes } from '../constants/statusCodes';
import { errorMessage } from './alerts';
import { generalMessages } from '../constants/messages/general';
import categoriesService from './../api/categories';

export function getCategories(paginationFilterModel) {
    
    return categoriesService
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
