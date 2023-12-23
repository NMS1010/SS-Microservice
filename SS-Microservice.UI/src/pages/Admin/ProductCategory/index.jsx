import ProductCategoryHead from './ProductCategoryHead';
import Data from './Data';
import './productCategory.scss';
import { useState } from 'react';

function ProductCategoryPage() {
    const [productCategoryIds, setProductCategoryIds] = useState([]);
    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });

    return (
        <div className="category-container">
            <ProductCategoryHead productCategoryIds={productCategoryIds} params={params} />
            <Data
                setProductCategoryIds={setProductCategoryIds}
                params={params}
                setParams={setParams}
            />
        </div>
    );
}

export default ProductCategoryPage;
