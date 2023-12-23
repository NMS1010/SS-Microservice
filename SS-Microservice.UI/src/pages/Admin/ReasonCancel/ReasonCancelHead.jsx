import Head from '../../../layouts/Admin/components/Head';
import config from '../../../config';
import { useDeleteListOrderCancellationReason } from '../../../hooks/api';
import { notification } from 'antd';

function ReasonCancelHead({ params, reasonCancellationIds }) {
    const mutationDelete = useDeleteListOrderCancellationReason({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các lý do huỷ đơn hàng đã được vô hiệu hoá',
            });},
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các lý do huỷ đơn hàng',
            });
        },
        obj: {
            ids: reasonCancellationIds,
            params: params,
        },
    });
    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(reasonCancellationIds);
    };
    return (
        <Head
            route={config.routes.admin.reason_cancel_create}
            title={'Quản lý lý do hủy đơn hàng'}
            isAdd={true}
            handleDisableAll={onDisableAll}
        />
    );
}

export default ReasonCancelHead;
