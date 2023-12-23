import { Button, Col, Form, Input, Row, Select, notification } from 'antd';
import { useNavigate, useParams } from 'react-router-dom';
import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useEffect, useState } from 'react';

import './reasonCancel.scss';
import config from '../../../config';
import {
    useCreateOrderCancellationReason,
    useGetOrderCancellationReason,
    useUpdateOrderCancellationReason,
} from '../../../hooks/api';
import SpinLoading from '../../../layouts/Ecommerce/components/SpinLoading';

function ReasonCancelFormPage() {
    let { id } = useParams();
    const navigate = useNavigate();

    const [processing, setProcessing] = useState(false);
    const { data, isLoading } = id
        ? useGetOrderCancellationReason(id)
        : { data: null, isLoading: null };
    const [form] = Form.useForm();
    const mutationCreate = useCreateOrderCancellationReason({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Lý do huỷ đơn hàng đã được thêm',
            });
            navigate(config.routes.admin.reason_cancel);
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: 'Có lỗi xảy ra khi thêm lý do huỷ đơn hàng',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });
    const mutationUpdate = useUpdateOrderCancellationReason({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Lý do huỷ đơn hàng đã được chỉnh sửa',
            });
            navigate(config.routes.admin.reason_cancel);
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa lý do huỷ đơn hàng',
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
        let cancelReason = data.data;

        form.setFieldsValue({
            name: cancelReason?.name,
            note: cancelReason?.note,
            status: cancelReason?.status,
        });
    }, [isLoading, data]);

    const onAdd = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }

        await mutationCreate.mutateAsync({
            name: form.getFieldValue('name'),
            note: form.getFieldValue('note'),
        });
    };
    const onEdit = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }

        await mutationUpdate.mutateAsync({
            id: id,
            body: {
                name: form.getFieldValue('name'),
                note: form.getFieldValue('note'),
                status: form.getFieldValue('status'),
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
        <div className="form-container">
            <div className="flex items-center gap-[1rem]">
                <FontAwesomeIcon
                    onClick={() => navigate(config.routes.admin.reason_cancel)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">
                    {id ? 'Cập nhật thông tin' : 'Thêm lý do hủy đơn hàng'}
                </h1>
            </div>
            <div className="flex items-center justify-start rounded-xl shadow text-[1.6rem] text-black gap-[1rem] bg-white p-7">
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
            <div className="bg-white p-7 mt-5 rounded-xl shadow">
                <Form
                    name="reason-cancel-form"
                    layout="vertical"
                    form={form}
                    labelCol={{
                        span: 5,
                    }}
                >
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                label="Tên lý do hủy đơn hàng"
                                name="name"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên lý do hủy đơn hàng!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item label="Trạng thái" name="status">
                                <Select
                                    defaultValue={true}
                                    onChange={(v) => form.setFieldValue('status', v)}
                                >
                                    <Option value={true}>Kích hoạt</Option>
                                    <Option value={false}>Vô hiệu hoá</Option>
                                </Select>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item label="Ghi chú lý do hủy đơn hàng" name="note" 
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập ghi chú lý do hủy đơn hàng!',
                                    },
                                ]}>
                                <Input.TextArea
                                    showCount
                                    maxLength={200}
                                    style={{
                                        height: 80,
                                        resize: 'none',
                                    }}
                                />
                            </Form.Item>
                        </Col>
                    </Row>
                    <div className="flex justify-between items-center gap-[1rem]">
                        <Button className="min-w-[10%]">Đặt lại</Button>
                        <Button
                            loading={processing}
                            htmlType="submit"
                            onClick={id ? onEdit : onAdd}
                            className="bg-blue-500 text-white min-w-[10%]"
                        >
                            {id ? 'Cập nhật' : 'Thêm mới'}
                        </Button>
                    </div>
                </Form>
            </div>
        </div>
    );
}

export default ReasonCancelFormPage;
