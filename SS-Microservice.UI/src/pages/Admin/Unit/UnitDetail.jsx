import { Tag } from 'antd';
import Detail from '../../../layouts/Admin/components/Detail';
import { useGetUnit } from '../../../hooks/api';
import { useEffect, useState } from 'react';

function transformData(unit) {
    return [
        {
            key: '1',
            property: 'ID',
            value: unit?.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(unit?.createdAt)?.toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: unit?.updatedAt && new Date(unit?.updatedAt)?.toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên đơn vị',
            value: unit?.name,
        },
        {
            key: '5',
            property: 'Trạng thái',
            value: (
                <Tag className="w-fit uppercase" color={unit?.status ? 'green' : 'red'}>
                    {unit?.status ? 'Đã kích hoạt' : 'Đã vô hiệu hóa'}
                </Tag>
            ),
        },
    ];
}
function UnitDetail({ isDetailOpen, setIsDetailOpen }) {
    const { data, isLoading } = useGetUnit(isDetailOpen.id);
    const [unit, setUnit] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setUnit(transformData(data?.data));
    }, [isLoading, data]);

    return <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={unit} />;
}

export default UnitDetail;
