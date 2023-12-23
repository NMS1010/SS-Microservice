import config from '../../../config';
import AccountLayout from '../../../layouts/Ecommerce/AccountLayout';
import Head from './Head';
import Wrapper from './Wrapper';

function ProductFavoritesPage() {
    return (
        <AccountLayout routeKey={config.routes.web.favorites} isSetMinHeight={false}>
            <div className="favorites-container h-full shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
                <Head />
                <Wrapper />
            </div>
        </AccountLayout>
    );
}

export default ProductFavoritesPage;
