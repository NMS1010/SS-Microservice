import { useState } from 'react';

import Data from './Data';
import RoleHead from './RoleHead';

function RolePage() {
    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });
    return (
        <div className="Role-container">
            <RoleHead />
            <Data params={params} setParams={setParams}/>
        </div>
    );
}

export default RolePage;
