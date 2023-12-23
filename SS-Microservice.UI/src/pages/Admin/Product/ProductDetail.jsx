import { Tag } from 'antd';
import Detail from '../../../layouts/Admin/components/Detail';
import { useGetProduct } from '../../../hooks/api';
import { useEffect, useState } from 'react';

function transformData(product) {
    return [
        {
            key: '1',
            property: 'ID',
            value: product.id,
        },
        {
            key: '2',
            property: 'Ngày tạo',
            value: new Date(product.createdAt).toLocaleString(),
        },
        {
            key: '3',
            property: 'Ngày cập nhật',
            value: new Date(product.updatedAt).toLocaleString(),
        },
        {
            key: '4',
            property: 'Tên sản phẩm',
            value: product.name,
        },
        {
            key: '5',
            property: 'Code',
            value: product.code,
        },
        {
            key: '6',
            property: 'Slug',
            value: product.slug,
        },
        {
            key: '7',
            property: 'Mô tả ngắn gọn sản phẩm',
            value: product.shortDescription,
        },
        {
            key: '8',
            property: 'Mô tả sản phẩm',
            value: <div className='max-w-lg'>{product.description}</div>,
        },
        {
            key: '9',
            property: 'Hình ảnh',
            value: (
                <div className="flex justify-center items-center">
                    {product.images.map((image) => (
                        <img className="w-20 h-20 rounded-xl mx-[0.5rem]" src={image.image} />
                    ))}
                </div>
            ),
        },
        {
            key: '10',
            property: 'Tên danh mục',
            value: product.category.name,
        },
        {
            key: '11',
            property: 'Tên thương hiệu',
            value: product.brand.name,
        },
        {
            key: '12',
            property: 'Trạng thái',
            value: (
                <Tag
                    className="w-fit uppercase"
                    color={
                        product.status === 'ACTIVE'
                            ? 'green'
                            : product.status === 'INACTIVE'
                            ? 'red'
                            : 'yellow'
                    }
                >
                    {product.status === 'ACTIVE'
                        ? 'Kích hoạt'
                        : product.status === 'INACTIVE'
                        ? 'Vô hiệu hóa'
                        : 'Hết hàng'}
                </Tag>
            ),
        },
        {
            key: '13',
            property: 'Dạng bán ra',
            value: (
                <div className="flex flex-col gap-[1rem]">
                    {product.variants.map((variant) => (
                        <Tag className="w-fit uppercase" color="magenta">
                            {variant.name} - {variant.quantity} {product.unit.name}
                        </Tag>
                    ))}
                </div>
            ),
        },
    ];
}

function ProductDetail({ isDetailOpen, setIsDetailOpen }) {
    const { isLoading, data } = useGetProduct(isDetailOpen.id);
    const [product, setProduct] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setProduct(transformData(data?.data));
    }, [isLoading, data]);

    return (
        <Detail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} rawData={product} />
    );
}

export default ProductDetail;
