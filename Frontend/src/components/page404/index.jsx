import React from 'react';
import { Result } from 'antd';
import { Link } from 'react-router-dom';
import { pageUrls } from './../../constants/pageUrls';

const ErrorPage = () => (
  <Result
    status="500"
    title="404"
    subTitle="Page not found!"
    extra={<Link to={pageUrls.HOME_PAGE}>Back Home</Link>}
  />
);

export default ErrorPage;
