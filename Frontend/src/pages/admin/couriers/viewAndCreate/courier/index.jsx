import React from "react";
import { Card } from "antd";
import user_icon from "../../../../../assets/icons/user.svg";
import email_icon from "../../../../../assets/icons/email.svg";
import phoneNumber_icon from "../../../../../assets/icons/phoneNumber.svg";
import { CheckOutlined, CloseOutlined } from "@ant-design/icons";

function Courier(props) {

    return (
        <Card className="userCard">
            <div className="userInformation">
                <div className="fullName">
                    <img src={user_icon} />
                    <p>{props.info.name} {props.info.surname}</p>
                </div>

                <div className="phoneNumber">
                    <img src={phoneNumber_icon} />
                    <p>{props.info.phoneNumber}</p>
                </div>

                <div className="email">
                    <img src={email_icon} />
                    <p>{props.info.email}</p>
                </div>

                <div className="isConfirmed">
                    <div>
                        <p>Is email confirmed</p>
                    </div>

                    <div>
                        {props.info?.emailConfirmed ?
                            <CheckOutlined /> :
                            <CloseOutlined />
                        }
                    </div>
                </div>
            </div>
        </Card>
    )
}

export default Courier;
