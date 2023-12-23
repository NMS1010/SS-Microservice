import Head from '../../../layouts/Admin/components/Head';
import config from '../../../config';
import { useDeleteListUnit } from '../../../hooks/api';
import { notification } from 'antd';

function UnitHead({ unitIds, params }) {
    const mutationDelete = useDeleteListUnit({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các đơn vị sản phẩm đã được vô hiệu hoá',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các đơn vị sản phẩm',
            });
        },
        obj: {
            ids: unitIds,
            params: params,
        },
    });

    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(unitIds);
    };

    return (
        <Head
            route={config.routes.admin.unit_create}
            title={'Quản lý đơn vị sản phẩm'}
            isAdd={true}
            handleDisableAll={onDisableAll}
        />
    );
}

export default UnitHead;
