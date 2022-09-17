import { generalMessages } from '../constants/messages/general';
import { userRoles } from '../constants/userRoles';
import { authenticationMessages } from './../constants/messages/authentication';
import jwt from 'jwt-decode';
import tokenService from '../services/tokens';
import { errorMessage } from '../services/alerts';
import * as types from '../reduxActions/authentication/types';

const intialState = {
    userRole: userRoles.GUEST,
    isUserAuthorized: false
};

const authenticationReducer = (state = intialState, action) => {
    switch (action.type) {
        case types.SET_ACCESS: {
            const { accessToken, refreshToken } = action.payload;

            let decodedAccessToken = jwt(accessToken);
            let role = decodedAccessToken.role;

            if (userRoles[role.toUpperCase()] !== undefined) {

                tokenService.setLocalAccessToken(accessToken);
                tokenService.setLocalRefreshToken(refreshToken);

                return {
                    ...state,
                    userRole: role,
                    isUserAuthorized: true
                }
            }

            errorMessage(
                authenticationMessages.LOGIN_FAILED,    // because we set a role only after login
                generalMessages.SOMETHING_WENT_WRONG
            );

            break;
        }
        case types.LOGOUT: {

            tokenService.deleteTokens();
            
            return {
                ...state,
                userRole: userRoles.GUEST,
                isUserAuthorized: false
            }
        }

        default: {
            return state;
        }
    }
}

export default authenticationReducer;
