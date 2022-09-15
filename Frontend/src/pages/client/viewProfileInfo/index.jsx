import React, { useEffect, useState } from 'react';
import { CheckOutlined, CloseOutlined } from "@ant-design/icons";
import { getUserProfileInfo, editUserInfo } from '../../../services/users';
import { changePassword } from '../../../services/authentication';
import { Form, Layout, Input, Button, Tooltip, Spin } from 'antd';
import InputRules from "../../../constants/inputRules";
import { confirmMessage, errorMessage } from "../../../services/alerts";
import { generalMessages } from "../../../constants/messages/general";
import { inputValidationErrorMessages } from "../../../constants/messages/inputValidationErrors";
import ChangePasswordModal from './../../../components/modals/changePassword/index';
import { AiOutlineEdit } from "react-icons/ai";
import { DEFAULT_ACTION_ICON_SIZE } from "../../../constants/others";
import { DEFAULT_MOUSE_ENTER_DELAY } from '../../../constants/others';

const ViewProfileInfoPage = () => {

    const [user, setUsers] = useState();
    const [temporaryFullName, setTemporaryFullName] = useState({});
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(async () => {
        let result = await getUserProfileInfo();

        setUsers(result);
        setTemporaryFullName({
            name: result.name,
            surname: result.surname
        });
        setLoading(false);
    }, []);

    const onFinishFailed = () => {
        errorMessage(
            generalMessages.CORRECT_ALL_COMMENTS,
            ""
        );
    };

    const onFinish = (values) => {
        setLoading(true);
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
                                    setLoading(false);
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
        <Spin size="large" spinning={loading}>
            <Layout className="viewProfileInfoPage">
                <p id="title">Profile</p>

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
                                <p>Phone Number</p>
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
                                        ),
                                        InputRules.lengthRange(
                                            10,
                                            20,
                                            inputValidationErrorMessages.PHONE_NUMBER_MUST_BE_BETWEEN_10_AND_20
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

                        <div className="info">
                            <div className="infoName">
                                <p>Change password</p>
                            </div>

                            <Tooltip
                                color="#224957"
                                title="Change password"
                                placement="bottomRight"
                                mouseEnterDelay={DEFAULT_MOUSE_ENTER_DELAY}
                            >
                                <AiOutlineEdit
                                    className="editIcon"
                                    size={DEFAULT_ACTION_ICON_SIZE}
                                    onClick={() => setIsModalOpen(true)}
                                />
                            </Tooltip>
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

                {
                    isModalOpen &&
                    <ChangePasswordModal
                        myClose={() => setIsModalOpen(false)}
                        changePassword={() => changePassword()}
                    />
                }
            </Layout>
        </Spin>
    );
};

export default ViewProfileInfoPage;
