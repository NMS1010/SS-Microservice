import { notification } from 'antd';

import { useDeleteListUser } from '../../../hooks/api/useUserApi';
import Head from '../../../layouts/Admin/components/Head';

function AccountHead({ params, accountIds }) {
    const mutationDelete = useDeleteListUser({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các tài khoản đã được vô hiệu hoá',
            });},
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các tài khoản',
            });
        },
        obj: {
            ids: accountIds,
            params: params,
        },
    });
    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(accountIds);
    };
    return <Head title={'Quản lý tài khoản'} isAdd={false} handleDisableAll={onDisableAll} />;
}

export default AccountHead;
