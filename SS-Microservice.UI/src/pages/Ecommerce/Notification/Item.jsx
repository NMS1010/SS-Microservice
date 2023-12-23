import { Button } from 'antd';
import { NavLink, useNavigate } from 'react-router-dom';
import { useUpdateNotification } from '../../../hooks/api';
import { useContext } from 'react';
import { NotificationContext } from '../../../context/NotificationContext';

function Item({ notification, isRead = false }) {
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
        <NavLink>
            <div
                className={`${
                    !isRead && 'bg-[#fff2ee]'
                } flex justify-between items-center gap-[2.2rem] p-[2rem] hover:bg-gray-100 transition-all`}
            >
                <div className="flex items-center gap-[1rem]">
                    <img className="w-[7.9rem] h-[7.9rem] " src={notification?.image} />
                    <div className="">
                        <p className="text-black text-[1.6rem] font-normal">
                            {notification?.title}
                        </p>
                        <p className="text-[1.4rem] mb-[1rem] mt-[.6rem]">
                            {notification?.content}
                        </p>
                        <p className="text-rose-600 text-opacity-70 font-medium text-[1.4rem]">
                            {new Date(notification?.createdAt).toLocaleString()}
                        </p>
                    </div>
                </div>
                <NavLink
                    onClick={() => onReadNotification()}
                    to={notification?.anchor}
                    className="detail-btn text-[1.2rem] min-w-[120px] text-center border border-solid px-7 py-[0.5rem] rounded-[5px] border-gray-500 hover:border-[var(--primary-color)]"
                >
                    Chi tiết
                </NavLink>
            </div>
        </NavLink>
    );
}

export default Item;
