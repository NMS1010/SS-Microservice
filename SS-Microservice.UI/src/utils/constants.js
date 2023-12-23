import {
    faCancel,
    faDownload,
    faReceipt,
    faSpinner,
    faTruck,
} from '@fortawesome/free-solid-svg-icons';

export const ORDER_STATUS = {
    NOT_PROCESSED: 'NOT_PROCESSED',
    PROCESSING: 'PROCESSING',
    SHIPPED: 'SHIPPED',
    DELIVERED: 'DELIVERED',
    CANCELLED: 'CANCELLED',
};

const ORDER_STATUS_OBJ = {
    NOT_PROCESSED: {
        title: 'Chưa xử lý',
        color: 'green',
        key: 1,
        icon: faReceipt,
    },
    PROCESSING: {
        title: 'Đang xử lý',
        color: 'green',
        key: 2,
        icon: faSpinner,
    },
    SHIPPED: {
        title: 'Đang giao hàng',
        color: 'green',
        key: 3,
        icon: faTruck,
    },
    DELIVERED: {
        title: 'Đã giao hàng',
        color: 'green',
        key: 4,
        icon: faDownload,
    },
    CANCELLED: {
        title: 'Đã hủy',
        color: 'green',
        key: 5,
        icon: faCancel,
    },
};

export const EXCHANGE_VALUE_USD_VND = 23000;
export const MIN_PRICE = 0;
export const MAX_PRICE = 10000000;

export const getOrderStatus = (status) => {
    return ORDER_STATUS_OBJ[status];
};

export const getAllOrderStatusSelect = () => {
    return Object.keys(ORDER_STATUS_OBJ).map((key) => {
        return {
            label: ORDER_STATUS_OBJ[key].title,
            value: key,
        };
    });
};
