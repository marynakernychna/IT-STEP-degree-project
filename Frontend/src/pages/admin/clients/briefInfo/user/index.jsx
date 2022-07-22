import React, { useState } from "react";
import { Card, Tooltip } from "antd";
import user_icon from "../../../../../assets/icons/user.svg";
import email_icon from "../../../../../assets/icons/email.svg";
import { CheckOutlined, CloseOutlined } from "@ant-design/icons";
import EditClientInfoModal from './../../../../../components/modals/editClientInfo/index';
import { AiOutlineEdit } from "react-icons/ai";
import { DEFAULT_ACTION_ICON_SIZE } from "../../../../../constants/others";
import { DEFAULT_MOUSE_ENTER_DELAY } from './../../../../../constants/others';

function User(props) {
    
    const [isModalOpen, setIsModalOpen] = useState(false);

    return (
        <Card className="userCard">
            <div className="userInformation">
                <div className="fullName">
                    <img src={user_icon} />
                    <p>{props.info.name} {props.info.surname}</p>
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

                <Tooltip
                    color="#224957"
                    title="Edit"
                    placement="bottomRight"
                    mouseEnterDelay={DEFAULT_MOUSE_ENTER_DELAY}
                >
                    <AiOutlineEdit
                        className="editIcon"
                        size={DEFAULT_ACTION_ICON_SIZE}
                        onClick={() => setIsModalOpen(true)}
                    />
                </Tooltip>
            </div>

            {
                isModalOpen &&
                <EditClientInfoModal
                    myClose={() => setIsModalOpen(false)}
                    data={props.info}
                    updateUserInfo={() => props.updateUserInfo()}
                />
            }
        </Card>
    )
}

export default User;
