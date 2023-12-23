import { DownOutlined } from '@ant-design/icons';
import { Button, Dropdown, Space } from 'antd';

const items = [
    {
        label: 'Tên A -> Z',
        key: '1',
    },
    {
        label: 'Tên Z -> A',
        key: '2',
    },
    {
        label: 'Giá tăng dần',
        key: '3',
    },
    {
        label: 'Giá giảm dần',
        key: '4',
    },
    {
        label: 'Hàng mới',
        key: '5',
    },
];

const menuProps = {
    items,
};

function SortProductDropdown() {
    return (
        <Dropdown className="lg:hidden" menu={menuProps}>
            <Button>
                <Space>
                    {`Tên A -> Z`}
                    <DownOutlined />
                </Space>
            </Button>
        </Dropdown>
    );
}

export default SortProductDropdown;
