import { Button, notification } from 'antd';
import { useState } from 'react';
import OTPInput from 'react-otp-input';

import { useRegisterOTPVerification, useResendRegisterOTPVerification } from '../../../hooks/api';
import { Navigate, useNavigate, useSearchParams } from 'react-router-dom';
import config from '../../../config';
import { getRoles, isTokenStoraged } from '../../../utils/storage';

function OTPVerificationPage() {
    const navigate = useNavigate();
    const [otp, setOtp] = useState('');
    const [searchParams] = useSearchParams();
    const [processing, setProcessing] = useState(false);

    const mutateResendOTPVefirication = useResendRegisterOTPVerification({
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
            } else if (detail?.includes('signed up')) {
                description = 'Tài khoản chưa được đăng ký trước đó, vui lòng đăng ký';
            } else if (detail?.includes('verified')) {
                description = 'Tài khoản đã được xác thực trước đó';
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

    const mutateOTPVefirication = useRegisterOTPVerification({
        success: (data) => {
            notification.success({
                message: 'Xác thực thành công',
                description: 'Bạn đã xác thực thành công tài khoản email, vui lòng đăng nhập lại',
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
                message: 'Xác thực thất bại',
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

    const onChange = (value) => {
        setOtp(value);
    };
    const onVerify = async () => {
        await mutateOTPVefirication.mutateAsync({
            otp,
            email: searchParams.get('email'),
        });
    };

    const onResend = async () => {
        await mutateResendOTPVefirication.mutateAsync({
            email: searchParams.get('email'),
        });
    };
    
    if (isTokenStoraged()) {
        let roles = getRoles();
        let url = config.routes.web.home;

        if (roles?.includes('ADMIN')) url = config.routes.admin.dashboard;

        return <Navigate to={url} replace />;
    }

    return (
        <div className="otp-container flex justify-center items-center w-full">
            <div className="relative flex flex-col justify-center py-12">
                <div className="relative bg-white px-6 pt-10 pb-9 shadow-xl w-full rounded-2xl">
                    <div className="mx-auto flex w-full flex-col space-y-16">
                        <div className="flex flex-col items-center justify-center text-center space-y-2">
                            <div className="font-semibold  text-[2rem]">
                                <p>Xác thực tài khoản email</p>
                            </div>
                            <div className="flex flex-row text-[1.2rem] font-medium text-gray-400">
                                <p>Chúng tôi đã gửi mã OTP đến email {searchParams.get('email')}</p>
                            </div>
                        </div>

                        <div>
                            <form action="" method="post">
                                <div className="flex flex-col space-y-16">
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
                                    <div className="flex flex-col space-y-5">
                                        <div>
                                            <Button
                                                onClick={onVerify}
                                                loading={processing}
                                                disabled={otp.length < 6}
                                                className={`flex flex-row items-center justify-center text-center w-full border rounded-xl outline-none py-8 ${
                                                    otp.length < 6 ? 'bg-gray-400' : 'bg-blue-700'
                                                }  border-none text-white text-[1.6rem] shadow-sm`}
                                            >
                                                Xác thực
                                            </Button>
                                        </div>

                                        <div className="flex flex-row items-center justify-center text-center text-[1.4rem] font-medium space-x-1 text-gray-500">
                                            <p>Bạn không nhận được OTP?</p>
                                            <a
                                                onClick={onResend}
                                                className="flex flex-row items-center text-blue-600 cursor-pointer"
                                            >
                                                Gửi lại
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default OTPVerificationPage;
