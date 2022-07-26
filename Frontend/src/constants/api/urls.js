export const SERVER_URL = "https://localhost:44314/api";
const AUTHENTICATION_URL = "/Authentication";
const USERS_URL = "/Users";
const CATEGORIES_URL = "/Categories";

export const AUTHENTICATION_URLS = {
    REGISTER_USER: AUTHENTICATION_URL + "/register",
    LOGIN: AUTHENTICATION_URL + "/login"
};

export const USERS_URLS = {
    BRIEF_USERS_INFO: USERS_URL + "/users-info",
    VIEW_PROFILE_INFO: USERS_URL + "/user-info",
    EDIT_CLIENT_INFO: USERS_URL + "/user-edit-info",
    EDIT_USER_INFO: USERS_URL + "/edit-info"
};

export const CATEGORIES_URLS = {
    GET_ALL: CATEGORIES_URL + "/all",
    UPDATE: CATEGORIES_URL + "/update",
    DELETE: CATEGORIES_URL + "/delete"
};
