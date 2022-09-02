import React from "react";
import { Card } from 'antd';

function Order(props) {

    const data = props.info;

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
        </Card>
    )
}

export default Order;
