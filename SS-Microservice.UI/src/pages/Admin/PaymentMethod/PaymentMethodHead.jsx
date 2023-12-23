import { notification } from 'antd';

import Head from '../../../layouts/Admin/components/Head';
import config from '../../../config';
import { useDeleteListPaymentMethod } from '../../../hooks/api';

function PaymentMethodHead({paymentMethodIds, params}) {
    const mutationDelete = useDeleteListPaymentMethod({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các phương thức thanh toán đã được vô hiệu hoá',
            });},
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các phương thức thanh toán',
            });
        },
        obj: {
            ids: paymentMethodIds,
            params: params,
        },
    });
    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(paymentMethodIds);
    };
    return (
        <Head
            route={config.routes.admin.payment_method_create}
            title={'Quản lý phương thức thanh toán'}
            handleDisableAll={onDisableAll}
        />
    );
}

export default PaymentMethodHead;
