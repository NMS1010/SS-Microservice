import Images from './Images';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHeart } from '@fortawesome/free-regular-svg-icons';
import PriceVariant from './PriceVariant';
import { useEffect, useState } from 'react';
import { Button, Rate, notification } from 'antd';
import { faCartShopping, faStar } from '@fortawesome/free-solid-svg-icons';
import { useAddVariantToCart } from '../../../hooks/api/useCartApi';
import { useFollowProduct } from '../../../hooks/api/useFollowProductApi';

function Information({ product }) {
    const [processingCart, setProcessingCart] = useState(false);
    const [processingFollow, setProcessingFollow] = useState(false);
    const [count, setCount] = useState(1);
    const [chosenVariant, setChosenVariant] = useState(product?.variants[0]);

    const mutationAdd = useAddVariantToCart({
        success: (data) => {
            notification.success({
                message: 'Thêm sản phẩm',
                description: 'Sản phẩm đã được thêm vào giỏ hàng',
            });
        },
        error: (err) => {
            console.log(err);
            let description = 'Sản phẩm chưa được thêm vào giỏ hàng, có lỗi xảy ra';
            let detail = err?.response?.data?.detail?.toLowerCase();
            if (detail?.includes('quantity')) {
                description = 'Số lượng sản phẩm trong kho không đủ';
            }
            notification.error({
                message: 'Thêm thất bại',
                description: description,
            });
        },
        mutate: (data) => {
            setProcessingCart(true);
        },
        settled: (data) => {
            setProcessingCart(false);
        },
        obj: {
            params: {
                // pageSize: 1000,
                all: true
            },
        },
    });

    const mutationFollow = useFollowProduct({
        success: (data) => {
            notification.success({
                message: 'Thêm sản phẩm',
                description: 'Sản phẩm đã được thêm vào danh sách yêu thích',
            });
        },
        error: (err) => {
            let description = 'Có lỗi xảy ra khi thêm sản phẩm vào danh sách yêu thích';
            let detail = err?.response?.data?.detail;
            if(detail?.includes('already')) {
                description = 'Sản phẩm đã tồn tại trong danh sách yêu thích';
            }
            notification.error({
                message: 'Thêm thất bại',
                description,
            });
        },
        mutate: (data) => {
            setProcessingFollow(true);
        },
        settled: (data) => {
            setProcessingFollow(false);
        },
    });

    useEffect(() => {
        if (!product) return;
        setChosenVariant(product?.variants[0]);
    }, [product]);

    const increaseCount = () => {
        setCount(count + 1);
    };
    const decreaseCount = () => {
        setCount(count > 1 ? count - 1 : count);
    };

    const onAddToCart = async () => {
        await mutationAdd.mutateAsync({
            variantId: chosenVariant?.id,
            quantity: count,
        });
    };

    const onFollowProduct = async () => {
        await mutationFollow.mutateAsync({
            productId: product?.id,
        });
    };
    return (
        <div className="infor-product grid grid-cols-12">
            <div className="col-span-5 max-lg:col-span-12">
                <Images productImages={product?.images} />
            </div>
            <div className="col-span-7 max-lg:col-span-12 ml-[2rem]">
                <h2 className="text-[2.8rem]">{product?.name}</h2>
                <div className="flex items-center text-[1.4rem]">
                    <div className="pr-[1rem] mr-[1rem] border-r-[1px]">
                        <span className="mr-[0.5rem]">SKU:</span>
                        <span className="star-color">{chosenVariant?.sku}</span>
                    </div>
                    {/* <div className="pr-[1rem] mr-[1rem] border-r-[1px]">
                        <span className="mr-[0.5rem] star-color">23773</span>
                        <span>lượt xem</span>
                    </div> */}
                    <div className="pr-[1rem] mr-[1rem]">
                        <span className="mr-[0.5rem] star-color">{product?.sold}</span>
                        <span>đã bán</span>
                    </div>
                </div>
                <div className="text-[1.6rem] my-[1rem]">
                    <div className="star-color mb-[1rem]">
                        <Rate
                            allowHalf
                            disabled
                            value={
                                product?.rating - 0.5 < Math.floor(product?.rating)
                                    ? Math.ceil(product?.rating * 2) / 2
                                    : Math.floor(product?.rating * 2) / 2
                            }
                            character={() => <FontAwesomeIcon icon={faStar} />}
                        />
                    </div>
                    <div className="flex items-center">
                        <div className="pr-[1rem] mr-[1rem] border-r-[1px]">
                            <span className="mr-[0.5rem]">Thương hiệu:</span>
                            <span className="primary-color">{product?.brand?.name}</span>
                        </div>
                        <div className="pr-[1rem] mr-[1rem]">
                            <span className="mr-[0.5rem]">Tình trạng:</span>
                            <span className="primary-color">
                                {product?.status === 'ACTIVE' ? 'Đang bán' : 'Ngưng bán'}
                            </span>
                        </div>
                    </div>
                    <div className="mt-[3rem]">
                        <span>Mua càng nhiều, giá càng rẻ:</span>
                        <PriceVariant
                            variants={product?.variants}
                            unit={product?.unit}
                            setChosenVariant={setChosenVariant}
                            chosenVariant={chosenVariant}
                        />
                    </div>
                </div>
                <div className="border-t-[1px] pt-[1.4rem]">
                    <span className="text-[1.6rem]">Số lượng:</span>
                    <div className="w-[124px] h-[30px] mt-[1.4rem] flex justify-between items-center text-[1.6rem] text-center border-[1px] rounded-[5px]">
                        <div
                            className="h-full leading-[1.7] px-[1.2rem] border-r-[1px] cursor-pointer"
                            onClick={decreaseCount}
                        >
                            -
                        </div>
                        <div>{count}</div>
                        <div
                            className="h-full leading-[1.7] px-[1.2rem] border-l-[1px] cursor-pointer"
                            onClick={increaseCount}
                        >
                            +
                        </div>
                    </div>
                </div>
                <div className="option mt-[2.6rem] max-sm:mt-[2rem] flex max-sm:flex-col max-sm:items-start items-center">
                    <Button
                        loading={processingFollow}
                        onClick={onFollowProduct}
                        className="add-product-like max-sm:mb-[1rem]"
                        block
                        icon={<FontAwesomeIcon icon={faHeart} />}
                    >
                        Yêu thích
                    </Button>
                    <Button
                        loading={processingCart}
                        onClick={onAddToCart}
                        className="add-to-cart"
                        block
                        icon={<FontAwesomeIcon icon={faCartShopping} />}
                    >
                        Giỏ hàng
                    </Button>
                </div>
            </div>
        </div>
    );
}

export default Information;
