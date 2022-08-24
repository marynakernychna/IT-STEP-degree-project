import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { getBriefUsersInfo } from './../../../../services/users';
import { Pagination, Result, Collapse } from 'antd';
import Cart from './carts/index';

const { Panel } = Collapse;

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const UserCart = () => {

    const [users, setUsers] = useState();

    useEffect(async () => {
        setUsers(await getBriefUsersInfo(paginationFilterModel));
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setUsers(await getBriefUsersInfo(paginationFilterModel));
    };

    const onChange = (key) => {
        console.log(key);
    };

    return (
        <div id="userCart">
            {users != null ?
                <div id='container'>
                    <p id="pageTitle">Users cart</p>
                    <Collapse accordion onChange={onChange}>
                        {users.items.map((user) =>
                            <Panel header={user.name + " " + user.surname + " " + user.email} key={user.email}>
                                <Cart
                                    info={user}
                                />
                            </Panel>
                        )}
                    </Collapse>

                    <Pagination
                        onChange={onPaginationChange}
                        total={users.totalItems}
                        showSizeChanger
                        showTotal={(total) => `Total ${total} items`}
                        pageSizeOptions={customPageSizeOptions}
                        defaultPageSize={paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE}
                    />
                </div>
                :
                <Result
                    status="404"
                    title="There are no registered users yet!"
                />
            }
        </div>
    );
};

export default UserCart;
