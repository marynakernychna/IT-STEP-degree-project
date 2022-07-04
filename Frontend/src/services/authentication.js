import { generalMessages } from './../constants/messages/general';
import { authenticationMessages } from './../constants/messages/authentication';
import { successMessage, errorMessage } from './alerts';
import { statusCodes } from './../constants/statusCodes';
import authenticationService from './../api/authentication';

export function registerUser(userData, history) {
    let model = {
        name: userData.name,
        surname: userData.surname,
        email: userData.email,
        password: userData.password
    };

    authenticationService
        .registerUser(model)
        .then(
            () => {
                successMessage(authenticationMessages.SUCCESSFUL_REGISTRATION);
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
