import './recommentProduct.scss';
import { Button } from 'antd';
import CardProductRecomment from '../../components/CardProductRecomment';
import { useGetListProduct } from '../../../../hooks/api';

function RecommentProduct() {
    const {data, isLoading} = useGetListProduct({
        pageIndex: 1,
        pageSize: 5,
        status: true
    });
    return (
        <div className="recomment">
            <Button className="w-full h-[38px] text-white font-medium text-[1.6rem] text-star mb-[1.2rem] hover:border-none border-none">
                Có thể bạn sẽ thích
            </Button>
            <div className="card-list">
                {
                    data?.data?.items?.map((product) => {
                        return <CardProductRecomment key={product?.id} product={product}/>
                    })
                }
            </div>
        </div>
    );
}

export default RecommentProduct;
