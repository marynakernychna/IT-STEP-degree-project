import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { Button, Pagination, Result } from 'antd';
import { getCategories } from './../../../../services/categories';
import Category from './category';
import { PlusOutlined } from '@ant-design/icons';
import CreateCategoryModal from './../../../../components/modals/category/createCategory/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ViewAndManageCategoriesPage = () => {

    const [categories, setCategories] = useState();
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(async () => {
        setCategories(await getCategories(paginationFilterModel));
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setCategories(await getCategories(paginationFilterModel));
    };

    const reloadCategories = async () => {
        setCategories(await getCategories(paginationFilterModel));
    };

    return (
        <div id="viewCategoriesPage">

            <div id="container">
                <p id="title">Categories</p>

                <Button
                    type="primary"
                    icon={<PlusOutlined />}
                    onClick={() => setIsModalOpen(true)}
                >
                    Create
                </Button>

                {categories != null ?
                    <>
                        {categories.items.map((category) =>
                            <Category
                                info={category}
                                updateCategoryInfo={() => reloadCategories()}
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
                    </>
                    :
                    <Result
                        status="404"
                        title="No category has been created yet!"
                    />
                }
            </div>

            {
                isModalOpen &&
                <CreateCategoryModal
                    myClose={() => setIsModalOpen(false)}
                    updateCategories={() => reloadCategories()}
                />
            }
        </div>
    );
};

export default ViewAndManageCategoriesPage;
