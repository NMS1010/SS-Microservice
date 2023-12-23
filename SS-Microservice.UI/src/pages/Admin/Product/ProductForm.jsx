import {
    Button,
    Col,
    Form,
    Input,
    Row,
    Select,
    Upload,
    InputNumber,
    Table,
    notification,
} from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useNavigate, useParams } from 'react-router-dom';
import { faChevronLeft, faEdit, faXmarkSquare } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { IconSquareRoundedX } from '@tabler/icons-react';
import { useEffect, useState } from 'react';
import './product.scss';
import config from '../../../config';
import {
    useCreateProduct,
    useGetListBrand,
    useGetListProductCategory,
    useGetListUnit,
    useGetProduct,
    useUpdateProduct,
} from '../../../hooks/api';
import ModalVariant from '../../../layouts/Admin/components/ModalVariant';
import slugify from '../../../utils/slugify';
import { CKEditor } from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};

const columns = [
    {
        title: 'Tên dạng sản phẩm bán ra',
        dataIndex: 'name',
        key: 'name',
    },
    {
        title: 'Sku',
        dataIndex: 'sku',
        key: 'sku',
    },
    {
        title: 'Giá 1 sản phẩm',
        dataIndex: 'itemPrice',
        key: 'itemPrice',
    },
    {
        title: 'Số lượng',
        dataIndex: 'quantity',
        key: 'quantity',
    },
    {
        title: 'Tổng giá bán',
        dataIndex: 'totalPrice',
        key: 'totalPrice',
    },
    {
        title: 'Hành động',
        dataIndex: 'action',
        key: 'action',
    },
];

function transformVariant(variants, setVariants, setModalVariant) {
    return variants?.map((variant, index) => {
        return {
            key: index,
            name: variant.name,
            sku: variant.sku,
            itemPrice: variant.itemPrice,
            quantity: variant.quantity,
            totalPrice: variant.totalPrice,
            action: (
                <div className="action-btn flex gap-3">
                    <Button
                        onClick={() =>
                            setModalVariant({
                                title: 'Chỉnh sửa dạng sản phẩm bán ra',
                                variant: variant,
                                edit: {
                                    index: index,
                                    isEdit: true,
                                },
                                isOpen: true,
                            })
                        }
                        className="text-blue-500 border border-blue-500"
                    >
                        <FontAwesomeIcon icon={faEdit} />
                    </Button>

                    <Button
                        onClick={() => {
                            if (variants.length === 1) return;
                            setVariants(variants.filter((variant, i) => i !== index));
                        }}
                        className={'text-red-500 border border-red-500'}
                    >
                        <FontAwesomeIcon icon={faXmarkSquare} />
                    </Button>
                </div>
            ),
        };
    });
}

function ProductFormPage() {
    let { id } = useParams();
    const navigate = useNavigate();
    const [processing, setProcessing] = useState(false);
    const [form] = Form.useForm();
    const formData = new FormData();
    const [images, setImages] = useState([
        {
            id: null,
            inputRef: null,
            imageUrl: null,
            imageFile: null,
        },
    ]);
    const [variants, setVariants] = useState([]);
    const [modalVariant, setModalVariant] = useState({
        title: null,
        variant: null,
        edit: {
            index: null,
            isEdit: false,
        },
        isOpen: false,
    });
    const [rowVariants, setRowVariants] = useState([]);
    const [units, setUnits] = useState([]);
    const [categories, setCategories] = useState([]);
    const [brands, setBrands] = useState([]);
    const { isLoading: isLoadingProduct, data: dProduct } = id
        ? useGetProduct(id)
        : { isLoading: null, data: null };
    const { isLoading: isLoadingUnits, data: dUnits } = useGetListUnit();
    const { isLoading: isLoadingCategories, data: dCategories } = useGetListProductCategory();
    const { isLoading: isLoadingBrands, data: dBrands } = useGetListBrand();

    useEffect(() => {
        if (isLoadingUnits || isLoadingCategories || isLoadingBrands || isLoadingProduct) return;
        if (dProduct) {
            let product = dProduct?.data;
            form.setFieldsValue({
                name: product.name,
                cost: product.cost,
                unitId: product.unit.id,
                code: product.code,
                slug: product.slug,
                shortDescription: product.shortDescription,
                description: product.description,
                categoryId: product.category.id,
                brandId: product.brand.id,
                status: product.status,
            });
        }
        setUnits(dUnits?.data?.items);
        setCategories(dCategories?.data?.items);
        setBrands(dBrands?.data?.items);
        setRowVariants(transformVariant(variants));
    }, [isLoadingUnits, isLoadingCategories, isLoadingBrands, isLoadingProduct]);

    useEffect(() => {
        setRowVariants(transformVariant(variants, setVariants, setModalVariant));
    }, [variants]);

    const handleImage = (info, index) => {
        if (info.file) {
            getBase64(info.file, (url) => {
                setImages((prev) => {
                    const image = prev.filter((item, i) => i === index)[0];
                    image.id = index;
                    image.imageUrl = url;
                    image.imageFile = info.fileList[0].originFileObj;
                    return [...prev];
                });
            });
        }
    };

    const addImage = () => {
        setImages((prev) => [
            ...prev,
            {
                id: null,
                inputRef: null,
                imageUrl: null,
                imageFile: null,
            },
        ]);
    };

    const deleteImage = (index) => {
        if (images.length === 1) return;
        setImages(images.filter((image, i) => i !== index));
    };

    const handleVariant = (edit, variant) => {
        if (edit.isEdit) {
            setVariants((prev) => {
                return prev.map((v, i) => {
                    if (i === edit.index) return variant;
                    return v;
                });
            });
        } else {
            setVariants([...variants, variant]);
        }
    };

    const mutationCreate = useCreateProduct({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Sản phẩm đã được thêm',
            });
            navigate(config.routes.admin.product);
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: 'Có lỗi xảy ra khi thêm sản phẩm',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const mutationUpdate = useUpdateProduct({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Sản phẩm đã được chỉnh sửa',
            });
            navigate(config.routes.admin.product);
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa sản phẩm',
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
        formData.append('categoryId', form.getFieldValue('categoryId'));
        formData.append('brandId', form.getFieldValue('brandId'));
        formData.append('unitId', form.getFieldValue('unitId'));
        formData.append('shortDescription', form.getFieldValue('shortDescription'));
        formData.append('description', form.getFieldValue('description'));
        formData.append('code', form.getFieldValue('code'));
        formData.append('quantity', form.getFieldValue('quantity'));
        formData.append('slug', form.getFieldValue('slug'));
        formData.append('cost', form.getFieldValue('cost'));
        images.forEach((image) => formData.append('productImages', image.imageFile));
        variants.forEach((variant) => formData.append('variants', JSON.stringify(variant)));
        console.log([...formData]);
        await mutationCreate.mutateAsync(formData);
    };

    const onEdit = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        formData.append('name', form.getFieldValue('name'));
        formData.append('categoryId', form.getFieldValue('categoryId'));
        formData.append('brandId', form.getFieldValue('brandId'));
        formData.append('unitId', form.getFieldValue('unitId'));
        formData.append('shortDescription', form.getFieldValue('shortDescription'));
        formData.append('description', form.getFieldValue('description'));
        formData.append('code', form.getFieldValue('code'));
        formData.append('slug', form.getFieldValue('slug'));
        formData.append('cost', form.getFieldValue('cost'));
        formData.append('status', form.getFieldValue('status'));

        console.log(form.getFieldValue('description'));

        await mutationUpdate.mutateAsync({
            id: id,
            body: formData,
        });
    };

    return (
        <div className="form-container">
            <div className="flex items-center gap-[1rem]">
                <FontAwesomeIcon
                    onClick={() => navigate(config.routes.admin.product)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">{id ? 'Cập nhật thông tin' : 'Thêm sản phẩm'}</h1>
            </div>
            <div className="flex items-center justify-start rounded-xl shadow text-[1.6rem] text-black gap-[1rem] bg-white p-7">
                <div className="flex flex-col gap-[1rem]">
                    <p>ID</p>
                    <code className="bg-blue-100 p-2">{dProduct?.data?.id || '_'}</code>
                </div>
                <div className="flex flex-col gap-[1rem]">
                    <p>Ngày tạo</p>
                    <code className="bg-blue-100 p-2">
                        {dProduct?.data?.createdAt
                            ? new Date(dProduct?.data?.createdAt).toLocaleString()
                            : '__/__/____'}
                    </code>
                </div>
                <div className="flex flex-col gap-[1rem]">
                    <p>Ngày cập nhật</p>
                    <code className="bg-blue-100 p-2">
                        {dProduct?.data?.updatedAt
                            ? new Date(dProduct?.data?.updatedAt).toLocaleString()
                            : '__/__/____'}
                    </code>
                </div>
            </div>
            <div className="bg-white p-7 mt-5 rounded-xl shadow">
                <Form
                    name=""
                    layout="vertical"
                    form={form}
                    labelCol={{
                        span: 5,
                    }}
                >
                    <div className="mb-[1rem]">
                        <h2 className="text-[2rem] font-bold text-[--text-color]">
                            Thông tin cơ bản
                        </h2>
                        <span className="text-[1.2rem] text-[--text-color]">
                            Một số thông tin chung
                        </span>
                    </div>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                label="Tên sản phẩm"
                                name="name"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên sản phẩm!',
                                    },
                                ]}
                            >
                                <Input
                                    onChange={(e) =>
                                        form.setFieldValue('slug', slugify(e.target.value))
                                    }
                                />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Form.Item
                                label="Giá vốn"
                                name="cost"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập giá vốn!',
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
                        <Col span={6}>
                            <Form.Item
                                label="Đơn vị tính"
                                name="unitId"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Chọn đơn sản phẩm!',
                                    },
                                ]}
                            >
                                <Select
                                    onChange={(v) => form.setFieldValue('unitId', v)}
                                    placeholder="--"
                                    showSearch
                                >
                                    {units &&
                                        units.map((unit) => (
                                            <Option value={unit.id}>{unit.name}</Option>
                                        ))}
                                </Select>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                label="Mã sản phẩm"
                                name="code"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập mã sản phẩm!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                label="Slug của sản phẩm"
                                name="slug"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập slug của sản phẩm!',
                                    },
                                ]}
                            >
                                <Input readOnly />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item
                                label="Mô tả ngắn của sản phẩm"
                                name="shortDescription"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập mô tả ngắn gọn sản phẩm!',
                                    },
                                ]}
                            >
                                <Input.TextArea
                                    showCount
                                    maxLength={100}
                                    style={{
                                        height: 60,
                                        resize: 'none',
                                    }}
                                    placeholder="Mô tả ngắn"
                                />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item
                                label="Mô tả của sản phẩm"
                                name="description"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập mô tả sản phẩm!',
                                    },
                                ]}
                            >
                                <CKEditor
                                    editor={ClassicEditor}
                                    data={dProduct?.data?.description}
                                    onReady={(editor) => {
                                        editor.editing.view.change((write) => {
                                            write.setStyle(
                                                'height',
                                                '200px',
                                                editor.editing.view.document.getRoot(),
                                            );
                                        });
                                    }}
                                    onChange={(e, editor) =>
                                        form.setFieldValue('description', editor.getData())
                                    }
                                />
                            </Form.Item>
                        </Col>
                    </Row>
                    {!id && (
                        <>
                            <div className="mb-[1rem] flex items-center justify-between">
                                <div>
                                    <h2 className="text-[2rem] font-bold text-[--text-color]">
                                        Dạng bán ra của sản phẩm
                                    </h2>
                                    <span className="text-[1.2rem] text-[--text-color]">
                                        Thông tin về các dạng bán ra của sản phẩm
                                    </span>
                                </div>
                                <Button
                                    onClick={() =>
                                        setModalVariant({
                                            title: 'Thêm dạng sản phẩm bán ra',
                                            variant: null,
                                            edit: {
                                                index: null,
                                                isEdit: false,
                                            },
                                            isOpen: true,
                                        })
                                    }
                                    className="border-[--primary-color] text-[--primary-color] hover:bg-[--background-item-menu-color]"
                                >
                                    Thêm
                                </Button>
                            </div>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Table
                                        pagination={false}
                                        columns={columns}
                                        dataSource={rowVariants}
                                    />
                                </Col>
                            </Row>
                        </>
                    )}
                    {!id && (
                        <>
                            <div className="mb-[1rem] mt-[2rem] flex items-center justify-between">
                                <div>
                                    <h2 className="text-[2rem] font-bold text-[--text-color]">
                                        Hình ảnh của sản phẩm
                                    </h2>
                                    <span className="text-[1.2rem] text-[--text-color]">
                                        Thêm danh sách hình ảnh sản phẩm
                                    </span>
                                </div>
                                <Button
                                    onClick={addImage}
                                    className="border-[--primary-color] text-[--primary-color] hover:bg-[--background-item-menu-color]"
                                >
                                    Thêm
                                </Button>
                            </div>
                            <Row gutter={16}>
                                <Col span={24}>
                                    <Form.Item name="images">
                                        <div className="flex items-center justify-start overflow-auto">
                                            {images.map((image, index) => (
                                                <div className="flex flex-col items-center">
                                                    <Upload
                                                        name="image"
                                                        listType="picture-card"
                                                        showUploadList={false}
                                                        beforeUpload={() => false}
                                                        onChange={(info) =>
                                                            handleImage(info, index)
                                                        }
                                                    >
                                                        {image.imageUrl ? (
                                                            <img
                                                                src={image.imageUrl}
                                                                alt="product"
                                                                className="w-full rounded-full"
                                                            />
                                                        ) : (
                                                            <div ref={image.inputRef}>
                                                                <PlusOutlined />
                                                                <div className="mt-[0.8rem]">
                                                                    Tải ảnh lên
                                                                </div>
                                                            </div>
                                                        )}
                                                    </Upload>
                                                    <Button
                                                        onClick={() => deleteImage(index)}
                                                        className="mb-[2rem] text-red-500 border-0"
                                                    >
                                                        <IconSquareRoundedX />
                                                    </Button>
                                                </div>
                                            ))}
                                        </div>
                                    </Form.Item>
                                </Col>
                            </Row>
                        </>
                    )}
                    <div className="mb-[1rem]">
                        <h2 className="text-[2rem] font-bold text-[--text-color]">
                            Thông tin bổ sung
                        </h2>
                        <span className="text-[1.2rem] text-[--text-color]">
                            Một số thông tin thêm
                        </span>
                    </div>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                label="Danh mục sản phẩm"
                                name="categoryId"
                                rules={[
                                    {
                                        required: true,
                                        message: 'chọn danh mục sản phẩm!',
                                    },
                                ]}
                            >
                                <Select
                                    onChange={(v) => form.setFieldValue('categoryId', v)}
                                    placeholder="--"
                                    showSearch
                                >
                                    {categories &&
                                        categories.map((category) => (
                                            <Option value={category.id}>{category.name}</Option>
                                        ))}
                                </Select>
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                label="Thương hiệu"
                                name="brandId"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Chọn thương hiệu sản phẩm!',
                                    },
                                ]}
                            >
                                <Select
                                    onChange={(v) => form.setFieldValue('brandId', v)}
                                    placeholder="--"
                                    showSearch
                                >
                                    {brands &&
                                        brands.map((brand) => (
                                            <Option value={brand.id}>{brand.name}</Option>
                                        ))}
                                </Select>
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item label="Trạng thái" name="status">
                                <Select
                                    onChange={(v) => form.setFieldValue('status', v)}
                                    defaultValue={'ACTIVE'}
                                    showSearch
                                >
                                    <Option value={'ACTIVE'}>Kích hoạt </Option>
                                    <Option value={'INACTIVE'}>Vô hiệu hóa</Option>
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
            {!id && (
                <ModalVariant
                    modalVariant={modalVariant}
                    setModalVariant={setModalVariant}
                    handleVariant={handleVariant}
                />
            )}
        </div>
    );
}

export default ProductFormPage;
