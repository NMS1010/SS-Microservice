import { faContactBook } from '@fortawesome/free-regular-svg-icons';
import { faCancel, faPhone } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from 'antd';
import { useState } from 'react';
import CancelModal from './CancelModal';
import { ORDER_STATUS } from '../../../utils/constants';

function Contact({ order, orderRefetch }) {
    const [isCancelModelOpen, setIsCancelModelOpen] = useState(false);
    return (
        <div>
            <div className="contact-container h-[9rem] flex justify-end items-center gap-[1rem]">
                {order?.status === ORDER_STATUS.NOT_PROCESSED && (
                    <button
                        onClick={() => setIsCancelModelOpen(true)}
                        className="text-rose-600 hover:bg-rose-600 transition-all hover:text-white mr-[1.9rem] border-[0.1rem] text-[1.6rem] border-rose-600 py-[1.4rem] px-[4.1rem]"
                    >
                        <FontAwesomeIcon className="text-[1.4rem] mr-[1rem]" icon={faCancel} />
                        Huỷ đơn hàng
                    </button>
                )}
                <button className="text-rose-600 hover:bg-rose-600 transition-all hover:text-white mr-[1.9rem] border-[0.1rem] text-[1.6rem] border-rose-600 py-[1.4rem] px-[4.1rem]">
                    <FontAwesomeIcon className="text-[1.4rem] mr-[1rem]" icon={faPhone} />
                    Liên hệ người bán
                </button>
                <CancelModal
                    orderRefetch={orderRefetch}
                    isCancelModelOpen={isCancelModelOpen}
                    setIsCancelModelOpen={setIsCancelModelOpen}
                    orderId={order?.id}
                />
            </div>
        </div>
    );
}

export default Contact;
