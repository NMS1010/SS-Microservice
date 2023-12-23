import { Tag } from 'antd';
import Detail from '../../../layouts/Admin/components/Detail';
import { useGetProductCategory } from '../../../hooks/api';
import { useEffect, useState } from 'react';

function transformData(productCategory) {
    return [
        {
            key: '1',
            property: 'ID',
            value: productCategory.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(productCategory.createdAt).toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: new Date(productCategory.updatedAt).toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên danh mục',
            value: productCategory.name,
        },
        {
            key: '5',
            property: 'Slug',
            value: productCategory.slug,
        },
        {
            key: '6',
            property: 'Hình ảnh',
            value: <img className="w-20 h-20 rounded-xl" src={productCategory.image} />,
        },
        {
            key: '7',
            property: 'Tên danh mục cha',
            value: (
                <>
                    {productCategory.parentName ? (
                        <Tag className="w-fit uppercase" color="magenta">
                            {productCategory.parentName}
                        </Tag>
                    ) : (
                        <span className="italic">Không có</span>
                    )}
                </>
            ),
        },
        {
            key: '8',
            property: 'Trạng thái',
            value: (
                <Tag className="w-fit uppercase" color={productCategory.status ? 'green' : 'red'}>
                    {productCategory.status ? 'Đã kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
        },
    ];
}

function ProductCategoryDetail({ isDetailOpen, setIsDetailOpen }) {
    const { isLoading, data } = useGetProductCategory(isDetailOpen.id);
    const [productCategory, setProductCategory] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setProductCategory(transformData(data?.data));
    }, [isLoading, data]);

    return (
        <Detail
            isDetailOpen={isDetailOpen}
            setIsDetailOpen={setIsDetailOpen}
            rawData={productCategory}
        />
    );
}

export default ProductCategoryDetail;
