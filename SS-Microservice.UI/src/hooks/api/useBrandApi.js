import apiRoutes from '../../config/apiRoutes';
import {
    useDelete,
    useDeleteList,
    useFetch,
    usePostForm,
    usePutForm,
} from '../../utils/reactQuery';

export const useGetListBrand = (params) => {
    return useFetch({ url: apiRoutes.admin.brand, params, key: 'getList' });
};

export const useGetBrand = (id) => {
    return useFetch({ url: `${apiRoutes.admin.brand}/${id}`, key: 'getById' });
};

export const useCreateBrand = (updater) => {
    return usePostForm(apiRoutes.admin.brand, updater);
};

export const useUpdateBrand = (updater) => {
    return usePutForm(apiRoutes.admin.brand, updater);
};

export const useDeleteBrand = (updater) => {
    return useDelete(apiRoutes.admin.brand, updater);
};

export const useDeleteListBrand = (updater) => {
    return useDeleteList(apiRoutes.admin.brand, updater);
};
