import Item from './Item';

function Wrapper({ orderItems, status, orderRefetch }) {
    return (
        <div>
            {orderItems?.map((item, index) => {
                return (
                    <Item
                        orderRefetch={orderRefetch}
                        key={index}
                        status={status}
                        item={item}
                        isLastItem={index === orderItems?.length - 1}
                    />
                );
            })}
        </div>
    );
}

export default Wrapper;
