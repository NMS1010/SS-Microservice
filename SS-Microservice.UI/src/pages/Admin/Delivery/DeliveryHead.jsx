import { notification } from 'antd';

import config from '../../../config';
import { useDeleteListDelivery } from '../../../hooks/api';
import Head from '../../../layouts/Admin/components/Head';

function DeliveryHead({ deliveryIds, params }) {
    const mutationDelete = useDeleteListDelivery({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các phương thức vận chuyển đã được vô hiệu hoá',
            });},
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các phương thức vận chuyển',
            });
        },
        obj: {
            ids: deliveryIds,
            params: params,
        },
    });
    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(deliveryIds);
    };
    return (
        <Head
            route={config.routes.admin.delivery_create}
            title={'Quản lý vận chuyển'}
            handleDisableAll={onDisableAll}
        />
    );
}

export default DeliveryHead;
