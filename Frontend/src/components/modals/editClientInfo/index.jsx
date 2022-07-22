import React from 'react';
import { confirmMessage, errorMessage } from './../../../services/alerts';
import { Modal, Form, Input, Button } from "antd";
import { generalMessages } from './../../../constants/messages/general';
import { editClientInfo } from '../../../services/users';
import InputRules from './../../../constants/inputRules';
import { inputValidationErrorMessages } from './../../../constants/messages/inputValidationErrors';

function EditClientInfoModal(props) {

    const userData = props.data;

    const close = () => {
        props.myClose();
    };

    const updateUserInfo = () => {
        props.updateUserInfo();
    }

    const onFinish = (values) => {
        confirmMessage()
            .then((result) => {
                if (result) {
                    if (userData.name !== values.name ||
                        userData.surname !== values.surname ||
                        userData.email !== values.email) {

                        editClientInfo(values, userData.email)
                            .then((result) => {
                                if (result) {
                                    close();
                                    updateUserInfo();
                                }
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
            title="Edit user info"
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
                    name="name"
                    label="Name: "
                    initialValue={userData?.name}
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.latinLetters(
                            inputValidationErrorMessages.NOT_VALID_NAME
                        ),
                        InputRules.lengthRange(
                            2,
                            50,
                            inputValidationErrorMessages.NAME_MUST_BE_BETWEEN_1_AND_50
                        )
                    ]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    name="surname"
                    label="Surname: "
                    initialValue={userData?.surname}
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.latinLetters(
                            inputValidationErrorMessages.NOT_VALID_SURNAME
                        ),
                        InputRules.lengthRange(
                            2,
                            50,
                            inputValidationErrorMessages.SURNAME_MUST_BE_BETWEEN_1_AND_50
                        )
                    ]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    name="email"
                    label="Email: "
                    initialValue={userData?.email}
                    rules={[
                        InputRules.specificType(
                            "email",
                            inputValidationErrorMessages.NOT_VALID_EMAIL
                        ),
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
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
        </Modal >
    );
};

export default EditClientInfoModal;
