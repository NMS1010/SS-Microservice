import { useGetListNotification } from '../../../hooks/api';
import Item from './Item';

function Wrapper({ notifications }) {
    return (
        <div>
            {notifications?.map((item) => {
                return <Item key={item.id} notification={item} isRead={item?.status} />;
            })}
        </div>
    );
}

export default Wrapper;
