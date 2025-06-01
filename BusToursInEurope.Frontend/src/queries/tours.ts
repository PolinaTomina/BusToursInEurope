import axios, { AxiosResponse } from 'axios';
import { CreateTourDto, ShortTourDto, UpdateTourDto } from '../types/Tours';
import { TOURS_URL } from '../utils/constants/urlConstants';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';

export const createTour = async (data: CreateTourDto) => {
  const formData = new FormData();
  
  formData.append('Id', data.id.toString());
  formData.append('Name', data.name || '');
  formData.append('Price', data.price.toString());
  formData.append('StartDate', data.startDate);
  formData.append('EndDate', data.endDate);
  formData.append('NumOfSeats', data.numOfSeats.toString());
  formData.append('Description', data.description || '');
  formData.append('BusId', data.busId.toString());
  formData.append('RouteBusId', data.routeBusId.toString());
  data.images.forEach((file) => {
    formData.append(`Images`, file, file.name);
  });

  // Получаем токен из localStorage
  const token = localStorage.getItem(JwtTokenKey);
  if (!token) {
    throw new Error('No JWT token found');
  }

  return axios.post(TOURS_URL, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
      'Authorization': token // Добавляем токен в заголовок
    },
  });
};

export const updateTour = async (id: number, data: UpdateTourDto) => {
  const formData = new FormData();
  
  // Добавляем простые поля, если они есть
  if (data.name !== null) formData.append('Name', data.name || '');
  if (data.price !== null) formData.append('Price', data.price?.toString() || '');
  if (data.startDate !== null) formData.append('StartDate', data.startDate || '');
  if (data.endDate !== null) formData.append('EndDate', data.endDate || '');
  if (data.numOfSeats !== null) formData.append('NumOfSeats', data.numOfSeats?.toString() || '');
  if (data.description !== null) formData.append('Description', data.description || '');
  
  // Добавляем изображения, если они есть
  if (data.images) {
    data.images.forEach((file) => {
      formData.append('Images', file, file.name);
    });
  }

  if (data.existingImages) {
    data.existingImages.forEach((link) => {
      formData.append('ExistingImages', link)
    })
  }

  // Получаем токен из localStorage
  const token = localStorage.getItem(JwtTokenKey);
  if (!token) {
    throw new Error('No JWT token found');
  }
  console.log("update request")
  console.log(token)
  return axios.put(`${TOURS_URL}/id?id=${id}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
      'Authorization': token // Добавляем токен в заголовок
    },
  });
};

export const deleteTour = async (id: number) => {
  return axios.delete(`${TOURS_URL}/id?id=${id}`);
};

export const getTour = async (id: number) => {
  return axios.get(`${TOURS_URL}/id`, {
    params: { id }
  });
};

export const getTopTours = async (): Promise<AxiosResponse<ShortTourDto[]>> => {
  return axios.get(`${TOURS_URL}/top`);
};

export const getToursByFilters = async (filters: {
  Country?: string;
  MinPrice?: number;
  MaxPrice?: number;
  StartDate?: string;
  EndDate?: string;
}) => {
  return axios.get(`${TOURS_URL}/filters`, { params: filters });
};