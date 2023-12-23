import { Radio } from 'antd';
import { NavLink } from 'react-router-dom';
import { useGetListDelivery } from '../../../hooks/api';
import { numberFormatter } from '../../../utils/formatter';
import { useEffect } from 'react';

function OrderDelivery({ setChosenDelivery, chosenDelivery }) {
    const { data, isLoading } = useGetListDelivery({
        status: true,
    });
    const onChange = (e) => {
        let id = e.target.value;
        setChosenDelivery(data?.data?.items?.find((v) => v.id === id));
    };
    useEffect(() => {
        setChosenDelivery(data?.data?.items[0]);
    }, [data, isLoading]);
    return (
        <div className="delivery-container my-[2rem] bg-white rounded-[0.3rem] shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
            <div className="flex items-center justify-between max-sm:flex-col max-md:gap-[2rem]  max-2xl:gap-[24rem] 2xl:gap-[34.5rem] p-[2rem] text-black font-medium text-opacity-60 text-[1.6rem]">
                <p className="text-[2rem] font-medium text-[#537F44]">Phương thức vận chuyển</p>
                <div className="flex items-center max-md:gap-[3rem] md:gap-[5.1rem] font-normal">
                    <Radio.Group
                        className="flex items-center"
                        onChange={onChange}
                        value={chosenDelivery?.id}
                    >
                        {data?.data?.items?.map((v) => {
                            return (
                                <Radio
                                    key={v.id}
                                    className="element w-[16rem] h-[9rem] p-[1rem] flex flex-col items-center"
                                    value={v.id}
                                >
                                    <div className="flex flex-col items-center">
                                        <span className="text-[1.4rem]">{v.name}</span>
                                        <span className="price text-[1.9rem] font-medium price-color">
                                            {numberFormatter(v.price)}
                                        </span>
                                        <svg
                                            className="ico-check hidden"
                                            xmlns="http://www.w3.org/2000/svg"
                                            width="20"
                                            height="20"
                                            viewBox="0 0 20 20"
                                        >
                                            <g>
                                                <path
                                                    fill="#537F44"
                                                    d="M0 0h16c2.21 0 4 1.79 4 4v16L0 0z"
                                                    transform="translate(-804 -366) translate(180 144) translate(484 114) translate(16 80) translate(0 28) translate(124)"
                                                ></path>
                                                <g fill="#FFF">
                                                    <path
                                                        d="M4.654 7.571L8.88 3.176c.22-.228.582-.235.81-.016.229.22.236.582.017.81L5.04 8.825c-.108.113-.258.176-.413.176-.176 0-.33-.076-.438-.203L2.136 6.37c-.205-.241-.175-.603.067-.808.242-.204.603-.174.808.068L4.654 7.57z"
                                                        transform="translate(-804 -366) translate(180 144) translate(484 114) translate(16 80) translate(0 28) translate(124) translate(7.5)"
                                                    ></path>
                                                </g>
                                            </g>
                                        </svg>
                                    </div>
                                </Radio>
                            );
                        })}
                    </Radio.Group>
                </div>
            </div>
        </div>
    );
}

export default OrderDelivery;
