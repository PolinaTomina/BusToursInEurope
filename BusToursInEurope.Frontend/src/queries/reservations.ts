import axios from 'axios';
import { CreateReservationDto, UpdatePaymentStatusDto } from '../types/Reservations';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';
import { BASE_URL } from '../utils/constants/urlConstants';

const BASE_RESERVATION_URL = `${BASE_URL}/reservations`;

export const createReservation = async (data: CreateReservationDto) => {
  return axios.post(BASE_RESERVATION_URL, data, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const updateReservation = async (id: number, data: CreateReservationDto) => {
  return axios.put(`${BASE_RESERVATION_URL}/${id}`, data, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const deleteReservation = async (id: number) => {
  return axios.delete(`${BASE_RESERVATION_URL}/${id}`, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const getReservation = async (id: number) => {
  return axios.get(`${BASE_RESERVATION_URL}/${id}`, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const getAllReservations = async () => {
  return axios.get(`${BASE_RESERVATION_URL}/All`, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const updatePayment = async (data: UpdatePaymentStatusDto) => {
  return axios.post(`${BASE_RESERVATION_URL}/update-payment`, data, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    }); 
}

export const getUsersForReservations = async () => {
  return axios.get(`${BASE_URL}/admin/reservations-users`, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    })
}