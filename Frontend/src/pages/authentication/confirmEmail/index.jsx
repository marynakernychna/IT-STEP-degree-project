import React, { useEffect, useState } from 'react';
import { Spin } from 'antd';
import { confirmEmailAsync } from "../../../services/authentication";

function ConfirmEmailPage() {

    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fetchData() {
            let data = window.location.pathname.split('/');
            let tokenParts = data.slice(2, -1);
            let token = tokenParts.join('/');
            let emailParts = data.slice(-1);
            let email = emailParts.join('/');

            console.log(token, email);
            await confirmEmailAsync(token, email);
        }

        fetchData();
        
        setLoading(false);
    }, []);

    return (

        <Spin size="large" spinning={loading}>
            <div className="authEmail">
                <div className="center">
                    <h1 className="title">Netlis</h1>

                    <h2>Email confirmation</h2>
                </div>
            </div>
        </Spin>
    );
}

export default ConfirmEmailPage;
