import apiRoutes from '../../config/apiRoutes';
import { useFetch } from '../../utils/reactQuery';

export const useStatisticTotal = () => {
    return useFetch({ url: `${apiRoutes.admin.statistic}/total`, key: 'statisticTotal' });
};

export const useStatisticRevenue = (params) => {
    return useFetch({
        url: `${apiRoutes.admin.statistic}/revenue`,
        params,
        key: 'statisticRevenue',
    });
};

export const useStatisticTopSellingProductYear = (params) => {
    return useFetch({
        url: `${apiRoutes.admin.statistic}/top-selling-product-year`,
        params,
        key: 'statisticTopSellingProductYear',
    });
};

export const useStatisticTopSellingProduct = (params) => {
    return useFetch({
        url: `${apiRoutes.admin.statistic}/top-selling-product`,
        params,
        key: 'statisticTopSellingProduct',
    });
};

export const useStatisticOrderStatus = (params) => {
    return useFetch({
        url: `${apiRoutes.admin.statistic}/order-status`,
        params,
        key: 'statisticOrderStatus',
    });
};

export const useStatisticRating = (params) => {
    return useFetch({ url: `${apiRoutes.admin.statistic}/rating`, params, key: 'statisticRating' });
};
