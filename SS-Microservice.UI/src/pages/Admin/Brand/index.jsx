import { useState } from 'react';
import BrandHead from './BrandHead';
import Data from './Data';
import './brand.scss';

function BrandPage() {
    const [brandIds, setBrandIds] = useState([]);
    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });

    return (
        <div className="brand-container">
            <BrandHead brandIds={brandIds} params={params} />
            <Data params={params} setParams={setParams} setBrandIds={setBrandIds} />
        </div>
    );
}

export default BrandPage;
