import { api } from './api';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { encodeQueryData } from './queryParams';
import { notification } from 'antd';

const fetcher = async ({ queryKey, pageParam }) => {
    const [url, key, params] = queryKey;
    return await api.get(url, { ...params, pageParam });
};

const useGenericMutation = (func, url, updater) => {
    const queryClient = useQueryClient();

    return useMutation(func, {
        onSuccess: (data) => {
            updater?.success && updater.success(data);
            if (updater.obj) {
                updater.obj.params &&
                    queryClient.invalidateQueries([url, 'getList', updater.obj.params]);
                updater.obj.id &&
                    queryClient.invalidateQueries([
                        `${url}/${updater.obj.id}`,
                        'getById',
                        undefined,
                    ]);
                updater.obj.ids &&
                    updater.obj.ids.forEach((id) => {
                        queryClient.invalidateQueries([`${url}/${id}`, 'getById', undefined]);
                    });
            }
        },
        onError: (err, _, context) => {
            updater?.error && updater.error(err);
        },
        onMutate: (data) => {
            updater?.mutate && updater.mutate(data);
        },
        onSettled: (data, error, variables, context) => {
            updater?.settled && updater.settled({ data, error, variables, context });
        },
    });
};

export const useFetch = ({ url, key, params, config }) => {
    const context = useQuery([url, key, params], ({ queryKey }) => fetcher({ queryKey }), {
        enabled: !!url,
        retry: config?.retry
            ? config?.retry
            : (failureCount, error) => {
                  return failureCount < 2;
              },
        ...config,
    });
    return context;
};

export const usePrefetch = (url, key, params) => {
    const queryClient = useQueryClient();

    return () => {
        if (!url) {
            return;
        }
        queryClient.prefetchQuery([url, key, params], ({ queryKey }) => fetcher({ queryKey }));
    };
};

export const useDelete = (url, updater) => {
    return useGenericMutation((id) => api.delete(`${url}/${id}`), url, updater);
};

export const useDeleteList = (url, updater) => {
    return useGenericMutation(
        (ids) => api.delete(`${url}?${encodeQueryData(ids, 'ids')}`),
        url,
        updater,
    );
};

export const usePost = (url, updater) => {
    return useGenericMutation((data) => api.post(url, data), url, updater);
};

export const usePostForm = (url, updater) => {
    return useGenericMutation((data) => api.postForm(url, data), url, updater);
};

export const usePut = (url, updater) => {
    return useGenericMutation((data) => api.put(`${url}/${data.id}`, data.body), url, updater);
};

export const usePutWithoutId = (url, updater) => {
    return useGenericMutation((data) => api.put(url, data), url, updater);
};

export const usePutFormWithoutId = (url, updater) => {
    return useGenericMutation((data) => api.putForm(url, data), url, updater);
};

export const usePutForm = (url, updater) => {
    return useGenericMutation((data) => api.putForm(`${url}/${data.id}`, data.body), url, updater);
};

export const usePatch = (url, updater) => {
    return useGenericMutation((data) => api.patch(`${url}/${data.id}`, data.body), url, updater);
};

export const usePatchForm = (url, updater) => {
    return useGenericMutation(
        (data) => api.patchForm(`${url}/${data.id}`, data.body),
        url,
        updater,
    );
};
