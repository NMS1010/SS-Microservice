import apiRoutes from '../../config/apiRoutes';
import {
    useDelete,
    useDeleteList,
    useFetch,
    usePatchForm,
    usePostForm,
    usePutForm,
} from '../../utils/reactQuery';

export const useGetListProductImage = (params) => {
    return useFetch({ url: apiRoutes.admin.product_image, params, key: 'getList' });
};

export const useGetProductImage = (id) => {
    return useFetch({ url: `${apiRoutes.admin.product_image}/${id}`, key: 'getById' });
};

export const useCreateProductImage = (updater) => {
    return usePostForm(apiRoutes.admin.product_image, updater);
};

export const useUpdateProductImage = (updater) => {
    return usePutForm(apiRoutes.admin.product_image, updater);
};

export const useSetDefaultProductImage = (updater) => {
    return usePatchForm(apiRoutes.admin.product_image, updater);
};

export const useDeleteProductImage = (updater) => {
    return useDelete(apiRoutes.admin.product_image, updater);
};

export const useDeleteListProductImage = (updater) => {
    return useDeleteList(apiRoutes.admin.product_image, updater);
};
