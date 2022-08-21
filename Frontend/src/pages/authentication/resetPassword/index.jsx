import React from 'react';
import InputRules from '../../../constants/inputRules';
import { generalMessages } from '../../../constants/messages/general';
import { inputValidationErrorMessages } from '../../../constants/messages/inputValidationErrors';
import { Form, Input, Button } from 'antd';
import { errorMessage } from '../../../services/alerts';
import { authenticationMessages } from './../../../constants/messages/authentication';
import { resetPassword } from '../../../services/authentication';
import { useHistory } from 'react-router-dom';

function ResetPassword() {
    let history = useHistory();
    let data = window.location.pathname.split('/');
    let tokenParts = data.slice(2, -1);
    let token = tokenParts.join('/')
    let emailParts = data.slice(-1)
    let email = emailParts.join('/')

    const onFinish = (values) => {
        values.email = email
        values.token = token;

        resetPassword(values, history);
    };

    const onFinishFailed = () => {
        errorMessage(
            authenticationMessages.RESET_PASSWORD_FAILED,
            generalMessages.CORRECT_ALL_COMMENTS
        );
    };

    return (
        <div className="authenticationBody">
            <div className="centerBlock">
                <div className="content">
                    <p className="title">Netlis</p>
                    <p>Reset your password!</p>

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
                            name="newPassword"
                            className="passwordForm"
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
                            <Input.Password
                                className="passwordInput"
                                placeholder="New password"
                            />
                        </Form.Item>

                        <Form.Item className="submitItem">
                            <Button
                                type="primary"
                                htmlType="submit"
                                className="submitButton"
                            >
                                Reset Password
                            </Button>
                        </Form.Item>
                    </Form>
                </div>
            </div>
        </div>
    );
}

export default ResetPassword;
