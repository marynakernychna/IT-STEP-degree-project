export const SERVER_URL = "https://localhost:44314/api";
const AUTHENTICATION_URL = "/Authentication";
const USERS_URL = "/Users";
const CATEGORIES_URL = "/Categories";
const GOODS_URL = "/Wares";

export const AUTHENTICATION_URLS = {
    REGISTER_USER: AUTHENTICATION_URL + "/register",
    LOGIN: AUTHENTICATION_URL + "/login",
    LOGOUT: AUTHENTICATION_URL + "/logout",
    CHANGE_PASSWORD: AUTHENTICATION_URL + "/change-password"
};

export const USERS_URLS = {
    BRIEF_USERS_INFO: USERS_URL + "/users-info",
    VIEW_PROFILE_INFO: USERS_URL + "/user-info",
    EDIT_CLIENT_INFO: USERS_URL + "/user-edit-info",
    EDIT_USER_INFO: USERS_URL + "/edit-info"
};

export const CATEGORIES_URLS = {
    GET_PAGINATED_ALL: CATEGORIES_URL + "/all",
    GET_ALL: CATEGORIES_URL,
    CREATE: CATEGORIES_URL + "/create",
    UPDATE: CATEGORIES_URL + "/update",
    DELETE: CATEGORIES_URL + "/delete"
};

export const GOODS_URLS = {
    CREATE: GOODS_URL + "/create",
    GET_PAGINATED_ALL: GOODS_URL,
    GET_PAGINATED_BY_CATEGORY: GOODS_URL + "/by-category",
    GET_BY_ID: GOODS_URL + "/by-id"
};
