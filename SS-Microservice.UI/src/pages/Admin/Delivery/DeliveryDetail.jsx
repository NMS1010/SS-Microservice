import { Tag } from 'antd';
import { useEffect, useState } from 'react';

import Detail from '../../../layouts/Admin/components/Detail';
import { useGetDelivery } from '../../../hooks/api';
import { numberFormatter } from '../../../utils/formatter';

function transformData(delivery) {
    return [
        {
            key: '1',
            property: 'ID',
            value: delivery?.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(delivery?.createdAt)?.toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: delivery?.updatedAt && new Date(delivery?.updatedAt)?.toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên phương thức',
            value: delivery?.name,
        },
        {
            key: '5',
            property: 'Giá',
            value: numberFormatter(delivery?.price),
        },
        {
            key: '6',
            property: 'Mô tả',
            value: delivery?.description,
        },
        {
            key: '7',
            property: 'Ảnh',
            value: (
                <img
                    className="w-20 h-20 rounded-xl"
                    src={delivery?.image}
                />
            ),
        },
        {
            key: '8',
            property: 'Trạng thái',
            value: (
                <Tag className="w-fit uppercase" color={delivery?.status ? 'green' : 'red'}>
                    {delivery?.status ? 'Kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
        },
    ];
}
function DeliveryDetail({ isDetailOpen, setIsDetailOpen }) {
    const { data, isLoading } = useGetDelivery(isDetailOpen.id);
    const [delivery, setDelivery] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setDelivery(transformData(data?.data));
    }, [isLoading, data]);
    return (
        <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={delivery} />
    );
}

export default DeliveryDetail;
