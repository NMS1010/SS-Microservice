import apiRoutes from '../../config/apiRoutes';
import { useFetch, usePost } from '../../utils/reactQuery';

export const useGetListDocketByProductId = (productId) => {
    return useFetch({ url: `${apiRoutes.admin.inventory}/${productId}`, key: 'getById' });
};

export const useImportProduct = (updater) => {
    return usePost(`${apiRoutes.admin.inventory}`, updater);
};
