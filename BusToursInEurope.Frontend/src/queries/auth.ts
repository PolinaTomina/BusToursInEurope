import axios from 'axios';
import { AuthorizationDto, RegistrationDto } from '../types/Authorization';
import { BASE_URL } from '../utils/constants/urlConstants';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';

const BASE_URL_AUTH = `${BASE_URL}/auth`;

export const register = async (data: RegistrationDto) => {
  return axios.post(`${BASE_URL_AUTH}/reg`, data);
};

export const login = async (data: AuthorizationDto) => {
  return axios.post(`${BASE_URL_AUTH}/auth`, data);
};

export const isAdmin = async (bearer: string) => {
  return axios.get(`${BASE_URL_AUTH}/admin`, {
    headers: {
      'Authorization': bearer
    }
  })
}

export const isAuthenticated = async () => {
  return axios.get(`${BASE_URL_AUTH}/is-authenticated`, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  })
}

export const ChangePassword = async (email:string, currentPassword: string, newPassword: string) => {
  console.log("user email = " + email)
  return axios.post(`${BASE_URL_AUTH}/change-password`, {
    email: email,
    currentPassword: currentPassword,
    newPassword: newPassword
  }, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  })
}