import React from 'react';
import { Modal, Form, Input, Button } from 'antd';
import { inputValidationErrorMessages } from './../../../../constants/messages/inputValidationErrors';
import InputRules from './../../../../constants/inputRules';
import { registerCourier } from './../../../../services/authentication';
import { generalMessages } from './../../../../constants/messages/general';
import { errorMessage, successMessage } from './../../../../services/alerts';
import { authenticationMessages } from './../../../../constants/messages/authentication';

function RegisterCourierModal(props) {

    const close = () => {
        props.myClose();
    };

    const updateCouriers = () => {
        props.updateCouriers();
    };

    const onFinish = async (values) => {
        var isSuccessful = await registerCourier(values);

        if (isSuccessful) {
            successMessage(
                generalMessages.CREATION_SUCCESSFUL
            );
            updateCouriers();
            close();
        }
    };

    const onFinishFailed = () => {
        errorMessage(
            authenticationMessages.REGISTRATION_BLOCKED,
            generalMessages.CORRECT_ALL_COMMENTS
        );
    };

    return (
        <Modal
            title="Register courier"
            visible={true}
            onCancel={() => close()}
            cancelButtonProps={{ style: { display: 'none' } }}
            okButtonProps={{ style: { display: 'none' } }}
            footer={null}
        >
            <Form
                labelCol={{ span: 7 }}
                wrapperCol={{ span: 30 }}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                scrollToFirstError
            >
                <Form.Item
                    name="name"
                    label="Name: "
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
                    <Input placeholder="Name" />
                </Form.Item>

                <Form.Item
                    name="surname"
                    label="Surname: "
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
                    <Input placeholder="Surname" />
                </Form.Item>

                <Form.Item
                    name="phoneNumber"
                    label="Phone number: "
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.phoneNumber(
                            inputValidationErrorMessages.NOT_VALID_PHONE_NUMBER
                        ),
                        InputRules.lengthRange(
                            10,
                            20,
                            inputValidationErrorMessages.PHONE_NUMBER_MUST_BE_BETWEEN_10_AND_20
                        )
                    ]}
                >
                    <Input placeholder="Phone number" />
                </Form.Item>

                <Form.Item
                    name="email"
                    label="Email: "
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
                    <Input placeholder="Email" />
                </Form.Item>

                <Form.Item
                    name="password"
                    label="Password: "
                    rules={[
                        InputRules.password(
                            inputValidationErrorMessages.NOT_VALID_PASSWORD
                        ),
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        )
                    ]}
                >
                    <Input.Password
                        className="passwordInput"
                        placeholder="Password"
                    />
                </Form.Item>

                <Form.Item
                    name="confirmedPassword"
                    label="Confirm password: "
                    rules={[
                        {
                            required: true,
                            message: inputValidationErrorMessages.CONFIRM_PASSWORD
                        },
                        ({ getFieldValue }) => ({
                            validator(_, value) {
                                if (
                                    !value ||
                                    getFieldValue("password") === value
                                ) {
                                    return Promise.resolve();
                                }
                                return Promise.reject(
                                    new Error(
                                        inputValidationErrorMessages.PASSWORD_DOESNT_MATCH
                                    )
                                );
                            }
                        })
                    ]}
                >
                    <Input.Password
                        className="passwordInput"
                        placeholder="Confirm password"
                    />
                </Form.Item>

                <Form.Item>
                    <Button
                        block
                        type="primary"
                        htmlType="submit"
                        className="submitButton"
                    >
                        Register
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
}

export default RegisterCourierModal;
