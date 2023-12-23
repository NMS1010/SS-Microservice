import { createContext, useEffect, useState } from 'react';
import { useGetListNotification } from '../hooks/api';
import { notification } from 'antd';
import getSignalRConnection from '../utils/signalR';

export const NotificationContext = createContext();

function NotificationContextProvider({ children }) {
    const { data, isLoading, refetch } = useGetListNotification({
        pageSize: 5,
        columnName: 'createdAt',
        isSortAscending: false,
    });
    const [countNotify, setCountNotify] = useState(0);

    useEffect(() => {
        (async () => {
            const connection = await getSignalRConnection();
            connection.on('ReceiveNotification', (data, count) => {
                refetch();
                notification.success({
                    message: data.title,
                    description: data.content,
                });
                setCountNotify(count);
            });
            
            connection.on('CountUnreadingNotification', (count) => {
                setCountNotify(count);
            });
        })();
    }, []);

    // useEffect(() => {
    //     if (data) {
    //         setCountNotify(data?.data?.items?.filter((item) => !item.status)?.length || 0);
    //     }
    // }, [data, isLoading]);

    return (
        <NotificationContext.Provider
            value={{
                countNotify,
                notifications: data?.data?.items || [],
                refetchNotification: refetch,
                setCountNotify: setCountNotify,
            }}
        >
            {children}
        </NotificationContext.Provider>
    );
}

export default NotificationContextProvider;
