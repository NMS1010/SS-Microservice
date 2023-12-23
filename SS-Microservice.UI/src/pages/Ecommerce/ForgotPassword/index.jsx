import { Button, Form, Input, notification } from 'antd';
import { useState } from 'react';
import OTPInput from 'react-otp-input';
import { Navigate, useNavigate, useSearchParams } from 'react-router-dom';
import {
    useForgotPassword,
    useResendForgotPasswordOTPVerification,
    useResetPassword,
} from '../../../hooks/api';
import { getRoles, isTokenStoraged } from '../../../utils/storage';
import config from '../../../config';

function ForgotPasswordPage() {
    const navigate = useNavigate();
    const [form] = Form.useForm();
    const [emailVerify, setEmailVerify] = useState(false);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [otp, setOtp] = useState('');
    const [searchParams] = useSearchParams();
    const [processing, setProcessing] = useState(false);

    const onChange = (value) => {
        setOtp(value);
    };
    const [disabledSave, setDisabledSave] = useState(true);

    const handleFormChange = () => {
        const hasErrors = form.getFieldsError().some(({ errors }) => errors.length);
        setDisabledSave(hasErrors);
    };

    const mutateResendOTPVefirication = useResendForgotPasswordOTPVerification({
        success: (data) => {
            notification.success({
                message: 'Gửi thành công',
                description: 'Vui lòng kiểm tra email để lấy mã OTP',
            });
        },
        error: (err) => {
            let description = 'Đã có lỗi xảy ra, vui lòng liên hệ Quản trị viên';
            let detail = err?.response?.data?.detail?.toLowerCase();
            if (detail?.includes('email')) {
                description = 'Tài khoản với địa chỉ email này không tồn tại';
            } else if (detail?.includes('password')) {
                description = 'Vui lòng thực hiện theo thứ tự';
            }

            notification.error({
                message: 'Gửi thất bại',
                description: description,
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const mutateForgotPassword = useForgotPassword({
        success: () => {
            notification.success({
                message: 'Thành công',
                description: 'Vui lòng kiểm tra email để lấy mã OTP',
            });
            setEmailVerify(true);
            setDisabledSave(true);
            form.resetFields();
        },
        error: (err) => {
            let description = 'Không thể thực hiện thao tác, vui lòng liên hệ Quản trị viên';
            let detail = err?.response?.data?.detail?.toLowerCase();
            if (detail?.includes('email')) description = 'Email không tồn tại';
            else if (detail?.includes('verify'))
                description = 'Tài khoản vớI email này chưa được xác thực, vui lòng xác thực trước';
            notification.error({
                message: 'Có lỗi xảy ra',
                description: description,
            });
            setDisabledSave(true);
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const mutateResetPassword = useResetPassword({
        success: (data) => {
            notification.success({
                message: 'Đặt lại thành công',
                description: 'Bạn đã đặt lại mật khẩu thành công, vui lòng đăng nhập lại',
            });
            navigate(config.routes.web.login);
        },
        error: (err) => {
            let description = 'Đã có lỗi xảy ra, vui lòng liên hệ Quản trị viên';
            let detail = err?.response?.data?.detail?.toLowerCase();
            if (detail?.includes('email')) {
                description = 'Tài khoản với địa chỉ email này không tồn tại';
            } else if (detail?.includes('invalid')) {
                description = 'Mã OTP không hợp lệ';
            } else if (detail?.includes('expired')) {
                description = 'Mã OTP đã hết hạn';
            }

            notification.error({
                message: 'Đặt lại thất bại',
                description: description,
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    const onForgotPassword = async () => {
        await mutateForgotPassword.mutateAsync({
            email: email,
        });
    };

    const onResetPassword = async () => {
        await mutateResetPassword.mutateAsync({
            email: email,
            otp: otp,
            password: password,
        });
    };

    const onResend = async () => {
        await mutateResendOTPVefirication.mutateAsync({
            email: email,
        });
    };

    if (isTokenStoraged()) {
        let roles = getRoles();
        let url = config.routes.web.home;

        if (roles?.includes('ADMIN')) url = config.routes.admin.dashboard;

        return <Navigate to={url} replace />;
    }

    return (
        <div className="forgot-password-container flex justify-center items-center w-full">
            <div className="relative flex flex-col justify-center py-12">
                <div className="relative bg-white px-6 pt-10 pb-9 shadow-xl w-full rounded-2xl">
                    {emailVerify ? (
                        <div className="mx-auto flex w-full flex-col space-y-16">
                            <div className="flex flex-col items-center justify-center text-center space-y-2">
                                <div className="font-semibold  text-[2rem]">
                                    <p>Đặt lại mật khẩu</p>
                                </div>
                                <div className="flex flex-row text-[1.2rem] font-medium text-gray-400">
                                    <p>Chúng tôi đã gửi mã OTP đến email {email}</p>
                                </div>
                            </div>

                            <div>
                                <div className="flex flex-col space-y-4">
                                    <Form
                                        form={form}
                                        onFieldsChange={handleFormChange}
                                        className="flex flex-col"
                                    >
                                        <Form.Item
                                            name="newPassword"
                                            rules={[
                                                {
                                                    required: true,
                                                    message: 'Vui lòng nhập mật khẩu của bạn!',
                                                },
                                            ]}
                                        >
                                            <Input.Password
                                                placeholder="Nhập mật khẩu mới"
                                                className="focus:border-[--primary-color] focus-within:border-[--primary-color] hover:border-[--primary-color]"
                                                onChange={(e) => setPassword(e.target.value)}
                                            />
                                        </Form.Item>
                                        <Form.Item
                                            name="confirmPassword"
                                            rules={[
                                                {
                                                    required: true,
                                                    message: 'Vui lòng nhập mật khẩu xác nhận!',
                                                },
                                                ({ getFieldValue }) => ({
                                                    validator(_, value) {
                                                        if (
                                                            !value ||
                                                            getFieldValue('newPassword') === value
                                                        ) {
                                                            return Promise.resolve();
                                                        }
                                                        return Promise.reject(
                                                            new Error(
                                                                'Mật khẩu xác nhận không khớp!',
                                                            ),
                                                        );
                                                    },
                                                }),
                                            ]}
                                        >
                                            <Input.Password
                                                placeholder="Xác nhận mật khẩu"
                                                className="focus:border-[--primary-color] focus-within:border-[--primary-color] hover:border-[--primary-color]"
                                                onChange={(e) => setConfirmPassword(e.target.value)}
                                            />
                                        </Form.Item>
                                    </Form>
                                    <div className="text-center">
                                        <h3 className="text-[1.6rem] mb-[1rem]">Nhập mã OTP</h3>
                                        <OTPInput
                                            value={otp}
                                            inputStyle={{
                                                width: '5rem',
                                                height: '5rem',
                                                margin: '0 1rem',
                                                fontSize: '2rem',
                                                borderRadius: '4px',
                                                border: '1px solid rgba(0,0,0,.3)',
                                            }}
                                            onChange={onChange}
                                            numInputs={6}
                                            renderSeparator={<span>-</span>}
                                            renderInput={(props) => <input {...props} />}
                                        />
                                    </div>
                                    <div className="flex flex-col space-y-5">
                                        <div>
                                            <Button
                                                onClick={onResetPassword}
                                                loading={processing}
                                                disabled={
                                                    otp.length < 6 ||
                                                    !password ||
                                                    !confirmPassword ||
                                                    disabledSave
                                                }
                                                className={`flex flex-row items-center justify-center text-center w-full border rounded-xl outline-none py-8 ${
                                                    otp.length < 6 ||
                                                    !password ||
                                                    !confirmPassword ||
                                                    disabledSave
                                                        ? 'bg-gray-400'
                                                        : 'bg-[--primary-color]'
                                                }  border-none text-white text-[1.6rem] shadow-sm`}
                                            >
                                                Đặt lại
                                            </Button>
                                        </div>

                                        <div className="flex flex-row items-center justify-center text-center text-[1.4rem] font-medium space-x-1 text-gray-500">
                                            <p>Bạn không nhận được OTP?</p>
                                            <a
                                                onClick={onResend}
                                                className="flex flex-row items-center text-[--primary-color] cursor-pointer"
                                            >
                                                Gửi lại
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    ) : (
                        <div className="mx-auto flex w-full flex-col min-[10rem]">
                            <div className="flex flex-col items-center justify-center text-center space-y-2 mb-[1rem]">
                                <div className="font-semibold  text-[2rem]">
                                    <p>Nhập địa chỉ email</p>
                                </div>
                            </div>
                            <div>
                                <Form
                                    form={form}
                                    onFieldsChange={handleFormChange}
                                    className="flex flex-col"
                                >
                                    <Form.Item
                                        name="email"
                                        rules={[
                                            {
                                                required: true,
                                                message: 'Vui lòng nhập email của bạn!',
                                            },
                                            {
                                                type: 'email',
                                                message: 'Nhập đúng định dạng email',
                                            },
                                        ]}
                                    >
                                        <Input.TextArea
                                            onChange={(e) => setEmail(e.target.value)}
                                            rows={1}
                                            className={'w-[40rem] focus:border-[--primary-color]'}
                                            autoSize={{
                                                maxRows: 1,
                                                minRows: 1,
                                            }}
                                        />
                                    </Form.Item>
                                </Form>
                            </div>
                            <div className="flex flex-col space-y-5 mt-[1rem]">
                                <Button
                                    loading={processing}
                                    disabled={disabledSave}
                                    onClick={onForgotPassword}
                                    className={`flex flex-row items-center justify-center text-center w-full border rounded-xl outline-none py-8 ${
                                        disabledSave ? 'bg-gray-400' : 'bg-[--primary-color]'
                                    }  border-none text-white text-[1.6rem] shadow-sm`}
                                >
                                    Kiểm tra
                                </Button>
                                <div className="text-center text-[1.2rem]">
                                    <span className="text-black text-opacity-60">
                                        Vui lòng nhập email đã dùng để đăng ký tài khoản
                                    </span>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}
export default ForgotPasswordPage;
