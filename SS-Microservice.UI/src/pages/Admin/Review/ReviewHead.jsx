import { notification } from 'antd';
import { useDeleteListReview } from '../../../hooks/api';
import Head from '../../../layouts/Admin/components/Head';

function ReviewHead({ reviewIds, params }) {
    const mutationDelete = useDeleteListReview({
        success: () => {
            notification.success({
                message: 'Ẩn thành công',
                description: 'Các đánh giá đã được ẩn',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Ẩn thất bại',
                description: 'Có lỗi xảy ra khi ẩn các đánh giá',
            });
        },
        obj: {
            ids: reviewIds,
            params: params,
        },
    });
    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(reviewIds);
    };
    return <Head title={'Quản lý đánh giá'} handleDisableAll={onDisableAll} isAdd={false} />;
}

export default ReviewHead;
