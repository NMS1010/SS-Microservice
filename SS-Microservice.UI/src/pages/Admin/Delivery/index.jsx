import { useState } from 'react';

import './delivery.scss';
import DeliveryHead from './DeliveryHead';
import Data from './Data';

function DeliveryPage() {
    const [deliveryIds, setDeliveryIds] = useState([]);

    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });

    return (
        <div className="delivery-container">
            <DeliveryHead deliveryIds={deliveryIds} params={params} />
            <Data params={params} setParams={setParams} setDeliveryIds={setDeliveryIds} />
        </div>
    );
}

export default DeliveryPage;
