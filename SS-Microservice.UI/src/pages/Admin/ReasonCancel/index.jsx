import { useState } from 'react';

import './reasonCancel.scss';
import Data from './Data';
import ReasonCancelHead from './ReasonCancelHead';

function ReasonCancelPage() {
    const [reasonCancellationIds, setReasonCancellationIds] = useState([]);

    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });
    return (
        <div className="cancel-reason-container">
            <ReasonCancelHead params={params} reasonCancellationIds={reasonCancellationIds}/>
            <Data params={params} setParams={setParams} setReasonCancellationIds={setReasonCancellationIds}/>
        </div>
    );
}

export default ReasonCancelPage;
