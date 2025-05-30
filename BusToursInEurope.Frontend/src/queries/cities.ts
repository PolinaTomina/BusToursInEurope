import axios from 'axios';
import { CityDto } from '../types/Cities';
import { BASE_URL } from '../utils/constants/urlConstants';

const BASE_URL_CITY = `${BASE_URL}/cities`;

export const createCity = async (data: CityDto) => {
  return axios.post(BASE_URL_CITY, data);
};

export const updateCity = async (id: number, data: CityDto) => {
  return axios.put(`${BASE_URL_CITY}/id?id=${id}`, data);
};

export const deleteCity = async (id: number) => {
  return axios.delete(`${BASE_URL_CITY}/id?id=${id}`);
};

export const getCities = async (filters: {
  Name?: string;
  Country?: string;
  VisaRequired?: boolean;
  SortBy?: string;
  IsDescending?: boolean;
}) => {
  return axios.get(`${BASE_URL_CITY}/filters`, { params: filters });
};