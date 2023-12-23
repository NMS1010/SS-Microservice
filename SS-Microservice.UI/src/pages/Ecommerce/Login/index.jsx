import { Link, NavLink, Navigate, useNavigate } from 'react-router-dom';
import { Button, Form, Input, notification } from 'antd';

import images from '../../../assets/images';
import './login.scss';
import { useGetCart, useGetMe, useLogin, useLoginByGoogle } from '../../../hooks/api';
import config from '../../../config';
import { getRoles, isTokenStoraged, saveToken } from '../../../utils/storage';
import { GoogleLogin, useGoogleLogin } from '@react-oauth/google';
import { useState } from 'react';

function LoginPage() {
    const navigate = useNavigate();
    const [processing, setProcessing] = useState(false);


    const handleToken = (token) => {
        saveToken(token);
        let roles = getRoles();
        let url = '/';

        if (roles?.includes('ADMIN')) url = config.routes.admin.dashboard;
        notification.success({
            message: 'Đăng nhập thành công',
            description: 'Chào mừng bạn đến với hệ thống của chúng tôi',
        });
        navigate(url);
    };

    const mutateGoogleLogin = useLoginByGoogle({
        success: (data) => {
            handleToken(data?.data);
        },
        error: (err) => {
            let description = 'Không thể đăng nhập, vui lòng liên hệ Quản trị viên';
            let detail = err?.response?.data?.detail?.toLowerCase();
            if (detail?.includes('banned')) {
                description = 'Tài khoản của bạn đã bị vô hiệu hoá';
            } else if (detail?.includes('lockout')) {
                description =
                    'Tài khoản của bạn đã tạm khoá do đăng nhập sai nhiều lần, vui lòng thử lại sau';
            }
            notification.error({
                message: 'Đăng nhập thất bại',
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

    const [form] = Form.useForm();

    const mutationLogin = useLogin({
        success: (data) => {
            handleToken(data?.data);
        },
        error: (err) => {
            let description = 'Không thể đăng nhập, vui lòng liên hệ Quản trị viên';
            let detail = err?.response?.data?.detail?.toLowerCase();
            let isOtpVerify = false;
            if (detail?.includes('email')) {
                description = 'Địa chỉ email không tồn tại';
            } else if (detail?.includes('password')) {
                description = 'Mật khẩu không đúng';
            } else if (detail?.includes('banned')) {
                description = 'Tài khoản của bạn đã bị vô hiệu hoá';
            } else if (detail?.includes('confirmed')) {
                description = 'Tài khoản của bạn chưa được xác thực, vui lòng xác thực';
                isOtpVerify = true;
            } else if (detail?.includes('lockout')) {
                description =
                    'Tài khoản của bạn đã tạm khoá do đăng nhập sai nhiều lần, vui lòng thử lại sau';
            }

            notification.error({
                message: 'Đăng nhập thất bại',
                description: description,
            });
            if (isOtpVerify) {
                navigate(config.routes.web.otp_verify + '?email=' + form.getFieldValue('email'));
            }
        },
        mutate: (data) => {
            setProcessing(true);
        },
        settled: (data) => {
            setProcessing(false);
        },
    });
    const onLogin = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }

        await mutationLogin.mutateAsync({
            email: form.getFieldValue('email'),
            password: form.getFieldValue('password'),
        });
    };

    if (isTokenStoraged()) {
        let roles = getRoles();
        let url = config.routes.web.home;

        if (roles?.includes('ADMIN')) url = config.routes.admin.dashboard;

        return <Navigate to={url} replace />;
    }
    else{
        localStorage.removeItem("isTokenRefreshing")
    }

    return (
        <>
            <div className="login-container py-[3.6rem]">
                <div className="w-[400px] max-sm:w-[350px] bg-white rounded-[5px] shadow-[0_2px_6px_0_rgba(0,0,0,0.3)] max-lg:mt-[7rem] mx-auto">
                    <div className="p-[2rem]">
                        <div className="flex justify-between items-center">
                            <h3 className="text-[2.8rem]">Đăng nhập</h3>
                            <div className="w-[110px] h-[68.28px]">
                                <img src={images.logo} alt="" />
                            </div>
                        </div>
                        <div className="h-px bg-stone-600 opacity-[0.3] mt-[3rem] mb-[3.6rem]"></div>
                        <Form
                            form={form}
                            name="register"
                            labelCol={{
                                span: 0,
                            }}
                            wrapperCol={{
                                span: 24,
                            }}
                            style={{
                                maxWidth: '360px',
                            }}
                            initialValues={{
                                remember: true,
                            }}
                            autoComplete="off"
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
                                <Input
                                    className="h-[36px] rounded-[3px] shadow-[0_0_2px_0_rgba(0,0,0,0.25)] border-none py-[1rem] px-[1.5rem] text-[1.6rem] placeholder:text-[1.6rem]"
                                    placeholder="Nhập Email . . ."
                                />
                            </Form.Item>
                            <Form.Item
                                name="password"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Vui lòng nhập mật khẩu của bạn!',
                                    },
                                ]}
                            >
                                <Input.Password
                                    className="h-[36px] rounded-[3px] shadow-[0_0_2px_0_rgba(0,0,0,0.25)] border-none py-[1rem] px-[1.5rem] text-[1.6rem] placeholder:text-[1.6rem]"
                                    placeholder="Nhập Password . . ."
                                />
                            </Form.Item>
                            <Form.Item>
                                <Button
                                    loading={processing}
                                    onClick={onLogin}
                                    className="h-[36px] mt-[0.6rem] text-[1.6rem] text-white font-medium border-none hover:border-none"
                                    block
                                    htmlType="submit"
                                >
                                    Đăng nhập
                                </Button>
                            </Form.Item>
                        </Form>
                        <div class="h-[1.5rem] relative flex items-center justify-between">
                            <div class="w-[38%] h-px bg-stone-600 opacity-[0.3]"></div>
                            <div class="text-center text-black text-opacity-40 text-[1.3rem]">
                                HOẶC
                            </div>
                            <div class="w-[38%] h-px bg-stone-600 opacity-[0.3]"></div>
                        </div>
                        <div className="google-login-container h-[34px] my-[3rem] flex justify-between relative">
                            <GoogleLogin
                                onSuccess={async (credentialResponse) => {
                                    await mutateGoogleLogin.mutateAsync({
                                        googleToken: credentialResponse?.credential,
                                    });
                                }}
                                onError={() => {
                                    console.log('Login Failed');
                                }}
                                useOneTap
                            />
                        </div>
                        <div className="text-center text-[1.2rem]">
                            <span className="text-black text-opacity-60">Quên mật khẩu?</span>
                            <span className="text-lime-700 text-opacity-60 px-[0.5rem]">
                                <NavLink to={config.routes.web.forgot_password}>
                                    Lấy lại mật khẩu
                                </NavLink>
                            </span>
                        </div>
                        <div className="text-center text-[1.2rem]">
                            <span className="text-black text-opacity-60">
                                Bạn là thành viên mới?
                            </span>
                            <span className="text-lime-700 text-opacity-60 px-[0.5rem]">
                                <NavLink to={config.routes.web.register}>Đăng ký</NavLink>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default LoginPage;
