import { Button, Col, Form, Input, Row, Select, Upload, notification } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useNavigate, useParams } from 'react-router-dom';
import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useEffect, useRef, useState } from 'react';
import './brand.scss';
import config from '../../../config';
import { useCreateBrand, useGetBrand, useUpdateBrand } from '../../../hooks/api';
import SpinLoading from '../../../layouts/Ecommerce/components/SpinLoading';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};

function BrandFormPage() {
    let { id } = useParams();
    const navigate = useNavigate();
    const inputRef = useRef(null);
    const [imageUrl, setImageUrl] = useState();
    const [imageFile, setImageFile] = useState(null);
    const [form] = Form.useForm();
    const formData = new FormData();
    const [processing, setProcessing] = useState(false);

    const { data, isLoading } = id ? useGetBrand(id) : { data: null, isLoading: null };

    useEffect(() => {
        if (isLoading || !data) return;
        let brand = data?.data;
        form.setFieldsValue({
            name: brand?.name,
            code: brand?.code,
            description: brand?.description,
            status: brand?.status,
        });
        setImageUrl(brand.image);
    }, [isLoading, data]);

    const mutationCreate = useCreateBrand({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Thương hiệu sản phẩm đã được thêm',
            });
            navigate(config.routes.admin.brand);
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: 'Có lỗi xảy ra khi thêm thương hiệu sản phẩm',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const mutationUpdate = useUpdateBrand({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Thương hiệu sản phẩm đã được chỉnh sửa',
            });
            navigate(config.routes.admin.brand);
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa thương hiệu sản phẩm',
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
        formData.append('name', form.getFieldValue('name'));
        formData.append('code', form.getFieldValue('code'));
        formData.append('description', form.getFieldValue('description'));
        formData.append('image', imageFile);
        await mutationCreate.mutateAsync(formData);
    };

    const onEdit = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        formData.append('name', form.getFieldValue('name'));
        formData.append('code', form.getFieldValue('code'));
        formData.append('description', form.getFieldValue('description'));
        formData.append('status', form.getFieldValue('status'));
        formData.append('image', imageFile);
        await mutationUpdate.mutateAsync({
            id: id,
            body: formData,
        });
    };

    const handleChange = (info) => {
        if (info.file) {
            setImageFile(info.file);
            getBase64(info.file, (url) => {
                setImageUrl(url);
            });
        }
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
                    onClick={() => navigate(config.routes.admin.brand)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">
                    {id ? 'Cập nhật thông tin' : 'Thêm thương hiệu sản phẩm'}
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
                    name="brand-form"
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
                                label="Tên thương hiệu"
                                name="name"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên thương hiệu!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                label="Code"
                                name="code"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập mã code cho thương hiệu!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item
                                label="Giới thiệu thương hiệu"
                                name="description"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập giới thiệu về thương hiệu!',
                                    },
                                ]}
                            >
                                <Input.TextArea
                                    showCount
                                    maxLength={1000}
                                    style={{
                                        height: 150,
                                        resize: 'none',
                                    }}
                                    placeholder="Giới thiệu về thương hiệu . . . ."
                                />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item label="Trạng thái" name="status">
                                <Select
                                    onChange={(v) => form.setFieldValue('status', v)}
                                    placeholder="--"
                                    showSearch
                                >
                                    <Option value={false}>Vô hiệu lực </Option>
                                    <Option value={true}>Kích hoạt</Option>
                                </Select>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item label="Hình ảnh thương hiệu" name="image">
                                <Upload
                                    name="image"
                                    listType="picture-circle"
                                    className="flex justify-center"
                                    showUploadList={false}
                                    beforeUpload={() => false}
                                    onChange={handleChange}
                                >
                                    {imageUrl ? (
                                        <img
                                            src={imageUrl}
                                            alt="category"
                                            className="w-full rounded-full"
                                        />
                                    ) : (
                                        <div ref={inputRef}>
                                            <PlusOutlined />
                                            <div className="mt-[0.8rem]">Tải ảnh lên</div>
                                        </div>
                                    )}
                                </Upload>
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

export default BrandFormPage;
