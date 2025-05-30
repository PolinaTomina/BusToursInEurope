import axios from 'axios';
import { CreateReservationDto } from '../types/Reservations';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';

const BASE_URL = 'http://your-api-url/reservations';

export const createReservation = async (data: CreateReservationDto) => {
  return axios.post(BASE_URL, data, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const updateReservation = async (id: number, data: CreateReservationDto) => {
  return axios.put(`${BASE_URL}/${id}`, data);
};

export const deleteReservation = async (id: number) => {
  return axios.delete(`${BASE_URL}/${id}`);
};

export const getReservation = async (id: number) => {
  return axios.get(`${BASE_URL}/${id}`);
};

export const getAllReservations = async () => {
  return axios.get(BASE_URL);
};