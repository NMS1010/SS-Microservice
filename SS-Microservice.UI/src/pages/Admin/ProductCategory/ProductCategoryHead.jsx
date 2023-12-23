import Head from '../../../layouts/Admin/components/Head';
import config from '../../../config';
import { useDeleteListProductCategory } from '../../../hooks/api';
import { notification } from 'antd';

function ProductCategoryHead({ productCategoryIds, params }) {
    const mutationDelete = useDeleteListProductCategory({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các thể loại sản phẩm đã được vô hiệu hoá',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các thể loại sản phẩm',
            });
        },
        obj: {
            ids: productCategoryIds,
            params: params,
        },
    });

    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(productCategoryIds);
    };

    return (
        <Head
            route={config.routes.admin.product_category_create}
            title={'Quản lý danh mục sản phẩm'}
            isAdd={true}
            handleDisableAll={onDisableAll}
        />
    );
}

export default ProductCategoryHead;
