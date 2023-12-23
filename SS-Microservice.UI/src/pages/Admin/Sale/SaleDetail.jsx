import { Tag } from 'antd';
import Detail from '../../../layouts/Admin/components/Detail';
import { useGetSale } from '../../../hooks/api';
import { useEffect, useState } from 'react';

function transformData(sale) {
    return [
        {
            key: '1',
            property: 'ID',
            value: sale.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(sale.createdAt).toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: new Date(sale.updatedAt).toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên khuyến mãi',
            value: sale.name,
        },
        {
            key: '5',
            property: 'Ngày bắt đầu khuyến mãi',
            value: new Date(sale.startDate).toLocaleString(),
        },
        {
            key: '6',
            property: 'Ngày kết thúc khuyến mãi',
            value: new Date(sale.endDate).toLocaleString(),
        },
        {
            key: '7',
            property: 'Phần trăm giảm giá',
            value: sale.promotionalPercent + '%',
        },
        {
            key: '8',
            property: 'Trạng thái khuyến mãi',
            value: (
                <Tag
                    className="w-fit uppercase"
                    color={`${
                        sale.status === 'ACTIVE'
                            ? 'green'
                            : sale.status === 'INACTIVE'
                            ? 'red'
                            : 'yellow'
                    }`}
                >
                    {sale.status === 'ACTIVE'
                        ? 'Đang áp dụng'
                        : sale.status === 'INACTIVE'
                        ? 'Vô hiệu lực'
                        : 'Hết hạn'}
                </Tag>
            ),
        },
        {
            key: '9',
            property: 'Hình ảnh',
            value: <img className="w-20 h-20 rounded-xl" src={sale.image} />,
        },
        {
            key: '10',
            property: 'Slug',
            value: sale.slug,
        },
        {
            key: '11',
            property: 'Áp dụng',
            value: sale.all ? (
                <Tag className="w-fit uppercase" color="yellow">
                    Áp dụng cho tất cả sản phẩm
                </Tag>
            ) : (
                sale.productCategories.map((item) => (
                    <Tag className="w-fit uppercase" color="green">
                        {item.name}
                    </Tag>
                ))
            ),
        },
    ];
}

function SaleDetail({ isDetailOpen, setIsDetailOpen }) {
    const { isLoading, data } = useGetSale(isDetailOpen.id);
    const [sale, setSale] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setSale(transformData(data?.data));
    }, [isLoading, data]);

    return <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={sale} />;
}

export default SaleDetail;
