import axios from 'axios';
import { HotelDto } from '../types/Hotels';
import { BASE_URL } from '../utils/constants/urlConstants';

const BASE_URL_HOTELS = `${BASE_URL}/hotels`;

export const createHotel = async (data: HotelDto) => {
  return axios.post(BASE_URL_HOTELS, data);
};

export const updateHotel = async (id: number, data: HotelDto) => {
  return axios.put(`${BASE_URL_HOTELS}/id?id=${id}`, data);
};

export const deleteHotel = async (id: number) => {
  return axios.delete(`${BASE_URL_HOTELS}/id?id=${id}`);
};

export const getHotels = async (filters: {
  Name?: string;
  MinRating?: number;
  MaxRating?: number;
  CityId?: number;
}) => {
  return axios.get(`${BASE_URL_HOTELS}/filters`, { params: filters });
};