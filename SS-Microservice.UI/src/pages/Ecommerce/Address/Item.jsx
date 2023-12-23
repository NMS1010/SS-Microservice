import { Button, notification } from 'antd';
import { useDeleteAddress, useSetDefaultAddress } from '../../../hooks/api';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';
import { useState } from 'react';

function Item({ address, addressApi, setIsFormOpen, isLastItem = false, isDefault = false }) {
    const [isConfirmDelete, setIsConfirmDelete] = useState({
        id: address?.id,
        isOpen: false,
    });
    const mutateSetDefault = useSetDefaultAddress({
        success: () => {
            notification.success({
                message: 'Thành công',
                description: 'Đã thiết lập địa chỉ mặc định',
            });
            addressApi.refetch();
        },
        error: () => {
            notification.error({
                message: 'Thất bại',
                description: 'Không thể thiết lập địa chỉ mặc định',
            });
        },
    });

    const mutateDelete = useDeleteAddress({
        success: () => {
            notification.success({
                message: 'Thành công',
                description: 'Đã xoá địa chỉ',
            });
            addressApi.refetch();
        },
        error: () => {
            notification.error({
                message: 'Thất bại',
                description: 'Không thể xoá địa chỉ',
            });
        },
    });

    const onSetDefault = async () => {
        await mutateSetDefault.mutateAsync({
            id: address?.id,
        });
    };

    const onDelete = async (id) => {
        await mutateDelete.mutateAsync(id);
    };

    return (
        <div
            className={`flex justify-between ${
                isLastItem || 'border-b-[0.1rem] border-b-gray-300'
            } py-[2.5rem] mx-[2.1rem] items-center`}
        >
            <div className="text-[1.6rem]">
                <div className="flex items-center text-[1.6rem]">
                    <span className="font-medium">{address?.receiver}</span>
                    <span className="mx-[1.5rem] font-normal block h-[2.5rem] border-l-[0.01rem] border-gray-600"></span>
                    <span>{address?.phone}</span>
                </div>
                <div className="py-[1.3rem] text-[1.4rem] font-normal">
                    <p>{address?.street}</p>
                    <p>{`${address?.ward?.name}, ${address?.district?.name}, ${address?.province?.name}`}</p>
                </div>
                {isDefault && (
                    <div>
                        <span className="address-default border-[0.2rem] px-[1rem] py-[0.3rem]">
                            Mặc định
                        </span>
                    </div>
                )}
            </div>
            <div className="text-right">
                <div className="flex justify-end gap-[1rem]">
                    <p
                        onClick={() => setIsFormOpen({ isOpen: true, id: address?.id })}
                        className="m-0 text-blue-800 text-[1.6rem] font-normal cursor-pointer"
                    >
                        Cập nhật
                    </p>
                    {!isDefault && (
                        <p
                            onClick={() => setIsConfirmDelete({ ...isConfirmDelete, isOpen: true })}
                            className="m-0 text-blue-800 text-[1.6rem] font-normal cursor-pointer"
                        >
                            Xoá
                        </p>
                    )}
                </div>
                <Button
                    disabled={isDefault}
                    onClick={onSetDefault}
                    className={`btn-default ${
                        isDefault ? 'bg-gray-200 border-gray-200' : 'bg-transparent'
                    } text-black text-[1.6rem] w-[16rem] h-[4rem] mt-[1rem]`}
                >
                    Thiết lập mặc định
                </Button>
            </div>
            <ConfirmPrompt
                content={'Xác nhận xoá địa chỉ này?'}
                handleConfirm={onDelete}
                isDisableOpen={isConfirmDelete}
                setIsDisableOpen={setIsConfirmDelete}
            />
        </div>
    );
}

export default Item;
