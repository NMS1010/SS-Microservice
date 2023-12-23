import apiRoutes from '../../config/apiRoutes';
import { useFetch, usePost } from '../../utils/reactQuery';

export const useFollowProduct = (updater) => {
    return usePost(apiRoutes.web.follow_product + '/like', updater);
};

export const useUnFollowProduct = (updater) => {
    return usePost(apiRoutes.web.follow_product + '/unlike', updater);
};

export const useGetFollowProduct = (params) => {
    return useFetch({ url: apiRoutes.web.follow_product, params, key: 'getList' });
};
