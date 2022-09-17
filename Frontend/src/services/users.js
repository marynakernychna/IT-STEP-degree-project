import { statusCodes } from '../constants/statusCodes';
import usersService from './../api/users';
import { errorMessage, successMessage } from './alerts';
import { generalMessages } from '../constants/messages/general';
import { usersMessages } from './../constants/messages/users';

export function getBriefUsersInfo(paginationFilterModel) {

    return usersService
        .getPageOfClients(paginationFilterModel)
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
        .getUserProfile()
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
            });
}

export async function editClientInfo(model, userEmail) {

    return await usersService
        .updateClientsProfile(model, userEmail)
        .then(
            () => {
                successMessage(usersMessages.EDIT_USER_INFO_SUCCESS);

                return true;
            },
            (err) => {
                err.response === statusCodes.BAD_REQUEST ?
                    errorMessage(
                        usersMessages.EDIT_USER_INFO_FAILED_EMAIL_ALREADY_EXIST,
                        ""
                    )
                    :
                    errorMessage(
                        usersMessages.EDIT_USER_INFO_FAILED,
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

export async function editUserInfo(model) {

    return await usersService
        .updateProfile(model)
        .then(
            () => {
                successMessage(usersMessages.EDIT_USER_INFO_SUCCESS);

                return true;
            },
            (err) => {
                err.response === statusCodes.BAD_REQUEST ?
                    errorMessage(
                        usersMessages.EDIT_USER_INFO_FAILED_EMAIL_ALREADY_EXIST,
                        ""
                    )
                    :
                    errorMessage(
                        usersMessages.EDIT_USER_INFO_FAILED,
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

export function getCouriersInfo(paginationFilterModel) {

    return usersService
        .getPageOfCouiers(paginationFilterModel)
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
