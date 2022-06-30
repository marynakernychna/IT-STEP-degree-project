import React from "react";
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Redirect
} from "react-router-dom";
import { createBrowserHistory } from "history";
import PrivateRoute from "./privateRoute";
import "antd/dist/antd.css";

const history = createBrowserHistory();

export default function App() {
    return (
        <Router history={history}>
            <Switch>
            </Switch>
        </Router>
    );
}
