import React, { useEffect, useState } from 'react';
import { CheckOutlined, CloseOutlined } from "@ant-design/icons";
import { getUserProfileInfo, editUserInfo } from '../../../services/users';
import { Form, Layout, Input, Button } from 'antd';
import InputRules from "../../../constants/inputRules";
import { confirmMessage, errorMessage } from "../../../services/alerts";
import { generalMessages } from "../../../constants/messages/general";
import { inputValidationErrorMessages } from "../../../constants/messages/inputValidationErrors";

const ViewProfileInfoPage = () => {

    const [user, setUsers] = useState();
    const [temporaryFullName, setTemporaryFullName] = useState({});

    useEffect(async () => {
        let result = await getUserProfileInfo();

        setUsers(result);
        setTemporaryFullName({
            name: result.name,
            surname: result.surname
        });
    }, []);

    const onFinishFailed = () => {
        errorMessage(
            generalMessages.CORRECT_ALL_COMMENTS
        );
    };

    const onFinish = (values) => {
        confirmMessage()
            .then((result) => {
                if (result) {

                    if (user.name !== values.name ||
                        user.surname !== values.surname ||
                        user.email !== values.email ||
                        user.phoneNumber != values.phoneNumber) {

                        editUserInfo(values)
                            .then((result) => {
                                if (result) {
                                    setUsers(values);
                                }
                            });
                    } else {
                        errorMessage(
                            generalMessages.CHANGE_SOMETHING_TO_SAVE
                        );
                    }
                }
            });
    };

    const onNameChange = (e) => {
        setTemporaryFullName({
            ...temporaryFullName,
            name: e.target.value
        });
    };

    const onSurnameChange = (e) => {
        setTemporaryFullName({
            ...temporaryFullName,
            surname: e.target.value

        });
    };

    if (user === undefined) {
        return <>Loading...</>
    }

    return (
        <Layout className="viewProfileInfoPage">

            <Layout id="infoBlock">
                <div className="info">
                    <div className="infoName">
                        <p>Full name</p>
                    </div>

                    <div>
                        <p>{temporaryFullName?.name + ' ' + temporaryFullName?.surname}</p>
                    </div>
                </div>

                <Form
                    onFinish={onFinish}
                    onFinishFailed={onFinishFailed}
                >
                    <div className="info">
                        <div className="infoName">
                            <p>Name</p>
                        </div>

                        <div className="inputBlock">
                            <Form.Item
                                className="formItem"
                                name="name"
                                initialValue={user?.name}
                                rules={[
                                    InputRules.latinLetters(
                                        inputValidationErrorMessages.NOT_VALID_NAME
                                    ),
                                    InputRules.lengthRange(
                                        1,
                                        50,
                                        inputValidationErrorMessages.NAME_MUST_BE_BETWEEN_1_AND_50
                                    ),
                                    InputRules.required(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    )
                                ]}
                            >
                                <Input
                                    onChange={(e) => {
                                        onNameChange(e)
                                    }} />
                            </Form.Item>
                        </div>
                    </div>

                    <div className="info">
                        <div className="infoName">
                            <p>Surname</p>
                        </div>

                        <div className="inputBlock">
                            <Form.Item
                                className="formItem"
                                name="surname"
                                initialValue={user?.surname}
                                rules={[
                                    InputRules.latinLetters(
                                        inputValidationErrorMessages.NOT_VALID_SURNAME
                                    ),
                                    InputRules.lengthRange(
                                        1,
                                        50,
                                        inputValidationErrorMessages.SURNAME_MUST_BE_BETWEEN_1_AND_50
                                    ),
                                    InputRules.required(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    )
                                ]}
                            >
                                <Input
                                    onChange={(e) => {
                                        onSurnameChange(e)
                                    }} />
                            </Form.Item>
                        </div>
                    </div>

                    <div className="info">
                        <div className="infoName">
                            <p>PhoneNumber</p>
                        </div>

                        <div className="inputBlock">

                            <Form.Item
                                className="formItem"
                                name="phoneNumber"
                                initialValue={user?.phoneNumber}
                                rules={[
                                    InputRules.phoneNumber(
                                        10,
                                        inputValidationErrorMessages.NOT_VALID_PHONE_NUMBER
                                    ),
                                    InputRules.required(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    )
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </div>
                    </div>

                    <div className="info">
                        <div className="infoName">
                            <p>Email</p>
                        </div>

                        <div className="inputBlock">
                            <Form.Item
                                className="formItem"
                                name="email"
                                initialValue={user?.email}
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
                        </div>
                    </div>

                    <div className="info">
                        <div className="infoName">
                            <p>Is your email confirmed</p>
                        </div>

                        <div className="infoInput">
                            {user?.emailConfirmed ?
                                <CheckOutlined /> :
                                <CloseOutlined />
                            }
                        </div>
                    </div>

                    <div className="blockButton">
                        <div className="profileButtons">
                            <Button
                                className="submitButton"
                                htmlType="submit"
                                type="primary"
                            >
                                Save
                            </Button>
                        </div>
                    </div>
                </Form>
            </Layout>
        </Layout>
    );
};

export default ViewProfileInfoPage;
