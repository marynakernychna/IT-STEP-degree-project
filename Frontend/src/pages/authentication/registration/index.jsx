import React from 'react';
import { useHistory, Link } from 'react-router-dom';
import { Form, Input, Button } from 'antd';
import { inputValidationErrorMessages } from './../../../constants/messages/inputValidationErrors';
import InputRules from './../../../constants/inputRules';
import { registerUser } from './../../../services/authentication';
import { generalMessages } from './../../../constants/messages/general';
import { errorMessage } from './../../../services/alerts';
import { authenticationMessages } from './../../../constants/messages/authentication';
import { pageUrls } from './../../../constants/pageUrls';

function RegistrationPage() {
    let history = useHistory();

    const onFinish = (values) => {
        registerUser(values, history);
    };

    const onFinishFailed = () => {
        errorMessage(
            authenticationMessages.REGISTRATION_BLOCKED,
            generalMessages.CORRECT_ALL_COMMENTS
        );
    };

    return (
        <div className="authenticationBody">
            <div className="centerBlock">
                <div className="content">
                    <p className="title">Netlis</p>
                    <p>Sign up and start shopping!</p>

                    <Form
                        initialValues={{ remember: true }}
                        autoComplete="off"
                        onFinish={onFinish}
                        onFinishFailed={onFinishFailed}
                        scrollToFirstError
                    >
                        <Form.Item
                            className="textForm"
                            name="name"
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
                            className="textForm"
                            name="surname"
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
                            name="email"
                            className="textForm"
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
                            className="passwordForm"
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
                            className="passwordForm"
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

                        <Form.Item className="submitItem">
                            <Button
                                type="primary"
                                htmlType="submit"
                                className="submitButton"
                            >
                                Register
                            </Button>
                        </Form.Item>
                    </Form>

                    <div className="linksDiv">
                        <Link>Home</Link>
                        <Link to={pageUrls.LOGIN}>Login</Link>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default RegistrationPage;
