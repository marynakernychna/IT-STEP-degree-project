import React from 'react';
import { connect } from 'react-redux';
import { withRouter, Route, Redirect } from 'react-router-dom';
import { Result } from 'antd';

const PrivateRoute = props => {
    const { isUserAuthorized, userRole, allowedRoles } = props;

    if (isUserAuthorized && allowedRoles.includes(userRole)) {
        return props.children;
    }
    if (isUserAuthorized && !allowedRoles.includes(userRole)) {
        return (
            <Result
                status="403"
                title="403"
                subTitle="You are not allowed to see this page!"
            />
        );
    }
    if (!isUserAuthorized) {
        return <Route component={() => <Redirect to="/login" />} />;
    }

    return <Route {...props} />;
};

const mapStateToProps = (stateRedux) => ({
    isUserAuthorized: stateRedux.authenticationReducer.isUserAuthorized,
    userRole: stateRedux.authenticationReducer.userRole
});

export default withRouter(connect(mapStateToProps)(PrivateRoute));
