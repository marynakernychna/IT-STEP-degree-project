import PrivateRoute from "../privateRoute";
import { Route } from 'react-router-dom';
import { Layout } from 'antd';
import CustomMenu from "../components/menu";

const { Header, Footer, Sider, Content } = Layout;

const PageLayoutRoute = ({ component: Component, ...rest }) => {
    return (
        <PrivateRoute {...rest}>
            <Route
                render={matchProps => (
                    <Layout id='page'>
                        <Header />

                        <Layout>
                            <Sider>
                                <CustomMenu />
                            </Sider>

                            <Content>
                                <Component {...matchProps} />
                            </Content>
                        </Layout>

                        <Footer />
                    </Layout>
                )}
            />
        </PrivateRoute>
    );
};

export default PageLayoutRoute;
