import React from 'react';
import { Button } from 'antd';
import { Link } from 'react-router-dom';
import { pageUrls } from '../../constants/pageUrls';

function HomePage() {

    return (
        <div className='main'>
            <div className='contentBlock'>
                <table >
                    <tbody>
                        <tr>
                            <td id='info'>
                                <div>
                                    <h1>
                                        Buy goods online in our online store! 
                                        <br />
                                        And order delivery directly to your home for free!
                                    </h1>
                                </div>
                            </td>

                            <td id='line'>
                                <div></div>
                            </td>

                            <td id='login'>
                                <div>
                                    <h1>
                                        Log in and buy!
                                    </h1>

                                    <Button type="primary">
                                        <Link to={pageUrls.LOGIN}>
                                            Log in
                                        </Link>
                                    </Button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default HomePage;
