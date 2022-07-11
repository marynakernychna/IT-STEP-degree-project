import { combineReducers } from 'redux';
import authenticationReducer from './reducers/authentication';

export default combineReducers({
    authenticationReducer: authenticationReducer
});
