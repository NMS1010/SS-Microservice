import { useEffect, useState } from 'react';
import { Button, Table, notification } from 'antd';
import Item from './Item';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { useGetCart, useRemoveListVariantFromCart } from '../../../hooks/api/useCartApi';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';

const columns = [
    {
        title: 'Tất cả',
        dataIndex: 'item',
        render: (text) => <a>{text}</a>,
    },
];

function transformData(data) {
    return data?.map((val) => {
        return {
            key: val.id,
            item: <Item cartItem={val} />,
        };
    });
}

function Info({ cartItems, chosenItem, setChosenItem }) {
    const [isConfirmPrompt, setIsConfirmPrompt] = useState({
        id: 0,
        isOpen: false,
    });

    const [items, setItems] = useState([]);

    useEffect(() => {
        setItems(
            cartItems?.map((val) => {
                return {
                    key: val.id,
                    item: <Item cartItem={val} />,
                };
            }),
        );
    }, [cartItems]);

    const rowSelection = {
        defaultSelectedRowKeys: localStorage.getItem('chosenCartItems')
            ? [...JSON.parse(localStorage.getItem('chosenCartItems'))]
            : [],
        onChange: (selectedRowKeys, selectedRows) => {
            let list = selectedRows?.map((val) => val?.item?.props?.cartItem?.id);
            setChosenItem(list);
            localStorage.setItem('chosenCartItems', JSON.stringify(list));
        },
        getCheckboxProps: (record) => ({
            item: record.name,
        }),
    };

    const mutateDeleteList = useRemoveListVariantFromCart({
        success: () => {
            notification.success({
                message: 'Xoá thành công',
                description: 'Đã xoá các sản phẩm được chọn khỏi giỏ hàng',
            });
            setIsConfirmPrompt({ ...isConfirmPrompt, isOpen: false });
            localStorage.removeItem('chosenCartItems');
            setChosenItem([]);
        },
        error: (err) => {
            notification.error({
                message: 'Xoá thất bại',
                description: 'Có lỗi xảy ra khi xoá các sản phẩm được chọn khỏi giỏ hàng',
            });
        },
        obj: {
            params: {
                all: true,
            },
        },
    });

    const onRemoveList = async (id) => {
        await mutateDeleteList.mutateAsync(chosenItem);
    };
    useEffect(() => {
        setChosenItem(
            localStorage.getItem('chosenCartItems')
                ? JSON.parse(localStorage.getItem('chosenCartItems'))
                : [],
        );
    }, []);
    return (
        <div className="w-[75rem] max-md:w-[100%]">
            <Table
                className="shadow-[0_1px_2px_0_rgba(0,0,0,0.13)] rounded-[5px]"
                locale={{ emptyText: 'Không có sản phẩm trong giỏ hàng' }}
                rowSelection={{
                    type: 'checkbox',
                    ...rowSelection,
                }}
                columns={columns}
                dataSource={items}
                pagination={false}
            ></Table>
            <div className="flex justify-center mt-10">
                <Button
                    onClick={() => setIsConfirmPrompt({ ...isConfirmPrompt, isOpen: true })}
                    icon={<FontAwesomeIcon icon={faTrash} />}
                    disabled={chosenItem?.length === 0}
                    className={` ${
                        chosenItem?.length === 0
                            ? 'bg-gray-400 w-1/2 h-[4rem] text-[1.6rem] text-white border-none hover:border-none'
                            : 'w-1/2 h-[4rem] text-[1.6rem] bg-red-500 text-white border-none hover:border-none'
                    }`}
                >
                    Xoá đã chọn
                </Button>
            </div>

            <ConfirmPrompt
                handleConfirm={onRemoveList}
                content="Bạn có muốn xoá các sản phẩm này khỏi giỏ hàng?"
                isDisableOpen={isConfirmPrompt}
                setIsDisableOpen={setIsConfirmPrompt}
            />
        </div>
    );
}

export default Info;
