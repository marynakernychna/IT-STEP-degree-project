import React from 'react';
import { adminItems, userItems } from './items';
import { userRoles } from './../../constants/userRoles';
import { useHistory } from 'react-router-dom';
import { Menu } from 'antd';

function CustomMenu(props) {
    let history = useHistory();

    const onSelect = (item) => {
        history.push(item.key);
    };

    return (
        <Menu
            defaultSelectedKeys={['1']}
            mode="inline"
            theme="dark"
            items={props.type[0] === userRoles.USER ? userItems : adminItems}
            id="menu"
            onSelect={(item) => onSelect(item)}
        />
    );
};

export default CustomMenu;
