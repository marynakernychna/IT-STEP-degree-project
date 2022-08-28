import React, { useEffect, useState } from 'react';
import { adminItems, userItems, courierItems } from './items';
import { userRoles } from './../../constants/userRoles';
import { useHistory } from 'react-router-dom';
import { Menu } from 'antd';
import { store } from './../../store';
import { logoutUser } from '../../services/authentication';

function CustomMenu() {
    const [menuType, setMenuType] = useState();

    let history = useHistory();
    const logoutKey = "Logout";

    useEffect(async () => {
        switch(store.getState().authenticationReducer.userRole) {
            case userRoles.USER:
                setMenuType(userItems);
                break;
            case userRoles.COURIER:
                setMenuType(courierItems);
                break;
            case userRoles.ADMIN:
                setMenuType(adminItems);
                break;
        }
    }, []);

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
            items={menuType}
            id="menu"
            onSelect={(item) => onSelect(item)}
        />
    );
};

export default CustomMenu;
