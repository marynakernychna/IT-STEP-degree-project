import React, { useEffect, useState } from 'react';
import { paginationDefaultFilter, customPageSizeOptions } from '../../../../constants/pagination';
import { Pagination, Result } from 'antd';
import Good from './good/index';
import { getCartByUserAdmin } from '../../../../services/carts';

let paginationFilterModel = {
    userEmail: "",
    pageNumber: paginationDefaultFilter.DEFAULT_PAGE_NUMBER,
    pageSize: paginationDefaultFilter.DEFAULT_SMALL_PAGE_SIZE
};

function Cart() {

    const [goods, setGoods] = useState();

    paginationFilterModel.userEmail = localStorage.getItem("email");

    useEffect(async () => {
        setGoods(await getCartByUserAdmin(paginationFilterModel));
    }, []);

    const onPaginationChange = async (page, pageSize) => {
        paginationFilterModel.pageNumber = page;
        paginationFilterModel.pageSize = pageSize;

        setGoods(await getCartByUserAdmin(paginationFilterModel));
    };

    const reloadCart = async () => {
        setGoods(await getCartByUserAdmin(paginationFilterModel));
    };

    return (
        <div id="cartPage">
            <p id="pageTitle">Basket</p>

            {goods != null ?
                <div id='container'>
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
