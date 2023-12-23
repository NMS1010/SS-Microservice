import { notification } from 'antd';
import { useDeleteListProductImage, useDeleteListVariant } from '../../../hooks/api';
import Head from '../../../layouts/Admin/components/Head';

function ProductImageHead({ productImageIds, productId }) {
    const mutationDelete = useDeleteListProductImage({
        success: () => {
            notification.success({
                message: 'Xóa hình ảnh thành công',
                description: 'Các hình ảnh sản phẩm đã được xóa',
            });
        },
        error: (err) => {
            notification.success({
                message: 'Xóa hình ảnh thất bại',
                description: 'Có lỗi xảy ra khi xóa các hình ảnh sản phẩm',
            });
        },
        obj: {
            ids: productImageIds,
            params: { productId },
        },
    });

    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(productImageIds);
    };

    return (
        <Head title={'Quản lý hình ảnh sản phẩm'} isAdd={false} handleDisableAll={onDisableAll} />
    );
}

export default ProductImageHead;
