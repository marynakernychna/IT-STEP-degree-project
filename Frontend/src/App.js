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
import PrivateRoute from './privateRoute';
import ClientsBriefInfo from './pages/admin/clients/briefInfo/index';
import { userRoles } from './constants/userRoles';

const history = createBrowserHistory();

export default function App() {
    return (
        <Router history={history}>
            <Switch>
                <PrivateRoute
                    exact
                    path="/clients/brief-info"
                    allowedRoles={[userRoles.ADMIN]}
                >
                    <ClientsBriefInfo />
                </PrivateRoute>

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
