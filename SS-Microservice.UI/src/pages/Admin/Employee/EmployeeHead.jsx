import { notification } from 'antd';

import config from '../../../config';
import { useDeleteListEmployee } from '../../../hooks/api';
import Head from '../../../layouts/Admin/components/Head';

function EmployeeHead({ params, employeeIds }) {
    const mutationDelete = useDeleteListEmployee({
        success: () => {
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Các nhân viên đã được vô hiệu hoá',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá các nhân viên',
            });
        },
        obj: {
            ids: employeeIds,
            params: params,
        },
    });
    const onDisableAll = async () => {
        await mutationDelete.mutateAsync(employeeIds);
    };
    return (
        <Head
            route={config.routes.admin.employee_create}
            title={'Quản lý nhân viên'}
            handleDisableAll={onDisableAll}
        />
    );
}

export default EmployeeHead;
