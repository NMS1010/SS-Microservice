import { useEffect, useState } from 'react';
import config from '../../../config';
import { useGetListUserOrder } from '../../../hooks/api';
import AccountLayout from '../../../layouts/Ecommerce/AccountLayout';
import Order from './Order';
import OrderStateTab from './OrderStateTab';
import './order.scss';
import SpinLoading from '../../../layouts/Ecommerce/components/SpinLoading';
import images from '../../../assets/images';

function OrderPage() {
    const [chosenStatus, setChosenStatus] = useState(null);
    const { data, isLoading } = useGetListUserOrder({
        orderStatus: chosenStatus,
        columnName: 'createdAt',
        isSortAscending: false,
    });

    return (
        <AccountLayout
            isSetMinHeight={false}
            routeKey={config.routes.web.order}
            isSetBackground={data?.data?.items?.length === 0}
        >
            <div className="order-container h-full">
                <OrderStateTab setChosenStatus={setChosenStatus} />
                <div className="space"></div>
                {isLoading ? (
                    <div className="flex justify-center">
                        <SpinLoading />
                    </div>
                ) : data?.data?.items?.length === 0 ? (
                    <div className="text-center h-[350px] shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
                        <div className="flex justify-center">
                            <img className="w-48 h-48 block mt-40" src={images.order_empty} />
                        </div>
                        <p className="text-[1.6rem]">Chưa có đơn hàng</p>
                    </div>
                ) : (
                    data?.data?.items?.map((order, index) => {
                        return (
                            <Order
                                key={index}
                                order={order}
                                isLastItem={index === data?.data?.items?.length - 1}
                            />
                        );
                    })
                )}
            </div>
        </AccountLayout>
    );
}

export default OrderPage;
