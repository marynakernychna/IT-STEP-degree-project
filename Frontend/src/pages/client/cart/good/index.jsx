import React, { useState } from "react";
import { Card } from 'antd';
import { MoreOutlined, DeleteOutlined } from '@ant-design/icons';
import { getFullGoodInfo } from '../../../../services/goods';
import ShowFullGoodInfoModal from './../../../../components/modals/good/showInfo/index';
import { confirmMessage } from './../../../../services/alerts';
import { deleteWareFromCartByUser } from "../../../../services/carts";

function Good(props) {

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [fullInfo, setFullInfo] = useState(false);

    const showMoreInfo = async () => {
        setFullInfo(await getFullGoodInfo(props.info.id));
        setIsModalOpen(true);
    };

    const onDelete = async () => {
        var result = await confirmMessage();

        if (result && await deleteWareFromCartByUser(props.info.id)) {
            props.updateCart();
        }
    };

    return (
        <Card id="goodCard">
            <div id="goodInformation">
                <div id="image">
                    <img src={props.info.photoBase64} />
                </div>

                <div id="infoBlock">
                    <p id="titles">{props.info.categoryTitle}</p>
                    <p id="titles">{props.info.title}</p>
                    <p id="cost">{props.info.cost} ₴ (UAN)</p>

                    <div className="more">
                        <MoreOutlined />
                        <p onClick={() => showMoreInfo()}>More</p>

                        <DeleteOutlined
                            className="icon"
                            onClick={() => onDelete()}
                        />
                    </div>
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
