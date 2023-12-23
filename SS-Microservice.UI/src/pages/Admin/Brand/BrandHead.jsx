import Head from '../../../layouts/Admin/components/Head';
import config from '../../../config';
import { useDeleteListBrand } from '../../../hooks/api';
import { notification } from 'antd';

function BrandHead({ brandIds, params }) {
    const mutationDelete = useDeleteListBrand({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các thương hiệu sản phẩm đã được vô hiệu hoá',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các thương hiệu sản phẩm',
            });
        },
        obj: {
            ids: brandIds,
            params: params,
        },
    });

    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(brandIds);
    };

    return (
        <Head
            route={config.routes.admin.brand_create}
            title={'Quản lý thương hiệu sản phẩm'}
            isAdd={true}
            handleDisableAll={onDisableAll}
        />
    );
}

export default BrandHead;
