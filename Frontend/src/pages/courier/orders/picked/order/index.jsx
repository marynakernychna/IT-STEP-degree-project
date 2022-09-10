import React from "react";
import { Card, Button } from 'antd';
import { rejectSelectedOrder, confirmOrderDelivery, rejectDeliveryConfirmation } from "../../../../../services/orders"

function Order(props) {

    const data = props.info;

    const onClick = async () => {
        if (await rejectSelectedOrder(data.id)) {
            props.updateOrder();
        }
    };

    const confirm = async () => {
        await confirmOrderDelivery(data.id);
    };

    const reject = async () => {
        await rejectDeliveryConfirmation(data.id);
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
                The client's full name:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                {data.clientFullName}
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                The client's phone number:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                {data.clientPhoneNumber}
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                Quantity of goods:
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '75%', boxShadow: 'none', display: 'inline' }}>
                {data.waresCount}
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                <Button
                    danger
                    className="submitButton"
                    type="primary"
                    onClick={() => onClick()}
                >
                    Reject the order
                </Button>
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                <Button
                    className="submitButton"
                    type="primary"
                    onClick={() => confirm()}
                >
                    Confirm delivery
                </Button>
            </Card.Grid>

            <Card.Grid hoverable={false} style={{ width: '25%', boxShadow: 'none', display: 'inline' }}>
                <Button
                    danger
                    className="submitButton"
                    type="primary"
                    onClick={() => reject()}
                >
                    Reject delivery confirmation
                </Button>
            </Card.Grid>
        </Card>
    )
}

export default Order;
