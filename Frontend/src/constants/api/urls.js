export const SERVER_URL = "https://localhost:44314/api";
const AUTHENTICATION_URL = "/Authentication";
const USERS_URL = "/Users";
const CATEGORIES_URL = "/Categories";
const GOODS_URL = "/Wares";
const CARTS_URL = "/Carts";

export const AUTHENTICATION_URLS = {
    REGISTER_USER: AUTHENTICATION_URL + "/register",
    LOGIN: AUTHENTICATION_URL + "/login",
    LOGOUT: AUTHENTICATION_URL + "/logout",
    CHANGE_PASSWORD: AUTHENTICATION_URL + "/change-password",
    REQUES_PASSWORD_RESET: AUTHENTICATION_URL + "/request-password-reset",
    RESET_PASSWORD: AUTHENTICATION_URL + "/reset-password"
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
    GET_BY_ID: GOODS_URL + "/by-id",
    GET_CREATED_BY_USER: GOODS_URL + "/created-by-user"
};

export const CARTS_URLS = {
    GET_BY_USER: CARTS_URL + "/by-user",
    DELETE_WARE_BY_USER: CARTS_URL + "/delete-ware",
    ADD_WARE_BY_USER: CARTS_URL + "/add-ware",
    GET_BY_USER_ADMIN: CARTS_URL + "/admin/by-user"
};
