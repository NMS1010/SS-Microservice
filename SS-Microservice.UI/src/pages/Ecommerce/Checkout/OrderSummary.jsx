import { numberFormatter } from '../../../utils/formatter';

function OrderSummary({ chosenDelivery, totalCartPrice }) {
    return (
        <div className=" bg-[#FDF7E8] flex justify-end border-b-[0.1rem] ">
            <div className=" p-[2rem] flex flex-col items-end gap-[2.2rem] w-[380px]">
                <div className="w-full flex justify-between gap-[11rem]  text-black text-[1.6rem] font-medium">
                    <p>Tổng tiền hàng</p>
                    <span>{numberFormatter(totalCartPrice)}</span>
                </div>
                <div className="w-full flex gap-[11rem] justify-between text-black text-[1.6rem] font-medium">
                    <p>Thuế (10%)</p>
                    <span>{numberFormatter(totalCartPrice * 0.1)}</span>
                </div>
                <div className="w-full flex justify-between gap-[11rem]  text-black text-[1.6rem] font-medium">
                    <p>Phí vận chuyển</p>
                    <span>{numberFormatter(chosenDelivery?.price || 0)}</span>
                </div>
                <div className="w-full flex justify-between gap-[6.2rem] max-[396px]:gap-[1.2rem] text-black items-center">
                    <p className="text-[1.6rem] font-medium">Tổng thanh toán</p>
                    <span className="text-[2.6rem] text-rose-600 font-bold">
                        {numberFormatter(
                            totalCartPrice + chosenDelivery?.price + totalCartPrice * 0.1 || 0,
                        )}
                    </span>
                </div>
            </div>
        </div>
    );
}

export default OrderSummary;
