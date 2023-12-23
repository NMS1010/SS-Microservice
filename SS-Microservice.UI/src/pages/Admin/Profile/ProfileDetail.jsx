import { IconAddressBook, IconCake, IconDeviceMobile, IconMail } from '@tabler/icons-react';
import { Image, Tag } from 'antd';

function ProfileDetail({ user }) {
    const handleAddress = (addresses) => {
        const address = addresses.find((item) => item.isDefault);
        return `${address.street}, ${address.ward.name}, ${address.district.name}, ${address.province.name}`;
    };

    return (
        <div className="bg-white p-7 mt-5 rounded-xl shadow">
            <h2 className="text-[2rem] font-bold text-[--text-color]">Hồ Sơ</h2>
            <div className="flex flex-col items-center justify-between my-[2.5rem]">
                <Image
                    className="rounded-[5px] shadow-[0_2px_8px_0_rgba(99,99,99,0.2)] mb-[0.5rem]"
                    width={150}
                    src={
                        user.avatar
                            ? user.avatar
                            : 'https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png'
                    }
                />
                <div className="text-[1.6rem] font-bold mb-[0.5rem]">{`${user.firstName} ${user.lastName}`}</div>
                <Tag bordered={false} color="green">
                    Đang hoạt động
                </Tag>
            </div>
            <div>
                <div className="flex items-center my-[0.5rem]">
                    <div>
                        <IconDeviceMobile className="w-[20px]" />
                    </div>
                    <span className={`text-[1.4rem] px-[5px] ${!user.phone && 'italic'}`}>
                        {user.phone ? user.phone : 'Đang cập nhật . . .'}
                    </span>
                </div>
                <div className="flex items-center my-[0.5rem]">
                    <div>
                        <IconMail className="w-[20px]" />
                    </div>
                    <span className="text-[1.4rem] px-[5px]">{user.email}</span>
                </div>
                <div className="flex items-center my-[0.5rem]">
                    <div>
                        <IconCake className="w-[20px]" />
                    </div>
                    <span className={`text-[1.4rem] px-[5px] ${!user.dob && 'italic'}`}>
                        {user.dob ? new Date(user.dob).toLocaleString() : 'Đang cập nhật . . .'}
                    </span>
                </div>
                <div className="flex items-center my-[0.5rem]">
                    <div>
                        <IconAddressBook className="w-[20px] h-[20px]" />
                    </div>
                    <span
                        className={`text-[1.4rem] px-[5px] ${
                            user.addresses.length <= 0 && 'italic'
                        }`}
                    >
                        {user.addresses.length > 0
                            ? handleAddress(user.addresses)
                            : 'Đang cập nhật . . .'}
                    </span>
                </div>
            </div>
            <Tag className="py-[0.5rem] w-full text-center text-[1.6rem] mt-[2rem]" color="volcano">
                Quản trị viên
            </Tag>
        </div>
    );
}

export default ProfileDetail;
