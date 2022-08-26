import React, { useState, useEffect } from 'react';
import { Modal, Descriptions } from "antd";
import moment from 'moment';
import 'moment-timezone';

function ShowFullGoodInfoModal(props) {

    const data = props.data;
    const [dateInLocalTime, setDateInLocalTime] = useState();

    const close = () => {
        props.myClose();
    };

    useEffect(async () => {
        const inputTz = "Europe/Kiev";
        const originTime = data.creationDate;
        const time = moment.tz(originTime, inputTz);
        const localtz = moment.tz.guess();
        const date = time.clone().tz(localtz);
        const formatDate = moment(date).format('MMMM Do YYYY');

        setDateInLocalTime(formatDate);
    }, []);

    return (
        <Modal
            visible={true}
            onCancel={() => close()}
            cancelButtonProps={{ style: { display: 'none' } }}
            okButtonProps={{ style: { display: 'none' } }}
            footer={null}
            width={750}
        >
            <Descriptions
                title="Good's info"
                bordered
            >
                <Descriptions.Item
                    label="Title"
                    span={3}
                >
                    {data.title}
                </Descriptions.Item>

                <Descriptions.Item
                    label="Description"
                    span={3}
                >
                    {data.description}
                </Descriptions.Item>

                <Descriptions.Item
                    label="Category title"
                    span={3}
                >
                    {data.categoryTitle}
                </Descriptions.Item>

                <Descriptions.Item
                    label="Cost"
                    span={3}
                >
                    {data.cost} ₴ (UAN)
                </Descriptions.Item>

                <Descriptions.Item
                    label="Available count"
                    span={3}
                >
                    {data.availableCount}
                </Descriptions.Item>

                <Descriptions.Item
                    label="Creation date"
                    span={3}
                >
                    {dateInLocalTime}
                </Descriptions.Item>

                <Descriptions.Item
                    label="Creator's full name"
                    span={3}
                >
                    {data.creatorFullName}
                </Descriptions.Item>

                {data.characteristics.length !== 0 &&
                    <Descriptions.Item
                        label="Characteristics"
                        bordered
                    >
                        <Descriptions>
                            {
                                data.characteristics.map((characteristic) =>
                                    <>
                                        <Descriptions.Item
                                            label={characteristic.name}
                                            span={3}
                                            labelStyle={{
                                                'width': '110px',
                                                'wordWrap': 'break-word'
                                            }}
                                            style={{
                                                'paddingBottom': '0px'
                                            }}
                                        >
                                            {characteristic.value}
                                        </Descriptions.Item>
                                    </>
                                )
                            }
                        </Descriptions>
                    </Descriptions.Item>
                }
            </Descriptions>
        </Modal>
    );
};

export default ShowFullGoodInfoModal;
