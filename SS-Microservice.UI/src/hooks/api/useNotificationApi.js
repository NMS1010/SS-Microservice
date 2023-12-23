import apiRoutes from '../../config/apiRoutes';
import { useFetch, usePatch, usePut, usePutWithoutId } from '../../utils/reactQuery';

export const useGetListNotification = (params) => {
    return useFetch({ url: apiRoutes.web.notification, params, key: 'getList' });
};

export const useUpdateNotification = (updater) => {
    return usePatch(apiRoutes.web.notification, updater);
};

export const useUpdateAllNotification = (updater) => {
    return usePutWithoutId(apiRoutes.web.notification + '/all', updater);
};
