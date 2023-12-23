import apiRoutes from '../../config/apiRoutes';
import {
    useDelete,
    useDeleteList,
    useFetch,
    usePostForm,
    usePutForm,
} from '../../utils/reactQuery';

export const useGetListProductCategory = (params) => {
    return useFetch({ url: apiRoutes.admin.product_category, params, key: 'getList' });
};

export const useGetProductCategory = (id) => {
    return useFetch({ url: `${apiRoutes.admin.product_category}/${id}`, key: 'getById' });
};

export const useGetProductCategoryBySlug = (slug) => {
    return useFetch({ url: `${apiRoutes.admin.product_category}/slug/${slug}`, key: 'getBySlug' });
};

export const useCreateProductCategory = (updater) => {
    return usePostForm(apiRoutes.admin.product_category, updater);
};

export const useUpdateProductCategory = (updater) => {
    return usePutForm(apiRoutes.admin.product_category, updater);
};

export const useDeleteProductCategory = (updater) => {
    return useDelete(apiRoutes.admin.product_category, updater);
};

export const useDeleteListProductCategory = (updater) => {
    return useDeleteList(apiRoutes.admin.product_category, updater);
};
