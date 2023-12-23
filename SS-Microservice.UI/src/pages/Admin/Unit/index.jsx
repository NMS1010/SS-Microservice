import UnitHead from './UnitHead';
import Data from './Data';
import './unit.scss';
import { useState } from 'react';

function UnitPage() {
    const [unitIds, setUnitIds] = useState([]);
    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });

    return (
        <div className="unit-container">
            <UnitHead unitIds={unitIds} params={params} />
            <Data params={params} setParams={setParams} setUnitIds={setUnitIds} />
        </div>
    );
}

export default UnitPage;
