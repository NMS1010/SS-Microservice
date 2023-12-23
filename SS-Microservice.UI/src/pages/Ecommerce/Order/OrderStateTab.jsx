import React, { useEffect } from 'react';
import { Tabs } from 'antd';
import { getAllOrderStatusSelect } from '../../../utils/constants';
import { useNavigate, useSearchParams } from 'react-router-dom';

const items = [
    {
        label: 'Tất cả',
        key: null,
    },
    ...getAllOrderStatusSelect()?.map((item) => ({
        label: item?.label,
        key: item?.value,
    })),
];

function OrderStateTab({ setChosenStatus }) {
    const [searchParams, setSearchParams] = useSearchParams();
    const navigate = useNavigate();
    const onChange = (key) => {
        navigate('?type=' + (key?.toLowerCase() || 'all'));
        setChosenStatus(key);
    };
    useEffect(() => {
        const type = searchParams.get('type')?.toUpperCase();
        setChosenStatus(type);
    }, []);
    return (
        <div className="bg-white shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
            <Tabs
                defaultActiveKey={searchParams.get('type')?.toUpperCase()}
                items={items}
                onChange={onChange}
            />
        </div>
    );
}

export default OrderStateTab;
