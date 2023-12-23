import apiRoutes from '../../config/apiRoutes';
import { useFetch } from '../../utils/reactQuery';

export const useGetRole = (id) => {
    return useFetch({ url: `${apiRoutes.admin.role}/${id}`, key: 'getById' });
};

export const useGetListRole = (params) => {
    return useFetch({ url: apiRoutes.admin.role, params, key: 'getList' });
};
