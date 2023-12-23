import Head from '../../../layouts/Admin/components/Head';
import config from '../../../config';
import { useDeleteListProduct } from '../../../hooks/api';
import { notification } from 'antd';

function ProductHead({ productIds, params }) {
    const mutationDelete = useDeleteListProduct({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các sản phẩm đã được vô hiệu hoá',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các sản phẩm',
            });
        },
        obj: {
            ids: productIds,
            params: params,
        },
    });

    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(productIds);
    };

    return (
        <Head
            route={config.routes.admin.product_create}
            title={'Quản lý sản phẩm'}
            isAdd={true}
            handleDisableAll={onDisableAll}
        />
    );
}

export default ProductHead;
