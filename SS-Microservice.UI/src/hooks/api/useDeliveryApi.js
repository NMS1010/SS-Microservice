import apiRoutes from '../../config/apiRoutes';
import { useDelete, useDeleteList, useFetch, usePost, usePostForm, usePut, usePutForm } from '../../utils/reactQuery';

export const useGetListDelivery = (params) => {
    return useFetch({ url: apiRoutes.admin.delivery, params, key: 'getList' });
};

export const useGetDelivery = (id) => {
    return useFetch({ url: `${apiRoutes.admin.delivery}/${id}`, key: 'getById' });
};

export const useCreateDelivery = (updater) => {
    return usePostForm(apiRoutes.admin.delivery, updater);
};

export const useUpdateDelivery = (updater) => {
    return usePutForm(apiRoutes.admin.delivery, updater);
};

export const useDeleteDelivery = (updater) => {
    return useDelete(apiRoutes.admin.delivery, updater);
};

export const useDeleteListDelivery = (updater) => {
    return useDeleteList(apiRoutes.admin.delivery, updater);
};
