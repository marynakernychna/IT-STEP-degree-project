import * as types from './types';

export const setAccess = (data) => {
    return {
        type: types.SET_ACCESS,
        payload: data
    }
}

export const logout = () => {
    return {
        type: types.LOGOUT
    };
}
