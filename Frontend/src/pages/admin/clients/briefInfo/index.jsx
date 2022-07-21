import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { getBriefUsersInfo } from './../../../../services/users';
import { Pagination, Result } from 'antd';
import User from './user/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ClientsBriefInfoPage = () => {

    const [users, setUsers] = useState();

    useEffect(async () => {
        setUsers(await getBriefUsersInfo(paginationFilterModel));
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setUsers(await getBriefUsersInfo(paginationFilterModel));
    };

    return (
        <div id="usersBriefInfoPage">
            {users != null ?
                <div id='container'>
                    <p id="title">Brief info</p>

                    {users.items.map((user) =>
                        <User info={user} />
                    )}

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

export default ClientsBriefInfoPage;
