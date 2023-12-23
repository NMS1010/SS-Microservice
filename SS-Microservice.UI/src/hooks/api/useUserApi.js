import apiRoutes from '../../config/apiRoutes';
import {
    useDelete,
    useDeleteList,
    useFetch,
    usePost,
    usePut,
    usePutForm,
    usePutFormWithoutId,
    usePutWithoutId,
} from '../../utils/reactQuery';

export const useGetMe = () => {
    return useFetch({ url: apiRoutes.common.user.me, key: 'getMe' });
};

export const useGetUser = (id) => {
    return useFetch({ url: `${apiRoutes.common.user._}/${id}`, key: 'getById' });
};

export const useGetListUser = (params) => {
    return useFetch({ url: apiRoutes.common.user._, params, key: 'getList' });
};

export const useUpdateUser = (updater) => {
    return usePutFormWithoutId(apiRoutes.common.user._, updater);
};

export const useDeleteListUser = (updater) => {
    return useDeleteList(apiRoutes.common.user._, updater);
};

export const useToggleUser = (updater) => {
    return useDelete(apiRoutes.common.user._, updater);
};

export const useChangePassword = (updater) => {
    return usePutWithoutId(apiRoutes.common.user._ + '/change-password', updater);
};

export const useEditUser = (updater) => {
    return usePutFormWithoutId(apiRoutes.common.user._, updater);
};
