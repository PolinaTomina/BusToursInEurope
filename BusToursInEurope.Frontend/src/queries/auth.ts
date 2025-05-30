import axios from 'axios';
import { AuthorizationDto, RegistrationDto } from '../types/Authorization';
import { BASE_URL } from '../utils/constants/urlConstants';

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