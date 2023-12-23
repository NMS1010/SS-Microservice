import axios from 'axios';
import config from '../config';
import myHistory from '../utils/myHistory';
import { notification } from 'antd';
import { isTokenStoraged } from '../utils/storage';

const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
});

axiosInstance.interceptors.request.use(
    (config) => {
        const token = JSON.parse(localStorage.getItem('token'));
        
        if (token?.accessToken) {
            config.headers.Authorization = `Bearer ${token.accessToken}`;
        }

        return config;
    },
    (error) => Promise.reject(error),
);

axiosInstance.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error?.config;

        if (error?.response?.status === 401 && !originalRequest._retry) {
            if (isTokenStoraged() && !localStorage.getItem('isTokenRefreshing')) {
                originalRequest._retry = true;
                return await refreshToken(originalRequest);
            }else{
                // notification.error({
                //     message: 'Thông báo',
                //     description: 'Bạn vui lòng đăng nhập để tiếp tục',
                // });
            }
        }

        return Promise.reject(error);
    },
);

const refreshToken = async (originalRequest) => {

    try {
        localStorage.setItem('isTokenRefreshing', 'true');
        const token = JSON.parse(localStorage.getItem('token'));
        const response = await axiosInstance.post(config.apiRoutes.common.auth.refresh_token, {
            ...token,
        });
        localStorage.removeItem('isTokenRefreshing');
        
        if (!response || !response?.data?.data) {
            localStorage.removeItem('token');
            myHistory.replace(config.routes.web.login);

            return Promise.reject();
        }

        const { accessToken } = response?.data?.data;

        localStorage.removeItem('token');
        localStorage.setItem('token', JSON.stringify(response?.data?.data));

        originalRequest.headers.Authorization = `Bearer ${accessToken}`;

        return axiosInstance(originalRequest);
    } 
    catch (error) {
        // notification.error({
        //     message: 'Thông báo',
        //     description: 'Bạn vui lòng đăng nhập để tiếp tục',
        // });
        myHistory.replace(config.routes.web.login);
        localStorage.removeItem('isTokenRefreshing');
        localStorage.removeItem('token');
        return Promise.reject(error);
    }
};

export default axiosInstance;
