import Head from '../../../layouts/Admin/components/Head';
import config from '../../../config';

function OrderHead() {
    return <Head title={'Quản lý đơn hàng'} isAdd={false} isDisableAll={false}/>;
}

export default OrderHead;
