import axios from "axios";
import { SERVER_URL } from './../../constants/api/urls';
import tokenService from './../../services/tokens';
import { AUTHORIZATION_TYPE } from './../../constants/api/others';

const instance = axios.create({
  baseURL: SERVER_URL
});

instance.interceptors.request.use(
  (configuration) => {
    var accessToken = tokenService.getLocalAccessToken();

    if (accessToken) {
      configuration.headers.Authorization = `${AUTHORIZATION_TYPE} ${accessToken}`;
    }

    return configuration;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default instance;
