import { Typography, notification } from 'antd';
import { useContext } from 'react';
import { NavLink } from 'react-router-dom';
import { useUpdateNotification } from '../../../../hooks/api';
import { NotificationContext } from '../../../../context/NotificationContext';



function NotificationItem({ notification, isRead = false }) {
    const { refetchNotification } = useContext(NotificationContext);
    const mutateRead = useUpdateNotification({
        success: (data) => {
            refetchNotification();
        },
    });

    const onReadNotification = async () => {
        await mutateRead.mutateAsync({
            id: notification?.id,
            body: {},
        });
    };

    return (
        <NavLink to={notification?.anchor} onClick={onReadNotification}>
            <div
                className={`${
                    !isRead && 'bg-[#fff2ee]'
                } flex justify-between items-center gap-[2.2rem] p-[2rem] border-b border-solid hover:bg-gray-100 transition-all w-[40rem] h-[10rem]`}
            >
                <div className="flex items-center gap-[1rem]">
                    <img
                        className="w-[7rem] h-[7rem] border border-solid"
                        src={notification?.image}
                    />
                    <div className="">
                        <p className="text-black text-[1.4rem] font-normal">
                            {notification?.title}
                        </p>
                        <Typography.Paragraph ellipsis={{ rows: 2 }}>
                            <p className="text-[1.2rem] mb-[1rem] mt-[.6rem] break-words">
                                {notification?.content}
                            </p>
                        </Typography.Paragraph>
                    </div>
                </div>
            </div>
        </NavLink>
    );
}

export default NotificationItem;
