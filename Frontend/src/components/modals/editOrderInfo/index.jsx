import React from 'react';
import { confirmMessage, errorMessage } from './../../../services/alerts';
import { Modal, Form, Input, Button, InputNumber } from "antd";
import { generalMessages } from './../../../constants/messages/general';
import { changeOrderInfo } from '../../../services/orders';
import InputRules from './../../../constants/inputRules';
import { inputValidationErrorMessages } from './../../../constants/messages/inputValidationErrors';

const { TextArea } = Input;

function EditOrderInfoModal(props) {

    const orderData = props.data;

    var arrayOfStrings = orderData.address.split(' ');

    const close = () => {
        props.myClose();
    };

    const updateOrderInfo = () => {
        props.updateOrders();
    }

    const onFinish = (values) => {
        confirmMessage()
            .then((result) => {
                if (result) {
                    changeOrderInfo({
                        address: values.address + ' ' + values.buildingNumber,
                        city: values.city,
                        country: values.country,
                        orderId: orderData.id
                    })
                        .then((result) => {
                            if (result) {
                                close();
                                updateOrderInfo();
                            }
                        });
                }
            });
    };

    const onFinishFailed = () => {
        errorMessage(
            generalMessages.CORRECT_ALL_COMMENTS,
            ""
        );
    };

    return (
        <Modal
            title="Edit order info"
            visible={true}
            onCancel={() => close()}
            cancelButtonProps={{ style: { display: 'none' } }}
            okButtonProps={{ style: { display: 'none' } }}
            footer={null}
        >
            <Form
                labelCol={{ span: 8 }}
                wrapperCol={{ span: 30 }}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                scrollToFirstError
            >
                <Form.Item
                    name="buildingNumber"
                    label="Building number: "
                    initialValue={arrayOfStrings[1]}
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        )
                    ]}
                >
                    <InputNumber
                        min={1}
                        max={99999}
                        style={{ 'width': '100%' }}
                        placeholder="Building number"
                    />
                </Form.Item>

                <Form.Item
                    name="address"
                    label="Address: "
                    initialValue={arrayOfStrings[0]}
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.latinLetters(
                            inputValidationErrorMessages.NOT_VALID_ADDRESS
                        )
                    ]}
                >
                    <TextArea
                        showCount
                        maxLength={100}
                        autoSize={true}
                        placeholder="Address"
                    />
                </Form.Item>

                <Form.Item
                    name="city"
                    label="City / town: "
                    initialValue={orderData?.city}
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.latinLetters(
                            inputValidationErrorMessages.NOT_VALID_CITY
                        )
                    ]}
                >
                    <TextArea
                        showCount
                        maxLength={100}
                        autoSize={true}
                        placeholder="City / town"
                    />
                </Form.Item>

                <Form.Item
                    name="country"
                    label="Country: "
                    initialValue={orderData?.country}
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.latinLetters(
                            inputValidationErrorMessages.NOT_VALID_COUNTRY
                        )
                    ]}
                >
                    <TextArea
                        showCount
                        maxLength={100}
                        autoSize={true}
                        placeholder="Country"
                    />
                </Form.Item>

                <Form.Item>
                    <Button
                        block
                        htmlType="submit"
                        type="primary"
                        className="submitButton"
                    >
                        Edit the order
                    </Button>
                </Form.Item>

                <Form.Item>
                    <Button
                        block
                        onClick={() => close()}
                    >
                        Close
                    </Button>
                </Form.Item>
            </Form>
        </Modal >
    );
};

export default EditOrderInfoModal;
