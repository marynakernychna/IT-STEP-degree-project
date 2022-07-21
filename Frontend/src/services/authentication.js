import { generalMessages } from './../constants/messages/general';
import { authenticationMessages } from './../constants/messages/authentication';
import { successMessage, errorMessage } from './alerts';
import { statusCodes } from './../constants/statusCodes';
import authenticationService from './../api/authentication';
import { store } from './../store';
import { setAccess } from './../reduxActions/authentication/index';
import { userRoles } from '../constants/userRoles';
import { pageUrls } from './../constants/pageUrls';

export function registerUser(userData, history) {

    const model = {
        name: userData.name,
        surname: userData.surname,
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

                if (userRoles[role.toUpperCase()] === undefined) {
                    errorMessage(
                        authenticationMessages.LOGIN_FAILED,
                        generalMessages.SOMETHING_WENT_WRONG
                    );

                    return;
                }

                history.push(pageUrls.CLIENTS_BRIEF_INFO);
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
