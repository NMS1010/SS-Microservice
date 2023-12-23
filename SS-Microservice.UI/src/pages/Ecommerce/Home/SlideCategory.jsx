import images from '../../../assets/images';
import { useEffect, useState } from 'react';
import { useGetListProductCategory } from '../../../hooks/api';
import { useNavigate } from 'react-router-dom';
import config from '../../../config';

function SlideCategory() {
    const navigate = useNavigate();
    const { isLoading, data } = useGetListProductCategory({
        status: true
    });
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setCategories(data?.data?.items);
    }, [isLoading, data]);

    const categoryList = document.querySelector('.category-list');

    const clickSlideBtn = (e) => {
        const direction = e.target.classList.contains('prev-slide') ? -1 : 1;
        categoryList.scrollBy({ left: 143 * direction, behavior: 'smooth' });
    };

    return (
        <div className="category my-[5rem]">
            <div className="w-full justify-center items-center gap-[1.6rem] inline-flex mb-[2rem]">
                <img className="h-[25px]" src={images.cart} />
                <div className="text-center text-color text-[3rem] font-bold">
                    Mua sắm thôi nào!
                </div>
            </div>
            <div className="category-list xl:w-[1120px] max-xl:w-[944px] max-lg:w-[688px] max-md:w-[560px] max-sm:w-[350px] mx-auto">
                <button className="prev-slide" onClick={(e) => clickSlideBtn(e)} />
                {categories.length > 0 &&
                    categories.map((item, index) => (
                        <div
                            key={index}
                            className="category-item flex flex-col items-center cursor-pointer"
                            onClick={() => navigate(item.slug)}
                        >
                            <div className="image w-[75px] h-[75px] rounded-[50%]">
                                <img src={item.image} alt="image category" />
                            </div>
                            <span className="text-center text-[1.4rem] mt-[0.4rem]">
                                {item.name}
                            </span>
                        </div>
                    ))}
                <button className="next-slide right-0" onClick={(e) => clickSlideBtn(e)} />
            </div>
        </div>
    );
}

export default SlideCategory;
