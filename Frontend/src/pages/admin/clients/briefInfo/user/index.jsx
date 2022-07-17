import React from "react";
import { Card } from "antd";
import user_icon from "../../../../../assets/icons/user.svg";
import email_icon from "../../../../../assets/icons/email.svg";
import { CheckOutlined, CloseOutlined } from "@ant-design/icons";

function User(props) {
    return (
        <Card id="userCard">
            <div id="userInformation">
                <div id="fullName">
                    <img src={user_icon}/>
                    <p>{props.info.name} {props.info.surname}</p>
                </div>

                <div id="email">
                    <img src={email_icon}/>
                    <p>{props.info.email}</p>
                </div>

                <div id="isConfirmed">
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

export default User;