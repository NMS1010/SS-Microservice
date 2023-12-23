import config from '../../../config';
import AccountLayout from '../../../layouts/Ecommerce/AccountLayout';
import Head from './Head';
import Wrapper from './Wrapper';
import './address.scss';

function AddressPage() {
    return (
        <AccountLayout routeKey={config.routes.web.address} isSetMinHeight={false}>
            <div className="address-container h-full shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
                <Head />
                <Wrapper />
            </div>
        </AccountLayout>
    );
}

export default AddressPage;
