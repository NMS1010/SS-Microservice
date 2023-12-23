import { Button, Col, Form, Input, InputNumber, Row, Select, Upload, notification } from 'antd';
import { useNavigate, useParams } from 'react-router-dom';
import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useEffect, useRef, useState } from 'react';
import { PlusOutlined } from '@ant-design/icons';

import './delivery.scss';
import config from '../../../config';
import { useCreateDelivery, useGetDelivery, useUpdateDelivery } from '../../../hooks/api';
import { objectToFormData } from '../../../utils/formValidation';
import SpinLoading from '../../../layouts/Ecommerce/components/SpinLoading';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};

function DeliveryFormPage() {
    let { id } = useParams();
    const navigate = useNavigate();
    const [processing, setProcessing] = useState(false);
    const [image, setImage] = useState(null);
    const [imageUrl, setImageUrl] = useState();
    const inputRef = useRef(null);

    const handleUploadChange = (info) => {
        if (info.file) {
            setImage(info.file);
            getBase64(info.file, (url) => {
                setImageUrl(url);
            });
        }
    };

    const { data, isLoading } = id ? useGetDelivery(id) : { data: null, isLoading: null };
    const [form] = Form.useForm();
    const mutationCreate = useCreateDelivery({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Phương thức vận chuyển đã được thêm',
            });
            navigate(config.routes.admin.delivery);
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: 'Có lỗi xảy ra khi thêm phương thức vận chuyển',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });
    const mutationUpdate = useUpdateDelivery({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Phương thức vận chuyển đã được chỉnh sửa',
            });
            navigate(config.routes.admin.delivery);
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa phương thức vận chuyển',
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
        let delivery = data.data;
        form.setFieldsValue({
            name: delivery?.name,
            description: delivery?.description,
            status: delivery?.status,
            price: delivery?.price,
        });
        setImageUrl(delivery?.image);
    }, [isLoading, data]);

    const onAdd = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        if (!image) return;
        let formDt = objectToFormData({
            name: form.getFieldValue('name'),
            description: form.getFieldValue('description'),
            price: form.getFieldValue('price'),
            image: image,
        });
        await mutationCreate.mutateAsync(formDt);
    };
    const onEdit = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }

        let formDt = objectToFormData({
            name: form.getFieldValue('name'),
            description: form.getFieldValue('description'),
            price: form.getFieldValue('price'),
            status: form.getFieldValue('status'),
            image: image,
        });

        await mutationUpdate.mutateAsync({
            id: id,
            body: formDt,
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
                    onClick={() => navigate(config.routes.admin.delivery)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">
                    {id ? 'Cập nhật thông tin' : 'Thêm phương thức vận chuyển'}
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
                    name="delivery-form"
                    layout="vertical"
                    form={form}
                    labelCol={{
                        span: 5,
                    }}
                >
                    <Row gutter={16}>
                        <Col span={8}>
                            <Form.Item
                                label="Tên"
                                name="name"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={8}>
                            <Form.Item
                                label="Giá tiền"
                                name="price"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập giá tiền!',
                                    },
                                ]}
                            >
                                <InputNumber
                                    className="w-full"
                                    formatter={(value) =>
                                        `đ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
                                    }
                                    parser={(value) => value.replace(/\đ\s?|(,*)/g, '')}
                                />
                            </Form.Item>
                        </Col>
                        <Col span={8}>
                            <Form.Item label="Trạng thái" name="status">
                                <Select
                                    onChange={(v) => form.setFieldValue('status', v)}
                                    defaultValue={true}
                                >
                                    <Option value={true}>Kích hoạt</Option>
                                    <Option value={false}>Vô hiệu hoá</Option>
                                </Select>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item
                                label="Mô tả"
                                name="description"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập mô tả!',
                                    },
                                ]}
                            >
                                <Input.TextArea rows={4} />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Upload
                                name="image"
                                listType="picture-circle"
                                className="avatar-uploader flex justify-center"
                                showUploadList={false}
                                beforeUpload={() => false}
                                onChange={handleUploadChange}
                            >
                                {imageUrl ? (
                                    <img
                                        src={imageUrl}
                                        alt="avatar"
                                        className="w-full rounded-full"
                                    />
                                ) : (
                                    <div ref={inputRef}>
                                        <PlusOutlined />
                                        <div className="mt-[0.8rem]">Tải ảnh lên</div>
                                    </div>
                                )}
                            </Upload>
                            {!image && !imageUrl && (
                                <p className="text-center text-[1.6rem] text-[#ff4d4f]">
                                    Vui lòng chọn ảnh!
                                </p>
                            )}
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

export default DeliveryFormPage;
