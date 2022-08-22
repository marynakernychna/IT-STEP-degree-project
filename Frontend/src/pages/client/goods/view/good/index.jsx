import React, { useState } from "react";
import { Card } from 'antd';
import { MoreOutlined, PlusOutlined } from "@ant-design/icons";
import { getFullGoodInfo } from "../../../../../services/goods";
import ShowFullGoodInfoModal from './../../../../../components/modals/good/showInfo/index';
import { confirmMessage } from './../../../../../services/alerts';
import { addWareToCartByUser } from './../../../../../services/carts';

function Good(props) {

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [fullInfo, setFullInfo] = useState(false);

    const showMoreInfo = async () => {
        setFullInfo(await getFullGoodInfo(props.info.id));
        setIsModalOpen(true);
    };

    const addToTheCart = async () => {
        var result = await confirmMessage();

        if (result) {
            await addWareToCartByUser(props.info.id);
        }
    };

    return (
        <Card id="goodCard">
            <div id="goodInformation">
                <img src={props.info.photoBase64} />
                <p id="titles">{props.info.categoryTitle} • {props.info.title}</p>
                <p id="cost">{props.info.cost} ₴ (UAN)</p>

                <div className="more">
                    <div id='view'>
                        <MoreOutlined />
                        <p onClick={() => showMoreInfo()}>More</p>
                    </div>

                    <PlusOutlined />
                    <p onClick={() => addToTheCart()}>Add to the cart</p>
                </div>
            </div>

            {
                isModalOpen &&
                <ShowFullGoodInfoModal
                    myClose={() => setIsModalOpen(false)}
                    data={fullInfo}
                />
            }
        </Card>
    )
}

export default Good;
