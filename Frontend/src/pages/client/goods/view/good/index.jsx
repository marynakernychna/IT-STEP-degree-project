import React from "react";
import { Card } from 'antd';

function Good(props) {

    return (
        <Card id="goodCard">
            <div id="goodInformation">
                <img src={props.info.photoBase64} />
                <p id="titles">{props.info.categoryTitle} • {props.info.title}</p>
                <p id="cost">{props.info.cost} ₴ (UAN)</p>
            </div>
        </Card>
    )
}

export default Good;
