import apiRoutes from '../../config/apiRoutes';
import { encodeQueryData } from '../../utils/queryParams';
import { useDelete, useDeleteList, useFetch, usePost, usePut } from '../../utils/reactQuery';

export const useAddVariantToCart = (updater) => {
    return usePost(apiRoutes.web.cart, updater);
};

export const useRemoveVariantFromCart = (updater) => {
    return useDelete(apiRoutes.web.cart, updater);
};

export const useRemoveListVariantFromCart = (updater) => {
    return useDeleteList(apiRoutes.web.cart, updater);
};

export const useUpdateCartQuantity = (updater) => {
    return usePut(apiRoutes.web.cart, updater);
};

export const useGetCart = (params) => {
    return useFetch({ url: apiRoutes.web.cart, params, key: 'getList' });
};

export const useGetListCartItemById = (array) => {
    return useFetch({
        url: `${apiRoutes.web.cart}/list-ids?${encodeQueryData(array, 'ids')}`,
        key: 'getListById',
    });
};
