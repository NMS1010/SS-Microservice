import { Button, Modal, Table } from 'antd';

const baseColumns = [
    {
        title: 'Thuộc tính',
        dataIndex: 'property',
    },
    {
        title: 'Giá trị',
        dataIndex: 'value',
    },
];

function Detail({ isDetailOpen, setIsDetailOpen, rawData }) {
    const onCancel = () => {
        setIsDetailOpen({ ...isDetailOpen, isOpen: false });
    };

    return (
        <Modal
            title={<p className="text-center text-[2rem] mb-6">Thông tin chi tiết</p>}
            open={isDetailOpen.isOpen}
            onCancel={onCancel}
            footer={[
                <Button onClick={onCancel} type="primary" className="bg-red-500 text-white">
                    OK
                </Button>,
            ]}
        >
            <div className="detail-container">
                <Table pagination={false} columns={baseColumns} dataSource={rawData} />
            </div>
        </Modal>
    );
}

export default Detail;
