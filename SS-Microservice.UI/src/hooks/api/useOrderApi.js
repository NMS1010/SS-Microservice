import apiRoutes from '../../config/apiRoutes';
import { useDelete, useDeleteList, useFetch, usePost, usePut } from '../../utils/reactQuery';

export const useCreateOrder = (updater) => {
    return usePost(apiRoutes.common.order, updater);
};

export const useGetListOrder = (params) => {
    return useFetch({
        url: apiRoutes.common.order,
        params,
        key: 'getList',
    });
};

export const useGetListUserOrder = (params) => {
    return useFetch({
        url: apiRoutes.common.order + '/me/list',
        params,
        key: 'getUserList',
    });
};

export const useGetTop5OrderLatest = () => {
    return useFetch({
        url: apiRoutes.common.order + '/top5-order-latest',
        key: 'getUserList',
    });
};

export const useGetOrder = (id) => {
    return useFetch({
        url: `${apiRoutes.common.order}/${id}`,
        key: 'getById',
    });
};

export const useGetOrderByCode = (code) => {
    return useFetch({
        url: `${apiRoutes.common.order}/detail/${code}`,
        key: 'getByCode',
    });
};

export const useUpdateOrder = (updater) => {
    return usePut(apiRoutes.common.order, updater);
};

export const useCompletePaypalOrder = (updater) => {
    return usePut(apiRoutes.common.order + '/paypal', updater);
};
