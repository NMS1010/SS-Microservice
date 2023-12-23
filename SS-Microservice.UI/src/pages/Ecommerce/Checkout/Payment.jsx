import { useEffect, useState } from 'react';
import OrderSummary from './OrderSummary';
import { Button, Input, Radio, notification } from 'antd';
import { NavLink, useNavigate } from 'react-router-dom';
import config from '../../../config';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { useCreateOrder, useGetListPaymentMethod } from '../../../hooks/api';

function Payment({ defaultAddress, chosenDelivery, totalCartPrice, chosenCartItems }) {
    const [chosenPaymentMethod, setChosenPaymentMethod] = useState(null);
    const { data, isLoading } = useGetListPaymentMethod({
        status: true,
    });
    const navigate = useNavigate();
    const [processing, setProcessing] = useState(false);
    const [note, setNote] = useState('');
    const onChange = (e) => {
        let id = e.target.value;
        setChosenPaymentMethod(data?.data?.items?.find((v) => v.id === id));
    };
    useEffect(() => {
        setChosenPaymentMethod(data?.data?.items[0]);
    }, [data, isLoading]);

    const mutateAdd = useCreateOrder({
        success: (data) => {
            // notification.success({
            //     message: 'Đặt hàng thành công',
            //     description: chosenPaymentMethod?.name?.toLowerCase().includes('cod')
            //         ? 'Đơn hàng của quý khách đã được ghi nhận và đang được xử lý. Vui lòng kiểm tra email để xem chi tiết.'
            //         : 'Đơn hàng của quý khách đã được ghi nhận và chưa được xử lý. Vui lòng thanh toán.',
            // });
            localStorage.removeItem('chosenCartItems');
            let url = config.routes.web.order;
            if (chosenPaymentMethod?.name?.toLowerCase().includes('paypal'))
                url = config.routes.web.checkout + '/payment/' + data?.data?.code;
            navigate(url);
        },
        error: (e) => {
            notification.error({
                message: 'Đặt hàng thất bại',
                description: 'Đã có lỗi xảy ra trong quá trình đặt hàng. Có thể số lượng sản phẩm không đủ, vui lòng thử lại sau.',
            });
        },
        mutate: (data) => {
            setProcessing(true);
        },
        settled: (data) => {
            setProcessing(false);
        },
    });

    const onCreateOrder = async () => {
        await mutateAdd.mutateAsync({
            note: note,
            paymentMethodId: chosenPaymentMethod?.id,
            deliveryId: chosenDelivery?.id,
            items: chosenCartItems,
        });
    };

    return (
        <div className="payment-container mt-[2rem] bg-white rounded-[0.3rem] shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
            <div className="flex items-center justify-between max-sm:flex-col max-md:gap-[2rem] max-xl:gap-[3rem] max-2xl:gap-[17rem] 2xl:gap-[30rem] p-[2rem] text-black font-medium text-opacity-60 text-[1.6rem]">
                <p className="text-[2rem] font-medium text-[#537F44]">Phương thức thanh toán</p>
                <Radio.Group
                    className="flex items-center"
                    onChange={onChange}
                    value={chosenPaymentMethod?.id}
                >
                    {data?.data?.items?.map((v) => {
                        return (
                            <Radio
                                key={v.id}
                                className="element w-[14rem] h-[7rem] p-[1rem] flex flex-col items-center"
                                value={v.id}
                            >
                                <div className="flex flex-col items-center">
                                    <span className="text-[1.4rem]">{v.name}</span>
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
            <div className="p-5">
                <h2 className="mb-2 ml-2 text-[1.6rem]">Lời nhắn</h2>
                <Input.TextArea
                    placeholder="Ghi chú cho shop"
                    className="text-[1.6rem]"
                    maxLength={200}
                    style={{
                        height: 100,
                        resize: 'none',
                    }}
                    onChange={(e) => setNote(e.target.value)}
                    value={note}
                ></Input.TextArea>
            </div>
            <OrderSummary chosenDelivery={chosenDelivery} totalCartPrice={totalCartPrice} />
            <div className="bg-[#FDF7E8] flex max-md:flex-col-reverse justify-end items-center py-[2rem] px-[1.2rem]">
                <div className="text-[1.6rem] font-normal text-[#537F44B2]">
                    <FontAwesomeIcon className="mr-[0.4rem]" icon={faArrowLeft} />
                    <NavLink to={config.routes.web.cart}>Quay về giỏ hàng</NavLink>
                </div>
                <Button
                    onClick={onCreateOrder}
                    loading={processing}
                    disabled={
                        !defaultAddress ||
                        chosenCartItems?.length === 0 ||
                        !chosenDelivery ||
                        !chosenPaymentMethod
                    }
                    className={`w-[21rem] h-[3.6rem] md:ml-[2.5rem] max-md:mb-[1rem] text-[1.8rem] font-normal bg-[#FF5722] text-white border-none rounded-md
                    ${
                        (!defaultAddress ||
                            chosenCartItems?.length === 0 ||
                            !chosenDelivery ||
                            !chosenPaymentMethod) &&
                        'bg-gray-400'
                    }`}
                >
                    Đặt hàng
                </Button>
            </div>
        </div>
    );
}

export default Payment;
