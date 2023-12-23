import { faStar } from '@fortawesome/free-regular-svg-icons';
import {
    faCancel,
    faDownload,
    faReceipt,
    faSpinner,
    faTruck,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Steps } from 'antd';
import { ORDER_STATUS, getOrderStatus } from '../../../utils/constants';

function OrderStatus({ order }) {
    return (
        <div className="order-status-container xl:px-[7rem] py-10 bg-white mx-auto my-5">
            <Steps current={order?.isReview ? 5 : getOrderStatus(order?.status)?.key}>
                <Steps.Step
                    className={ORDER_STATUS.NOT_PROCESSED == order?.status && 'current-step'}
                    title={
                        <div className="text-gray-400 text-[1.2rem]">
                            <p>{new Date(order?.createdAt).toLocaleDateString()}</p>
                            <p className='leading-4'>{new Date(order?.createdAt).toLocaleTimeString()}</p>
                        </div>
                    }
                    icon={<FontAwesomeIcon icon={faReceipt} className='w-8 h-8'/>}
                />
                {order?.status === ORDER_STATUS.CANCELLED ? (
                    <Steps.Step
                    title={
                        <div className="text-gray-400 text-[1.2rem]">
                            <p>{new Date(order?.updatedAt).toLocaleDateString()}</p>
                            <p className='leading-4'>{new Date(order?.updatedAt).toLocaleTimeString()}</p>
                        </div>
                    }
                        className="current-step"
                        icon={<FontAwesomeIcon icon={faCancel} className='w-8 h-8' />}
                    />
                ) : (
                    <>
                        <Steps.Step
                            className={ORDER_STATUS.PROCESSING == order?.status && 'current-step'}
                            icon={<FontAwesomeIcon icon={faSpinner} className='w-8 h-8' />}
                        />
                        <Steps.Step
                            className={ORDER_STATUS.SHIPPED == order?.status && 'current-step'}
                            icon={<FontAwesomeIcon icon={faTruck} className='w-8 h-8' />}
                        />
                        <Steps.Step
                            className={ORDER_STATUS.DELIVERED == order?.status && !order?.isReview && 'current-step'}
                            title={
                                <div className="text-gray-400 text-[1.2rem]">
                                    <p>
                                        {order?.transaction?.completedAt &&
                                            new Date(
                                                order?.transaction?.completedAt,
                                            ).toLocaleDateString()}
                                    </p>
                                    <p className='leading-4'>
                                        {order?.transaction?.completedAt &&
                                            new Date(
                                                order?.transaction?.completedAt,
                                            ).toLocaleTimeString()}
                                    </p>
                                </div>
                            }
                            icon={<FontAwesomeIcon icon={faDownload}  className='w-8 h-8'/>}
                        />
                        <Steps.Step
                            className={order?.isReview && 'current-step'}
                            title={
                                <div className="text-gray-400 text-[1.2rem]">
                                    <p>
                                        {order?.reviewedDate &&
                                            new Date(
                                                order?.reviewedDate,
                                            ).toLocaleDateString()}
                                    </p>
                                    <p className='leading-4'>
                                        {order?.reviewedDate &&
                                            new Date(
                                                order?.reviewedDate,
                                            ).toLocaleTimeString()}
                                    </p>
                                </div>
                            }
                            icon={<FontAwesomeIcon icon={faStar} className='w-8 h-8' />}
                        />
                    </>
                )}
            </Steps>
        </div>
    );
}

export default OrderStatus;
