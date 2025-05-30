import axios from 'axios';
import { BusDto } from '../types/Buses';
import { BASE_URL } from '../utils/constants/urlConstants';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';

export const createBus = async (data: BusDto) => {
  return axios.post(`${BASE_URL}/buses`, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const updateBus = async (id: number, data: BusDto) => {
  return axios.put(`${BASE_URL}/buses/id?id=${id}`, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const deleteBus = async (id: number) => {
  return axios.delete(`${BASE_URL}/buses/id?id=${id}`, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const getBuses = async (filters: {
  Name?: string;
  MinSeats?: number;
  MaxSeats?: number;
  SortBy?: string;
  IsDescending?: boolean;
}) => {
  return axios.get(`${BASE_URL}/buses/filters`, { params: filters, 
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
     },);
};