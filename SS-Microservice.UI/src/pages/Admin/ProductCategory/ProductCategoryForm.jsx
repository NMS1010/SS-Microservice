import { Button, Col, Form, Input, Row, Select, Upload, notification } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useNavigate, useParams } from 'react-router-dom';
import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useEffect, useRef, useState } from 'react';
import './productCategory.scss';
import config from '../../../config';
import {
    useCreateProductCategory,
    useGetListProductCategory,
    useGetProductCategory,
    useUpdateProductCategory,
} from '../../../hooks/api';
import slugify from '../../../utils/slugify';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};

function ProductCategoryFormPage() {
    let { id } = useParams();
    const navigate = useNavigate();
    const [processing, setProcessing] = useState(false);
    const inputRef = useRef(null);
    const { isLoading: isLoadingCategory, data: category } = id
        ? useGetProductCategory(id)
        : { isLoading: null, data: null };
    const { isLoading: isLoadingCategories, data: categories } = useGetListProductCategory(null);
    const [listCategory, setListCategory] = useState([]);
    const [imageUrl, setImageUrl] = useState();
    const [imageFile, setImageFile] = useState(null);
    const [form] = Form.useForm();
    const formData = new FormData();

    useEffect(() => {
        if (isLoadingCategory || isLoadingCategories || !categories) return;
        if (category?.data) {
            let productCategory = category.data;
            form.setFieldsValue({
                name: productCategory.name,
                slug: productCategory.slug,
                parentId: productCategory.parentId,
                status: productCategory.status,
            });
            setImageUrl(productCategory.image);
            setListCategory(
                categories?.data?.items.filter((item) => item.id !== productCategory.id),
            );
        } else {
            setListCategory(categories?.data?.items);
        }
    }, [isLoadingCategory, isLoadingCategories, category]);

    const mutationCreate = useCreateProductCategory({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Thể loại sản phẩm đã được thêm',
            });
            navigate(config.routes.admin.product_category);
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: 'Có lỗi xảy ra khi thêm thể loại sản phẩm',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const mutationUpdate = useUpdateProductCategory({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Thể loại sản phẩm đã được chỉnh sửa',
            });
            navigate(config.routes.admin.product_category);
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa thể loại sản phẩm',
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
        formData.append('slug', form.getFieldValue('slug'));
        form.getFieldValue('parentId') &&
            formData.append('parentId', form.getFieldValue('parentId'));
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
        formData.append('slug', form.getFieldValue('slug'));
        form.getFieldValue('parentId') &&
            formData.append('parentId', form.getFieldValue('parentId'));
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

    return (
        <div className="form-container">
            <div className="flex items-center gap-[1rem]">
                <FontAwesomeIcon
                    onClick={() => navigate(config.routes.admin.product_category)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">
                    {id ? 'Cập nhật thông tin' : 'Thêm danh mục sản phẩm'}
                </h1>
            </div>
            <div className="flex items-center justify-start rounded-xl shadow text-[1.6rem] text-black gap-[1rem] bg-white p-7">
                <div className="flex flex-col gap-[1rem]">
                    <p>ID</p>
                    <code className="bg-blue-100 p-2">{category?.data?.id || '_'}</code>
                </div>
                <div className="flex flex-col gap-[1rem]">
                    <p>Ngày tạo</p>
                    <code className="bg-blue-100 p-2">
                        {category?.data?.createdAt
                            ? new Date(category?.data?.createdAt).toLocaleString()
                            : '__/__/____'}
                    </code>
                </div>
                <div className="flex flex-col gap-[1rem]">
                    <p>Ngày cập nhật</p>
                    <code className="bg-blue-100 p-2">
                        {category?.data?.updatedAt
                            ? new Date(category?.data?.updatedAt).toLocaleString()
                            : '__/__/____'}
                    </code>
                </div>
            </div>
            <div className="bg-white p-7 mt-5 rounded-xl shadow">
                <Form
                    name="employee-form"
                    layout="vertical"
                    form={form}
                    labelCol={{
                        span: 5,
                    }}
                >
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                label="Tên danh mục"
                                name="name"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên danh mục!',
                                    },
                                ]}
                            >
                                <Input onChange={(e) => form.setFieldValue('slug', slugify(e.target.value))}/>
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                label="Slug"
                                name="slug"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập slug!',
                                    },
                                ]}
                            >
                                <Input readOnly/>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item label="Danh mục cha" name="parentId" initialValue={null}>
                                <Select
                                    onChange={(v) => form.setFieldValue('parentId', v)}
                                    showSearch
                                    placeholder="--"
                                >
                                    {listCategory &&
                                        listCategory.map((c, i) => (
                                            <Option key={i} value={c.id}>
                                                {c.name}
                                            </Option>
                                        ))}
                                </Select>
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item label="Trạng thái" name="status">
                                <Select
                                    onChange={(v) => form.setFieldValue('status', v)}
                                    placeholder="--"
                                    defaultValue={true}
                                    showSearch
                                >
                                    <Option value={true}>Kích hoạt</Option>
                                    <Option value={false}>Vô hiệu lực</Option>
                                </Select>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item label="Hình ảnh" name="image">
                                <Upload
                                    name="image"
                                    listType="picture-circle"
                                    className="avatar-uploader flex justify-center"
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

export default ProductCategoryFormPage;
