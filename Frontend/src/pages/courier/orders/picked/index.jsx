import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { Pagination, Result, Spin } from 'antd';
import { getOrdersByCourier } from './../../../../services/orders';
import Order from './order/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ViewPickedOrders = () => {

    const [orders, setOrders] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(async () => {
        updatePickedOrders(paginationFilterModel);
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        updatePickedOrders(paginationFilterModel);
    };

    const updatePickedOrders = async () => {
        setOrders(await getOrdersByCourier(paginationFilterModel));
        setLoading(false);
    };

    return (
        <Spin size="large" spinning={loading}>
            <div id="courierOrdersPage">
                <p id="pageTitle">Picked orders</p>

                {orders != null ?
                    <div id='container'>
                        {orders.items.map((order) =>
                            <Order
                                info={order}
                                updateOrder={() => updatePickedOrders()}
                            />
                        )}

                        <Pagination
                            onChange={onPaginationChange}
                            total={orders.totalItems}
                            showSizeChanger
                            showTotal={(total) => `Total ${total} items`}
                            pageSizeOptions={customPageSizeOptions}
                            defaultPageSize={paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE}
                        />
                    </div>
                    :
                    <Result
                        status="404"
                        title="There are no picked orders yet!"
                    />
                }
            </div>
        </Spin>
    );
};

export default ViewPickedOrders;
