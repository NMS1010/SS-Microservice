import { Button, Col, Form, Input, Row, Select, Upload, Table, notification } from 'antd';
import { useNavigate, useParams } from 'react-router-dom';
import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import './order.scss';
import config from '../../../config';
import { useEffect, useState } from 'react';
import { useGetListOrderCancellationReason, useGetOrder, useUpdateOrder } from '../../../hooks/api';
import { numberFormatter } from '../../../utils/formatter';
import { ORDER_STATUS, getAllOrderStatusSelect } from '../../../utils/constants';
import SpinLoading from '../../../layouts/Ecommerce/components/SpinLoading';

const columns = [
    {
        title: 'STT',
        dataIndex: 'index',
        key: 'index',
    },
    {
        title: 'Sản phẩm',
        dataIndex: 'product',
        key: 'product',
    },
    {
        title: 'Giá bán',
        dataIndex: 'price',
        key: 'price',
    },
    {
        title: 'Số lượng',
        dataIndex: 'quantity',
        key: 'quantity',
    },
    {
        title: 'Thành tiền',
        dataIndex: 'totalPrice',
        key: 'totalPrice',
    },
];
const rdata = [
    {
        key: '1',
        index: '1',
        product: (
            <div>
                <p className="text-[1.4rem]">Ly giấy kraft 250ml - 9oz double wall</p>
                <p className="text-[1.2rem] opacity-[0.6]">Lốc - 50 cái {`(SKU: UPC075-1)`}</p>
            </div>
        ),
        price: <div className="font-bold">100.000 ₫</div>,
        quantity: '1',
        totalPrice: <div className="font-bold">100.000 ₫</div>,
    },
];

function transformData(data) {
    return data?.map((item, idx) => {
        return {
            key: item?.id,
            index: idx + 1,
            product: (
                <div className="flex items-center gap-[0.5rem]">
                    <img className="w-16 border border-solid" src={item?.productImage} />
                    <div>
                        <p className="text-[1.4rem]">{item?.productName}</p>
                        <p className="text-[1.2rem] opacity-[0.6]">
                            {item?.variantName} - {item?.variantQuantity} {item?.productUnit} (
                            {item?.sku})
                        </p>
                    </div>
                </div>
            ),
            price: <div className="font-bold">{numberFormatter(item?.unitPrice)}</div>,
            quantity: item?.quantity,
            totalPrice: <div className="font-bold">{numberFormatter(item?.totalPrice)}</div>,
        };
    });
}

function OrderFormPage() {
    let { id } = useParams();
    const navigate = useNavigate();
    const [processing, setProcessing] = useState(false);
    const { data, isLoading } = useGetOrder(id);
    const cancelReasonApi = useGetListOrderCancellationReason({
        status: true,
    });
    const [form] = Form.useForm();
    const mutationUpdate = useUpdateOrder({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Đơn hàng đã được cập nhật',
            });
            navigate(config.routes.admin.order);
        },
        error: (err) => {
            let description = 'Có lỗi xảy ra khi thao tác, vui lòng thử lại sau';
            let detail = err?.response?.data?.detail?.toLowerCase();
            if (detail?.includes('paypal')) {
                description = 'Người dùng chưa thanh toán PayPal cho đơn hàng này';
            } else if (detail?.includes('delivered')) {
                description = 'Đơn hàng đã được giao, không thể thay đổi trạng thái';
            }
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: description,
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });
    useEffect(() => {
        if (isLoading || !data) return;
        let order = data.data;
        let address = order?.address;
        form.setFieldsValue({
            user: order?.user?.firstName + ' ' + order?.user?.lastName,
            email: order?.user?.email,
            code: order?.code,
            status: order?.status,
            receiver: address?.receiver,
            phone: address?.phone,
            address: `${address?.street}\n${address?.ward?.name}, ${address?.district?.name}, ${address?.province?.name}`,
            cancelReason: order?.cancelReason?.id,
            otherCancelReason: order?.otherCancelReason,
            paymentMethod: order?.transaction?.paymentMethod,
            paymentStatus: order?.paymentStatus ? 'Đã thanh toán' : 'Chưa thanh toán',
        });
    }, [isLoading, data]);

    const onEdit = async () => {
        await mutationUpdate.mutateAsync({
            id: id,
            body: {
                status: form.getFieldValue('status'),
                otherCancellation: form.getFieldValue('otherCancelReason'),
                orderCancellationReasonId: form.getFieldValue('cancelReason'),
            },
        });
    };
    if (isLoading && id)
        return (
            <div className="flex justify-center">
                <SpinLoading />
            </div>
        );

    return (
        <div className="form-container w-full">
            <div className="flex items-center gap-[1rem]">
                <FontAwesomeIcon
                    onClick={() => navigate(config.routes.admin.order)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">Cập nhật đơn hàng</h1>
            </div>
            <div className="flex items-center justify-between rounded-xl shadow text-[1.6rem] text-black gap-[1rem] bg-white p-7">
                <div className="flex items-center justify-start  gap-[1rem]">
                    <div className="flex flex-col gap-[1rem]">
                        <p>ID</p>
                        <code className="bg-blue-100 p-2">{data?.data?.id || '_'}</code>
                    </div>
                    <div className="flex flex-col gap-[1rem]">
                        <p>Ngày tạo</p>
                        <code className="bg-blue-100 p-2">
                            {data?.data?.createdAt
                                ? new Date(data?.data?.createdAt).toLocaleString()
                                : '__/__/____'}
                        </code>
                    </div>
                    <div className="flex flex-col gap-[1rem]">
                        <p>Ngày cập nhật</p>
                        <code className="bg-blue-100 p-2">
                            {data?.data?.updatedAt
                                ? new Date(data?.data?.updatedAt).toLocaleString()
                                : '__/__/____'}
                        </code>
                    </div>
                </div>
                <div>
                    {data?.data?.status !== ORDER_STATUS.DELIVERED && (
                        <Button
                            loading={processing}
                            onClick={async () => {
                                await mutationUpdate.mutateAsync({
                                    id: id,
                                    body: {
                                        status: ORDER_STATUS.DELIVERED,
                                        otherCancellation: form.getFieldValue('otherCancelReason'),
                                        orderCancellationReasonId:
                                            form.getFieldValue('cancelReason'),
                                    },
                                });
                            }}
                            className="px-[3rem] bg-[--primary-color] border-none text-[1.5rem] text-white font-medium"
                        >
                            Đã giao
                        </Button>
                    )}
                </div>
            </div>
            <div className="grid grid-cols-12 gap-[1.8rem]">
                <div className="col-span-8">
                    <div className="bg-white p-7 mt-5 rounded-xl shadow">
                        <Table
                            className="mb-[2rem]"
                            pagination={false}
                            columns={columns}
                            dataSource={transformData(data?.data?.items)}
                        />
                        <div className="flex flex-col items-end">
                            <div className="w-[280px] text-[1.4rem] flex justify-between items-center my-[0.5rem]">
                                <span className="font-medium">Tổng thành tiền:</span>
                                <span className="text-[--primary-color] text-[1.6rem] font-bold">
                                    {numberFormatter(
                                        data?.data?.items?.reduce(
                                            (acc, val) => acc + val.totalPrice,
                                            0,
                                        ),
                                    )}
                                </span>
                            </div>
                            <div className="w-[280px] text-[1.4rem] flex justify-between items-center my-[0.5rem]">
                                <span className="font-medium">{`Thuế (10%):`}</span>
                                <span className="text-[--primary-color] text-[1.6rem] font-bold">
                                    {numberFormatter(
                                        ((data?.data?.totalAmount - data?.data?.shippingCost) *
                                            data?.data?.tax) /
                                            (1 + data?.data?.tax),
                                    )}
                                </span>
                            </div>
                            <div className="w-[280px] text-[1.4rem] flex justify-between items-center my-[0.5rem]">
                                <span className="font-medium">Phí vận chuyển:</span>
                                <span className="text-[--primary-color] text-[1.6rem] font-bold">
                                    {numberFormatter(data?.data?.shippingCost)}
                                </span>
                            </div>
                            <div className="w-[280px] text-[1.4rem] flex justify-between items-center my-[0.5rem]">
                                <span className="font-medium">Tổng tiền phải trả:</span>
                                <span className="text-[--price-color] text-[2rem] font-bold">
                                    {numberFormatter(data?.data?.totalAmount)}
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="col-span-4">
                    <div className="bg-white p-7 mt-5 rounded-xl shadow">
                        <Form
                            name="order-detail-form"
                            layout="vertical"
                            form={form}
                            labelCol={{
                                span: 5,
                            }}
                        >
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Người đặt hàng" name="user">
                                        <Input readOnly />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Email đặt hàng" name="email">
                                        <Input readOnly />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Mã đơn hàng" name="code">
                                        <Input readOnly />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Trạng thái đơn hàng" name="status">
                                        <Select
                                            showSearch
                                            filterOption={(input, option) =>
                                                (option?.label ?? '')
                                                    .toLowerCase()
                                                    .includes(input.toLowerCase())
                                            }
                                            options={getAllOrderStatusSelect()}
                                        />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Tên người nhận" name="receiver">
                                        <Input readOnly />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Số điện thoại người nhận" name="phone">
                                        <Input readOnly />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Địa chỉ người nhận" name="address">
                                        <Input.TextArea
                                            readOnly
                                            style={{
                                                height: 80,
                                                resize: 'none',
                                            }}
                                        />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Lý do hủy đơn hàng" name="cancelReason">
                                        <Select
                                            showSearch
                                            filterOption={(input, option) =>
                                                (option?.label ?? '')
                                                    .toLowerCase()
                                                    .includes(input.toLowerCase())
                                            }
                                            options={cancelReasonApi?.data?.data?.items?.map(
                                                (item) => {
                                                    return {
                                                        label: item?.name,
                                                        value: item?.id,
                                                    };
                                                },
                                            )}
                                        ></Select>
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Lý do khác" name="otherCancelReason">
                                        <Input.TextArea
                                            style={{
                                                height: 80,
                                                resize: 'none',
                                            }}
                                            defaultValue={''}
                                        />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Hình thức thanh toán" name="paymentMethod">
                                        <Input readOnly />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item label="Trạng thái thanh toán" name="paymentStatus">
                                        <Input readOnly />
                                    </Form.Item>
                                </Col>
                            </Row>
                            <div className="flex justify-between items-center gap-[1rem]">
                                <Button className="min-w-[10%]">Đặt lại</Button>
                                <Button
                                    loading={processing}
                                    onClick={onEdit}
                                    className="bg-blue-500 text-white min-w-[10%]"
                                >
                                    {id ? 'Cập nhật' : 'Thêm mới'}
                                </Button>
                            </div>
                        </Form>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default OrderFormPage;
