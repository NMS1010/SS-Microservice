import apiRoutes from '../../config/apiRoutes';
import {
    useDelete,
    useDeleteList,
    useFetch,
    usePatch,
    usePostForm,
    usePut,
    usePutForm,
} from '../../utils/reactQuery';

export const useCreateReview = (updater) => {
    return usePostForm(apiRoutes.common.review, updater);
};
export const useUpdateReview = (updater) => {
    return usePutForm(apiRoutes.common.review, updater);
};

export const useGetReviewByOrderItem = (id) => {
    return useFetch({
        url: `${apiRoutes.common.review}/order-item/${id}`,
        key: 'getByOrderItemId',
        config: {
            retry: false,
        },
    });
};

export const useGetCountReview = (productId) => {
    return useFetch({ url: `${apiRoutes.common.review}/count/${productId}`, key: 'getCountList' });
};

export const useGetListReview = (params) => {
    return useFetch({ url: apiRoutes.common.review, params, key: 'getList' });
};

export const useGetTop5ReviewLatest = () => {
    return useFetch({ url: `${apiRoutes.common.review}/top5-review-latest`, key: 'getList' });
};

export const useGetReview = (id) => {
    return useFetch({ url: `${apiRoutes.common.review}/${id}`, key: 'getById' });
};

export const useDeleteReview = (updater) => {
    return useDelete(apiRoutes.common.review, updater);
};

export const useToggleReview = (updater) => {
    return usePatch(apiRoutes.common.review + '/toggle', updater);
};

export const useDeleteListReview = (updater) => {
    return useDeleteList(apiRoutes.common.review, updater);
};

export const useReplyReview = (updater) => {
    return usePut(apiRoutes.common.review + '/reply', updater);
};
