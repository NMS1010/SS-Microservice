const apiRoutes = {
    common: {
        auth: {
            login: '/api/auths/login',
            google_login: '/api/auths/google-login',
            register: '/api/auths/register',
            register_otp_verify: '/api/auths/register/verify',
            register_resend_otp_verify: '/api/auths/register/resend',
            forgot_password: '/api/auths/forgot-password',
            forgot_password_resend_otp_verify: '/api/auths/forgot-password/resend',
            reset_password: '/api/auths/reset-password',
            refresh_token: '/api/auths/refresh-token',
        },
        user: {
            _: '/api/users',
            me: '/api/users/profile/me',
        },
        review: '/api/reviews',
        address: '/api/addresses',
        order: '/api/orders',
    },
    admin: {
        unit: '/api/units',
        delivery: '/api/deliveries',
        paymentMethod: '/api/payment-methods',
        orderCancellationReason: '/api/order-cancel-reasons',
        employee: '/api/staffs',
        role: '/api/roles',
        transaction: '/api/transactions',
        brand: '/api/brands',
        product_category: '/api/product-categories',
        product: '/api/products',
        product_image: '/api/products/images',
        variant: '/api/variants',
        inventory: '/api/inventories',
        sale: '/api/sales',
        statistic: '/api/statistics',
    },
    web: {
        product: '/api/products',
        cart: '/api/carts',
        follow_product: '/api/user-follow-products',
        notification: '/api/notifications',
    },
};

export default apiRoutes;
