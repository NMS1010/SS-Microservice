import { useNavigate } from 'react-router-dom';
import { Menu } from 'antd';
import { faBell, faEdit, faUser } from '@fortawesome/free-regular-svg-icons';
import { faBook } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import config from '../../../config';
import { useGetMe } from '../../../hooks/api';
function getItem(label, key, icon, children, type) {
    return {
        key,
        icon,
        children,
        label,
        type,
    };
}

const items = [
    getItem('Tài khoản của tôi', 'my-account', <FontAwesomeIcon icon={faUser} />, [
        getItem('Hồ sơ', config.routes.web.profile),
        getItem('Địa chỉ', config.routes.web.address),
        getItem('Yêu thích', config.routes.web.favorites),
        getItem('Đổi mật khẩu', config.routes.web.password),
    ]),
    getItem('Đơn mua', config.routes.web.order, <FontAwesomeIcon icon={faBook} />),
    getItem('Thông báo', config.routes.web.notification, <FontAwesomeIcon icon={faBell} />),
];
function SideBar({ routeKey }) {
    const { data } = useGetMe();
    const navigate = useNavigate();
    const onClick = (e) => {
        navigate(e.key);
    };

    return (
        <div className="xl:w-1/5">
            <div className="flex items-center max-md:flex-col justify-center pt-7 pb-[2.7rem] border-b-[0.1rem] border-b-gray-300">
                <img
                    src={data?.data?.avatar}
                    className="rounded-full w-[4.8rem] h-[4.8rem] bg-gray-500"
                />
                <div className="ml-3 max-md:text-center">
                    <h1 className="my-0 text-black text-[1.6rem] font-normal">
                        {data?.data?.firstName} {data?.data?.lastName}
                    </h1>
                    <button className="text-black text-opacity-60 text-[1.2rem] mt-2 font-normal">
                        <FontAwesomeIcon className="mr-1" icon={faEdit} />
                        Sửa hồ sơ
                    </button>
                </div>
            </div>
            <Menu
                className="bg-transparent border-none font-[roboto]"
                onClick={onClick}
                defaultSelectedKeys={[routeKey]}
                defaultOpenKeys={[items[0].key]}
                mode="inline"
                items={items}
            />
        </div>
    );
}

export default SideBar;
