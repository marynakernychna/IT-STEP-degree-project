import { statusCodes } from '../constants/statusCodes';
import usersService from './../api/users';
import { errorMessage } from './alerts';
import { generalMessages } from '../constants/messages/general';
import { usersMessages } from '../constants/messages/users'

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

export function getUserProfileInfo() {

    return usersService
        .getUserInfo()
        .then(
            (response) => {
                if (response.status === statusCodes.NO_CONTENT) {
                    return null;
                }

                return response.data;
            },
            () => {
                errorMessage(
                    usersMessages.GET_USER_INFO_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                usersMessages.GET_USER_INFO_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}
