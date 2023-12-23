import { PayPalButtons, PayPalScriptProvider } from '@paypal/react-paypal-js';
import { useCompletePaypalOrder, useGetOrderByCode } from '../../../hooks/api';
import config from '../../../config';
import { useNavigate, useParams } from 'react-router-dom';
import { EXCHANGE_VALUE_USD_VND, ORDER_STATUS } from '../../../utils/constants';
import './checkout.scss';
import { Alert, Button, Result, Spin, notification } from 'antd';
import { useCallback, useContext, useEffect } from 'react';
import WebLoading from '../../../layouts/Ecommerce/components/WebLoading';
import { NotificationContext } from '../../../context/NotificationContext';

function PaypalPaymentPage() {
    const {countNotify} = useContext(NotificationContext)
    const navigate = useNavigate();
    const { code } = useParams();

    const orderApi = useGetOrderByCode(code);
    const mutateCompletePaypalOrder = useCompletePaypalOrder({
        success: (data) => {
            // notification.success({
            //     message: 'Thanh toán thành công',
            //     description:
            //         'Đơn hàng của quý khách đã được thanh toán, vui lòng kiểm tra email chi tiết',
            // });
            navigate(config.routes.web.order);
        },
        error: (e) => {
            notification.error({
                message: 'Thanh toán thất bại',
                description:
                    'Đã có lỗi xảy ra trong quá trình thanh toán, đơn hàng chưa được xử lý. Vui lòng thử lại sau.',
            });
        },
        mutate: (data) => {
            // setProcessing(true);
        },
        settled: (data) => {
            // setProcessing(false);
        },
    });

    useEffect(() => {
        orderApi?.refetch();
    }, [countNotify]);

    if (orderApi?.isLoading) return <WebLoading />;

    const createPaypalOrder = (data, actions) => {
        let order = orderApi?.data?.data;
        let sub = order?.items
            ?.reduce((total, currVal, currIdx) => {
                return (
                    total +
                    (currVal?.unitPrice / EXCHANGE_VALUE_USD_VND).toFixed(2) * currVal?.quantity
                );
            }, 0.0)
            .toFixed(2);

        let ship = (order?.shippingCost / EXCHANGE_VALUE_USD_VND).toFixed(2);

        let tax = (sub / 10).toFixed(2);

        let orderObj = {
            application_context: {
                shipping_preferences: 'SET_PROVIDED_ADDRESS',
            },
            purchase_units: [
                {
                    amount: {
                        value: (parseFloat(sub) + parseFloat(ship) + parseFloat(tax)).toFixed(2),
                        breakdown: {
                            item_total: {
                                currency_code: 'USD',
                                value: sub,
                            },
                            shipping: {
                                currency_code: 'USD',
                                value: ship,
                            },
                            tax_total: {
                                currency_code: 'USD',
                                value: tax,
                            },
                        },
                    },
                    shipping: {
                        name: {
                            full_name: `${order?.address?.receiver}`,
                        },
                        address: {
                            address_line_1: order?.address?.street,
                            address_line_2: order?.address?.ward?.name,
                            admin_area_2: order?.address?.district?.name,
                            admin_area_1: order?.address?.province?.name,
                            postal_code: '71000',
                            country_code: 'VN',
                        },
                    },
                    items: order?.items?.map((ci) => {
                        return {
                            name: `${ci.productName} - (${ci.variantName} - ${ci.variantQuantity} ${ci.productUnit}})`,
                            quantity: ci.quantity,
                            unit_amount: {
                                currency_code: 'USD',
                                value: (ci?.unitPrice / EXCHANGE_VALUE_USD_VND).toFixed(2),
                            },
                        };
                    }),
                },
            ],
        };

        return actions.order.create(orderObj).then((orderID) => {
            return orderID;
        });
    };

    const onApprove = (data, actions) => {
        return actions.order.capture().then(function (details) {
            mutateCompletePaypalOrder.mutate({
                id: orderApi?.data?.data?.id,
                body: {
                    paypalOrderId: details.id,
                    paypalOrderStatus: details.status,
                },
            });
        });
    };

    const onError = (data, actions) => {
        notification.error({
            message: 'Thanh toán thất bại',
            description: 'Đã có lỗi xảy ra trong quá trình liên kết Paypal với đơn hàng.',
        });
    };

    return (
        <div className="paypal-payment-container flex justify-center mt-20 shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
            {orderApi?.data?.data?.transaction?.paymentMethod.toLowerCase().includes('paypal') ? (
                !orderApi?.data?.data?.paymentStatus ? (
                    orderApi?.data?.data?.status === ORDER_STATUS.NOT_PROCESSED ? (
                        <div>
                            <div className="text-[2rem] mb-6">
                                <p>
                                    Bạn đã sử dụng phương thức thanh toán thông qua PayPal cho đơn
                                    hàng
                                    <span className="font-bold"> {code}</span>
                                </p>
                                <p>
                                    Vui lòng thanh toán để hoàn tất đơn hàng này. Nếu không, đơn
                                    hàng sẽ tự động huỷ sau 24h
                                </p>
                            </div>
                            <div className="text-center relative h-full">
                                <PayPalScriptProvider
                                    options={{
                                        'client-id': `AanwBJHPgHiuWHMM74g6ECSph0hptxt-M8Ax-XOZXW31QnWmiqjCGITfkOGrUC5BoX_ItoXG9Np0gwx4`,
                                    }}
                                >
                                    <PayPalButtons
                                        className="flex justify-center w-1/2 absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2"
                                        createOrder={createPaypalOrder}
                                        onApprove={onApprove}
                                        onError={onError}
                                        containerWidth={'100px'}
                                        style={{ layout: 'horizontal', label: 'checkout' }}
                                    />
                                </PayPalScriptProvider>
                            </div>
                        </div>
                    ) : (
                        <Result
                            status="error"
                            title="Yêu cầu không hợp lệ"
                            subTitle={
                                <div className="text-[1.6rem]">
                                    <p>
                                        Đơn hàng <strong>{code}</strong> đã bị huỷ.
                                    </p>
                                </div>
                            }
                            extra={[
                                <Button
                                    onClick={() => navigate(config.routes.web.order)}
                                    className="text-green-400 border border-solide border-green-400"
                                >
                                    Danh sách đơn hàng
                                </Button>,
                            ]}
                        />
                    )
                ) : (
                    <Result
                        status="success"
                        title="Thanh toán thành công"
                        subTitle={
                            <div className="text-[1.6rem]">
                                <p>
                                    Đơn hàng <strong>{code}</strong> đã được thanh toán trước đó
                                    bằng phương thức PayPal.
                                </p>
                                <p>Vui lòng kiểm tra email để xem chi tiết đơn hàng</p>
                            </div>
                        }
                        extra={[
                            <Button
                                onClick={() => navigate(config.routes.web.order + '/' + code)}
                                className="text-green-400 border border-solide border-green-400"
                            >
                                Chi tiết đơn hàng
                            </Button>,
                        ]}
                    />
                )
            ) : (
                <Result
                    status="error"
                    title="Yêu cầu không hợp lệ"
                    subTitle={
                        <div className="text-[1.6rem]">
                            <p>
                                Đơn hàng <strong>{code}</strong> không cần thanh toán trước.
                            </p>
                        </div>
                    }
                    extra={[
                        <Button
                            onClick={() => navigate(config.routes.web.order)}
                            className="text-green-400 border border-solide border-green-400"
                        >
                            Danh sách đơn hàng
                        </Button>,
                    ]}
                />
            )}
        </div>
    );
}

export default PaypalPaymentPage;
