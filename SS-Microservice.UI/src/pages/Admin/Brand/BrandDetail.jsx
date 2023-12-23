import { Tag } from 'antd';
import Detail from '../../../layouts/Admin/components/Detail';
import { useEffect, useState } from 'react';
import { useGetBrand } from '../../../hooks/api';

function transformData(brand) {
    return [
        {
            key: '1',
            property: 'ID',
            value: brand?.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(brand?.createdAt)?.toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: brand?.updatedAt && new Date(brand?.updatedAt)?.toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên thương hiệu',
            value: brand?.name,
        },
        {
            key: '5',
            property: 'Code',
            value: brand?.code,
        },
        {
            key: '6',
            property: 'Giới thiệu',
            value: brand?.description,
        },
        {
            key: '7',
            property: 'Hình ảnh',
            value: <img className="w-20 h-20 rounded-xl" src={brand?.image} />,
        },
        {
            key: '8',
            property: 'Trạng thái',
            value: (
                <Tag className="w-fit uppercase" color={brand?.status ? 'green' : 'red'}>
                    {brand?.status ? 'Đã kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
        },
    ];
}

function BrandDetail({ isDetailOpen, setIsDetailOpen }) {
    const { isLoading, data } = useGetBrand(isDetailOpen.id);
    const [brand, setBrand] = useState();

    useEffect(() => {
        if (isLoading || !data) return;
        setBrand(transformData(data?.data));
    }, [isLoading, data]);

    return <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={brand} />;
}

export default BrandDetail;
