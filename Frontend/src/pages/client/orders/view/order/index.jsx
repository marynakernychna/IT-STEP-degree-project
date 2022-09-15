import React, { useState } from "react";
import { Card, Button } from 'antd';
import { CheckOutlined, CloseOutlined } from "@ant-design/icons";
import { deleteOrder, confirmOrderDelivery, rejectDeliveryConfirmation } from "../../../../../services/orders"
import { confirmMessage } from '../../../../../services/alerts';
import { DeleteOutlined } from '@ant-design/icons';
import { DEFAULT_ACTION_ICON_SIZE } from './../../../../../constants/others';
import { AiOutlineEdit } from 'react-icons/ai';
import EditOrderInfoModal from '../../../../../components/modals/editOrderInfo/index'

function Order(props) {

    const [isModalOpen, setIsModalOpen] = useState(false);
    const data = props.info;

    const onDelete = async () => {
        var result = await confirmMessage();

        if (result && await deleteOrder(data.id)) {
            props.updateOrders();
        }
    };

    const confirm = async () => {
        if (await confirmOrderDelivery(data.id)) {
            props.updateOrders();
        }
    };

    const reject = async () => {
        if (await rejectDeliveryConfirmation(data.id)) {
            props.updateOrders();
        }
    };

    return (
        <Card id="orderCard">
            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                The address:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                {data.address}, {data.city}, {data.country}
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                The client's phone number:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                {data.phoneNumber}
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                Quantity of goods:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                {data.waresCount}
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                Is picked:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                <div >
                    {data?.isPicked ?
                        <CheckOutlined /> :
                        <CloseOutlined />
                    }
                </div>
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                Confirmed by you:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                <div >
                    {data?.isAcceptedByClient ?
                        <CheckOutlined /> :
                        <CloseOutlined />
                    }
                </div>
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                Confirmed by the courier:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                <div >
                    {data?.isAcceptedByCourier ?
                        <CheckOutlined /> :
                        <CloseOutlined />
                    }
                </div>
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                <Button
                    danger
                    type="primary"
                    onClick={() => onDelete()}
                >
                    <DeleteOutlined />
                </Button>
            </Card.Grid>

            {!data?.isAcceptedByClient && !data?.isAcceptedByCourier ?
                <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                    <Button
                        type="primary"
                    >
                        <AiOutlineEdit
                            size={DEFAULT_ACTION_ICON_SIZE}
                            onClick={() => setIsModalOpen(true)}
                        />
                    </Button>
                </Card.Grid> :

                <Card.Grid hoverable={false} style={{ width: '0', boxShadow: 'none', display: 'inline' }}>
                </Card.Grid>
            }

            {data?.isAcceptedByClient ?
                <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                    <Button
                        danger
                        type="primary"
                        onClick={() => reject()}
                    >
                        Reject delivery confirmation
                    </Button>
                </Card.Grid> :

                <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                    <Button
                        className="submitButton"
                        type="primary"
                        onClick={() => confirm()}
                    >
                        Confirm delivery
                    </Button>
                </Card.Grid>
            }

            {
                isModalOpen &&
                <EditOrderInfoModal
                    myClose={() => setIsModalOpen(false)}
                    data={props.info}
                    updateOrders={() => props.updateOrders()}
                />
            }
        </Card>
    )
}

export default Order;
