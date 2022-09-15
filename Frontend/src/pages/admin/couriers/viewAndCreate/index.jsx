import React, { useEffect, useState } from 'react';
import { customPageSizeOptions, paginationDefaultFilter } from './../../../../constants/pagination';
import { getCouriersInfo } from './../../../../services/users';
import { Button, Pagination, Result, Spin } from 'antd';
import Courier from './courier/index';
import { PlusOutlined } from '@ant-design/icons';
import RegisterCourierModal from './../../../../components/modals/courier/register/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE
};

const ViewAndCreateCouriersPage = () => {

    const [couriers, setCouriers] = useState();
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(async () => {
        setCouriers(await getCouriersInfo(paginationFilterModel));
        setLoading(false);
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        setLoading(true);

        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setCouriers(await getCouriersInfo(paginationFilterModel));
        setLoading(false);
    };

    const reloadCouriers = async () => {
        setLoading(true);
        setCouriers(await getCouriersInfo(paginationFilterModel));
        setLoading(false);
    };

    return (
        <Spin size="large" spinning={loading}>
            <div id="courierRegisterPage">
                {couriers != null ?
                    <div id='container'>
                        <p id="title">Couriers info</p>

                        <Button
                            type="primary"
                            icon={<PlusOutlined />}
                            onClick={() => setIsModalOpen(true)}
                        >
                            Register couriers
                        </Button>

                        {couriers.items.map((user) =>
                            <Courier
                                info={user}
                            />
                        )}

                        <Pagination
                            onChange={onPaginationChange}
                            total={couriers.totalItems}
                            showSizeChanger
                            showTotal={(total) => `Total ${total} items`}
                            pageSizeOptions={customPageSizeOptions}
                            defaultPageSize={paginationDefaultFilter.DEFAULT_LARGE_PAGE_SIZE}
                        />
                    </div>
                    :
                    <Result
                        status="404"
                        title="There are no registered couriers yet!"
                    />
                }

                {
                    isModalOpen &&
                    <RegisterCourierModal
                        myClose={() => setIsModalOpen(false)}
                        updateCouriers={() => reloadCouriers()}
                    />
                }
            </div>
        </Spin>
    );
};

export default ViewAndCreateCouriersPage;
