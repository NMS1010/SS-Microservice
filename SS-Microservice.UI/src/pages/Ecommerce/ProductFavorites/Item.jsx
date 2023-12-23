import { faCartPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Rate, Spin, notification } from 'antd';
import { NavLink } from 'react-router-dom';
import config from '../../../config';
import { useUnFollowProduct } from '../../../hooks/api/useFollowProductApi';
import { useState } from 'react';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';

function Item({ dataApi, item, isLastItem = false }) {
    const [isConfirmPrompt, setIsConfirmPrompt] = useState({
        id: item?.id,
        isOpen: false,
    });
    const [processing, setProcessing] = useState(false);
    let mutateUnlikeProduct = useUnFollowProduct({
        success: () => {
            notification.success({
                message: 'Thành công',
                description: 'Xóa sản phẩm khỏi danh sách yêu thích thành công',
            });
            dataApi.refetch();
            setIsConfirmPrompt({ ...isConfirmPrompt, isOpen: false });
        },
        error: () => {
            notification.error({
                message: 'Thất bại',
                description: 'Xóa sản phẩm khỏi danh sách yêu thích thất bại',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const onUnfollow = async (id) => {
        await mutateUnlikeProduct.mutateAsync({
            productId: item?.id,
        });
    };

    return (
        <div
            className={`flex justify-between mt-5 px-5 rounded-lg hover:bg-gray-100 transition-all ${
                isLastItem || 'border-b-[0.1rem] border-b-gray-300'
            } pt-[3.7rem] pb-[2.2rem] mx-[2.1rem] items-center`}
        >
            <div className="text-[1.6rem] flex items-center gap-[2.5rem]">
                <div className="flex justify-center items-center gap-[2.2rem]">
                    <img
                        className="w-[7.9rem] h-[7.9rem] border border-solid"
                        src={item?.images?.find((x) => x.isDefault)?.image}
                    />
                    <div className="text-black font-normal">
                        <div className="flex flex-col">
                            <NavLink
                                to={`${config.routes.web.product_detail}/${item?.slug}`}
                                className="text-[1.8rem] hover:text-red-400 transition-colors"
                            >
                                {item?.name}
                            </NavLink>
                            <Rate
                                className="my-2 text-[1.5rem]"
                                disabled
                                value={
                                    item?.rating - 0.5 < Math.floor(item?.rating)
                                        ? Math.ceil(item?.rating * 2) / 2
                                        : Math.floor(item?.rating * 2) / 2
                                }
                                allowHalf
                            />
                        </div>
                        <div className="flex gap-[1rem]">
                            <p className="text-[1.2rem]">Mã: {item?.code}</p>
                            <p className="text-[1.2rem]">Phân loại: {item?.category?.name}</p>
                        </div>
                    </div>
                </div>
            </div>
            <div className="text-right">
                <Button
                    onClick={() => setIsConfirmPrompt({ ...isConfirmPrompt, isOpen: true })}
                    loading={processing}
                    icon={<FontAwesomeIcon icon={faTrash} />}
                    className="text-red-600 text-[1.6rem] h-[4rem] border-red-600"
                >
                    <span className="max-md:hidden">Xoá</span>
                </Button>
            </div>
            <ConfirmPrompt
                handleConfirm={onUnfollow}
                content="Bạn có muốn xoá các sản phẩm này khỏi danh sách yêu thích?"
                isDisableOpen={isConfirmPrompt}
                setIsDisableOpen={setIsConfirmPrompt}
            />
        </div>
    );
}

export default Item;
