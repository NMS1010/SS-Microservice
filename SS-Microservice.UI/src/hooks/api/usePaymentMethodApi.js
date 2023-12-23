import apiRoutes from '../../config/apiRoutes';
import { useDelete, useDeleteList, useFetch, usePost, usePostForm, usePut, usePutForm } from '../../utils/reactQuery';

export const useGetListPaymentMethod = (params) => {
    return useFetch({ url: apiRoutes.admin.paymentMethod, params, key: 'getList' });
};

export const useGetPaymentMethod = (id) => {
    return useFetch({ url: `${apiRoutes.admin.paymentMethod}/${id}`, key: 'getById' });
};

export const useCreatePaymentMethod = (updater) => {
    return usePostForm(apiRoutes.admin.paymentMethod, updater);
};

export const useUpdatePaymentMethod = (updater) => {
    return usePutForm(apiRoutes.admin.paymentMethod, updater);
};

export const useDeletePaymentMethod = (updater) => {
    return useDelete(apiRoutes.admin.paymentMethod, updater);
};

export const useDeleteListPaymentMethod = (updater) => {
    return useDeleteList(apiRoutes.admin.paymentMethod, updater);
};
