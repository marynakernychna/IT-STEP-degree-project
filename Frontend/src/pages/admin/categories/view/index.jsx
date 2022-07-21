import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { Pagination, Result, Statistic } from 'antd';
import { getCategories } from './../../../../services/categories';
import { InboxOutlined } from '@ant-design/icons';
import { Card } from 'antd';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ViewCategoriesPage = () => {

    const [categories, setCategories] = useState();

    useEffect(async () => {
        setCategories(await getCategories(paginationFilterModel));
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setCategories(await getCategories(paginationFilterModel));
    };

    return (
        <div id="viewCategoriesPage">
            {categories != null ?
                <div id="container">
                    <p id="title">Categories</p>

                    {categories.items.map((category) =>
                        <Card className='categoryCard'>
                            <p>{category.title}</p>

                            <Statistic
                                title="Total goods"
                                value={category.goodsTotalCount}
                                prefix={<InboxOutlined />}
                            />

                            <Statistic
                                title="Available total count"
                                value={category.availableTotalCount}
                                prefix={<InboxOutlined />}
                            />
                        </Card>
                    )}

                    <Pagination
                        id="pagination"
                        onChange={onPaginationChange}
                        total={categories.totalItems}
                        showSizeChanger
                        showTotal={(total) => `Total ${total} items`}
                        pageSizeOptions={customPageSizeOptions}
                        defaultPageSize={paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE}
                    />
                </div>
                :
                <Result
                    status="404"
                    title="No category has been created yet!"
                />
            }
        </div>
    );
};

export default ViewCategoriesPage;
