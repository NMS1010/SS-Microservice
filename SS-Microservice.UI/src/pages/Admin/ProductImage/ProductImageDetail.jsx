import { Image, Tag } from 'antd';
import Detail from '../../../layouts/Admin/components/Detail';
import { useGetVariant } from '../../../hooks/api';
import { useEffect, useState } from 'react';
import { useGetProductImage } from '../../../hooks/api/useProductImageApi';

function transformData(image) {
    return [
        {
            key: '1',
            property: 'ID',
            value: image.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(image.createdAt).toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: new Date(image.updatedAt).toLocaleString(),
        },
        {
            key: '4',
            property: 'Hình ảnh',
            value: <Image width={80} src={image.image} />,
        },
        {
            key: '5',
            property: 'Kích thước',
            value: image.size,
        },
        {
            key: '6',
            property: 'Loại hình ảnh',
            value: image.contentType,
        },
    ];
}

function ProductImageDetail({ isDetailOpen, setIsDetailOpen }) {
    const { isLoading, data } = useGetProductImage(isDetailOpen.id);
    const [image, setImage] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setImage(transformData(data?.data));
    }, [isLoading, data]);

    return <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={image} />;
}

export default ProductImageDetail;
