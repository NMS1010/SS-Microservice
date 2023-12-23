import config from '../config';
import { AdminLayout, EcommerceLayout } from '../layouts';
// page web
import AddressPage from '../pages/Ecommerce/Address';
import ProductFavoritesPage from '../pages/Ecommerce/ProductFavorites';
import HomePage from '../pages/Ecommerce/Home';
import ProfilePage from '../pages/Ecommerce/Profile';
import ChangePasswordPage from '../pages/Ecommerce/ChangePassword';
import OrderPage from '../pages/Ecommerce/Order';
import OrderDetailPage from '../pages/Ecommerce/OrderDetail';
import NotificationPage from '../pages/Ecommerce/Notification';
import LoginPage from '../pages/Ecommerce/Login';
import RegisterPage from '../pages/Ecommerce/Register';
import CartPage from '../pages/Ecommerce/Cart';
import CheckoutPage from '../pages/Ecommerce/Checkout';
import ShopPage from '../pages/Ecommerce/Shop';
// page admin
import DashboardPage from '../pages/Admin/Dashboard';
import ProductDetailPage from '../pages/Ecommerce/ProductDetail';
import ProductPage from '../pages/Admin/Product';
import InventoryPage from '../pages/Admin/Inventory';
import OrderPages from '../pages/Admin/Order';
import ReviewPage from '../pages/Admin/Review';
import AccountPage from '../pages/Admin/Account';
import EmployeePage from '../pages/Admin/Employee';
import EmployeeFormPage from '../pages/Admin/Employee/EmployeeForm';
import SalePage from '../pages/Admin/Sale';
import SaleFormPage from '../pages/Admin/Sale/SaleForm';
import DeliveryFormPage from '../pages/Admin/Delivery/DeliveryForm';
import DeliveryPage from '../pages/Admin/Delivery';
import TransactionPage from '../pages/Admin/Transaction';
import PaymentMethodPage from '../pages/Admin/PaymentMethod';
import PaymentMethodFormPage from '../pages/Admin/PaymentMethod/PaymentMethodForm';
import Dashboard from '../pages/Admin/Dashboard';
import ProductFormPage from '../pages/Admin/Product/ProductForm';
import UnitPage from '../pages/Admin/Unit';
import UnitFormPage from '../pages/Admin/Unit/UnitForm';
import BrandFormPage from '../pages/Admin/Brand/BrandForm';
import BrandPage from '../pages/Admin/Brand';
import OrderFormPage from '../pages/Admin/Order/OrderForm';
import ReasonCancelPage from '../pages/Admin/ReasonCancel';
import ReasonCancelFormPage from '../pages/Admin/ReasonCancel/ReasonCancelForm';
import Forbidden from '../components/Forbidden';
import RolePage from '../pages/Admin/Role';
import ProductCategoryPage from '../pages/Admin/ProductCategory';
import ProductCategoryFormPage from '../pages/Admin/ProductCategory/ProductCategoryForm';
import VariantPage from '../pages/Admin/Variant';
import ProductImagePage from '../pages/Admin/ProductImage';
import PaypalPaymentPage from '../pages/Ecommerce/Checkout/PaypalPayment';
import OTPVerificationPage from '../pages/Ecommerce/OTPVerification';
import ForgotPasswordPage from '../pages/Ecommerce/ForgotPassword';
import AdminProfilePage from '../pages/Admin/Profile';
import ContactPage from '../pages/Ecommerce/Contact';

const privateRoutes = [
    // Admin Layout
    {
        path: config.routes.admin.dashboard,
        component: DashboardPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.profile,
        component: AdminProfilePage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.forbidden,
        component: Forbidden,
        layout: null,
        roles: ['USER'],
        private: true,
    },
    // Product
    {
        path: config.routes.admin.product,
        component: ProductPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.product_create,
        component: ProductFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.product_update + '/:id',
        component: ProductFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // ProductImage
    {
        path: config.routes.admin.product_image + '/:productId',
        component: ProductImagePage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Variant
    {
        path: config.routes.admin.product_variant + '/:productId',
        component: VariantPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Category
    {
        path: config.routes.admin.product_category,
        component: ProductCategoryPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.product_category_create,
        component: ProductCategoryFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.product_category_update + '/:id',
        component: ProductCategoryFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Unit
    {
        path: config.routes.admin.unit,
        component: UnitPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.unit_create,
        component: UnitFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.unit_update + '/:id',
        component: UnitFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Brand
    {
        path: config.routes.admin.brand,
        component: BrandPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.brand_create,
        component: BrandFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.brand_update + '/:id',
        component: BrandFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Order
    {
        path: config.routes.admin.order,
        component: OrderPages,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.order_update + '/:id',
        component: OrderFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.reason_cancel,
        component: ReasonCancelPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.reason_cancel_create,
        component: ReasonCancelFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.reason_cancel_update + '/:id',
        component: ReasonCancelFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Inventory
    {
        path: config.routes.admin.inventory,
        component: InventoryPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Review
    {
        path: config.routes.admin.review,
        component: ReviewPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Account
    {
        path: config.routes.admin.account,
        component: AccountPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.role,
        component: RolePage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Employee
    {
        path: config.routes.admin.employee,
        component: EmployeePage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.employee_create,
        component: EmployeeFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.employee_update + '/:id',
        component: EmployeeFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Sale
    {
        path: config.routes.admin.sale,
        component: SalePage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.sale_create,
        component: SaleFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.sale_update + '/:id',
        component: SaleFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Delivery
    {
        path: config.routes.admin.delivery,
        component: DeliveryPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.delivery_create,
        component: DeliveryFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.delivery_update + '/:id',
        component: DeliveryFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Payment method
    {
        path: config.routes.admin.payment_method,
        component: PaymentMethodPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.payment_method_create,
        component: PaymentMethodFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    {
        path: config.routes.admin.payment_method_update + '/:id',
        component: PaymentMethodFormPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    // Transaction
    {
        path: config.routes.admin.transaction,
        component: TransactionPage,
        layout: AdminLayout,
        roles: ['ADMIN'],
        private: true,
    },
    //================================================================//
    // Web Ecommerce Layout
    {
        path: config.routes.web.checkout,
        component: CheckoutPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.checkout + '/payment/:code',
        component: PaypalPaymentPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.cart,
        component: CartPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.profile,
        component: ProfilePage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.address,
        component: AddressPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.favorites,
        component: ProductFavoritesPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.password,
        component: ChangePasswordPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.order,
        component: OrderPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.order + '/:code',
        component: OrderDetailPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
    {
        path: config.routes.web.notification,
        component: NotificationPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: true,
    },
];

const publicRoutes = [
    {
        path: config.routes.web.register,
        component: RegisterPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.login,
        component: LoginPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.otp_verify,
        component: OTPVerificationPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.forgot_password,
        component: ForgotPasswordPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.home,
        component: HomePage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.product_detail + '/:slug',
        component: ProductDetailPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.home + '/:productCategory',
        component: ShopPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.search,
        component: ShopPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.product,
        // component: Product,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
    {
        path: config.routes.web.contact,
        component: ContactPage,
        layout: EcommerceLayout,
        roles: ['USER'],
        private: false,
    },
];
const routes = [...publicRoutes, ...privateRoutes];
export default routes;
