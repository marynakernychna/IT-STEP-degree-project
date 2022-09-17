import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { Pagination, Result, Spin } from 'antd';
import { getClientOrders } from '../../../../services/orders';
import Order from './order/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_SMALL_PAGE_SIZE
};

const OpenOrders = () => {

    const [orders, setOrders] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(async () => {
        updateOrders(paginationFilterModel);
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        updateOrders(paginationFilterModel);
    };

    const updateOrders = async () => {
        setOrders(await getClientOrders(paginationFilterModel));
        setLoading(false);
    };

    return (
        <Spin size="large" spinning={loading}>
            <div id="ordersPage">
                <p id="pageTitle">Open orders</p>

                {orders != null ?
                    <div id='container'>
                        {orders.items.map((order) =>
                            <Order
                                info={order}
                                updateOrders={() => updateOrders()}
                            />
                        )}

                        <Pagination
                            onChange={onPaginationChange}
                            total={orders.totalItems}
                            showSizeChanger
                            showTotal={(total) => `Total ${total} items`}
                            pageSizeOptions={customPageSizeOptions}
                            defaultPageSize={paginationDefaultFilter.DEFAULT_SMALL_PAGE_SIZE}
                        />
                    </div>
                    :
                    <Result
                        status="404"
                        title="There are no open orders yet!"
                    />
                }
            </div>
        </Spin>
    );
};

export default OpenOrders;
