import { Tag } from 'antd';
import Detail from '../../../layouts/Admin/components/Detail';
import { useGetOrder } from '../../../hooks/api/useOrderApi';
import { useEffect, useState } from 'react';
import { getOrderStatus } from '../../../utils/constants';
import { numberFormatter } from '../../../utils/formatter';

function transformData(item) {
    return [
        {
            key: '1',
            property: 'ID',
            value: item?.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(item?.createdAt)?.toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: item?.updatedAt && new Date(item?.updatedAt)?.toLocaleString(),
        },
        {
            key: '4',
            property: 'Mã đơn hàng',
            value: item?.code,
        },
        {
            key: '5',
            property: 'Trạng thái',
            value: (
                <Tag className="w-fit uppercase" color={getOrderStatus(item?.status)?.color}>
                    {getOrderStatus(item?.status)?.title}
                </Tag>
            ),
        },
        {
            key: '6',
            property: 'Lý do hủy đơn hàng',
            value: item?.otherCancelReason || item?.cancelReason?.name,
        },
        {
            key: '7',
            property: 'Ghi chú đơn hàng',
            value: item?.note,
        },
        {
            key: '8',
            property: 'Thông tin đặt hàng',
            value: (
                <div>
                    <p>{item?.user?.firstName + ' ' + item?.user?.lastName}</p>
                    <p>{item?.user?.email}</p>
                </div>
            ),
        },
        {
            key: '9',
            property: 'Thông tin nhận hàng',
            value: (
                <div>
                    <p>{item?.address?.receiver}</p>
                    <p>{item?.address?.phone}</p>
                    <p className="opacity-[0.6]">
                        {item?.address?.street}, {item?.address?.ward?.name},{' '}
                        {item?.address?.district?.name}, {item?.address?.province?.name}
                    </p>
                </div>
            ),
        },
        {
            key: '10',
            property: 'Tổng tiền',
            value: (
                <div className=" text-red-500">
                    {numberFormatter(item?.items?.reduce((acc, val) => acc + val.totalPrice, 0))}
                </div>
            ),
        },
        {
            key: '11',
            property: 'Thuế',
            value: (
                <div className=" text-red-500">
                    {numberFormatter(
                        ((item?.totalAmount - item?.shippingCost) * item?.tax) / (1 + item?.tax),
                    )}
                </div>
            ),
        },
        {
            key: '12',
            property: 'Phương thức vận chuyển',
            value: (
                <Tag className="w-fit uppercase" color="green">
                    {item?.deliveryMethod}
                </Tag>
            ),
        },
        {
            key: '13',
            property: 'Phí vận chuyển',
            value: <div className="text-red-500">{numberFormatter(item?.shippingCost)}</div>,
        },
        {
            key: '14',
            property: 'Tổng phí phải trả',
            value: (
                <div className="font-bold text-red-500 text-[2rem]">
                    {numberFormatter(item?.totalAmount)}
                </div>
            ),
        },
        {
            key: '15',
            property: 'Hình thức thanh toán',
            value: (
                <Tag className="w-fit uppercase" color="green">
                    {item?.transaction?.paymentMethod}
                </Tag>
            ),
        },
        {
            key: '16',
            property: 'Trạng thái thanh toán',
            value: (
                <Tag className="w-fit uppercase" color={`${item?.paymentStatus ? 'green' : 'red'}`}>
                    {item?.paymentStatus ? 'Đã thanh toán' : 'Chưa thanh toán'}
                </Tag>
            ),
        },
    ];
}

function OrderDetail({ isDetailOpen, setIsDetailOpen }) {
    const { data, isLoading } = useGetOrder(isDetailOpen?.id);

    const [order, setOrder] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setOrder(transformData(data?.data));
    }, [isLoading, data]);

    return <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={order} />;
}

export default OrderDetail;
