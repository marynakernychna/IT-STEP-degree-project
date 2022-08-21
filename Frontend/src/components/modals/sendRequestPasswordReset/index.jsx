import React from 'react';
import { confirmMessage, errorMessage, successMessage } from '../../../services/alerts';
import { Modal, Form, Input, Button } from "antd";
import { generalMessages } from '../../../constants/messages/general';
import InputRules from '../../../constants/inputRules';
import { inputValidationErrorMessages } from '../../../constants/messages/inputValidationErrors';
import { requestPasswordReset } from '../../../services/authentication';

function SendRequestModal(props) {

    const close = () => {
        props.myClose();
    };

    const onFinish = (values) => {
        confirmMessage()
            .then((result) => {
                if (result) {
                        const model = {
                            email: values.email
                        };

                        requestPasswordReset(model)
                            .then(() => {
                                close();
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
            title="Send request password reset to email"
            visible={true}
            onCancel={() => close()}
            cancelButtonProps={{ style: { display: 'none' } }}
            okButtonProps={{ style: { display: 'none' } }}
            footer={null}
        >
            <Form
                labelCol={{ span: 5 }}
                wrapperCol={{ span: 32 }}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                scrollToFirstError
            >
                <Form.Item
                    name="email"
                    label="Email: "
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.specificType(
                            "email",
                            inputValidationErrorMessages.NOT_VALID_EMAIL
                        ),
                    ]}
                >
                    <Input />
                </Form.Item>

                <Form.Item>
                    <Button
                        block
                        htmlType="submit"
                        type="primary"
                        className="submitButton"
                    >
                        Send
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
        </Modal>
    );
};

export default SendRequestModal;
