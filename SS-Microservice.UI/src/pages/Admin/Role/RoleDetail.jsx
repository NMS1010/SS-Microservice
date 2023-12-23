import { useEffect, useState } from 'react';

import Detail from '../../../layouts/Admin/components/Detail';
import { useGetRole } from '../../../hooks/api';

function transformData(role) { 
    return [
        {
            key: '1',
            property: 'ID',
            value: role?.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(role?.createdAt)?.toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: role?.updatedAt && new Date(role?.updatedAt)?.toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên vai trò',
            value: role?.name,
        }
    ];
}   
function RoleDetail({ isDetailOpen, setIsDetailOpen }) {
    const { data, isLoading } = useGetRole(isDetailOpen.id);
    const [role, setRole] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setRole(transformData(data?.data));
    }, [isLoading, data]);
    return (
        <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={role} />
    );
}

export default RoleDetail;
