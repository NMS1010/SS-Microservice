import config from '../../../config';
import AccountLayout from '../../../layouts/Ecommerce/AccountLayout';
import Head from './Head';
import FormInput from './FormInput';
import './changepassword.scss';

function ChangePasswordPage() {
    return (
        <AccountLayout routeKey={config.routes.web.password}>
            <div className="changepassword-container h-full shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
                <Head />
                <FormInput />
            </div>
        </AccountLayout>
    );
}

export default ChangePasswordPage;
