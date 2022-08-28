import { generalMessages } from './../constants/messages/general';
import { authenticationMessages } from './../constants/messages/authentication';
import { successMessage, errorMessage } from './alerts';
import { statusCodes } from './../constants/statusCodes';
import authenticationService from './../api/authentication';
import { store } from './../store';
import { setAccess, logout } from './../reduxActions/authentication/index';
import { userRoles } from '../constants/userRoles';
import { pageUrls } from './../constants/pageUrls';
import tokenService from './tokens';

export function registerUser(userData, history) {

    const model = {
        name: userData.name,
        surname: userData.surname,
        phoneNumber: userData.phoneNumber,
        email: userData.email,
        password: userData.password
    };

    authenticationService
        .registerUser(model)
        .then(
            () => {
                successMessage(
                    authenticationMessages.SUCCESSFUL_REGISTRATION
                );

                history.push(pageUrls.LOGIN);
            },
            (err) => {
                err.response.status === statusCodes.BAD_REQUEST
                    ? errorMessage(
                        authenticationMessages.REGISTRATION_FAILED,
                        authenticationMessages.REGISTRATION_FAILED_USER_ALREADY_EXIST
                    )
                    : errorMessage(
                        authenticationMessages.REGISTRATION_FAILED,
                        generalMessages.SOMETHING_WENT_WRONG
                    );
            }
        )
        .catch(() => {
            errorMessage(
                authenticationMessages.REGISTRATION_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function loginUser(userData, history) {

    const model = {
        email: userData.email,
        password: userData.password
    };

    authenticationService
        .loginUser(model)
        .then(
            (response) => {
                store.dispatch(setAccess(response.data));

                const role = store.getState().authenticationReducer.userRole;

                switch (role) {
                    case userRoles.USER:
                        history.push(pageUrls.VIEW_GOODS);
                        break;
                    case userRoles.ADMIN:
                        history.push(pageUrls.CLIENTS_BRIEF_INFO);
                        break;
                    case userRoles.COURIER:
                        history.push(pageUrls.AVAILABLE_ORDERS);
                        break;
                    default:
                        errorMessage(
                            authenticationMessages.LOGIN_FAILED,
                            generalMessages.SOMETHING_WENT_WRONG
                        );
                        break;
                }
            },
            () => {
                errorMessage(
                    authenticationMessages.LOGIN_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                authenticationMessages.LOGIN_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export function logoutUser() {
    const model = {
        refreshToken: tokenService.getLocalRefreshToken()
    };

    authenticationService
        .logout(model)
        .then(
            () => {
                store.dispatch(logout());
            },
            () => {
                errorMessage(
                    authenticationMessages.LOGOUT_FAILED,
                    generalMessages.SOMETHING_WENT_WRONG
                );
            }
        )
        .catch(() => {
            errorMessage(
                authenticationMessages.LOGOUT_FAILED,
                generalMessages.SOMETHING_WENT_WRONG
            );
        });
}

export async function changePassword(model) {

    return await
        authenticationService.changePassword(model)
            .then(
                () => {
                    successMessage(authenticationMessages.CHANGE_PASSWORD_SUCCESS);
                },
                (err) => {
                    err.response.status === statusCodes.BAD_REQUEST
                        ? errorMessage(
                            err.response.data,
                            authenticationMessages.CHANGE_PASSWORD_FAILED
                        )
                        : errorMessage(
                            authenticationMessages.CHANGE_PASSWORD_FAILED,
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

export async function requestPasswordReset(model) {

    return await
        authenticationService.requestPasswordReset(model)
            .then(
                () => {
                    successMessage(authenticationMessages.SEND_REQUES_SUCCESS);
                },
                (err) => {
                    err.response.status === statusCodes.BAD_REQUEST
                        ? errorMessage(
                            authenticationMessages.USER_NOT_FOUND,
                            ""
                        )
                        : errorMessage(
                            authenticationMessages.SEND_REQUES_FAILED,
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

export async function resetPassword(model, history) {

    return await
        authenticationService.resetPassword(model)
            .then(
                () => {
                    successMessage(authenticationMessages.RESET_PASSWORD_SUCCESS);
                    history.push(pageUrls.LOGIN);
                },
                (err) => {
                    err.response.status === statusCodes.BAD_REQUEST
                        ? errorMessage(
                            err.response.data,
                            ""
                        )
                        : errorMessage(
                            authenticationMessages.RESET_PASSWORD_FAILED,
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
