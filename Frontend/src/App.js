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

const history = createBrowserHistory();

export default function App() {
    return (
        <Router history={history}>
            <Switch>
                <Route
                    exact
                    path="/registration"
                >
                    <RegistrationPage />
                </Route>

                <Redirect to="/registration" /> // there will be a 404 page later
            </Switch>
        </Router>
    );
}
