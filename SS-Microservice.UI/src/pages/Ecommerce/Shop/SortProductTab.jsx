import { Tabs } from 'antd';
import { useSearchParams } from 'react-router-dom';

const items = [
    {
        key: 'name',
        label: 'Tên A -> Z',
    },
    {
        key: 'name_desc',
        label: 'Tên Z -> A',
    },
    {
        key: 'price',
        label: 'Giá tăng dần',
    },
    {
        key: 'price_desc',
        label: 'Giá giảm dần',
    },
    {
        key: 'sold_desc',
        label: 'Bán chạy',
    },
];
function SortProductTab({ params, setParams }) {
    const [searchParams, setSearchParams] = useSearchParams();
    const onChange = (key) => {
        let temp = key.split('_');
        setParams({
            ...params,
            isSortAscending: temp.length === 1,
            columnName: temp[0],
        });
    }; 

    return (
        <div className="max-lg:hidden">
            <Tabs
                defaultActiveKey={`${searchParams.get('columnName')}${
                    searchParams.get('isSortAscending') === 'true' ? '' : '_desc'
                }`}
                items={items}
                onChange={onChange}
            />
        </div>
    );
}

export default SortProductTab;
