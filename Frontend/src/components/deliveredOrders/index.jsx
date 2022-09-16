import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from '../../constants/pagination';
import { Pagination, Result, Spin } from 'antd';
import { getDeliveredOrders } from '../../services/orders';
import Order from './order/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_SMALL_PAGE_SIZE
};

const ViewDeliveredOrders = () => {

    const [orders, setOrders] = useState();
    const [loading, setLoading] = useState(false);

    useEffect(async () => {
        updateOrders(paginationFilterModel);
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        updateOrders(paginationFilterModel);
    };

    const updateOrders = async () => {
        setLoading(true);
        setOrders(await getDeliveredOrders(paginationFilterModel));
        setLoading(false);
    };

    return (
        <Spin size="large" spinning={loading}>
            <div id="ordersPage">
                <p id="pageTitle">Delivered orders</p>

                {orders != null ?
                    <div id='container'>
                        {orders.items.map((order) =>
                            <Order
                                info={order}
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
                        title="There are no delivered orders yet!"
                    />
                }
            </div>
        </Spin>
    );
};

export default ViewDeliveredOrders;
