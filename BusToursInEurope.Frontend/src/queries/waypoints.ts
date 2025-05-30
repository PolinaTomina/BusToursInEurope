import axios from 'axios';
import { CreateWPDto } from '../types/WayPoints';

const BASE_URL = 'http://your-api-url/wayPoints';

export const createWayPoint = async (data: CreateWPDto) => {
  return axios.post(BASE_URL, data);
};

export const updateWayPoint = async (id: number, data: CreateWPDto) => {
  return axios.put(`${BASE_URL}/id?id=${id}`, data);
};

export const deleteWayPoint = async (id: number) => {
  return axios.delete(`${BASE_URL}/id?id=${id}`);
};