import './navbar.scss';
import config from '../../../../config';
import React, { useState } from 'react';
import {
    IconHome2,
    IconFingerprint,
    IconBuilding,
    IconPackage,
    IconBuildingStore,
    IconClipboardText,
    IconTruckDelivery,
    IconMessage,
    IconCertificate,
    IconTransfer,
    IconBrandCashapp,
} from '@tabler/icons-react';
import { Menu } from 'antd';
import { useLocation, useNavigate } from 'react-router-dom';

const items = [
    getItem('Trang chủ', config.routes.admin.dashboard, <IconHome2 />),
    getItem('Người dùng', 'user', <IconFingerprint />, [
        getItem('Tài khoản', config.routes.admin.account),
        getItem('Vai trò', config.routes.admin.role),
    ]),
    getItem('Nhân viên', config.routes.admin.employee, <IconBuilding />),
    getItem('Sản phẩm', 'product', <IconPackage />, [
        getItem('Sản phẩm', config.routes.admin.product),
        getItem('Danh mục', config.routes.admin.product_category),
        getItem('Thương hiệu', config.routes.admin.brand),
        getItem('Đơn vị tính', config.routes.admin.unit),
    ]),
    getItem('Kho hàng', config.routes.admin.inventory, <IconBuildingStore />),
    getItem('Đơn hàng', 'order', <IconClipboardText />, [
        getItem('Đơn mua hàng', config.routes.admin.order),
        getItem('Lý do hủy đơn hàng', config.routes.admin.reason_cancel),
    ]),
    getItem('Vận chuyển', config.routes.admin.delivery, <IconTruckDelivery />),
    getItem('Đánh giá', config.routes.admin.review, <IconMessage />),
    getItem('Khuyến mãi', config.routes.admin.sale, <IconCertificate />),
    getItem('Giao dịch', config.routes.admin.transaction, <IconTransfer />),
    getItem('Thanh toán', config.routes.admin.payment_method, <IconBrandCashapp />),
];

const rootSubmenuKeys = ['user', 'product', 'order'];

function getItem(label, key, icon, children, type) {
    return {
        key,
        icon,
        children,
        label,
        type,
    };
}

function Navbar() {
    const navigate = useNavigate();
    let routeKey = useLocation().pathname;

    const [openKeys, setOpenKeys] = useState(['home']);
    const onOpenChange = (keys) => {
        const latestOpenKey = keys.find((key) => openKeys.indexOf(key) === -1);
        if (latestOpenKey && rootSubmenuKeys.indexOf(latestOpenKey) === -1) {
            setOpenKeys(keys);
        } else {
            setOpenKeys(latestOpenKey ? [latestOpenKey] : []);
        }
    };
    const onClick = (e) => {
        navigate(e.key);
    };

    return (
        <div className="navbar-admin-container w-[225px]">
            <Menu
                mode="inline"
                onClick={onClick}
                defaultSelectedKeys={[routeKey]}
                openKeys={openKeys}
                onOpenChange={onOpenChange}
                items={items}
            />
        </div>
    );
}

export default Navbar;
