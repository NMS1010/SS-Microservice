import { Button, Form, Input, InputNumber, Modal } from 'antd';

function Edit({ isImportProduct, setIsImportProduct, importProduct, loading }) {
    const [form] = Form.useForm();
    form.setFieldValue('quantity', isImportProduct.docket.quantity);
    form.setFieldValue('actualInventory', isImportProduct.docket.actualInventory);
    form.setFieldValue('note', isImportProduct.docket.note);

    const handleEdit = () => {
        importProduct({
            docket: {
                ...isImportProduct.docket,
                quantity: form.getFieldValue('quantity'),
                actualInventory: form.getFieldValue('actualInventory'),
                note: form.getFieldValue('note'),
            },
            isEdit: false,
        });
    };

    return (
        <Modal
            title={<p className="text-center text-[2rem] mb-6">Cập nhật kho hàng</p>}
            open={isImportProduct.isEdit}
            onCancel={() =>
                setIsImportProduct({
                    docket: {
                        productId: 0,
                        quantity: 0,
                        actualInventory: 0,
                        note: null,
                    },
                    isEdit: false,
                })
            }
            footer={[
                <Button
                    loading={loading}
                    onClick={() => handleEdit()}
                    type="primary"
                    className="bg-yellow-500 text-white"
                >
                    Cập nhật
                </Button>,
            ]}
        >
            <div className="form-container">
                <Form
                    form={form}
                    labelCol={{
                        span: 12,
                    }}
                    initialValues={{
                        remember: true,
                    }}
                    autoComplete="off"
                >
                    <Form.Item label="Số lượng nhập vào" name="quantity">
                        <InputNumber
                            min={0}
                            style={{
                                width: 250,
                            }}
                        />
                    </Form.Item>
                    <Form.Item label="Số lượng bán thực tế" name="actualInventory">
                        <InputNumber
                            min={0}
                            style={{
                                width: 250,
                            }}
                        />
                    </Form.Item>
                    <Form.Item label="Ghi chú" name="note">
                        <Input.TextArea
                            showCount
                            maxLength={100}
                            style={{
                                height: 100,
                                width: 250,
                                resize: 'none',
                            }}
                            placeholder="Ghi chú"
                        />
                    </Form.Item>
                </Form>
            </div>
        </Modal>
    );
}

export default Edit;
