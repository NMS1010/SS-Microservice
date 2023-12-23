import images from '../../../assets/images';
import CardProduct from '../../../layouts/Ecommerce/components/CardProduct';
import SortProductDropdown from './SortProductDropdown';
import SortProductTab from './SortProductTab';
import { Pagination } from 'antd';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFilter } from '@fortawesome/free-solid-svg-icons';
import { useGetListFilteringProduct } from '../../../hooks/api';
import SpinLoading from '../../../layouts/Ecommerce/components/SpinLoading';
import { useDebounce } from '../../../hooks/custom';

function Product({ params, setParams, categoryName }) {
    const { isLoading, data } = useGetListFilteringProduct(useDebounce(params, 1000));

    const handlePagingChange = (page, pageSize) => {
        setParams({
            ...params,
            pageIndex: page,
            pageSize: pageSize,
        });
    };

    return (
        <div className="product py-[1rem] px-[2.6rem]">
            <div className="banner h-[320px] rounded-[5px] shadow-[2px_2px_6px_0_rgba(0,0,0,0.4)]">
                <img
                    className="h-full w-full object-top object-cover rounded-[5px]"
                    src={images.banner.main}
                    alt=""
                />
            </div>
            <h2 className="text-[2.6rem] mt-[4rem] mb-[2rem]">{categoryName}</h2>
            <div className="sort">
                <div className="max-lg:border-b-[1px] max-lg:pb-[1rem] max-lg:mb-[1rem] flex justify-between items-center">
                    <div>
                        <span className="text-[1.6rem] mr-[2rem]">Sắp xếp:</span>
                        <SortProductDropdown />
                    </div>
                    <label className="lg:hidden text-[1.4rem] cursor-pointer" for="open">
                        <FontAwesomeIcon className="mr-[1rem]" icon={faFilter} />
                        Lọc
                    </label>
                </div>
                <SortProductTab params={params} setParams={setParams} />
            </div>
            <div className="product-list">
                {!isLoading ? (
                    data?.data?.items?.length > 0 ? (
                        <div className="grid grid-cols-4 max-xl:grid-cols-3 max-md:grid-cols-2 max-sm:grid-cols-1 gap-[2rem]">
                            {data?.data?.items?.map((item, index) => (
                                <CardProduct key={index} product={item} />
                            ))}
                        </div>
                    ) : (
                        <div className="text-center text-[2rem]">Không có sản phẩm nào</div>
                    )
                ) : (
                    <div className="text-center">
                        <SpinLoading />
                    </div>
                )}
            </div>
            {data?.data?.items?.length > 0 && (
                <div className="mt-[4rem] text-center">
                    <Pagination
                        onChange={handlePagingChange}
                        pageSize={data?.data?.itemsPerPage}
                        current={data?.data?.pageIndex}
                        total={data?.data?.totalItems}
                    />
                </div>
            )}
        </div>
    );
}

export default Product;
