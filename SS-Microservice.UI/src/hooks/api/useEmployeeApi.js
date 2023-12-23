import apiRoutes from '../../config/apiRoutes';
import { useDelete, useDeleteList, useFetch, usePost, usePostForm, usePut, usePutForm } from '../../utils/reactQuery';

export const useGetListEmployee = (params) => {
    return useFetch({ url: apiRoutes.admin.employee, params, key: 'getList' });
};

export const useGetEmployee = (id) => {
    return useFetch({ url: `${apiRoutes.admin.employee}/${id}`, key: 'getById' });
};

export const useCreateEmployee = (updater) => {
    return usePost(apiRoutes.admin.employee, updater);
};

export const useUpdateEmployee = (updater) => {
    return usePut(apiRoutes.admin.employee, updater);
};

export const useDeleteEmployee = (updater) => {
    return useDelete(apiRoutes.admin.employee, updater);
};

export const useDeleteListEmployee = (updater) => {
    return useDeleteList(apiRoutes.admin.employee, updater);
};
