import { useState } from 'react';
import Data from './Data';
import InventoryHead from './InventoryHead';
import './inventory.scss';

function InventoryPage() {
    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 10,
    });

    return (
        <div className="inventory-container">
            <InventoryHead />
            <Data params={params} setParams={setParams} />
        </div>
    );
}

export default InventoryPage;
