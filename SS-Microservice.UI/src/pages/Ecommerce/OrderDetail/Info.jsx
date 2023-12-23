import { Tag } from 'antd';
import { numberFormatter } from '../../../utils/formatter';

function Info({ order }) {
    return (
        <div className="flex max-md:flex-col justify-between p-[2rem] border-t-[0.1rem] gap-[1.2rem]">
            <div>
                <div>
                    <h1 className="capitalize text-[2rem] text-black">Thông tin thanh toán</h1>

                    <div className="bg-[#FFFAF4] p-[2rem] text-[1.6rem] flex flex-col gap-[1rem]">
                        <div className="flex items-center justify-between">
                            <h3>Trạng thái</h3>
                            <Tag
                                className="text-[1.6rem]"
                                color={order?.paymentStatus ? 'green' : 'red'}
                            >
                                {order?.paymentStatus ? 'Đã thanh toán' : 'Chưa thanh toán'}
                            </Tag>
                        </div>
                        <div className="flex items-center justify-between">
                            <h3>Thời gian thanh toán</h3>
                            <Tag
                                className="text-[1.6rem]"
                                color={order?.paymentStatus ? 'green' : 'red'}
                            >
                                {order?.transaction?.paidAt &&
                                    new Date(order?.transaction?.paidAt).toLocaleString()}
                            </Tag>
                        </div>
                        <div className="flex items-center justify-between">
                            <h3>Phương thức</h3>
                            <Tag
                                className="text-[1.6rem]"
                                color={order?.paymentStatus ? 'green' : 'red'}
                            >
                                {order?.transaction?.paymentMethod}
                            </Tag>
                        </div>
                    </div>
                </div>
                <div>
                    <h1 className="capitalize text-[2rem] text-black">Địa chỉ nhận hàng</h1>
                    <div className="bg-[#FFFAF4] p-[2rem] text-[1.6rem]">
                        <h2 className="font-medium text-[1.8rem]">{order?.address?.receiver}</h2>
                        <p className="my-[0.5rem]">{order?.address?.phone}</p>
                        <p>
                            {order?.address?.street}, {order?.address?.ward?.name},{' '}
                            {order?.address?.district?.name}, {order?.address?.province?.name}
                        </p>
                    </div>
                </div>
            </div>
            <div className="md:text-right">
                <h1 className="capitalize text-[2rem] text-black">Tổng tiền</h1>
                <div className="bg-[#FFFAF4] p-[2rem] flex flex-col gap-[2.2rem]">
                    <div className="flex justify-between gap-[11rem] text-[1.6rem] font-normal">
                        <p>Tổng tiền hàng</p>
                        <span>
                            {numberFormatter(
                                order?.items?.reduce((acc, val) => acc + val.totalPrice, 0),
                            )}
                        </span>
                    </div>
                    <div className="flex justify-between gap-[11rem] text-[1.6rem] font-normal">
                        <p>Thuế (10%)</p>
                        <span>
                            {numberFormatter(
                                ((order?.totalAmount - order?.shippingCost) * order?.tax) /
                                    (1 + order?.tax),
                            )}
                        </span>
                    </div>
                    <div className="flex justify-between gap-[11rem] text-[1.6rem] font-normal">
                        <p>Phí vận chuyển</p>
                        <span>{numberFormatter(order?.shippingCost)}</span>
                    </div>
                    <div className="flex gap-[6.2rem] items-center">
                        <p className="text-[1.6rem]">Tổng thanh toán</p>
                        <span className="text-[2.6rem] text-rose-600 font-bold">
                            {numberFormatter(order?.totalAmount)}
                        </span>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Info;
