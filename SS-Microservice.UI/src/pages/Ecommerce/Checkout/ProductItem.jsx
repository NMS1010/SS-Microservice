import { NavLink } from 'react-router-dom';
import { numberFormatter } from '../../../utils/formatter';

function ProductItem({ cartItem, isLastItem = false }) {
    return (
        <div
            className={`${
                !isLastItem ? 'border-b-[0.1rem]' : 'pb-[3.7rem]'
            } grid grid-cols-5 text-center py-[1.1rem] mx-[2rem]`}
        >
            <div className="flex max-md:flex-col gap-[2.2rem] items-center col-span-2">
                <img className="w-[7.9rem] h-[7.9rem] " src={cartItem?.productImage} />
                <div className="md:text-left">
                    <NavLink className="text-black text-[1.5rem] font-medium">
                        {cartItem?.productName}
                    </NavLink>
                    <p className="text-[1.2rem] text-black mb-[1rem] mt-[.6rem]">
                        {cartItem?.variantName} - {cartItem?.variantQuantity}{' '}
                        {cartItem?.productUnit}
                    </p>
                </div>
            </div>
            <div className="flex justify-center items-center">
                <p className="text-rose-600 font-bold text-[1.4rem]">
                    {numberFormatter(cartItem?.totalPromotionalPrice || cartItem?.totalPrice)}
                </p>
            </div>
            <div className="flex justify-center xl:ml-[6rem] items-center">
                <p className="text-rose-600 font-bold text-[1.4rem]">
                    {cartItem?.quantity} {cartItem?.variantName}
                </p>
            </div>
            <div className="flex justify-end items-center">
                <p className="text-rose-600 font-bold text-[1.4rem]">
                    {numberFormatter(
                        (cartItem?.totalPromotionalPrice || cartItem?.totalPrice) *
                            cartItem?.quantity,
                    )}
                </p>
            </div>
        </div>
    );
}

export default ProductItem;
