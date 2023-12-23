import { Button, Checkbox, Input, InputNumber, Menu, Rate, Slider } from 'antd';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronDown, faChevronUp } from '@fortawesome/free-solid-svg-icons';
import RecommentProduct from '../../../layouts/Ecommerce/components/RecommentProduct';
import { useGetListBrand, useGetListProductCategory } from '../../../hooks/api';
import { useState } from 'react';
import { MAX_PRICE, MIN_PRICE } from '../../../utils/constants';
import { useLocation } from 'react-router-dom';

const LIMIT = 5;

function params({ params, setParams, categoryId }) {
    const location = useLocation();
    const { data: brandData } = useGetListBrand({
        status: true,
    });
    const { data: categoryData } = useGetListProductCategory({
        status: true,
        parentCategoryId: categoryId,
    });
    const [isMoreBrand, setIsMoreBrand] = useState(false);
    const [isMoreCategory, setIsMoreCategory] = useState(false);

    const onPriceChange = (value) => {
        setParams({
            ...params,
            minPrice: value[0],
            maxPrice: value[1],
        });
    };

    const onBrandCheckBoxChange = (checked, id) => {
        let brandArr = params.brandIds;
        let temp = [];
        if (checked) {
            temp = [...brandArr, id];
        } else {
            temp = [...brandArr]?.filter((item) => item !== id);
        }

        setParams({
            ...params,
            brandIds: temp,
        });
    };

    const onCategoryCheckBoxChange = (checked, id) => {
        let categoryArr = params.categoryIds;
        let temp = [];
        if (checked) {
            temp = [...categoryArr, id];
        } else {
            temp = [...categoryArr]?.filter((item) => item !== id);
        }

        setParams({
            ...params,
            categoryIds: temp,
        });
    };

    const onRatingSelect = (e) => {
        setParams({
            ...params,
            rating: e.key,
        });
    };

    return (
        <div className="params max-lg:px-[3rem] p-[1rem] lg:border-r-[1px]">
            <div className="brand pt-[1.5rem] pb-[2.8rem] border-b-[1px]">
                <h3 className="text-[1.6rem] uppercase mb-[1.4rem]">Thương hiệu</h3>
                <div className="brand-list flex flex-col">
                    {brandData?.data?.items?.map((item, index) => {
                        if (isMoreBrand || index < LIMIT)
                            return (
                                <Checkbox
                                    onChange={(e) =>
                                        onBrandCheckBoxChange(e.target.checked, item.id)
                                    }
                                    checked={params?.brandIds?.includes(item.id)}
                                    key={item.id}
                                    className="text-[1.6rem] mb-[1rem]"
                                >
                                    {item.name}
                                </Checkbox>
                            );
                    })}
                </div>
                <div>
                    {brandData?.data?.items?.length > LIMIT && (
                        <Button
                            onClick={() => setIsMoreBrand(!isMoreBrand)}
                            className="text-[1.6rem] hover:bg-transparent"
                            type="text"
                        >
                            {isMoreBrand ? 'Ẩn bớt' : 'Xem thêm'}
                            <FontAwesomeIcon
                                className="text-[1.4rem] ml-[0.5rem] mb-[0.1rem]"
                                icon={isMoreBrand ? faChevronUp : faChevronDown}
                            />
                        </Button>
                    )}
                </div>
            </div>
            <div className="price pt-[1.5rem] pb-[2.8rem] border-b-[1px]">
                <h3 className="text-[1.6rem] uppercase mb-[1.4rem]">Mức giá</h3>
                <div className="price-list flex flex-col">
                    <Slider
                        step={10}
                        min={MIN_PRICE}
                        max={MAX_PRICE}
                        range={true}
                        defaultValue={[params.minPrice, params.maxPrice]}
                        onChange={onPriceChange}
                        value={[params.minPrice, params.maxPrice]}
                    />
                    <div className="flex justify-between">
                        <InputNumber
                            value={params.minPrice}
                            min={MIN_PRICE}
                            max={MAX_PRICE}
                            onChange={(value) => setParams({ ...params, minPrice: value })}
                            className="text-[1.5rem] mb-[1rem] w-[11rem]"
                            formatter={(value) =>
                                `đ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
                            }
                            parser={(value) => value.replace(/\đ\s?|(,*)/g, '')}
                        />
                        <InputNumber
                            value={params.maxPrice}
                            min={MIN_PRICE}
                            max={MAX_PRICE}
                            onChange={(value) => setParams({ ...params, maxPrice: value })}
                            className="text-[1.5rem] mb-[1rem] w-[11rem]"
                            formatter={(value) =>
                                `đ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
                            }
                            parser={(value) => value.replace(/\đ\s?|(,*)/g, '')}
                        />
                    </div>
                </div>
            </div>
            <div className="rating pt-[1.5rem] pb-[2.8rem]">
                <h3 className="text-[1.6rem] uppercase mb-[1.4rem]">Đánh giá</h3>
                <div className="rating-list flex flex-col">
                    <Menu onSelect={onRatingSelect} selectedKeys={[params.rating]}>
                        <Menu.Item key={5}>
                            <Rate value={5} disabled />
                        </Menu.Item>
                        <Menu.Item key={4}>
                            <Rate value={4} disabled /> <span className="text-black">trở lên</span>
                        </Menu.Item>
                        <Menu.Item key={3}>
                            <Rate value={3} disabled /> <span className="text-black">trở lên</span>
                        </Menu.Item>
                        <Menu.Item key={2}>
                            <Rate value={2} disabled /> <span className="text-black">trở lên</span>
                        </Menu.Item>
                        <Menu.Item key={1}>
                            <Rate value={1} disabled /> <span className="text-black">trở lên</span>
                        </Menu.Item>
                    </Menu>
                </div>
            </div>
            <div className="category pt-[1.5rem] pb-[2.8rem]">
                <h3 className="text-[1.6rem] uppercase mb-[1.4rem]">Danh mục</h3>
                <div className="category-list flex flex-col">
                    {categoryData?.data?.items?.map((item, index) => {
                        if (isMoreCategory || index < LIMIT)
                            return (
                                <Checkbox
                                    onChange={(e) =>
                                        onCategoryCheckBoxChange(e.target.checked, item.id)
                                    }
                                    checked={params?.categoryIds?.includes(item.id)}
                                    key={item.id}
                                    className="text-[1.6rem] mb-[1rem]"
                                >
                                    {item.name}
                                </Checkbox>
                            );
                    })}
                </div>
                <div>
                    {categoryData?.data?.items?.length > LIMIT && (
                        <Button
                            onClick={() => setIsMoreCategory(!isMoreCategory)}
                            className="text-[1.6rem] hover:bg-transparent"
                            type="text"
                        >
                            {isMoreCategory ? 'Ẩn bớt' : 'Xem thêm'}
                            <FontAwesomeIcon
                                className="text-[1.4rem] ml-[0.5rem] mb-[0.1rem]"
                                icon={isMoreCategory ? faChevronUp : faChevronDown}
                            />
                        </Button>
                    )}
                </div>
            </div>
            <RecommentProduct />
        </div>
    );
}

export default params;
