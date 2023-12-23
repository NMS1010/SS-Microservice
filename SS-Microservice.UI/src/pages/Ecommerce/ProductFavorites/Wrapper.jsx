import Item from './Item';
import { useGetFollowProduct } from '../../../hooks/api';
import WebLoading from '../../../layouts/Ecommerce/components/WebLoading';

function Wrapper() {
    const dataApi = useGetFollowProduct();

    if (dataApi?.isLoading) return <WebLoading />;

    return (
        <div>
            {dataApi?.data?.data?.items?.map((item, index) => {
                return (
                    <Item
                        dataApi={dataApi}
                        key={index}
                        item={item}
                        isLastItem={index === dataApi?.data?.data?.items?.length - 1}
                    />
                );
            })}
        </div>
    );
}

export default Wrapper;
