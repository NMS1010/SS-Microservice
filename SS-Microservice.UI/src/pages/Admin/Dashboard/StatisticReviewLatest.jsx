import { Card, Rate, Table, Tag } from 'antd';
import { useGetTop5ReviewLatest } from '../../../hooks/api';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import config from '../../../config';
const baseColumns = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
        width: 50,
    },
    {
        title: 'Ngày tạo',
        dataIndex: 'createdAt',
        sorter: true,
    },
    {
        title: 'Tiêu đề',
        dataIndex: 'title',
        sorter: true,
    },
    {
        title: 'Sản phẩm',
        dataIndex: 'product',
        sorter: true,
    },
    {
        title: 'Số sao',
        dataIndex: 'rating',
        sorter: true,
    },
];
function transformData(dt) {
    return dt?.map((item) => {
        return {
            key: item?.id,
            id: item?.id,
            createdAt: new Date(item?.createdAt)?.toLocaleString(),
            title: item?.title,
            product: item?.product?.name,
            rating: <Rate className="text-2xl" disabled defaultValue={item?.rating} />,
            status: (
                <Tag className="w-fit uppercase" color={item?.status ? 'green' : 'red'}>
                    {item?.status ? 'Kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
        };
    });
}

function StatisticReviewLatest() {
    const navigate = useNavigate();
    const { isLoading, data } = useGetTop5ReviewLatest();
    const [tdata, setTData] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setTData(transformData(data?.data));
    }, [isLoading, data]);

    return (
        <Card bordered={false} className="card-container min-h-[382px]">
            <div className="flex items-center justify-between mb-[2rem]">
                <h5 className="font-medium text-center text-[2rem]">Các đánh giá mới nhất</h5>
                <span
                    className="text-[#3ea4ff] cursor-pointer"
                    onClick={() => navigate(config.routes.admin.review)}
                >
                    Xem thêm
                </span>
            </div>
            <Table
                scroll={{
                    x: 600,
                }}
                pagination={false}
                columns={baseColumns}
                dataSource={tdata}
                size="small"
            />
        </Card>
    );
}

export default StatisticReviewLatest;
