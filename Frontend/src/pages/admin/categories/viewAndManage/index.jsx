import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { Pagination, Result } from 'antd';
import { getCategories } from './../../../../services/categories';
import Category from './category';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ViewAndManageCategoriesPage = () => {

    const [categories, setCategories] = useState();

    useEffect(async () => {
        setCategories(await getCategories(paginationFilterModel));
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setCategories(await getCategories(paginationFilterModel));
    };

    const updateCategoryInfo = async () => {
        setCategories(await getCategories(paginationFilterModel));
    };

    return (
        <div id="viewCategoriesPage">
            {categories != null ?
                <div id="container">
                    <p id="title">Categories</p>

                    {categories.items.map((category) =>
                        <Category
                            info={category}
                            updateCategoryInfo={() => updateCategoryInfo()}
                        />
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

export default ViewAndManageCategoriesPage;
