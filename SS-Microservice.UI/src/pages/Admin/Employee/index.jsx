import { useState } from 'react';

import './employee.scss';
import Data from './Data';
import EmployeeHead from './EmployeeHead';

function EmployeePage() {
    const [employeeIds, setEmployeeIds] = useState([]);

    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });
    return (
        <div className="employee-container">
            <EmployeeHead employeeIds={employeeIds} params={params} />
            <Data params={params} setParams={setParams} setEmployeeIds={setEmployeeIds} />
        </div>
    );
}
export default EmployeePage;
