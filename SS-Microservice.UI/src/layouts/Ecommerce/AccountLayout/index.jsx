import { useState } from 'react';
import BreadCrumb from '../components/Breadcrumb';
import './accountlayout.scss';
import SideBar from './SideBar';
import config from '../../../config';

const matchedRoutes = {
    'Thông tin cá nhân': config.routes.web.profile,
    'Địa chỉ': config.routes.web.address,
    'Sản phẩm yêu thích': config.routes.web.favorites,
    'Mật khẩu': config.routes.web.password,
    'Đơn mua': config.routes.web.order,
    'Thông báo': config.routes.web.notification,
};

function findKeyByValue(obj, value) {
    const keys = Object.keys(obj);
    const key = keys.find((key) => value.includes(obj[key]));
    return key;
}

function AccountLayout({ children, routeKey, isSetMinHeight = true, isSetBackground = true }) {

    return (
        <>
            <BreadCrumb
                routes={[
                    { title: 'Tài khoản' },
                    { title: findKeyByValue(matchedRoutes, routeKey) },
                ]}
            />
            <div className="account-layout-container xl:px-36 xl:py-20 sm:p-10 overflow-auto">
                <div className="flex max-xl:flex-col rounded-[0.5rem]">
                    <SideBar routeKey={routeKey} />
                    <div
                        className={`container ${
                            isSetMinHeight && 'min-h-[50rem]'
                        } max-md:my-7 xl:ml-[5.5rem] ${
                            isSetBackground && 'bg-white'
                        }  rounded-[0.2rem] `}
                    >
                        {children}
                    </div>
                </div>
            </div>
        </>
    );
}

export default AccountLayout;
