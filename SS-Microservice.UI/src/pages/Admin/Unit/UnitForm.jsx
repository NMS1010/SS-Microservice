import { Button, Col, Form, Input, Row, Select, Upload, notification } from 'antd';
import { useNavigate, useParams } from 'react-router-dom';
import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import './unit.scss';
import config from '../../../config';
import { useCreateUnit, useGetUnit, useUpdateUnit } from '../../../hooks/api';
import { useEffect, useState } from 'react';
import SpinLoading from '../../../layouts/Ecommerce/components/SpinLoading';

function UnitFormPage() {
    let { id } = useParams();
    const navigate = useNavigate();
    const [form] = Form.useForm();
    const { data, isLoading } = id ? useGetUnit(id) : { data: null, isLoading: null };
    const [processing, setProcessing] = useState(false);

    useEffect(() => {
        if (isLoading || !data) return;
        let unit = data.data;
        form.setFieldsValue({
            name: unit?.name,
            status: unit?.status,
        });
    }, [isLoading, data]);

    const mutationCreate = useCreateUnit({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Đơn vị sản phẩm đã được thêm',
            });
            navigate(config.routes.admin.unit);
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: 'Có lỗi xảy ra khi thêm đơn vị sản phẩm',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const mutationUpdate = useUpdateUnit({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Đơn vị sản phẩm đã được chỉnh sửa',
            });
            navigate(config.routes.admin.unit);
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa đơn vị sản phẩm',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const onAdd = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        await mutationCreate.mutateAsync({
            name: form.getFieldValue('name'),
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
                    onClick={() => navigate(config.routes.admin.unit)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">{id ? 'Cập nhật thông tin' : 'Thêm đơn vị sản phẩm'}</h1>
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
                    name="unit-form"
                    layout="vertical"
                    form={form}
                    initialValues={{
                        remember: true,
                    }}
                    labelCol={{
                        span: 5,
                    }}
                >
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                label="Tên đơn vị"
                                name="name"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên danh mục!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item label="Trạng thái" name="status">
                                <Select
                                    onChange={(v) => form.setFieldValue('status', v)}
                                    defaultValue={true}
                                    showSearch
                                >
                                    <Option value={false}>Vô hiệu lực </Option>
                                    <Option value={true}>Kích hoạt</Option>
                                </Select>
                            </Form.Item>
                        </Col>
                    </Row>
                    <div className="flex justify-between items-center gap-[1rem]">
                        <Button className="min-w-[10%]">Đặt lại</Button>
                        <Button
                            loading={processing}
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

export default UnitFormPage;
