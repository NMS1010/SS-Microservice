import './order.scss';
import Data from './Data';
import OrderHead from './OrderHead';

function OrdersPage() {
    return (
        <div className="unit-container">
            <OrderHead />
            <Data />
        </div>
    );
}

export default OrdersPage;
