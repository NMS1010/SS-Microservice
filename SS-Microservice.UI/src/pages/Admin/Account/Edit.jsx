import { Button, Form, Modal, Select } from 'antd';

const roleOptions = [
    {
        label: 'Quản trị viên',
        value: 'ADMIN',
    },
    {
        label: 'Nhân viên',
        value: 'STAFF',
    },
    {
        label: 'Người dùng',
        value: 'USER',
    },
];
const statusOptions = [
    {
        label: 'Kích hoạt',
        value: 'ACTIVE',
    },
    {
        label: 'Vô hiệu hoá',
        value: 'INACTIVE',
    },
];
function Edit({ isEditOpen, setIsEditOpen }) {
    return (
        <Modal
            title={<p className="text-center text-[2rem] mb-6">Chỉnh sửa vai trò tài khoản</p>}
            open={isEditOpen.open}
            onCancel={() => setIsEditOpen(false)}
            footer={[
                <Button type="primary" className="bg-red-500 text-white">
                    OK
                </Button>,
            ]}
        >
            <div>
                <Form
                    name="account"
                    labelCol={{
                        span: 5,
                    }}
                    wrapperCol={{
                        span: 16,
                    }}
                    initialValues={{
                        remember: true,
                    }}
                    autoComplete="off"
                >
                    <Form.Item label="Vai trò">
                        <Select
                            mode="multiple"
                            allowClear
                            style={{
                                width: '100%',
                            }}
                            placeholder="Chọn vai trò"
                            options={roleOptions}
                        />
                    </Form.Item>
                    <Form.Item label="Trạng thái">
                        <Select
                            allowClear
                            style={{
                                width: '100%',
                            }}
                            placeholder="Trạng thái"
                            options={statusOptions}
                        />
                    </Form.Item>
                </Form>
            </div>
        </Modal>
    );
}

export default Edit;
