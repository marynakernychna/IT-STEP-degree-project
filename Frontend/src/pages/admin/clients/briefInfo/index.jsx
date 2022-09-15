import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { getBriefUsersInfo } from './../../../../services/users';
import { Pagination, Result, Spin } from 'antd';
import User from './user/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ClientsBriefInfoPage = () => {

    const [users, setUsers] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(async () => {
        setUsers(await getBriefUsersInfo(paginationFilterModel));
        setLoading(false);
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        setLoading(true);

        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setUsers(await getBriefUsersInfo(paginationFilterModel));
        setLoading(false);
    };

    const updateUserInfo = async () => {
        setLoading(true);
        setUsers(await getBriefUsersInfo(paginationFilterModel));
        setLoading(false);
    }

    return (
        <Spin size="large" spinning={loading}>
            <div id="usersBriefInfoPage">
                {users != null ?
                    <div id='container'>
                        <p id="title">Brief info</p>

                        {users.items.map((user) =>
                            <User
                                info={user}
                                updateUserInfo={() => updateUserInfo()}
                            />
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
        </Spin>
    );
};

export default ClientsBriefInfoPage;
