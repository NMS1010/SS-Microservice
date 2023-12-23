import apiRoutes from '../../config/apiRoutes';
import { useDelete, useDeleteList, useFetch, usePost, usePut } from '../../utils/reactQuery';

export const useGetListUnit = (params) => {
    return useFetch({ url: apiRoutes.admin.unit, params, key: 'getList' });
};

export const useGetUnit = (id) => {
    return useFetch({ url: `${apiRoutes.admin.unit}/${id}`, key: 'getById' });
};

export const useCreateUnit = (updater) => {
    return usePost(apiRoutes.admin.unit, updater);
};

export const useUpdateUnit = (updater) => {
    return usePut(apiRoutes.admin.unit, updater);
};

export const useDeleteUnit = (updater) => {
    return useDelete(apiRoutes.admin.unit, updater);
};

export const useDeleteListUnit = (updater) => {
    return useDeleteList(apiRoutes.admin.unit, updater);
};
