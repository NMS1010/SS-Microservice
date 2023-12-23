import apiRoutes from '../../config/apiRoutes';
import { usePost, usePut, usePutWithoutId } from '../../utils/reactQuery';

export const useLogin = (updater) => {
    return usePost(apiRoutes.common.auth.login, updater);
}

export const useLoginByGoogle = (updater) => {
    return usePost(apiRoutes.common.auth.google_login, updater);
}

export const useRegister = (updater) => {
    return usePost(apiRoutes.common.auth.register, updater);
}

export const useRegisterOTPVerification = (updater) => {
    return usePutWithoutId(apiRoutes.common.auth.register_otp_verify, updater);
}

export const useResendRegisterOTPVerification = (updater) => {
    return usePutWithoutId(apiRoutes.common.auth.register_resend_otp_verify, updater);
}

export const useForgotPassword = (updater) => {
    return usePutWithoutId(apiRoutes.common.auth.forgot_password, updater);
}

export const useResetPassword = (updater) => {
    return usePutWithoutId(apiRoutes.common.auth.reset_password, updater);
}

export const useResendForgotPasswordOTPVerification = (updater) => {
    return usePutWithoutId(apiRoutes.common.auth.forgot_password_resend_otp_verify, updater);
}