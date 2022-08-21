import React, { useState } from "react";
import { Card } from 'antd';
import { MoreOutlined } from "@ant-design/icons";
import { getFullGoodInfo } from "../../../../../services/goods";
import ShowFullGoodInfoModal from './../../../../../components/modals/good/showInfo/index';

function Good(props) {

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [fullInfo, setFullInfo] = useState(false);
    
    const showMoreInfo = async () => {
        setFullInfo(await getFullGoodInfo(props.info.id));
        setIsModalOpen(true);
    }

    return (
        <Card id="goodCard">
            <div id="goodInformation">
                <img src={props.info.photoBase64} />
                <p id="titles">{props.info.categoryTitle} • {props.info.title}</p>
                <p id="cost">{props.info.cost} ₴ (UAN)</p>

                <div className="more">
                    <MoreOutlined />
                    <p onClick={() => showMoreInfo()}>More</p>
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
