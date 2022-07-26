import React from 'react';
import { adminItems, userItems } from './items';
import { userRoles } from './../../constants/userRoles';
import { useHistory } from 'react-router-dom';
import { Menu } from 'antd';
import { store } from './../../store';
import { logoutUser } from '../../services/authentication';

function CustomMenu() {
    let history = useHistory();
    const role = store.getState().authenticationReducer.userRole;
    const logoutKey = "Logout";

    const onSelect = (item) => {
        if (item.key == logoutKey) {
            logoutUser(history);
        }

        history.push(item.key);
    };

    return (
        <Menu
            defaultSelectedKeys={['1']}
            mode="inline"
            theme="dark"
            items={role === userRoles.USER ? userItems : adminItems}
            id="menu"
            onSelect={(item) => onSelect(item)}
        />
    );
};

export default CustomMenu;
