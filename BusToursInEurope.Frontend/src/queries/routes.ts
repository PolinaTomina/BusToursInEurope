import axios from 'axios';
import { CreateRouteBusDto, UpdateRouteBusDto } from '../types/Routes';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';
import { BASE_URL } from '../utils/constants/urlConstants';

const BASE_ROUTES_URL = `${BASE_URL}/routes`;

export const createRoute = async (data: CreateRouteBusDto) => {
  return axios.post(`${BASE_ROUTES_URL}/Create`, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const updateRoute = async (data: UpdateRouteBusDto) => {
  return axios.put(`${BASE_ROUTES_URL}/Update`, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const deleteRoute = async (id: number) => {
  return axios.delete(`${BASE_ROUTES_URL}/Delete?id=${id}`, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const getAll = async () => {
  return axios.get(`${BASE_ROUTES_URL}/GetAll`, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};