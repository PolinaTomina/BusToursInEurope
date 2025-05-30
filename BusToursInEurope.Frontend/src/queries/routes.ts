import axios from 'axios';
import { CreateRouteBusDto, UpdateRouteBusDto } from '../types/Routes';

const BASE_URL = 'http://your-api-url/routes';

export const createRoute = async (data: CreateRouteBusDto) => {
  return axios.post(`${BASE_URL}/Create`, data);
};

export const updateRoute = async (data: UpdateRouteBusDto) => {
  return axios.post(`${BASE_URL}/Update`, data);
};

export const deleteRoute = async (id: number) => {
  return axios.post(`${BASE_URL}/Delete`, id);
};