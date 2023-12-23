import { IconMapPin, IconMail, IconPhoneCall, IconCalendarTime } from '@tabler/icons-react';

function ContactInfo() {
    return (
        <div className="contact-info-container">
            <h2 className="text-[2.5rem] font-bold mb-[2rem]">Thông tin liên hệ</h2>
            <div>
                <span className="flex items-start justify-start mb-[1.5rem]">
                    <IconMapPin className="w-[60px]" />
                    <span className="ml-[1rem]">
                        <h3 className="text-[1.6rem] font-medium">Địa chỉ</h3>
                        <p>
                            Văn phòng: Số 59 đường số 5, Phường 11, Quận 6, TPHCM - Showroom và nhà
                            máy: B23/476M đường Trần Đại Nghĩa, Ấp 2, xã Tân Nhựt, huyện Bình Chánh,
                            TPHCM
                        </p>
                    </span>
                </span>
                <span className="flex items-start justify-start mb-[1.5rem]">
                    <IconMail />
                    <span className="ml-[1rem]">
                        <h3 className="text-[1.6rem] font-medium">Email</h3>
                        <p>contact@eco-pro.vn</p>
                    </span>
                </span>
                <span className="flex items-start justify-start mb-[1.5rem]">
                    <IconPhoneCall />
                    <span className="ml-[1rem]">
                        <h3 className="text-[1.6rem] font-medium">Điện thoại</h3>
                        <p>028 2245 4477 | 0913 813 039 (Zalo)</p>
                    </span>
                </span>
                <span className="flex items-start justify-start">
                    <IconCalendarTime />
                    <span className="ml-[1rem]">
                        <h3 className="text-[1.6rem] font-medium">Thời gian làm việc</h3>
                        <p>Thứ 2 - Thứ 7 từ 8h30 - 18h30</p>
                    </span>
                </span>
            </div>
        </div>
    );
}

export default ContactInfo;
