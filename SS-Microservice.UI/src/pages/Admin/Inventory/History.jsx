import { Button, Modal, Table, Tag } from 'antd';
import { useGetListDocketByProductId } from '../../../hooks/api';
import { useEffect, useState } from 'react';

const baseColumns = [
    {
        title: 'Loại',
        dataIndex: 'type',
    },
    {
        title: 'Ngày tạo',
        dataIndex: 'createdAt',
    },
    {
        title: 'Mã đơn hàng',
        dataIndex: 'orderId',
    },
    {
        title: 'Số lượng',
        dataIndex: 'quantity',
    },
    {
        title: 'Ghi chú',
        dataIndex: 'note',
    },
];

function transformData(dockets) {
    return dockets?.map((item) => {
        return {
            key: item.id,
            type: <Tag color={item.type === 'IMPORT' ? 'green' : 'blue'}>{item.type}</Tag>,
            createdAt: new Date(item.createdAt).toLocaleString(),
            orderId: item.orderId ? item.orderId : <span className="italic">Không có</span>,
            quantity: item.quantity,
            note: item.note ? item.note : <span className="italic">Không có</span>,
        };
    });
}

function History({ isDetailOpen, setIsDetailOpen }) {
    const { isLoading, data } = useGetListDocketByProductId(isDetailOpen.productId);
    const [tdata, setTData] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setTData(transformData(data?.data));
    }, [isLoading, data]);

    const onCancel = () => {
        setIsDetailOpen({ ...isDetailOpen, isOpen: false });
    };

    return (
        <Modal
            width={1000}
            title={
                <p className="text-center text-[2rem] mb-6">{`Lịch sử nhập xuất của sản phẩm "${isDetailOpen.productName}"`}</p>
            }
            open={isDetailOpen.isOpen}
            onCancel={onCancel}
            footer={[
                <Button onClick={onCancel} type="primary" className="bg-red-500 text-white">
                    OK
                </Button>,
            ]}
        >
            <div className="detail-container">
                <Table
                    pagination={false}
                    columns={baseColumns}
                    dataSource={tdata}
                    scroll={{ y: 350 }}
                />
            </div>
        </Modal>
    );
}

export default History;
