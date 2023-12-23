import './header.scss';
import images from '../../../../assets/images';
import { Link, useNavigate } from 'react-router-dom';
import {
    IconUser,
    IconBell,
    IconBrandMessenger,
    IconTableShare,
    IconMoonStars,
    IconSquareArrowRight,
} from '@tabler/icons-react';
import { Tooltip } from 'antd';
import { clearToken } from '../../../../utils/storage';
import config from '../../../../config';

function Header() {
    const navigate = useNavigate();
    const onLogout = () => {
        clearToken();
        navigate(config.routes.web.login);
    };

    return (
        <div className="header-container w-screen fixed z-10 bg-white border-b-[1px]">
            <div className="container mx-auto h-[--height-header-admin] flex items-center justify-between">
                <div className="h-full">
                    <Link>
                        <img className="h-full" src={images.logo} alt="logo" />
                    </Link>
                </div>
                <div className="text-[1.4rem] font-medium flex items-center justify-between">
                    <ul className="flex items-center justify-between">
                        <li className="mr-[2rem] py-[0.5rem] px-[1rem] hover:bg-[--background-item-menu-color] rounded-[5px]">
                            <Link to={config.routes.admin.profile} className="flex items-center">
                                <IconUser className="h-[1.8rem]" />
                                Tài khoản
                            </Link>
                        </li>
                        <li className="mr-[2rem] py-[0.5rem] px-[1rem] hover:bg-[--background-item-menu-color] rounded-[5px]">
                            <Link className="flex items-center">
                                <IconBell className="h-[1.8rem]" />
                                Thông báo
                            </Link>
                        </li>
                        <li className="mr-[2rem] py-[0.5rem] px-[1rem] hover:bg-[--background-item-menu-color] rounded-[5px]">
                            <Link className="flex items-center">
                                <IconBrandMessenger className="h-[1.8rem]" />
                                Tin nhắn
                            </Link>
                        </li>
                        <li className="mr-[2rem] py-[0.5rem] px-[1rem] hover:bg-[--background-item-menu-color] rounded-[5px]">
                            <Link to={'/'} className="flex items-center">
                                <IconTableShare className="h-[1.6rem]" />
                                Website
                            </Link>
                        </li>
                    </ul>
                    <ul className="flex items-center justify-between text-[1.6rem]">
                        <li className="mr-[1rem] hover:bg-lime-50">
                            <Tooltip placement="bottom" title="Thay đổi chế độ màu">
                                <button className="px-[0.7rem] py-[0.4rem] border-[1px] border-[--primary-color] rounded-[5px] text-center text-[--primary-color]">
                                    <IconMoonStars className="h-[1.8rem]" />
                                </button>
                            </Tooltip>
                        </li>
                        <li className="hover:bg-lime-50">
                            <Tooltip placement="bottom" title="Đăng xuất">
                                <button
                                    onClick={onLogout}
                                    className="px-[0.7rem] py-[0.4rem] border-[1px] border-[--primary-color] rounded-[5px] text-center text-[--primary-color]"
                                >
                                    <IconSquareArrowRight className="h-[1.8rem]" />
                                </button>
                            </Tooltip>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    );
}

export default Header;
