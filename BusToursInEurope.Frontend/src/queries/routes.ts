import axios from 'axios';
import { CreateRouteBusDto, UpdateRouteBusDto } from '../types/Routes';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';

const BASE_URL = 'http://your-api-url/routes';

export const createRoute = async (data: CreateRouteBusDto) => {
  return axios.post(`${BASE_URL}/Create`, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const updateRoute = async (data: UpdateRouteBusDto) => {
  return axios.post(`${BASE_URL}/Update`, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const deleteRoute = async (id: number) => {
  return axios.post(`${BASE_URL}/Delete`, id, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};