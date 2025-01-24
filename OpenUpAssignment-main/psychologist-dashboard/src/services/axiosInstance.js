import axios from 'axios';

// Create an Axios instance with the base URL
const axiosInstance = axios.create({
    baseURL: 'https://localhost:5001/', 
    timeout: 5000,
});

axiosInstance.interceptors.request.use(
    (config) => {
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

export default axiosInstance;
