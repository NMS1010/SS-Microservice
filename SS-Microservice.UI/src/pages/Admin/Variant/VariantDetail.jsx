import { Tag } from 'antd';
import Detail from '../../../layouts/Admin/components/Detail';
import { useGetVariant } from '../../../hooks/api';
import { useEffect, useState } from 'react';

function transformData(variant) {
    return [
        {
            key: '1',
            property: 'ID',
            value: variant.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(variant.createdAt).toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: new Date(variant.updatedAt).toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên',
            value: variant.name,
        },
        {
            key: '5',
            property: 'Sku',
            value: variant.sku,
        },
        {
            key: '6',
            property: 'Giá 1 sản phẩm',
            value: variant.itemPrice,
        },
        {
            key: '7',
            property: 'Số lượng',
            value: variant.quantity,
        },
        {
            key: '8',
            property: 'Giá khuyến mãi cho 1 sản phẩm',
            value: variant.promotionalItemPrice,
        },
        {
            key: '9',
            property: 'Tổng giá',
            value: variant.totalPrice,
        },
        {
            key: '10',
            property: 'Trạng thái',
            value: (
                <Tag
                    className="w-fit uppercase"
                    color={variant.status === 'ACTIVE' ? 'green' : 'red'}
                >
                    {variant.status === 'ACTIVE' ? 'Kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
        },
    ];
}

function VariantDetail({ isDetailOpen, setIsDetailOpen }) {
    const { isLoading, data } = useGetVariant(isDetailOpen.id);
    const [variant, setVariant] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setVariant(transformData(data?.data));
    }, [isLoading, data]);

    return (
        <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={variant} />
    );
}

export default VariantDetail;
