import { Tag } from 'antd';
import { useEffect, useState } from 'react';

import Detail from '../../../layouts/Admin/components/Detail';
import { useGetPaymentMethod } from '../../../hooks/api';

function transformData(paymentMethod) {
    return [
        {
            key: '1',
            property: 'ID',
            value: paymentMethod?.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(paymentMethod?.createdAt)?.toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: paymentMethod?.updatedAt && new Date(paymentMethod?.updatedAt)?.toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên phương thức',
            value: paymentMethod?.name,
        },
        {
            key: '5',
            property: 'Mã',
            value: paymentMethod?.code,
        },
        {
            key: '6',
            property: 'Ảnh',
            value: <img className="w-20 h-20 rounded-xl" src={paymentMethod?.image} />,
        },
        {
            key: '7',
            property: 'Trạng thái',
            value: (
                <Tag className="w-fit uppercase" color={paymentMethod?.status ? 'green' : 'red'}>
                    {paymentMethod?.status ? 'Kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
        },
    ];
}
function PaymentMethodDetail({ isDetailOpen, setIsDetailOpen }) {
    const { data, isLoading } = useGetPaymentMethod(isDetailOpen.id);
    const [paymentMethod, setPaymentMethod] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setPaymentMethod(transformData(data?.data));
    }, [isLoading, data]);
    return (
        <Detail
            isDetailOpen={isDetailOpen}
            setIsDetailOpen={setIsDetailOpen}
            rawData={paymentMethod}
        />
    );
}

export default PaymentMethodDetail;
