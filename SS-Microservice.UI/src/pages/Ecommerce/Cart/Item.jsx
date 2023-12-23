import { Input, InputNumber, notification } from 'antd';
import { useEffect, useState } from 'react';
import { numberFormatter } from '../../../utils/formatter';
import { useRemoveVariantFromCart, useUpdateCartQuantity } from '../../../hooks/api';
import { NavLink } from 'react-router-dom';
import config from '../../../config';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';

function Item({ cartItem }) {
    const [count, setCount] = useState(cartItem?.quantity);
    const [isRemoveOpen, setIsRemoveOpen] = useState({
        id: cartItem?.id,
        isOpen: false,
    });
    const mutateUpdate = useUpdateCartQuantity({
        success: (data) => {
            // console.log(data);
        },
        error: (error) => {
            notification.error({
                message: 'Không thể cập nhật số lượng sản phẩm, số lượng trong kho không đủ!'
            });
            // setCount(cartItem?.quantity)
            // console.log(error);
        },
        obj: {
            params: {
                all: true
            },
        },
    });
    const mutateDelete = useRemoveVariantFromCart({
        success: (data) => {
            let chosenItems = JSON.parse(localStorage.getItem('chosenCartItems'));
            if (chosenItems?.includes(cartItem?.id)) {
                chosenItems = chosenItems?.filter((val) => val !== cartItem?.id);
                localStorage.setItem('chosenCartItems', JSON.stringify(chosenItems));
            }
            setIsRemoveOpen({ ...isRemoveOpen, isOpen: false });
            // console.log(data);
        },
        error: (error) => {
            // console.log(error);
            setIsRemoveOpen({ ...isRemoveOpen, isOpen: false });
        },
        obj: {
            params: {
                all: true
            },
        },
    });
    useEffect(() => {
        if (count < 1) return;
        mutateUpdate.mutate({
            id: cartItem?.id,
            body: {
                quantity: count,
            },
        });
    }, [count]);
    useEffect(() => {
        setCount(cartItem?.quantity);
    }, [cartItem]);

    const onDelete = async (id) => {
        await mutateDelete.mutateAsync(cartItem?.id);
    };
    const onIncrease = () => {
        setCount((value) => value + 1);
    };
    const onDescrease = () => {
        if (count <= 1) setIsRemoveOpen({ ...isRemoveOpen, isOpen: true });
        else setCount(count - 1);
    };
    const onChange = (e) => {
        if(Number.parseInt(e.target.value) < 1) {
            setCount(1);
            return;
        }
        !isNaN(e.target.value) && setCount(Number.parseInt(e.target.value));
    };

    return (
        <div
            className={`flex items-center justify-between gap-[2.2rem] hover:cursor-default rounded-[0.5rem]`}
        >
            <div className="flex gap-[2.2rem] max-[440px]:flex-col items-center">
                <img className="w-[7.9rem] h-[7.9rem] " src={cartItem?.productImage} />
                <div className="">
                    <NavLink
                        to={`/${config.routes.web.product_detail}/${cartItem?.productSlug}`}
                        className="text-black text-[1.5rem] font-medium"
                    >
                        {cartItem?.productName}
                    </NavLink>
                    <p className="text-[1.2rem] text-black mb-[1rem] mt-[.6rem]">
                        {cartItem?.variantName}: {cartItem?.variantQuantity} {cartItem?.productUnit}
                    </p>
                    <div>
                        {cartItem?.totalPromotionalPrice ? (
                            <div className="flex items-center gap-[1rem]">
                                <p className="text-black text-opacity-70 line-through font-medium text-[1.4rem]">
                                    {numberFormatter(cartItem?.totalPrice)}
                                </p>
                                <p className="text-rose-600 text-opacity-70 font-medium text-[1.4rem]">
                                    {numberFormatter(cartItem?.totalPromotionalPrice)}
                                </p>
                            </div>
                        ) : (
                            <p className="text-rose-600 text-opacity-70 font-medium text-[1.4rem]">
                                {numberFormatter(cartItem?.totalPrice)}
                            </p>
                        )}
                    </div>
                </div>
            </div>
            <div className="flex items-center gap-[5rem]">
                <div className="text-right">
                    <p className="text-rose-600 text-[1.4rem] font-bold mb-0">
                        {numberFormatter(
                            (cartItem?.totalPromotionalPrice || cartItem?.totalPrice) * count,
                        )}
                    </p>
                    <div className="flex items-center gap-[0.7rem] mt-[1.2rem]">
                        <button
                            onClick={onDescrease}
                            className="h-[2.5rem] w-[2.5rem] rounded-[4px] bg-white text-black border-none shadow-[0px_0px_2px_0px_#0000004D]"
                        >
                            -
                        </button>
                        <Input
                            onChange={onChange}
                            className="w-[5rem] h-[2.5rem] rounded-[4px] focus:outline-none focus-within:border-none text-center text-black border-none text-[1.5rem] font-medium shadow-[0px_0px_2px_0px_#0000004D]"
                            value={count}
                            min={1}
                        />
                        <button
                            onClick={onIncrease}
                            className="h-[2.5rem] w-[2.5rem] rounded-[4px] bg-white cursor-pointer text-black border-none shadow-[0px_0px_2px_0px_#0000004D]"
                        >
                            +
                        </button>
                    </div>
                </div>
                <div>
                    <span
                        className="text-black cursor-pointer hover:text-red-300"
                        onClick={() => setIsRemoveOpen({ ...isRemoveOpen, isOpen: true })}
                    >
                        Xoá
                    </span>
                </div>
            </div>
            <ConfirmPrompt
                handleConfirm={onDelete}
                content="Bạn có muốn xoá sản phẩm khỏi giỏ hàng?"
                isDisableOpen={isRemoveOpen}
                setIsDisableOpen={setIsRemoveOpen}
            />
        </div>
    );
}

export default Item;
