import { useEffect } from 'react';
import { useGetListCartItemById } from '../../../hooks/api';
import ProductItem from './ProductItem';

function OrderProducts({ setTotalCartPrice, setChosenCartItems }) {
    const { data, isLoading } = useGetListCartItemById(
        JSON.parse(localStorage.getItem('chosenCartItems')),
    );
    useEffect(() => {
        if (isLoading || !data) return;
        let t = data?.data?.reduce((acc, val) => {
            return acc + (val?.totalPromotionalPrice || val?.totalPrice) * val?.quantity;
        }, 0);
        setTotalCartPrice(t);
        setChosenCartItems(
            data?.data?.map((v) => ({
                quantity: v?.quantity,
                variantId: v?.variantId,
            })),
        );
    }, [data, isLoading]);
    return (
        <div className="my-[2rem] bg-white rounded-[0.3rem] shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
            <div className="grid grid-cols-5 p-[2rem] text-black font-medium text-opacity-60 text-[1.6rem] text-center">
                <p className="col-span-2 text-left text-[2rem] font-medium text-[#537F44]">
                    Sản phẩm
                </p>
                <p>Đơn giá</p>
                <p className="xl:ml-[6rem]">Số lượng</p>
                <p className="text-right">Thành tiền</p>
            </div>
            <div>
                {data?.data?.map((val, idx) => {
                    return (
                        <ProductItem
                            key={val.id}
                            cartItem={val}
                            isLastItem={idx === data?.data?.length - 1}
                        />
                    );
                })}
            </div>
        </div>
    );
}

export default OrderProducts;
