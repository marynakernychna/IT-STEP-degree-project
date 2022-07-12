import React from 'react';
import { Layout } from 'antd';
import CustomMenu from '../../../../components/menu/index';
import { userRoles } from '../../../../constants/userRoles';

const { Header, Footer, Sider, Content } = Layout;

const ClientsBriefInfo = () => {

    return (
        <Layout id='page'>
            <Header />

            <Layout>
                <Sider>
                    <CustomMenu type={userRoles.ADMIN} />
                </Sider>
                
                <Content />
            </Layout>

            <Footer />
        </Layout>
    );
};

export default ClientsBriefInfo;
