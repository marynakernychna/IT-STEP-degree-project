import React, { useEffect, useState } from 'react';
import { Pagination, Result, Spin } from 'antd';
import { customPageSizeOptions, paginationDefaultFilter } from '../../../../constants/pagination';
import { getCreatedByUserGoods } from '../../../../services/goods';
import Good from './good/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ViewMyGoods = () => {

    const [goods, setGoods] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(async () => {
        setGoods(await getCreatedByUserGoods(paginationFilterModel));
        setLoading(false);
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        setLoading(true);
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setGoods(await getCreatedByUserGoods(paginationFilterModel));
        setLoading(false);
    };

    return (
        <Spin size="large" spinning={loading}>
            <div id="viewGoodsPage">
                <p id="pageTitle">My goods</p>

                {goods != null ?
                    <div id='container'>
                        {goods.items.map((good) =>
                            <Good info={good} />
                        )}

                        <Pagination
                            onChange={onPaginationChange}
                            total={goods.totalItems}
                            showSizeChanger
                            showTotal={(total) => `Total ${total} items`}
                            pageSizeOptions={customPageSizeOptions}
                            defaultPageSize={paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE}
                        />
                    </div>
                    :
                    <Result
                        status="404"
                        title="There are no goods yet!"
                    />
                }
            </div>
        </Spin>
    );
};

export default ViewMyGoods;
