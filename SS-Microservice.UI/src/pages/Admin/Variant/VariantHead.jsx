import { useDeleteListVariant } from '../../../hooks/api';
import Head from '../../../layouts/Admin/components/Head';

function VariantHead({ variantIds, productId }) {
    const mutationDelete = useDeleteListVariant({
        success: () => {},
        error: (err) => {
            console.log(err);
        },
        obj: {
            ids: variantIds,
            params: { productId },
        },
    });

    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(variantIds);
    };

    return <Head title={'Quản lý dạng sản phẩm'} isAdd={false} handleDisableAll={onDisableAll} />;
}

export default VariantHead;
