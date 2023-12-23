import { useState } from 'react';
import { useGetListAddress } from '../../../hooks/api';
import AddressForm from './AddressForm';
import Item from './Item';
import WebLoading from '../../../layouts/Ecommerce/components/WebLoading';

function Wrapper() {
    const addressAPI = useGetListAddress({
        status: true,
    });
    const [isFormOpen, setIsFormOpen] = useState({
        id: 0,
        isOpen: false,
    });
    if(addressAPI.isLoading)
        return <WebLoading />
    return (
        <div>
            {addressAPI?.data?.data?.items?.map((v, idx) => {
                return (
                    <Item
                        addressApi={addressAPI}
                        setIsFormOpen={setIsFormOpen}
                        key={v.id}
                        address={v}
                        isDefault={v?.isDefault}
                        isLastItem={idx === addressAPI?.data?.data?.items?.length - 1}
                    />
                );
            })}
            {isFormOpen.id !== 0 && (
                <AddressForm isFormOpen={isFormOpen} setIsFormOpen={setIsFormOpen} />
            )}
        </div>
    );
}

export default Wrapper;
