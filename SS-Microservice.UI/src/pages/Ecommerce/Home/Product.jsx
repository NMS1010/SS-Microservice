import CardProduct from '../../../layouts/Ecommerce/components/CardProduct';
import { Button } from 'antd';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import { useGetListProduct } from '../../../hooks/api';
import { useEffect, useState } from 'react';
import WebLoading from '../../../layouts/Ecommerce/components/WebLoading';

function Product() {
    const { isLoading, data } = useGetListProduct({
        status: true
    });
    const [products, setProducts] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setProducts(data?.data?.items);
    }, [isLoading, data]);

    if (isLoading) return <WebLoading />;

    return (
        <div className="products">
            <div className="grid grid-cols-5 max-xl:grid-cols-4 max-lg:grid-cols-3 max-md:grid-cols-2 gap-[2rem]">
                {products.length > 0 &&
                    products.map((item, index) => <CardProduct key={index} product={item} />)}
            </div>
            {products.length > 50 && (
                <div className="text-center my-[2.6rem]">
                    <Button className="primary-color" shape="round">
                        Xem thêm <FontAwesomeIcon className="text-[1.2rem]" icon={faChevronRight} />
                    </Button>
                </div>
            )}
        </div>
    );
}

export default Product;
