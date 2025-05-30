import axios from 'axios';
import { CreateReviewDto } from '../types/Reviews';

const BASE_URL = 'http://your-api-url/reviews';

export const createReview = async (data: CreateReviewDto) => {
  return axios.post(`${BASE_URL}/Create`, data);
};

export const getReviewsByTourId = async (tourId: number) => {
  return axios.get(`${BASE_URL}/GetAllByTourId`, {
    params: { tourId }
  });
};