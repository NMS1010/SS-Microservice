import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from 'antd';
import { NavLink } from 'react-router-dom';
import config from '../../../config';
import { numberFormatter } from '../../../utils/formatter';
import { useEffect, useState } from 'react';
import { faPaypal } from '@fortawesome/free-brands-svg-icons';

function Summary({ cartItems, chosenItem }) {
    const [totalPay, setTotalPay] = useState(0);
    useEffect(() => {
        let total = cartItems?.reduce((acc, val) => {
            if (chosenItem?.includes(val?.id)) {
                return acc + val?.quantity * (val?.totalPromotionalPrice || val?.totalPrice);
            }
            return acc;
        }, 0);
        setTotalPay(total);
    }, [cartItems, chosenItem]);
    return (
        <div className="bg-white md:ml-[5.5rem] max-h-[21rem] max-md:my-[3rem] max-md:w-[100%] w-[34rem] rounded-[5px] shadow-[0_1px_2px_0_rgba(0,0,0,0.13)] pb-[1rem]">
            <div className="mx-[2rem] my-[1.1rem] border-b-[0.1rem]">
                <p className="mb-0 text-[1.9rem] text-[#537F44] font-bold">Giỏ hàng</p>
            </div>
            <div className="px-[2rem] pt-[0.9rem]">
                <div className="flex justify-between items-center text-[1.9rem] font-medium">
                    <p className="mb-0 text-black">Tổng tiền</p>
                    <span className="text-rose-600">{numberFormatter(totalPay)}</span>
                </div>
                <NavLink
                    to={totalPay > 0 ? config.routes.web.checkout : ''}
                    className={` ${
                        totalPay === 0
                            ? 'cursor-not-allowed bg-gray-400 flex shadow-md items-center justify-center rounded-[.3rem] my-[1.2rem] w-full xl:text-[1.9rem] max-xl:text-[1.5rem] border-none text-white'
                            : 'flex shadow-md items-center justify-center rounded-[.3rem] my-[1.2rem] w-full xl:text-[1.9rem] max-xl:text-[1.5rem] border-none text-white bg-orange-600'
                    }`}
                >
                    <FontAwesomeIcon icon={faPaypal} />
                    <span className="block py-[0.5rem] text-center">Tiến hành thanh toán</span>
                </NavLink>
                <NavLink to={"/" + config.routes.web.home} className="flex items-center justify-center text-[1.6rem] max-md:mb-3 font-normal cursor-pointer">
                    <FontAwesomeIcon icon={faArrowLeft} />
                    <span className="ml-2">Tiếp tục mua hàng</span>
                </NavLink>
            </div>
        </div>
    );
}

export default Summary;
