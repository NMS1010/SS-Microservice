import './productDetail.scss';
import Information from './Information';
import Description from './Description';
import Review from './Review';
import { useLocation, useParams } from 'react-router-dom';
import { useGetProductBySlug } from '../../../hooks/api';
import { Spin } from 'antd';
import config from '../../../config';
import { useEffect, useState } from 'react';
import BreadCrumb from '../../../layouts/Ecommerce/components/Breadcrumb';
import WebLoading from '../../../layouts/Ecommerce/components/WebLoading';

function ProductDetailPage() {
    let { slug } = useParams();

    const { data, isLoading } = useGetProductBySlug(slug);
    const [routes, setRoutes] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setRoutes([
            {
                title: data?.data?.category.name,
                href: config.routes.web.home + '/' + data?.data?.category.slug,
            },
            {
                title: data?.data?.name,
            },
        ]);
    }, [isLoading, data]);

    if (isLoading) return <WebLoading />;

    return (
        <>
            <BreadCrumb routes={routes} />
            <div className="product-detail-container p-[2rem]">
                <div className="max-w-[1200px] h-full mx-auto p-[1.2rem] px-[3rem] rounded-[2px] shadow-[0_1px_2px_0_rgba(0,0,0,0.1)] bg-white">
                    {data?.data ? (
                        <>
                            <Information product={data?.data} />
                            <Description product={data?.data} />
                            <Review product={data?.data} />
                        </>
                    ) : (
                        <div className="text-center text-[2rem]">Không tìm thấy sản phẩm</div>
                    )}
                </div>
            </div>
        </>
    );
}

export default ProductDetailPage;
