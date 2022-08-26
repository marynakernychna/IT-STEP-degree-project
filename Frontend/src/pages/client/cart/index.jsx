import React, { useEffect, useState } from 'react';
import { paginationDefaultFilter, customPageSizeOptions } from '../../../constants/pagination';
import { Pagination, Result, Button } from 'antd';
import { getCartByUser } from '../../../services/carts';
import Good from './../cart/good/index';
import { PlusOutlined } from '@ant-design/icons';
import MakeAnOderModal from './../../../components/modals/makeAnOrder/index';

let paginationFilterModel = {
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_SMALL_PAGE_SIZE
};

const Cart = () => {

    const [goods, setGoods] = useState();
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(async () => {
        setGoods(await getCartByUser(paginationFilterModel));
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setGoods(await getCartByUser(paginationFilterModel));
    };

    const reloadCart = async () => {
        setGoods(await getCartByUser(paginationFilterModel));
    };

    return (
        <div id="cartPage">
            <p id="pageTitle">Basket</p>

            {goods != null ?
                <div id='container'>
                    <Button
                        type="primary"
                        icon={<PlusOutlined />}
                        onClick={() => setIsModalOpen(true)}
                    >
                        Order all
                    </Button>

                    {goods.items.map((good) =>
                        <Good
                            info={good}
                            updateCart={() => reloadCart()}
                        />
                    )}

                    <Pagination
                        onChange={onPaginationChange}
                        total={goods.totalItems}
                        showSizeChanger
                        showTotal={(total) => `Total ${total} items`}
                        pageSizeOptions={customPageSizeOptions}
                        defaultPageSize={paginationDefaultFilter.DEFAULT_SMALL_PAGE_SIZE}
                    />

                    {
                        isModalOpen &&
                        <MakeAnOderModal
                            myClose={() => setIsModalOpen(false)}
                            updateCartInfo={() => reloadCart()}
                        />
                    }
                </div>
                :
                <Result
                    status="404"
                    title="There are no goods yet!"
                />
            }
        </div>
    );
};

export default Cart;
