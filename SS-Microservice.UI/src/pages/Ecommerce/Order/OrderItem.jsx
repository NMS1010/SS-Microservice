import { numberFormatter } from '../../../utils/formatter';

function OrderItem({ item, isLastItem = false }) {
    return (
        <div className={`flex gap-[2.2rem] mx-[2.1rem] py-[1.2rem] ${!isLastItem && 'border-b'}`}>
            <img className="w-[7.9rem] h-[7.9rem] " src={item?.productImage} />
            <div className="">
                <p className="text-black text-[1.5rem] font-normal">{item?.productName}</p>
                <p className="text-[1.2rem] mb-[1rem] mt-[.6rem]">
                    x{item?.quantity} {item?.variantName}
                </p>
                <p className="text-rose-600 text-opacity-70 font-medium text-[1.4rem]">
                    {numberFormatter(item?.totalPrice)}
                </p>
            </div>
        </div>
    );
}

export default OrderItem;
