import { SolutionOutlined, TagsOutlined } from '@ant-design/icons';
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
    getItem("Clients", 'sub1', <SolutionOutlined />, [
        getItem("Brief info", pageUrls.CLIENTS_BRIEF_INFO)
    ]),
    getItem("Categories", 'sub2', <TagsOutlined />, [
        getItem("View", pageUrls.CATEGORIES_VIEW)
    ])
];

export const userItems = [];
