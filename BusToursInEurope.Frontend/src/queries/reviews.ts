import axios from 'axios';
import { CreateReviewDto } from '../types/Reviews';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';
import { BASE_URL } from '../utils/constants/urlConstants';

const BASE_REVIEWS_URL = `${BASE_URL}/reviews`;

export const createReview = async (data: CreateReviewDto) => {
  return axios.post(BASE_REVIEWS_URL, data, {
      headers: {
        'Authorization': localStorage.getItem(JwtTokenKey)
      }
    });
};

export const getReviewsByTourId = async (tourId: number) => {
  return axios.get(`${BASE_REVIEWS_URL}/GetAllByTourId`, {
    params: { tourId }
  });
};