import { faCoins } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import OrderItem from './OrderItem';
import { NavLink, useNavigate } from 'react-router-dom';
import config from '../../../config';
import { ORDER_STATUS, getOrderStatus } from '../../../utils/constants';
import { numberFormatter } from '../../../utils/formatter';
import { Tag } from 'antd';

function Order({ order, isLastItem = false }) {
    const isOrderNotProcessed =
        order?.transaction?.paymentMethod?.toLowerCase()?.includes('paypal') &&
        !order?.paymentStatus && order?.status !== ORDER_STATUS.CANCELLED;
    return (
        <>
            <div className="bg-white shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
                <div
                    className={`flex ${
                        isOrderNotProcessed ? 'justify-between' : 'justify-end'
                    } items-center border-b-[0.1rem] text-[1.6rem] mx-[2.2rem] py-[1.4rem]`}
                >
                    {isOrderNotProcessed && (
                        <div className="">
                            <Tag color="red">Đơn hàng này cần thanh toán trước</Tag>
                        </div>
                    )}
                    <div
                        style={{
                            color: getOrderStatus(order?.status).color,
                        }}
                        className={`flex justify-end items-center`}
                    >
                        <FontAwesomeIcon
                            icon={getOrderStatus(order?.status).icon}
                            className="mr-[0.7rem]"
                        />
                        <p className="mb-0">{getOrderStatus(order?.status).title}</p>
                    </div>
                </div>
                <NavLink
                    to={config.routes.web.order + '/' + order?.code}
                    className="border-b-[0.16rem] border-b-gray-300 cursor-pointer"
                >
                    {order?.items?.map((item, index) => {
                        return (
                            <OrderItem
                                key={item?.id}
                                item={item}
                                isLastItem={index === order?.items?.length - 1}
                            />
                        );
                    })}
                </NavLink>
                <div
                    className={`bg-stone-100  px-[2.2rem] py-[1.5rem] flex ${
                        isOrderNotProcessed ? 'justify-between' : 'justify-end'
                    }`}
                >
                    {isOrderNotProcessed && (
                        <NavLink
                            to={config.routes.web.checkout + '/payment/' + order?.code}
                            className={
                                'border-red-400 border border-solid px-4 hover:bg-red-500 hover:text-white transition-all py-2 text-[1.4rem] text-red-500 rounded-lg'
                            }
                        >
                            Thanh toán
                        </NavLink>
                    )}
                    <div className="flex justify-end">
                        <div className="flex items-center text-black text-[1.8rem]">
                            <FontAwesomeIcon icon={faCoins} />
                            <p className="ml-[.5rem]">Thành tiền: </p>
                        </div>
                        <span className="text-rose-600 font-medium text-[1.8rem] ml-[3.1rem]">
                            {numberFormatter(order?.totalAmount)}
                        </span>
                    </div>
                </div>
            </div>
            {!isLastItem && <div className="space h-[1rem]"></div>}
        </>
    );
}

export default Order;
