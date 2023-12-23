import apiRoutes from '../../config/apiRoutes';
import { encodeQueryData } from '../../utils/queryParams';
import { useDelete, useDeleteList, useFetch, usePost, usePut } from '../../utils/reactQuery';

export const useGetListVariant = (params) => {
    return useFetch({ url: apiRoutes.admin.variant, params, key: 'getList' });
};

export const useGetVariant = (id) => {
    return useFetch({ url: `${apiRoutes.admin.variant}/${id}`, key: 'getById' });
};

export const useCreateVariant = (updater) => {
    return usePost(apiRoutes.admin.variant, updater);
};

export const useUpdateVariant = (updater) => {
    return usePut(apiRoutes.admin.variant, updater);
};

export const useDeleteVariant = (updater) => {
    return useDelete(apiRoutes.admin.variant, updater);
};

export const useDeleteListVariant = (updater) => {
    return useDeleteList(apiRoutes.admin.variant, updater);
};
