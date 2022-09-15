import React, { useEffect, useState } from 'react';
import { Pagination, Result, Select, Spin } from 'antd';
import { customPageSizeOptions, paginationDefaultFilter } from '../../../../constants/pagination';
import { getAllGoods } from '../../../../services/goods';
import { getCategories } from '../../../../services/categories';
import { getGoodsByCategory } from './../../../../services/goods';
import Good from './good/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ViewGoods = () => {

    const [goods, setGoods] = useState();
    const [categories, setCategories] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(async () => {
        let goodCategories = await getCategories();

        goodCategories.map(category => {
            category.value = category.title;
            category.label = category.title;
        });

        setCategories(goodCategories);
        setGoods(await getAllGoods(paginationFilterModel));
        setLoading(false);
    }, []);

    const searchByCategoryTitle = async (title) => {
        setLoading(true);
        paginationFilterModel.pageNumber = paginationDefaultFilter.DEFAULT_PAGE_NUMBER;
        paginationFilterModel.pageSize = paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE;

        if (title === undefined) {
            setGoods(await getAllGoods(paginationFilterModel));
            setLoading(false);
            return;
        }

        const model = {
            pageNumber: paginationFilterModel.pageNumber,
            pageSize: paginationFilterModel.pageSize,
            categoryTitle: title
        };

        setGoods(await getGoodsByCategory(model));
        setLoading(false);
    };

    const onPaginationChange = async (page, pageSize) => {
        setLoading(true);

        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setGoods(await getAllGoods(paginationFilterModel));
        setLoading(false);
    };

    return (
        <Spin size="large" spinning={loading}>
            <div id="viewGoodsPage">
                <p id="pageTitle">Goods</p>

                <Select
                    className='categorySelect'
                    showSearch
                    allowClear
                    placeholder="Category name"
                    optionFilterProp="children"
                    filterOption={(input, option) =>
                        option.value.toLowerCase().indexOf(input.toLowerCase()) >= 0
                    }
                    options={categories}
                    onChange={(value) => searchByCategoryTitle(value)}
                >
                </Select>

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

export default ViewGoods;
