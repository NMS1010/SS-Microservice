import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import {
    Button,
    Col,
    DatePicker,
    Form,
    Input,
    InputNumber,
    Row,
    Select,
    Switch,
    Tabs,
    Upload,
    notification,
} from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useNavigate, useParams } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useEffect, useRef, useState } from 'react';
import './sale.scss';
import config from '../../../config';
import SearchingInput from './SearchingInput';
import CategoryItem from './CategoryItem';
import dayjs from 'dayjs';
import {
    useCreateSale,
    useGetListProductCategory,
    useGetSale,
    useUpdateSale,
} from '../../../hooks/api';
import slugify from '../../../utils/slugify';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};

function SaleFormPage() {
    const navigate = useNavigate();
    let { id } = useParams();
    const { isLoading: isLoadingSale, data: dSale } = id
        ? useGetSale(id)
        : { isLoading: null, data: null };
    const { isLoading: isLoadingProductCategory, data: dProductCategory } =
        useGetListProductCategory(null);
    const [form] = Form.useForm();
    const formData = new FormData();
    const [imageUrl, setImageUrl] = useState();
    const [imageFile, setImageFile] = useState(null);
    const [checked, setChecked] = useState(false);
    const [categories, setCategories] = useState([]);
    const [chosenCategoryList, setChosenCategoryList] = useState([]);
    const [processing, setProcessing] = useState(false);

    useEffect(() => {
        if (isLoadingProductCategory || isLoadingSale) return;
        if (dSale) {
            let sale = dSale?.data;
            form.setFieldsValue({
                name: sale.name,
                slug: sale.slug,
                promotionalPercent: sale.promotionalPercent,
                description: sale.description,
                daterange: [
                    dayjs(new Date(sale.startDate).toLocaleString()),
                    dayjs(new Date(sale.endDate).toLocaleString()),
                ],
            });
            setChecked(sale.all);
            setImageUrl(sale.image);
            setChosenCategoryList(sale.productCategories);
        }
        setCategories(dProductCategory?.data?.items);
    }, [isLoadingProductCategory, isLoadingSale, dSale, dProductCategory]);

    const handleChange = (info) => {
        if (info.file) {
            setImageFile(info.file);
            getBase64(info.file, (url) => {
                setImageUrl(url);
            });
        }
    };

    const mutationCreate = useCreateSale({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Đợt giảm giá đã được thêm',
            });
            navigate(config.routes.admin.sale);
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: 'Có lỗi xảy ra khi thêm đợt giảm giá',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const mutationEdit = useUpdateSale({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Đợt giảm giá đã được chỉnh sửa',
            });
            navigate(config.routes.admin.sale);
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa đợt giảm giá',
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
        const daterange = form.getFieldValue('daterange');
        formData.append('name', form.getFieldValue('name'));
        formData.append('description', form.getFieldValue('description'));
        formData.append('image', imageFile);
        formData.append('startDate', daterange[0].$d.toLocaleString());
        formData.append('endDate', daterange[1].$d.toLocaleString());
        formData.append('promotionalPercent', form.getFieldValue('promotionalPercent'));
        formData.append('slug', form.getFieldValue('slug'));
        formData.append('all', !!form.getFieldValue('all'));
        chosenCategoryList
            .map((item) => item.id)
            .forEach((id) => formData.append('categoryIds', id));
        await mutationCreate.mutateAsync(formData);
    };

    const onEdit = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        const daterange = form.getFieldValue('daterange');
        formData.append('name', form.getFieldValue('name'));
        formData.append('description', form.getFieldValue('description'));
        formData.append('image', imageFile);
        formData.append('startDate', daterange[0].$d.toLocaleString());
        formData.append('endDate', daterange[1].$d.toLocaleString());
        formData.append('promotionalPercent', form.getFieldValue('promotionalPercent'));
        formData.append('slug', form.getFieldValue('slug'));
        formData.append('all', !!form.getFieldValue('all'));
        chosenCategoryList
            .map((item) => item.id)
            .forEach((id) => formData.append('categoryIds', id));
        await mutationEdit.mutateAsync({
            id: id,
            body: formData,
        });
    };

    return (
        <div className="form-container w-full">
            <div className="flex items-center gap-[1rem]">
                <FontAwesomeIcon
                    onClick={() => navigate(config.routes.admin.sale)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">{id ? 'Cập nhật thông tin' : 'Thêm khuyến mãi'}</h1>
            </div>
            <div className="flex items-center justify-start rounded-xl shadow text-[1.6rem] text-black gap-[1rem] bg-white p-7">
                <div className="flex flex-col gap-[1rem]">
                    <p>ID</p>
                    <code className="bg-blue-100 p-2">{dSale?.data?.id || '_'}</code>
                </div>
                <div className="flex flex-col gap-[1rem]">
                    <p>Ngày tạo</p>
                    <code className="bg-blue-100 p-2">
                        {dSale?.data?.createdAt
                            ? new Date(dSale?.data?.createdAt).toLocaleString()
                            : '__/__/____'}
                    </code>
                </div>
                <div className="flex flex-col gap-[1rem]">
                    <p>Ngày cập nhật</p>
                    <code className="bg-blue-100 p-2">
                        {dSale?.data?.updatedAt
                            ? new Date(dSale?.data?.updatedAt).toLocaleString()
                            : '__/__/____'}
                    </code>
                </div>
            </div>
            <Form
                name="sale-form"
                layout="vertical"
                className="my-5"
                form={form}
                labelCol={{
                    span: 5,
                }}
            >
                <Row gutter={16}>
                    <Col span={14}>
                        <div className="bg-white p-5 rounded-xl shadow">
                            <Tabs
                                type="card"
                                className=""
                                items={[
                                    {
                                        label: 'Danh mục',
                                        key: 1,
                                        children: (
                                            <Form.Item
                                                label="Thêm danh mục sản phẩm"
                                                name="category"
                                            >
                                                <SearchingInput
                                                    setChosenList={setChosenCategoryList}
                                                    chosenList={chosenCategoryList}
                                                    itemList={categories}
                                                    placeholder={'Nhập danh mục sản phẩm'}
                                                    ItemComponent={CategoryItem}
                                                />
                                            </Form.Item>
                                        ),
                                    },
                                ]}
                            />
                        </div>
                    </Col>
                    <Col span={10}>
                        <div className="bg-white p-5 rounded-xl shadow">
                            <Form.Item
                                label="Tên khuyến mãi"
                                name="name"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập giá trị!',
                                    },
                                ]}
                            >
                                <Input  onChange={(e) => form.setFieldValue('slug', slugify(e.target.value))}/>
                            </Form.Item>
                            <Form.Item
                                label="Slug"
                                name="slug"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập giá trị!',
                                    },
                                ]}
                            >
                                <Input readOnly/>
                            </Form.Item>
                            <Form.Item
                                label="Phần trăm giảm giá"
                                name="promotionalPercent"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập giá trị!',
                                    },
                                ]}
                            >
                                <InputNumber />
                            </Form.Item>
                            <Form.Item
                                label="Khoảng thời gian"
                                name="daterange"
                                initialValue={[dayjs(new Date()), dayjs(new Date())]}
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập giá trị!',
                                    },
                                ]}
                            >
                                <DatePicker.RangePicker showTime format="YYYY-MM-DD HH:mm:ss" />
                            </Form.Item>
                            <Form.Item
                                label="Mô tả"
                                name="description"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập giá trị!',
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
                                    placeholder="Mô tả"
                                />
                            </Form.Item>
                            <Form.Item label="Áp dụng cho tất cả sản phẩm" name="all">
                                <Switch
                                    checked={checked}
                                    onChange={() =>
                                        setChecked((prev) => {
                                            if (!prev) {
                                                setChosenCategoryList([]);
                                            }
                                            return !prev;
                                        })
                                    }
                                />
                            </Form.Item>
                            <Form.Item label="Hình ảnh" name="image">
                                <Upload
                                    name="image"
                                    listType="picture-card"
                                    className="flex justify-center"
                                    showUploadList={false}
                                    beforeUpload={() => false}
                                    onChange={handleChange}
                                >
                                    {imageUrl ? (
                                        <img src={imageUrl} alt="category" className="w-full" />
                                    ) : (
                                        <div>
                                            <PlusOutlined />
                                            <div className="mt-[0.8rem]">Tải ảnh lên</div>
                                        </div>
                                    )}
                                </Upload>
                            </Form.Item>
                            <div className="flex items-center justify-end">
                                <Button
                                    loading={processing}
                                    onClick={id ? onEdit : onAdd}
                                    htmlType="submit"
                                    className="bg-[--primary-color] text-white min-w-[100px] border-none"
                                >
                                    {id ? 'Cập nhật' : 'Thêm'}
                                </Button>
                            </div>
                        </div>
                    </Col>
                </Row>
            </Form>
        </div>
    );
}

export default SaleFormPage;
