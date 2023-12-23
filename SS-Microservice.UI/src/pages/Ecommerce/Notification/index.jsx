import { Pagination } from 'antd';
import config from '../../../config';
import { useGetListNotification } from '../../../hooks/api';
import AccountLayout from '../../../layouts/Ecommerce/AccountLayout';
import Head from './Head';
import Wrapper from './Wrapper';
import './notification.scss';
import { useState } from 'react';

function NotificationPage() {
    const [params, setParams] = useState({
        columnName: 'createdAt',
        isSortAscending: false,
        pageIndex: 1,
        pageSize: 5
    })
    const { data, isLoading, refetch } = useGetListNotification(params);
    const onChange = (value) => {
        setParams((prev) => ({ ...prev, pageIndex: value }));
    };

    return (
        <AccountLayout isSetMinHeight={false} routeKey={config.routes.web.notification}>
            <div className="notification-container shadow-[0_1px_2px_0_rgba(0,0,0,0.13)] h-full">
                <Head refetchNotify={refetch} />
                <Wrapper notifications={data?.data?.items} />
                {data?.data?.items?.length > 0 && (
                <div className="flex justify-center my-7">
                    <Pagination
                        onChange={onChange}
                        current={params.pageIndex}
                        total={data?.data?.totalItems}
                        pageSize={params.pageSize}
                    />
                </div>
            )}
            </div>
        </AccountLayout>
    );
}

export default NotificationPage;
