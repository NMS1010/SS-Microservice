import { Radio } from 'antd';
import { useState } from 'react';
import { numberFormatter } from '../../../utils/formatter';

function PriceVariant({ variants, unit, setChosenVariant, chosenVariant }) {
    const onChange = (e) => {
        let id = e.target.value;
        setChosenVariant(variants?.find((v) => v.id === id));
    };
    return (
        <div className="price-variant">
            <Radio.Group
                className="flex items-center"
                onChange={onChange}
                value={chosenVariant?.id}
            >
                {variants?.map((v) => {
                    return (
                        <Radio
                            key={v.id}
                            className="element w-[20rem] h-[106px] p-[8px] flex flex-col items-center"
                            value={v.id}
                        >
                            <div className="flex flex-col items-center">
                                <span className="text-[1.2rem]">
                                    {v.name} - {v.quantity} {unit?.name}
                                </span>
                                {v.promotionalItemPrice ? (
                                    <>
                                        <div className="flex items-center gap-[0.5rem]">
                                            <span className="price text-[2rem] font-medium price-color">
                                                {numberFormatter(v?.totalPromotionalPrice || v.totalPrice)}
                                            </span>
                                            <span className="text-[1.2rem] opacity-[0.8]">
                                                (
                                                {numberFormatter(
                                                    v.promotionalItemPrice || v.itemPrice,
                                                )}
                                                /{unit?.name})
                                            </span>
                                        </div>
                                        <div className="flex items-center gap-[0.5rem] line-through">
                                            <span className="price text-[1.4rem] font-medium price-color">
                                                {numberFormatter(v.itemPrice * v.quantity)}
                                            </span>
                                            <span className="text-[1.2rem] opacity-[0.8]">
                                                (
                                                {numberFormatter(
                                                    v.itemPrice,
                                                )}
                                                /{unit?.name})
                                            </span>
                                        </div>
                                    </>
                                ) : (
                                    <div className="flex items-center gap-[0.5rem]">
                                        <span className="price text-[2rem] font-medium price-color">
                                            {numberFormatter(v.totalPrice)}
                                        </span>
                                        <span className="text-[1.2rem] opacity-[0.8]">
                                            (
                                            {numberFormatter(v.promotionalItemPrice || v.itemPrice)}
                                            /{unit?.name})
                                        </span>
                                    </div>
                                )}
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
    );
}

export default PriceVariant;
