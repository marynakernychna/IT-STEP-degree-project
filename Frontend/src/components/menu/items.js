import {
    SolutionOutlined,
    TagsOutlined,
    UserOutlined,
    LogoutOutlined,
    AppstoreOutlined
} from '@ant-design/icons';
import { BsBasket } from 'react-icons/bs';
import { pageUrls } from './../../constants/pageUrls';

function getItem(label, key, icon, children, type) {
    return {
        key,
        icon,
        children,
        label,
        type
    }
}

export const adminItems = [
    getItem("Profile", pageUrls.VIEW_PROFILE_INFO, <UserOutlined />),
    getItem("Clients", 'sub1', <SolutionOutlined />, [
        getItem("Brief info", pageUrls.CLIENTS_BRIEF_INFO)
    ]),
    getItem("Categories", 'sub2', <TagsOutlined />, [
        getItem("View & manage", pageUrls.CATEGORIES_VIEW_AND_MANAGE)
    ]),
    getItem("Courier", 'sub3', <TagsOutlined />, [
        getItem("View & create", pageUrls.COURIERS_VIEW_AND_CREATE)
    ]),
    getItem("Logout", "Logout", <LogoutOutlined />)
];

export const userItems = [
    getItem("Profile", pageUrls.VIEW_PROFILE_INFO, <UserOutlined />),
    getItem("Goods", 'sub1', <AppstoreOutlined />, [
        getItem("View", pageUrls.VIEW_GOODS),
        getItem("Create", pageUrls.CREATE_GOOD),
        getItem("My goods", pageUrls.VIEW_MY_GOODS)
    ]),
    getItem("Cart", pageUrls.CART, <BsBasket />),
    getItem("Orders", 'sub2', <AppstoreOutlined />, [
        getItem("Open", pageUrls.OPEN_ORDERS),
        getItem("Delivered", pageUrls.DELIVERED_ORDERS)
    ]),
    getItem("Logout", "Logout", <LogoutOutlined />)
];

export const courierItems = [
    getItem("Profile", pageUrls.VIEW_PROFILE_INFO, <UserOutlined />),
    getItem("Orders", 'sub1', <AppstoreOutlined />, [
        getItem("Available", pageUrls.AVAILABLE_ORDERS),
        getItem("Picked", pageUrls.PICKED_ORDERS),
        getItem("Delivered", pageUrls.DELIVERED_ORDERS)
    ]),
    getItem("Logout", "Logout", <LogoutOutlined />)
];
