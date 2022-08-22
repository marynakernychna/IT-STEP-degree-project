import React from "react";
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Redirect
} from "react-router-dom";
import { createBrowserHistory } from "history";
import "antd/dist/antd.css";
import RegistrationPage from './pages/authentication/registration/index';
import LoginPage from './pages/authentication/login/index';
import ClientsBriefInfoPage from './pages/admin/clients/briefInfo/index';
import { userRoles } from './constants/userRoles';
import PageLayoutRoute from './layouts/index';
import ViewAndManageCategoriesPage from './pages/admin/categories/viewAndManage/index';
import { pageUrls } from './constants/pageUrls';
import ViewProfileInfoPage from './pages/client/viewProfileInfo/index';
import CreateGoodPage from './pages/client/goods/create/index';
import ViewGoods from './pages/client/goods/view/index';
import ResetPasswordPage from "./pages/authentication/resetPassword/index";
import Cart from './pages/client/cart/index';

const history = createBrowserHistory();

export default function App() {
    return (
        <Router history={history}>
            <Switch>
                <PageLayoutRoute
                    exact
                    path={pageUrls.VIEW_GOODS}
                    allowedRoles={[userRoles.USER]}
                    component={ViewGoods}
                />

                <PageLayoutRoute
                    exact
                    path={pageUrls.CREATE_GOOD}
                    allowedRoles={[userRoles.USER]}
                    component={CreateGoodPage}
                />

                <PageLayoutRoute
                    exact
                    path={pageUrls.CART}
                    allowedRoles={[userRoles.USER]}
                    component={Cart}
                />

                <PageLayoutRoute
                    exact
                    path={pageUrls.VIEW_PROFILE_INFO}
                    allowedRoles={[userRoles.USER, userRoles.ADMIN]}
                    component={ViewProfileInfoPage}
                />

                <PageLayoutRoute
                    exact
                    path={pageUrls.CLIENTS_BRIEF_INFO}
                    allowedRoles={[userRoles.ADMIN]}
                    component={ClientsBriefInfoPage}
                />

                <PageLayoutRoute
                    exact
                    path={pageUrls.CATEGORIES_VIEW_AND_MANAGE}
                    allowedRoles={[userRoles.ADMIN]}
                    component={ViewAndManageCategoriesPage}
                />

                <Route
                    path={pageUrls.RESET_PASSWORD}
                >
                    <ResetPasswordPage />
                </Route>

                <Route
                    exact
                    path="/registration"
                >
                    <RegistrationPage />
                </Route>

                <Route
                    exact
                    path={pageUrls.LOGIN}
                >
                    <LoginPage />
                </Route>

                <Redirect to="/login" />    {/* there will be a 404 page later */}
            </Switch>
        </Router>
    );
}
