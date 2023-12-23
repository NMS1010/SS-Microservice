import { useState } from 'react';
import './image.scss';
import Data from './Data';
import ProductImageHead from './ProductImageHead';

function ProductImagePage() {
    const [productImageIds, setProductImageIds] = useState([]);
    const [productId, setProductId] = useState();

    return (
        <div className="account-container">
            <ProductImageHead productImageIds={productImageIds} productId={productId} />
            <Data setProductImageIds={setProductImageIds} setProductId={setProductId} />
        </div>
    );
}

export default ProductImagePage;
