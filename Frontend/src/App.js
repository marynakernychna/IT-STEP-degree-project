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
import ClientsBriefInfo from './pages/admin/clients/briefInfo/index';
import { userRoles } from './constants/userRoles';
import PageLayoutRoute from './layouts/index';

const history = createBrowserHistory();

export default function App() {
    return (
        <Router history={history}>
            <Switch>
                <PageLayoutRoute
                    exact
                    path="/clients/brief-info"
                    allowedRoles={[userRoles.ADMIN]}
                    component={ClientsBriefInfo}
                />

                <Route
                    exact
                    path="/registration"
                >
                    <RegistrationPage />
                </Route>

                <Route
                    exact
                    path="/login"
                >
                    <LoginPage />
                </Route>

                <Redirect to="/login" />    {/* there will be a 404 page later */}
            </Switch>
        </Router>
    );
}
