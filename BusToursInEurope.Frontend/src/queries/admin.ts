import axios from "axios"
import { JwtTokenKey } from "../utils/constants/localStorageConstants"
import { BASE_URL } from "../utils/constants/urlConstants"

const BASE_URL_ADMIN = `${BASE_URL}/Admin`

export const blockUser = async (userId: string) => {
  return axios.post(`${BASE_URL_ADMIN}/block/${userId}`, null, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  })
}

export const unblockUser = async (userId: string) => {
  return axios.post(`${BASE_URL_ADMIN}/unblock/${userId}`, null, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  })
}

export const getUsers = async () => {
  return axios.get(`${BASE_URL_ADMIN}/reservations-users`, {
    headers: {
      'Authorization': localStorage.getItem(JwtTokenKey)
    }
  })
}