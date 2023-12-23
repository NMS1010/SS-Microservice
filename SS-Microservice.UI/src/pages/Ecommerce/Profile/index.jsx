import AccountLayout from '../../../layouts/Ecommerce/AccountLayout';
import './profile.scss';
import Info from './Info';
import Head from './Head';
import config from '../../../config';

function ProfilePage() {
    return (
        <AccountLayout routeKey={config.routes.web.profile}>
            <div className="profile-container shadow-[0_1px_2px_0_rgba(0,0,0,0.13)]">
                <Head />
                <Info />
            </div>
        </AccountLayout>
    );
}

export default ProfilePage;
