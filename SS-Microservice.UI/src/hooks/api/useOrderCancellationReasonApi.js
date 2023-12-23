import apiRoutes from '../../config/apiRoutes';
import { useDelete, useDeleteList, useFetch, usePost, usePut } from '../../utils/reactQuery';

export const useGetListOrderCancellationReason = (params) => {
    return useFetch({ url: apiRoutes.admin.orderCancellationReason, params, key: 'getList' });
};

export const useGetOrderCancellationReason = (id) => {
    return useFetch({ url: `${apiRoutes.admin.orderCancellationReason}/${id}`, key: 'getById' });
};

export const useCreateOrderCancellationReason = (updater) => {
    return usePost(apiRoutes.admin.orderCancellationReason, updater);
};

export const useUpdateOrderCancellationReason = (updater) => {
    return usePut(apiRoutes.admin.orderCancellationReason, updater);
};

export const useDeleteOrderCancellationReason = (updater) => {
    return useDelete(apiRoutes.admin.orderCancellationReason, updater);
};

export const useDeleteListOrderCancellationReason = (updater) => {
    return useDeleteList(apiRoutes.admin.orderCancellationReason, updater);
};
