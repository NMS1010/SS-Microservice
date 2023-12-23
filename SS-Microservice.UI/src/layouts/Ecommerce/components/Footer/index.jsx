import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import './footer.scss';
import { faLocationPin, faMailBulk, faPhone } from '@fortawesome/free-solid-svg-icons';
import { faFacebook, faInstagram, faTwitch, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { Link } from 'react-router-dom';
import images from '../../../../assets/images';

function Footer() {
    return (
        <div className="footer-container">
            <div className="wrapper 2xl:mx-[36rem] mx-[5rem] pt-[4rem] flex justify-center items-center">
                <div className="grid lg:grid-cols-4 gap-[2rem] md:grid-cols-2 sm:grid-cols-1">
                    <div className="mb-5">
                        <div className="uppercase text-white text-[1.8rem] font-medium leading-snug">
                            Về chúng tôi
                        </div>
                        <img src={images.logo_footer} alt="logo" />
                        <div className="pr-[0.3rem]">
                            <div className="text-white uppercase text-[1.6rem] my-3 font-normal leading-snug">
                                Công ty TNHH HTV GreenCraze
                            </div>
                        </div>
                        <div className="justify-start my-2 items-baseline gap-[.5rem] inline-flex">
                            <div className="w-5 mr-5 flex-col justify-start items-start inline-flex">
                                <div className="text-white text-[1.5rem]  font-black leading-[1.5rem] ">
                                    <FontAwesomeIcon icon={faLocationPin} />
                                </div>
                            </div>
                            <div className="flex-col justify-start items-start inline-flex">
                                <div className="text-white text-[1.5rem]  font-normal leading-snug">
                                    Số 1 Võ Văn Ngân, P. Linh Chiểu, TP.Thủ Đức, TP.HCM
                                </div>
                            </div>
                        </div>
                        <div className="pr-[6.8rem] my-2 justify-start items-baseline gap-[0.5rem] inline-flex">
                            <div className="w-5 mr-5 flex-col justify-start items-start inline-flex">
                                <div className="text-white text-[1.5rem]  font-black leading-[1.5rem] ">
                                    <FontAwesomeIcon icon={faPhone} />
                                </div>
                            </div>
                            <div className="flex-col justify-start items-start inline-flex">
                                <div className="text-white text-[1.5rem]  font-normal leading-snug">
                                    093 858 7191
                                </div>
                            </div>
                        </div>
                        <div className="pr-[6rem] my-2 justify-start items-baseline gap-[0.5rem] inline-flex">
                            <div className="w-5 mr-5 flex-col justify-start items-start inline-flex">
                                <div className="text-white text-[1.5rem]  font-black leading-[1.5rem] ">
                                    <FontAwesomeIcon icon={faMailBulk} />
                                </div>
                            </div>
                            <div className="flex-col justify-start items-start inline-flex">
                                <div className="text-white text-[1.5rem]  font-normal leading-snug">
                                    xinchao@greencraze.vn
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="mb-5">
                        <div className="text-white uppercase mb-7 text-[1.8rem] font-medium leading-snug">
                            Hướng dẫn
                        </div>
                        <div className="flex-col justify-start items-start gap-[1rem] flex">
                            <Link className="text-index text-white text-[1.5rem] font-normal leading-snug">
                                Điều Khoản
                            </Link>
                            <Link className="text-index text-white text-[1.5rem]  font-normal leading-snug">
                                Mua Hàng và Thanh Toán
                            </Link>
                            <Link className="text-index text-white text-[1.5rem]  font-normal leading-snug">
                                Chính Sách Giao Hàng
                            </Link>
                            <Link className="text-index text-white text-[1.5rem]  font-normal leading-snug">
                                Chính Sách Đổi Trả
                            </Link>
                            <Link className="text-index text-white text-[1.5rem]  font-normal leading-snug">
                                Chính Sách Hoàn Tiền
                            </Link>
                            <Link className="text-index text-white text-[1.5rem]  font-normal leading-snug">
                                Chính Sách Bảo Mật Thông Tin
                            </Link>
                        </div>
                    </div>
                    <div className="mb-5">
                        <div className="text-white uppercase mb-7 text-3xl font-medium leading-snug">
                            Thông tin
                        </div>
                        <div className="flex-col justify-start items-start gap-[1rem] flex">
                            <Link className="text-index text-white text-[1.5rem]  font-normal leading-snug">
                                Trang chủ
                            </Link>
                            <Link className="text-index text-white text-[1.5rem]  font-normal leading-snug">
                                Giới thiệu
                            </Link>
                            <Link className="text-index text-white text-[1.5rem]  font-normal leading-snug">
                                Tuyển dụng
                            </Link>
                        </div>
                    </div>
                    <div className="mb-5">
                        <div className="flex-col justify-start items-start gap-[1.7rem] flex">
                            <div className="text-white text-3xl font-medium leading-snug">
                                Theo dõi chúng tôi
                            </div>
                            <div className="justify-start items-start inline-flex">
                                <Link className="self-stretch pt-[0.5rem] pb-[0.8rem] flex-col justify-start items-start inline-flex">
                                    <div className="text-white text-[2.5rem] font-normal leading-[2.5rem]">
                                        <FontAwesomeIcon
                                            className="text-index "
                                            icon={faFacebook}
                                        />
                                    </div>
                                </Link>
                                <Link className="self-stretch pl-4 pt-[.5rem] pb-[0.8rem] flex-col justify-start items-start inline-flex">
                                    <div className="text-white text-[2.5rem] font-normal leading-[2.5rem]">
                                        <FontAwesomeIcon className="text-index " icon={faTwitter} />
                                    </div>
                                </Link>
                                <Link className="self-stretch pl-4 pt-[.5rem] pb-[0.8rem] flex-col justify-start items-start inline-flex">
                                    <div className="text-white text-[2.5rem] font-normal leading-[2.5rem]">
                                        <FontAwesomeIcon className="text-index " icon={faTwitch} />
                                    </div>
                                </Link>
                                <Link className="self-stretch pl-4 pt-[.5rem] pb-[0.8rem] flex-col justify-start items-start inline-flex">
                                    <div className="text-white text-[2.5rem] font-normal leading-[2.5rem]">
                                        <FontAwesomeIcon
                                            className="text-index "
                                            icon={faInstagram}
                                        />
                                    </div>
                                </Link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="lg:px-[36rem] pt-3 pb-2.5 flex-col bg-white text-center">
                <div className="text-slate-700 text-[1.6rem] font-normal leading-[2.1rem]">
                    © Bản quyền thuộc về The Green Craze
                </div>
            </div>
        </div>
    );
}
export default Footer;
