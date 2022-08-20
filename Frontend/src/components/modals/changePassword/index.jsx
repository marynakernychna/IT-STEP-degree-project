import React from 'react';
import { confirmMessage, errorMessage, successMessage } from '../../../services/alerts';
import { Modal, Form, Input, Button } from "antd";
import { generalMessages } from '../../../constants/messages/general';
import InputRules from '../../../constants/inputRules';
import { inputValidationErrorMessages } from '../../../constants/messages/inputValidationErrors';
import { changePassword } from '../../../services/authentication';

function ChangePasswordModal(props) {

    const close = () => {
        props.myClose();
    };

    const onFinish = (values) => {
        confirmMessage()
            .then((result) => {
                if (result) {
                    if (values.currentPassword !== values.newPassword) {

                        const model = {
                            currentPassword: values.currentPassword,
                            newPassword: values.newPassword
                        };

                        changePassword(model)
                            .then(() => {
                                close();
                            });
                    } else {
                        errorMessage(
                            generalMessages.CHANGE_SOMETHING_TO_SAVE,
                            ""
                        );
                    }
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
            title="Change Password"
            visible={true}
            onCancel={() => close()}
            cancelButtonProps={{ style: { display: 'none' } }}
            okButtonProps={{ style: { display: 'none' } }}
            footer={null}
            width={750}
        >
            <Form
                labelCol={{ span: 5 }}
                wrapperCol={{ span: 32 }}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                scrollToFirstError
            >
                <Form.Item
                    name="currentPassword"
                    label="Current password: "
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.password(
                            inputValidationErrorMessages.NOT_VALID_PASSWORD
                        ),
                        InputRules.lengthRange(
                            8,
                            50,
                            inputValidationErrorMessages.PASSWORD_MUST_BE_BETWEEN_8_AND_50
                        )
                    ]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    name="newPassword"
                    label="New password: "
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.password(
                            inputValidationErrorMessages.NOT_VALID_PASSWORD
                        ),
                        InputRules.lengthRange(
                            8,
                            50,
                            inputValidationErrorMessages.PASSWORD_MUST_BE_BETWEEN_8_AND_50
                        )
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
                        Save
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

export default ChangePasswordModal;
