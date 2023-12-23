import './cardProduct.scss';
import Meta from 'antd/es/card/Meta';
import Card from 'antd/es/card/Card';
import { Rate, Tooltip, notification } from 'antd';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { numberFormatter } from '../../../../utils/formatter';
import { useNavigate } from 'react-router-dom';
import config from '../../../../config';
import { faHeart } from '@fortawesome/free-regular-svg-icons';
import { useFollowProduct } from '../../../../hooks/api';

function CardProduct({ product }) {
    const navigate = useNavigate();
    const handlePrice = (variants) => {
        const variantWithItemPriceMin = variants.reduce((min, current) =>
            current.itemPrice < min.itemPrice ? current : min,
        );

        return {
            itemPrice: numberFormatter(variantWithItemPriceMin.itemPrice),
            promotionalItemPrice:
                variantWithItemPriceMin.promotionalItemPrice !== null
                    ? numberFormatter(variantWithItemPriceMin.promotionalItemPrice)
                    : null,
        };
    };
    const price = handlePrice(product.variants);

    const mutationFollow = useFollowProduct({
        success: () => {
            notification.success({
                message: 'Thêm sản phẩm',
                description: `Sản phẩm "${product.name}" đã được thêm vào danh sách yêu thích`,
            });
        },
        error: (err) => {
            let description = 'Có lỗi xảy ra khi thêm sản phẩm vào danh sách yêu thích';
            let detail = err?.response?.data?.detail;
            if(detail?.includes('already')) {
                description = `Sản phẩm "${product.name}" đã tồn tại trong danh sách yêu thích`;
            }
            notification.error({
                message: 'Thêm thất bại',
                description,
            });
        },
    });

    const onFollowProduct = async () => {
        await mutationFollow.mutateAsync({
            productId: product?.id,
        });
    };

    return (
        <Card
            onClick={() => navigate(`${config.routes.web.product_detail}/${product.slug}`)}
            hoverable
            className="card-product"
            style={{
                width: '100%',
            }}
            cover={
                <img
                    alt="product"
                    src={product.images.filter((p) => p.isDefault === true)[0].image}
                />
            }
        >
            <Meta title={product.name} />
            <div className="star-color mt-[1.2rem] mb-[0.5rem] flex">
                {<Rate allowHalf disabled defaultValue={Math.ceil(product.rating * 2) / 2} />}
            </div>
            <div className="price text-[1.6rem] font-bold flex justify-between items-center">
                <div>
                    <span
                        className={` ${
                            price.promotionalItemPrice
                                ? 'text-[1.6rem] opacity-[0.8] font-normal line-through mr-[0.5rem]'
                                : 'price-color text-[2rem]'
                        }`}
                    >
                        {price.itemPrice}
                    </span>
                    {price.promotionalItemPrice && (
                        <span className="price-color text-[2rem]">
                            {price.promotionalItemPrice}
                        </span>
                    )}
                </div>
                <Tooltip placement="bottom" title="Yêu thích">
                    <FontAwesomeIcon
                        onClick={(e) => {
                            e.stopPropagation();
                            onFollowProduct();
                        }}
                        className="price-color transition-all delay-[0.3] text-[2.4rem] text-end hidden heart"
                        icon={faHeart}
                    />
                </Tooltip>
            </div>
        </Card>
    );
}

export default CardProduct;
