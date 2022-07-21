import React from 'react';
import { adminItems, userItems } from './items';
import { userRoles } from './../../constants/userRoles';
import { useHistory } from 'react-router-dom';
import { Menu } from 'antd';

function CustomMenu(type) {
    let history = useHistory();

    const onPaginationChange = (item) => {
        history.push(item.key);
    };

    return (
        <Menu
            defaultSelectedKeys={['1']}
            mode="inline"
            theme="dark"
            items={type === userRoles.USER ? userItems : adminItems}
            id="menu"
            onSelect={(item) => onPaginationChange(item)}
        />
    );
};

export default CustomMenu;
