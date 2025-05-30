import axios from 'axios';
import { HotelDto } from '../types/Hotels';
import { BASE_URL } from '../utils/constants/urlConstants';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';

const BASE_URL_HOTELS = `${BASE_URL}/hotels`;

export const createHotel = async (data: HotelDto) => {
  return axios.post(BASE_URL_HOTELS, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const updateHotel = async (id: number, data: HotelDto) => {
  return axios.put(`${BASE_URL_HOTELS}/id?id=${id}`, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  });
};

export const deleteHotel = async (id: number) => {
  return axios.delete(`${BASE_URL_HOTELS}/id?id=${id}`, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const getHotels = async (filters: {
  Name?: string;
  MinRating?: number;
  MaxRating?: number;
  CityId?: number;
}) => {
  return axios.get(`${BASE_URL_HOTELS}/filters`, { params: filters, 
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
     });
};