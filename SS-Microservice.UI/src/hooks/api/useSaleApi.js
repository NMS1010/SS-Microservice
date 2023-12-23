import apiRoutes from '../../config/apiRoutes';
import {
    useDelete,
    useDeleteList,
    useFetch,
    usePost,
    usePostForm,
    usePutForm,
} from '../../utils/reactQuery';

export const useGetListSale = (params) => {
    return useFetch({ url: apiRoutes.admin.sale, params, key: 'getList' });
};

export const useGetSale = (id) => {
    return useFetch({ url: `${apiRoutes.admin.sale}/${id}`, key: 'getById' });
};

export const useGetSaleLatest = () => {
    return useFetch({ url: `${apiRoutes.admin.sale}/latest`, key: 'getById' });
};

export const useCreateSale = (updater) => {
    return usePostForm(apiRoutes.admin.sale, updater);
};

export const useUpdateSale = (updater) => {
    return usePutForm(apiRoutes.admin.sale, updater);
};

export const useApplySale = (updater) => {
    return usePostForm(apiRoutes.admin.sale + '/apply', updater);
};

export const useCancelSale = (updater) => {
    return usePostForm(apiRoutes.admin.sale + '/cancel', updater);
};
