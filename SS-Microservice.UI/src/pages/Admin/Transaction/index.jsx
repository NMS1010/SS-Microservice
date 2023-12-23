import { useState } from 'react';

import './transaction.scss';
import Head from '../../../layouts/Admin/components/Head';
import Data from './Data';

function TransactionPage() {
    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });
    return (
        <div className="transaction-container">
            <Head title="Quản lý giao dịch" isDisableAll={false} isAdd={false} />
            <Data params={params} setParams={setParams}/>
        </div>
    );
}

export default TransactionPage;
