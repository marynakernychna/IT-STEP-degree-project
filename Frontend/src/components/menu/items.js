import { SolutionOutlined, TagsOutlined, UserOutlined, LogoutOutlined } from '@ant-design/icons';
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
        getItem("View", pageUrls.CATEGORIES_VIEW)
    ]),
    getItem("Logout", "Logout", <LogoutOutlined />)
];

export const userItems = [
    getItem("Profile", pageUrls.VIEW_PROFILE_INFO, <UserOutlined />),
    getItem("Logout", "Logout", <LogoutOutlined />)
];
