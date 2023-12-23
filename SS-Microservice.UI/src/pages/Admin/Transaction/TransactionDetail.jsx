import { Tag } from 'antd';
import { useEffect, useState } from 'react';

import { useGetTransaction } from '../../../hooks/api';
import Detail from '../../../layouts/Admin/components/Detail';
import { numberFormatter } from '../../../utils/formatter';

function transformData(transaction) {
    return [
        {
            key: '1',
            property: 'ID',
            value: transaction.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(transaction?.createdAt)?.toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: transaction?.updatedAt && new Date(transaction?.updatedAt)?.toLocaleString(),
        },
        {
            key: '4',
            property: 'Mã đơn hàng',
            value: transaction?.orderCode,
        },
        {
            key: '5',
            property: 'Ngày hoàn tất giao dịch',
            value: transaction?.completedAt && new Date(transaction?.completedAt)?.toLocaleString(),
        },
        {
            key: '6',
            property: 'Ngày thanh toán',
            value: transaction?.paidAt && new Date(transaction?.paidAt)?.toLocaleString(),
        },
        {
            key: '7',
            property: 'Phương thức thanh toán',
            value: transaction?.paymentMethod,
        },
        {
            key: '8',
            property: 'Tổng tiền',
            value: numberFormatter(transaction?.totalPay),
        },
        {
            key: '9',
            property: 'Trạng thái giao dịch',
            value: (
                <Tag className="w-fit uppercase" color={transaction?.paidAt ? 'green' : 'red'}>
                    {transaction?.completedAt ? 'Hoàn tất' : 'Chưa hoàn tất'}
                </Tag>
            ),
        },
        {
            key: '10',
            property: 'Trạng thái thanh toán',
            value: (
                <Tag className="w-fit uppercase" color={transaction?.paidAt ? 'green' : 'red'}>
                    {transaction?.paidAt ? 'Đã thanh toán' : 'Chưa thanh toán'}
                </Tag>
            ),
        },
        {
            key: '11',
            property: 'PayPal ID',
            value: transaction?.paypalOrderId,
        },
    ];
}

function TransactionDetail({ isDetailOpen, setIsDetailOpen }) {
    const { data, isLoading } = useGetTransaction(isDetailOpen.id);
    const [transaction, setTransaction] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setTransaction(transformData(data?.data));
    }, [isLoading, data]);
    return (
        <Detail
            isDetailOpen={isDetailOpen}
            setIsDetailOpen={setIsDetailOpen}
            rawData={transaction}
        />
    );
}

export default TransactionDetail;
