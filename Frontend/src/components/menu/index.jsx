import React from 'react';
import { Menu } from 'antd';
import { adminItems, userItems } from './items';
import { userRoles } from './../../constants/userRoles';

function CustomMenu(type) {
    return (
        <Menu
            defaultSelectedKeys={['1']}
            defaultOpenKeys={['sub1']}
            mode="inline"
            theme="dark"
            items={type === userRoles.USER ? userItems : adminItems}
            id="menu"
        />
    );
};

export default CustomMenu;
