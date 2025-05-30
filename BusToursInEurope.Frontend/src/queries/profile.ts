import axios from 'axios';
import { BASE_URL } from '../utils/constants/urlConstants';
import { JwtTokenKey } from '../utils/constants/localStorageConstants';
import { UpdateProfileDto } from '../types/Profile';

const BASE_URL_PROFILE = `${BASE_URL}/profiles`;

export const getProfileQuery = async () => {
  return axios.get(`${BASE_URL_PROFILE}`, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  })
}

export const updateProfileQuery = async (data: UpdateProfileDto) => {
  return axios.put(`${BASE_URL_PROFILE}`, data, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  })
}