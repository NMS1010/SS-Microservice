import { Card, Table, Tag } from 'antd';
import { useGetTop5OrderLatest } from '../../../hooks/api';
import { useEffect, useState } from 'react';
import { numberFormatter } from '../../../utils/formatter';
import { getOrderStatus } from '../../../utils/constants';
import { useNavigate } from 'react-router-dom';
import config from '../../../config';

const baseColumns = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
    },
    {
        title: 'Ngày đặt',
        dataIndex: 'createdAt',
        sorter: true,
    },
    {
        title: 'Mã đơn',
        dataIndex: 'code',
        sorter: true,
    },
    {
        title: 'Phương thức',
        dataIndex: 'paymentMethod',
        sorter: true,
    },
    {
        title: 'Thanh toán',
        dataIndex: 'paymentStatus',
        sorter: true,
    },
    {
        title: 'Trạng thái',
        dataIndex: 'status',
        sorter: true,
    },
    {
        title: 'Tổng tiền',
        dataIndex: 'totalAmount',
        sorter: true,
    },
];

function transformData(dt) {
    return dt?.map((item) => {
        return {
            key: item?.id,
            id: item?.id,
            createdAt: new Date(item?.createdAt)?.toLocaleString(),
            code: item?.code,
            paymentStatus: (
                <Tag className="uppercase" color={`${item?.paymentStatus ? 'green' : 'red'}`}>
                    {item?.paymentStatus ? 'Đã thanh toán' : 'Chưa thanh toán'}
                </Tag>
            ),
            paymentMethod: (
                <div className="flex justify-center">
                    <Tag className="w-fit uppercase" color="green">
                        {item?.transaction?.paymentMethod}
                    </Tag>
                </div>
            ),
            totalAmount: (
                <div className="font-bold text-red-500">{numberFormatter(item?.totalAmount)}</div>
            ),
            status: (
                <div className="flex justify-center">
                    <Tag className="uppercase" color={getOrderStatus(item?.status)?.color}>
                        {getOrderStatus(item?.status)?.title}
                    </Tag>
                </div>
            ),
        };
    });
}

function StatisticOrderLatest() {
    const navigate = useNavigate();
    const { isLoading, data } = useGetTop5OrderLatest();
    const [tdata, setTData] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setTData(transformData(data?.data));
    }, [isLoading, data]);

    return (
        <Card bordered={false} className="card-container min-h-[382px]">
            <div className="flex items-center justify-between mb-[2rem]">
                <h5 className="font-medium text-center text-[2rem]">Các đơn hàng mới nhất</h5>
                <span
                    className="text-[#3ea4ff] cursor-pointer"
                    onClick={() => navigate(config.routes.admin.order)}
                >
                    Xem thêm
                </span>
            </div>
            <Table
                scroll={{
                    x: 1500,
                }}
                className="overflow-x-auto"
                size="small"
                pagination={false}
                columns={baseColumns}
                dataSource={tdata}
            />
        </Card>
    );
}

export default StatisticOrderLatest;
