import { Tag } from 'antd';
import { useEffect, useState } from 'react';

import Detail from '../../../layouts/Admin/components/Detail';
import { useGetOrderCancellationReason } from '../../../hooks/api';

function transformData(cancelReason) {
    return [
        {
            key: '1',
            property: 'ID',
            value: cancelReason?.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(cancelReason?.createdAt)?.toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: cancelReason?.updatedAt && new Date(cancelReason?.updatedAt)?.toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên lý do huỷ',
            value: cancelReason?.name,
        },
        {
            key: '5',
            property: 'Ghi chú',
            value: cancelReason?.note,
        },
        {
            key: '6',
            property: 'Trạng thái',
            value: (
                <Tag className="w-fit uppercase" color={cancelReason?.status ? 'green' : 'red'}>
                    {cancelReason?.status ? 'Kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
        },
    ];
}
function ReasonCancelDetail({ isDetailOpen, setIsDetailOpen }) {
    const { data, isLoading } = useGetOrderCancellationReason(isDetailOpen.id);
    const [cancelReason, setCancelReason] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setCancelReason(transformData(data?.data));
    }, [isLoading, data]);

    return (
        <Detail
            isDetailOpen={isDetailOpen}
            setIsDetailOpen={setIsDetailOpen}
            rawData={cancelReason}
        />
    );
}

export default ReasonCancelDetail;
