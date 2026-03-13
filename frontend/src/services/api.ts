import axios from 'axios';

const API_BASE = 'https://localhost:5001/api';

export const api = axios.create({
  baseURL: API_BASE
});
