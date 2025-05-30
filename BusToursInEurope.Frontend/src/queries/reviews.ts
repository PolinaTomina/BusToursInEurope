import axios from 'axios';
import { CreateReviewDto } from '../types/Reviews';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';

const BASE_URL = 'http://your-api-url/reviews';

export const createReview = async (data: CreateReviewDto) => {
  return axios.post(`${BASE_URL}/Create`, data, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const getReviewsByTourId = async (tourId: number) => {
  return axios.get(`${BASE_URL}/GetAllByTourId`, {
    params: { tourId }
  });
};