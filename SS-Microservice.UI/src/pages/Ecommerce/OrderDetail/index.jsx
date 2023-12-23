import { useParams } from 'react-router-dom';
import config from '../../../config';
import AccountLayout from '../../../layouts/Ecommerce/AccountLayout';
import Contact from './Contact';
import Head from './Head';
import Info from './Info';
import OrderStatus from './OrderStatus';
import Wrapper from './Wrapper';
import './orderdetail.scss';
import { useGetOrderByCode } from '../../../hooks/api';
import WebLoading from '../../../layouts/Ecommerce/components/WebLoading';
import { ORDER_STATUS } from '../../../utils/constants';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCancel } from '@fortawesome/free-solid-svg-icons';

function OrderDetailPage() {
    const { code } = useParams();
    const { data, isLoading, refetch } = useGetOrderByCode(code);
    if (isLoading) return <WebLoading />;
    return (
        <AccountLayout routeKey={config.routes.web.order} isSetMinHeight={false}>
            <div className="order-detail-container">
                <Head code={data?.data?.code} status={data?.data?.status} />
                <OrderStatus order={data?.data} />
                <div className='flex justify-center'>
                    {data?.data?.status === ORDER_STATUS.CANCELLED && (
                        <div className="my-10">
                            <p className="text-[2rem] border border-solid hover:bg-rose-600 hover:text-white transition-all border-rose-600 w-fit p-4 text-rose-600">
                                <FontAwesomeIcon icon={faCancel} className="mr-3" />
                                {data?.data?.cancelReason?.name || data?.data?.otherCancelReason || 'Hệ thống đã huỷ đơn hàng của bạn'}
                            </p>
                        </div>
                    )}
                </div>
                <Contact order={data?.data} orderRefetch={refetch} />
                <Wrapper
                    orderRefetch={refetch}
                    orderItems={data?.data?.items}
                    status={data?.data?.status}
                />
                <Info order={data?.data} />
            </div>
        </AccountLayout>
    );
}

export default OrderDetailPage;
