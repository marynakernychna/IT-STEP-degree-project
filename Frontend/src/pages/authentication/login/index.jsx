import React from 'react';
import { Link, useHistory } from 'react-router-dom';
import InputRules from '../../../constants/inputRules';
import { generalMessages } from '../../../constants/messages/general';
import { inputValidationErrorMessages } from '../../../constants/messages/inputValidationErrors';
import { Form, Input, Button } from 'antd';
import { errorMessage } from '../../../services/alerts';
import { authenticationMessages } from './../../../constants/messages/authentication';
import { loginUser } from '../../../services/authentication';

function LoginPage() {
    let history = useHistory();

    const onFinish = (values) => {
        loginUser(values, history);
    };

    const onFinishFailed = () => {
        errorMessage(
            authenticationMessages.LOGIN_BLOCKED,
            generalMessages.CORRECT_ALL_COMMENTS
        );
    };

    return (
        <div className="authenticationBody">
            <div className="centerBlock">
                <div className="content">
                    <p className="title">Netlis</p>
                    <p>Sign in and start shopping!</p>

                    <Form
                        labelCol={{ span: 8 }}
                        wrapperCol={{ span: 16 }}
                        initialValues={{ remember: true }}
                        autoComplete="off"
                        onFinish={onFinish}
                        onFinishFailed={onFinishFailed}
                        scrollToFirstError
                    >
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

                        <Form.Item className="submitItem">
                            <Button
                                type="primary"
                                htmlType="submit"
                                className="submitButton"
                            >
                                Login
                            </Button>
                        </Form.Item>
                    </Form>

                    <div className="linksDiv">
                        <Link>Home</Link>
                        <Link to="/registration">Registration</Link>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default LoginPage;
