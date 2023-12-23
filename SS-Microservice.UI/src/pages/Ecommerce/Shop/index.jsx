import { useParams, useSearchParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import './shop.scss';
import Product from './Product';
import Filter from './Filter';
import BreadCrumb from '../../../layouts/Ecommerce/components/Breadcrumb';
import { useGetProductCategoryBySlug } from '../../../hooks/api';
import { MAX_PRICE, MIN_PRICE } from '../../../utils/constants';

function ShopPage() {
    const [searchParams, setSearchParams] = useSearchParams();
    const search = searchParams.get('search');

    let { productCategory } = useParams();

    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 50,
        status: true,
        categorySlug: productCategory,
        minPrice: MIN_PRICE,
        maxPrice: MAX_PRICE,
        brandIds: [],
        categoryIds: [],
        rating: 1,
        isSortAscending: true,
        columnName: 'name',
        search: search || '',
    });
    const { isLoading, data } = useGetProductCategoryBySlug(productCategory);
    const [routes, setRoutes] = useState([
        {
            title: data?.data?.name || 'Tìm kiếm',
        },
    ]);

    useEffect(() => {
        if (!data || isLoading) return;
        setRoutes([
            {
                title: data?.data?.name,
            },
        ]);
    }, [isLoading, data]);

    useEffect(() => {
        const { status, total, categorySlug, ...rest } = params;

        setSearchParams(rest);
    }, [params]);

    useEffect(() => {
        if (!search) return;

        setParams({
            ...params,
            search: search,
        });
    }, [search]);

    useEffect(() => {
        let obj = {
            ...params,
            pageIndex: searchParams.get('pageIndex') || params.pageIndex,
            pageSize: searchParams.get('pageSize') || params.pageSize,
            minPrice: searchParams.get('minPrice') || params.minPrice,
            maxPrice: searchParams.get('maxPrice') || params.maxPrice,
            rating: searchParams.get('rating') || params?.rating,
            columnName: searchParams.get('columnName') || params?.columnName,
            isSortAscending: searchParams.get('isSortAscending') || params?.isSortAscending,
        };
        if (search) {
            obj = { search: search, ...obj };
        }
        setParams(obj);
    }, []);

    return (
        <>
            <BreadCrumb routes={routes} />
            <div className="shop-container p-[2rem]">
                <div className="max-w-[1200px] mx-auto grid grid-cols-12 bg-white">
                    <div className="col-span-3 max-lg:hidden">
                        <Filter params={params} setParams={setParams} categoryId={data?.data?.id} />
                    </div>
                    <div className="col-span-9 max-lg:col-span-12">
                        <Product
                            categoryName={data?.data?.name}
                            params={params}
                            setParams={setParams}
                            setRoutes={setRoutes}
                        />
                    </div>
                </div>

                <input className="hidden" type="checkbox" id="open" />
                <label
                    for="open"
                    className="slidebar hidden h-screen w-screen fixed top-0 left-0 bg-black opacity-[0.2] z-10"
                />
                <div className="slidebar bg-white hidden h-screen w-[300px] fixed top-0 left-0 z-10">
                    <div className="z-10">
                        <Filter params={params} setParams={setParams} categoryId={data?.data?.id} />
                    </div>
                </div>
            </div>
        </>
    );
}

export default ShopPage;
