import React, { useEffect, useState } from 'react';
import { CheckOutlined, CloseOutlined } from "@ant-design/icons";
import { getUserProfileInfo } from '../../../services/users';
import { Form, Layout } from 'antd';

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
                >
                    <div className="info">
                        <div className="infoName">
                            <p>Name</p>
                        </div>

                        <div>
                            <p>{user.name}</p>
                        </div>
                    </div>

                    <div className="info">
                        <div className="infoName">
                            <p>Surname</p>
                        </div>

                        <div>
                            <p>{user.surname}</p>
                        </div>
                    </div>

                    <div className="info">
                        <div className="infoName">
                            <p>PhoneNumber</p>
                        </div>

                        <div>
                            <p>{user.phoneNumber !== null ? user.phoneNumber : "Phone number not found!"}</p>
                        </div>
                    </div>

                    <div className="info">
                        <div className="infoName">
                            <p>Email</p>
                        </div>

                        <div>
                            <p>{user.email}</p>
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

                </Form>
            </Layout>
        </Layout>
    );
};

export default ViewProfileInfoPage;
