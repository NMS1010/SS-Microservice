import apiRoutes from '../../config/apiRoutes';
import { useDelete, useDeleteList, useFetch, usePost, usePut } from '../../utils/reactQuery';

export const useGetListAddress = (params) => {
    return useFetch({ url: apiRoutes.common.address, params, key: 'getList' });
};

export const useGetListProvince = (params) => {
    return useFetch({ url: `${apiRoutes.common.address}/p`, params, key: 'getList' });
};

export const useGetListDistrict = (provinceId) => {
    return useFetch({
        url: `${apiRoutes.common.address}/p/${provinceId}/d`,
        key: 'getList',
    });
};

export const useGetListWard = (districtId) => {
    return useFetch({
        url: `${apiRoutes.common.address}/p/d/${districtId}/w`,
        key: 'getList',
    });
};

export const useGetAddress = (id) => {
    return useFetch({ url: `${apiRoutes.common.address}/${id}`, key: 'getById' });
};

export const useGetDefaultAddress = () => {
    return useFetch({ url: `${apiRoutes.common.address}/default`});
};

export const useCreateAddress = (updater) => {
    return usePost(apiRoutes.common.address, updater);
};

export const useUpdateAddress = (updater) => {
    return usePut(apiRoutes.common.address, updater);
};

export const useSetDefaultAddress = (updater) => {
    return usePut(apiRoutes.common.address + '/set-default', updater);
};

export const useDeleteAddress = (updater) => {
    return useDelete(apiRoutes.common.address, updater);
};

export const useDeleteListAddress = (updater) => {
    return useDeleteList(apiRoutes.common.address, updater);
};
