import { useState } from 'react';
import OrderAddress from './OrderAddress';
import OrderDelivery from './OrderDelivery';
import OrderProducts from './OrderProducts';
import Payment from './Payment';
import './checkout.scss';
import BreadCrumb from '../../../layouts/Ecommerce/components/Breadcrumb';

function CheckoutPage() {
    const [defaultAddress, setDefaultAddress] = useState(null);
    const [chosenDelivery, setChosenDelivery] = useState(null);
    const [totalCartPrice, setTotalCartPrice] = useState(null);
    const [chosenCartItems, setChosenCartItems] = useState([]);

    return (
        <>
            <BreadCrumb routes={[{ title: 'Thanh toán' }]} />
            <div className="w-[80%] mx-auto checkout-container pt-[5.6rem] pb-[10rem]">
                <OrderAddress setDefaultAddress={setDefaultAddress} />
                <OrderProducts
                    setChosenCartItems={setChosenCartItems}
                    setTotalCartPrice={setTotalCartPrice}
                />
                <OrderDelivery
                    chosenDelivery={chosenDelivery}
                    setChosenDelivery={setChosenDelivery}
                />
                <Payment
                    chosenCartItems={chosenCartItems}
                    defaultAddress={defaultAddress}
                    chosenDelivery={chosenDelivery}
                    totalCartPrice={totalCartPrice}
                />
            </div>
        </>
    );
}

export default CheckoutPage;
