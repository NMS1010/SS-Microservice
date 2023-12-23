import config from '../../../config';
import Head from '../../../layouts/Admin/components/Head';

function SaleHead() {
    return <Head route={config.routes.admin.sale_create} title={'Quản lý khuyến mãi'} />;
}

export default SaleHead;
