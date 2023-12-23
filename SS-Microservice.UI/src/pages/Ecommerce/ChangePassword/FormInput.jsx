import { Button, Form, Input, Radio, notification } from 'antd';
import { NavLink } from 'react-router-dom';
import { useChangePassword } from '../../../hooks/api/useUserApi';
import { useState } from 'react';

function FormInput() {
    const [form] = Form.useForm();
    const [processing, setProcessing] = useState(false);
    const mutateChangePassword = useChangePassword({
        success: (data) => {
            notification.success({
                message: 'Đổi mật khẩu thành công',
            });
            form.resetFields();
        },
        error: (err) => {
            let description = 'Không thể đổi mật khẩu, vui lòng liên hệ Quản trị viên';
            let detail = err?.response?.data?.detail?.toLowerCase();

            if (detail?.includes('match')) {
                description = 'Xác nhận mật khẩu mới không khớp';
            } else if (detail?.includes('incorrect')) {
                description = 'Mật khẩu cũ không đúng, vui lòng thử lại';
            }

            notification.error({
                message: 'Đổi mật khẩu thất bại',
                description: description,
            });
        },
        mutate: (data) => {
            setProcessing(true);
        },
        settled: (data) => {
            setProcessing(false);
        },
    });
    const onHandleFormSubmit = async (values) => {
        const { password, newpassoword, confirmpassword } = values;
        await mutateChangePassword.mutateAsync({
            oldPassword: password,
            newPassword: newpassoword,
            confirmPassword: confirmpassword,
        });
    };

    return (
        <div className="flex items-center justify-center mt-[8rem] form-container w-full">
            <Form
                name="my-profile"
                labelCol={{
                    span: 8,
                }}
                wrapperCol={{
                    span: 16,
                }}
                initialValues={{
                    remember: true,
                }}
                form={form}
                autoComplete="off"
                onFinish={onHandleFormSubmit}
            >
                <div>
                    <Form.Item
                        label="Mật khẩu hiện tại"
                        name="password"
                        className="text-[1.6rem]"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập mật khẩu hiện tại của bạn!',
                            },
                        ]}
                    >
                        <Input.Password
                            className="text-[1.6rem] w-[32.6rem] h-[3.5rem] bg-white rounded-[0.3rem] shadow outline-none"
                            placeholder="Nhập mật khẩu hiện tại"
                            defaultValue=""
                        />
                    </Form.Item>
                    <Form.Item
                        label="Mật khẩu mới"
                        name="newpassoword"
                        className=" text-[1.6rem]"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập mật khẩu mới!',
                            },
                        ]}
                    >
                        <Input.Password
                            className="text-[1.6rem] w-[32.6rem] h-[3.5rem] bg-white rounded-[0.3rem] shadow outline-none"
                            placeholder="Nhập mật khẩu mới"
                            defaultValue=""
                        />
                    </Form.Item>
                    <Form.Item
                        label="Xác nhận mật khẩu"
                        name="confirmpassword"
                        className="text-[1.6rem]"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập mật khẩu xác nhận!',
                            },
                            ({ getFieldValue }) => ({
                                validator(_, value) {
                                    if (!value || getFieldValue('newpassoword') === value) {
                                        return Promise.resolve();
                                    }
                                    return Promise.reject(
                                        new Error('Mật khẩu xác nhận không khớp!'),
                                    );
                                },
                            }),
                        ]}
                    >
                        <Input.Password
                            className="text-black text-[1.6rem] w-[32.6rem] h-[3.5rem] bg-white rounded-[0.3rem] shadow"
                            placeholder="Nhập mật khẩu xác nhận"
                            defaultValue={''}
                        />
                    </Form.Item>
                </div>
                <div className="text-center ml-[5rem] md:mt-[4rem]">
                    <Button
                        className="submit-btn text-white text-[1.8rem] h-[3rem] leading-4 pb-[0.5rem] px-[4rem] rounded-[5px] border-none"
                        htmlType="submit"
                        loading={processing}
                    >
                        Xác nhận
                    </Button>
                </div>
            </Form>
        </div>
    );
}

export default FormInput;
