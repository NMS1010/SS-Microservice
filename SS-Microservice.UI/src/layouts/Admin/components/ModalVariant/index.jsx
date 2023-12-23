import { Button, Form, Input, InputNumber, Modal, Select } from 'antd';

function ModalVariant({ modalVariant, setModalVariant, handleVariant, loading }) {
    const [form] = Form.useForm();

    const handleSubmit = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        handleVariant(modalVariant.edit, {
            name: form.getFieldValue('name'),
            sku: form.getFieldValue('sku'),
            itemPrice: form.getFieldValue('itemPrice'),
            quantity: form.getFieldValue('quantity'),
            totalPrice: form.getFieldValue('itemPrice') * form.getFieldValue('quantity'),
            status: form.getFieldValue('status'),
        });
        form.resetFields();
        setModalVariant({ variant: null, edit: { index: null, isEdit: false }, isOpen: false });
    };

    if (modalVariant.edit.isEdit) {
        form.setFieldsValue({
            name: modalVariant.variant.name,
            sku: modalVariant.variant.sku,
            itemPrice: modalVariant.variant.itemPrice,
            quantity: modalVariant.variant.quantity,
            status: modalVariant.variant.status,
        });
    }

    return (
        <Modal
            title={<p className="text-center text-[2rem] mb-6">{modalVariant.title}</p>}
            open={modalVariant.isOpen}
            onCancel={() => {
                setModalVariant({
                    variant: null,
                    edit: { index: null, isEdit: false },
                    isOpen: false,
                });
                form.resetFields();
            }}
            footer={[
                <Button
                    loading={loading}
                    onClick={handleSubmit}
                    type="primary"
                    className={`${
                        modalVariant.edit.isEdit ? 'bg-yellow-500' : 'bg-[--primary-color]'
                    } text-white`}
                >
                    {modalVariant.edit.isEdit ? 'Cập nhật' : 'Thêm'}
                </Button>,
            ]}
        >
            <div className="form-container mx-auto w-full">
                <Form
                    initialValues={{
                        remember: true,
                    }}
                    form={form}
                    labelCol={{
                        span: 10,
                    }}
                    wrapperCol={{
                        span: 16,
                    }}
                    autoComplete="off"
                >
                    <Form.Item
                        label="Tên dạng sản phẩm"
                        name="name"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập tên dạng sản phẩm!',
                            },
                        ]}
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        label="Sku"
                        name="sku"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập sku cho sản phẩm!',
                            },
                        ]}
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        label="Giá 1 sản phẩm"
                        name="itemPrice"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập giá của 1 sản phẩm!',
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
                    <Form.Item
                        label="Số lượng"
                        name="quantity"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập số lượng!',
                            },
                        ]}
                    >
                        <InputNumber className="w-full" min={1} />
                    </Form.Item>
                    <Form.Item label="Trạng thái" name="status">
                        <Select
                            onChange={(v) => form.setFieldValue('status', v)}
                            placeholder="--"
                            showSearch
                        >
                            <Option value={'ACTIVE'}>Kích hoạt </Option>
                            <Option value={'INACTIVE'}>Vô hiệu hóa</Option>
                        </Select>
                    </Form.Item>
                </Form>
            </div>
        </Modal>
    );
}

export default ModalVariant;
