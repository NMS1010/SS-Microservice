import {
    Button,
    Col,
    DatePicker,
    Form,
    Image,
    Input,
    Row,
    Select,
    Tag,
    Upload,
    notification,
} from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useEffect, useState } from 'react';
import config from '../../../config';
import { useChangePassword, useUpdateUser } from '../../../hooks/api';
import { useNavigate } from 'react-router-dom';
import { clearToken } from '../../../utils/storage';
import dayjs from 'dayjs';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};

function ProfileForm({ user, refetch }) {
    const navigate = useNavigate();
    const onLogout = () => {
        clearToken();
        navigate(config.routes.web.login);
    };
    const [imageUrl, setImageUrl] = useState();
    const [imageFile, setImageFile] = useState(null);
    const [processing, setProcessing] = useState(false);
    const [form] = Form.useForm();
    const formData = new FormData();

    useEffect(() => {
        setImageUrl(user?.avatar);
        form.setFieldsValue({
            firstName: user.firstName,
            lastName: user.lastName,
            email: user.email,
            phone: user.phone,
        });
    }, [user]);

    const handleChange = (info) => {
        if (info.file) {
            setImageFile(info.file);
            getBase64(info.file, (url) => {
                setImageUrl(url);
            });
        }
    };

    const mutationUpdate = useUpdateUser({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Thông tin người dùng đã được chỉnh sửa',
            });
            refetch();
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa thông tin người dùng',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const muationChangePassword = useChangePassword({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Mật khẩu người dùng đã được chỉnh sửa',
            });
            onLogout();
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa mật khẩu người dùng',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const onEdit = async () => {
        formData.append('firstName', form.getFieldValue('firstName'));
        formData.append('lastName', form.getFieldValue('lastName'));
        formData.append('phone', form.getFieldValue('phone'));
        formData.append('gender', form.getFieldValue('gender'));
        formData.append('dob', form.getFieldValue('dob').$d.toISOString());
        formData.append('avatar', imageFile);
        await mutationUpdate.mutateAsync(formData);
    };

    const onChangePassword = async () => {
        await muationChangePassword.mutateAsync({
            oldPassword: form.getFieldValue('oldPassword'),
            newPassword: form.getFieldValue('newPassword'),
            confirmPassword: form.getFieldValue('confirmPassword'),
        });
    };

    return (
        <div className="form-container w-full">
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
                <div className="bg-white p-7 mt-5 rounded-xl shadow">
                    <div className="mb-[1rem]">
                        <h2 className="text-[2rem] font-bold text-[--text-color]">
                            Thông tin cơ bản
                        </h2>
                    </div>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item label="Hình đại diện" name="image">
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
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                label="Họ"
                                name="firstName"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên thông tin!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                label="Tên"
                                name="lastName"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên thông tin!',
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
                                label="Email"
                                name="email"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên thông tin!',
                                    },
                                    {
                                        type: 'email',
                                        message: 'Nhập đúng định dạng email',
                                    },
                                ]}
                            >
                                <Input readOnly />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={8}>
                            <Form.Item label="Số điện thoại" name="phone">
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={8}>
                            <Form.Item label="Giới tính" name="gender" initialValue={'MALE'}>
                                <Select
                                    onChange={(v) => form.setFieldValue('status', v)}
                                    placeholder="Chọn giới tính"
                                    showSearch
                                >
                                    <Option value={'MALE'}>Nam</Option>
                                    <Option value={'FEMALE'}>Nữ</Option>
                                    <Option value={'OTHER'}>Khác</Option>
                                </Select>
                            </Form.Item>
                        </Col>
                        <Col span={8}>
                            <Form.Item
                                label="Ngày sinh"
                                name="dob"
                                initialValue={dayjs(new Date(user.dob).toLocaleString())}
                            >
                                <DatePicker
                                    className="w-full h-[33px]"
                                    // defaultValue={dayjs(new Date(user.dob).toLocaleString())}
                                    format="YYYY-MM-DD"
                                />
                            </Form.Item>
                        </Col>
                    </Row>
                    <div className="flex justify-between items-center gap-[1rem]">
                        <Button className="min-w-[10%]">Đặt lại</Button>
                        <Button
                            onClick={onEdit}
                            loading={processing}
                            className="bg-blue-500 text-white min-w-[10%]"
                        >
                            Cập nhật
                        </Button>
                    </div>
                </div>
                <div className="bg-white p-7 mt-5 rounded-xl shadow">
                    <div className="mb-[1rem]">
                        <h2 className="text-[2rem] font-bold text-[--text-color]">
                            Thông tin bảo mật
                        </h2>
                    </div>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item label="Mật khẩu hiện tại" name="oldPassword">
                                <Input.Password />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item label="Mật khẩu mới" name="newPassword">
                                <Input.Password />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item label="Xác nhận mật khẩu mới" name="confirmPassword">
                                <Input.Password />
                            </Form.Item>
                        </Col>
                    </Row>
                    <div className="flex justify-between items-center gap-[1rem]">
                        <Button className="min-w-[10%]">Đặt lại</Button>
                        <Button
                            onClick={onChangePassword}
                            loading={processing}
                            className="bg-blue-500 text-white min-w-[10%]"
                        >
                            Cập nhật
                        </Button>
                    </div>
                </div>
            </Form>
        </div>
    );
}

export default ProfileForm;
