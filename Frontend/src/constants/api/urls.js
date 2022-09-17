export const SERVER_URL = "https://localhost:44314/api";
const AUTHENTICATION_URL = "/Authentication";
const USERS_URL = "/Users";
const CATEGORIES_URL = "/Categories";
const GOODS_URL = "/Wares";
const CARTS_URL = "/Carts";
const ORDERS_URL = "/Orders";

export const AUTHENTICATION_URLS = {
    CLIENT_REGISTER: AUTHENTICATION_URL + "/clients/register",
    LOGIN: AUTHENTICATION_URL + "/login",
    LOGOUT: AUTHENTICATION_URL + "/logout",
    CHANGE_PASSWORD: AUTHENTICATION_URL + "/change-password",
    RESET_PASSWORD_REQUEST: AUTHENTICATION_URL + "/reset-password-request",
    RESET_PASSWORD: AUTHENTICATION_URL + "/reset-password",
    COURIER_REGISTER: AUTHENTICATION_URL + "/couriers/register",
    CONFIRM_EMAIL: AUTHENTICATION_URL + "/confirm-email"
};

export const USERS_URLS = {
    VIEW_PAGE_OF_CLIENTS: USERS_URL + "/admins/clients/page",
    PROFILE: USERS_URL + "/profile",
    UPDATE_CLIENT_PROFILE: USERS_URL + "/admins/clients/by-client/profile/update",
    UPDATE_PROFILE: USERS_URL + "/profile/update",
    VIEW_PAGE_OF_COURIERS: USERS_URL + "/admins/couriers/page"
};

export const CATEGORIES_URLS = {
    GET_PAGE: CATEGORIES_URL + "/admins/page",
    GET_ALL: CATEGORIES_URL,
    CREATE: CATEGORIES_URL + "/admins/create",
    UPDATE: CATEGORIES_URL + "/admins/update",
    DELETE: CATEGORIES_URL + "/admins/delete"
};

export const GOODS_URLS = {
    CREATE: GOODS_URL + "/create",
    GET_PAGE: GOODS_URL + "/page",
    GET_PAGE_BY_CATEGORY: GOODS_URL + "/by-category/page",
    GET_BY_ID: GOODS_URL + "/by-id",
    GET_CREATED_BY_CLIENT: GOODS_URL + "/clients/by-client/page"
};

export const CARTS_URLS = {
    GET_PAGE_BY_CLIENT: CARTS_URL + "/by-client/page",
    DELETE_WARE_BY_CLIENT: CARTS_URL + "/by-client/delete-ware",
    ADD_WARE_BY_CLIENT: CARTS_URL + "/by-client/add-ware",
    GET_PAGE_BY_CLIENT_ADMIN: CARTS_URL + "/admins/by-client/page"
};

export const ORDERS_URLS = {
    CREATE: ORDERS_URL + "/create",
    GET_PAGE_BY_CLIENT: ORDERS_URL + "/clients/by-client/page",
    GET_PAGE_OF_ASSIGNED_BY_COURIER: ORDERS_URL + "/couriers/by-courier/assigned/page",
    GET_PAGE_OF_AVAILABLE: ORDERS_URL + "/couriers/available/page",
    ASSIGN: ORDERS_URL + "/couriers/assign",
    REJECT: ORDERS_URL + "/couriers/reject",
    DELETE: ORDERS_URL + "/delete",
    CONFIRM_DELIVERY: ORDERS_URL + "/delivery/confirm",
    REJECT_DELIVERY_CONFIRMATION: ORDERS_URL + "/delivery/reject",
    GET_PAGE_OF_DELIVERED_ORDERS: ORDERS_URL + "/delivered/by-user/page",
    UPDATE: ORDERS_URL + "/update"
};
