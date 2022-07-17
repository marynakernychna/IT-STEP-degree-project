import { SolutionOutlined } from '@ant-design/icons';

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
        getItem("Brief info", '1')
    ])
];

export const userItems = [];
