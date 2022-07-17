import { statusCodes } from '../constants/statusCodes';
import usersService from './../api/users';
import { errorMessage } from './alerts';
import { usersMessages } from './../constants/messages/users';
import { generalMessages } from '../constants/messages/general';

export function getBriefUsersInfo(paginationFilterModel) {
    return usersService
        .getBriefUsersInfo(paginationFilterModel)
        .then(
            (response) => {
                if (response.status === statusCodes.NO_CONTENT) {
                    return null;
                }

                return response.data;
            },
            () => {
                errorMessage(
                    usersMessages.GET_BRIEF_USERS_INFO_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                usersMessages.GET_BRIEF_USERS_INFO_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}
