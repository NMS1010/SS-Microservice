import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useGetMe } from '../../../hooks/api';
import ProfileDetail from './ProfileDetail';
import ProfileForm from './ProfileForm';
import { faHashtag } from '@fortawesome/free-solid-svg-icons';

function AdminProfilePage() {
    const { isLoading, data, refetch } = useGetMe();

    return (
        <div className="profile-container">
            <div className="flex items-center gap-[1rem]">
                <FontAwesomeIcon
                    className="text-[2rem] bg-[--primary-color] p-4 rounded-xl text-white"
                    icon={faHashtag}
                />
                <h1 className="font-bold">Thông tin tài khoản</h1>
            </div>
            <div className="grid grid-cols-12 gap-4">
                <div className="col-span-4">
                    <ProfileDetail user={data?.data} />
                </div>
                <div className="col-span-8">
                    <ProfileForm user={data?.data} refetch={refetch} />
                </div>
            </div>
        </div>
    );
}

export default AdminProfilePage;
