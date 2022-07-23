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
import ViewCategoriesPage from './pages/admin/categories/view/index';
import { pageUrls } from './constants/pageUrls';
import ViewProfileInfoPage from './pages/client/viewProfileInfo/index';

const history = createBrowserHistory();

export default function App() {
    return (
        <Router history={history}>
            <Switch>
                <PageLayoutRoute
                    exact
                    path={pageUrls.CLIENTS_BRIEF_INFO}
                    allowedRoles={[userRoles.ADMIN]}
                    component={ClientsBriefInfoPage}
                />

                <PageLayoutRoute
                    exact
                    path={pageUrls.CATEGORIES_VIEW}
                    allowedRoles={[userRoles.ADMIN]}
                    component={ViewCategoriesPage}
                />

                <PageLayoutRoute
                    exact
                    path={pageUrls.VIEW_PROFILE_INFO}
                    allowedRoles={[userRoles.USER]}
                    component={ViewProfileInfoPage}
                />

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
