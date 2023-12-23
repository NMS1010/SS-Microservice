import './variant.scss';
import Data from './Data';
import VariantHead from './VariantHead';
import { useState } from 'react';

function VariantPage() {
    const [variantIds, setVariantIds] = useState([]);
    const [productId, setProductId] = useState();

    return (
        <div className="account-container">
            <VariantHead variantIds={variantIds} productId={productId} />
            <Data setVariantIds={setVariantIds} setProductId={setProductId} />
        </div>
    );
}

export default VariantPage;
