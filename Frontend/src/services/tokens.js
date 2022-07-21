import { tokensName } from './../constants/others';

export default class tokenService {
  static getLocalRefreshToken() {
    return localStorage.getItem(tokensName.REFRESH_TOKEN);
  }

  static getLocalAccessToken() {
    return localStorage.getItem(tokensName.ACCESS_TOKEN);
  }

  static setLocalRefreshToken(refreshToken) {
    localStorage.setItem(tokensName.REFRESH_TOKEN, refreshToken);
  }

  static setLocalAccessToken(accessToken) {
    localStorage.setItem(tokensName.ACCESS_TOKEN, accessToken);
  }

  static deleteTokens() {
    localStorage.removeItem(tokensName.ACCESS_TOKEN);
    localStorage.removeItem(tokensName.REFRESH_TOKEN);
  }
}
