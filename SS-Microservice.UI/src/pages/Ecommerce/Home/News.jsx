import images from '../../../assets/images';
import { Link } from 'react-router-dom';
import CardNews from './CardNews';

function News() {
    return (
        <div className="news  max-w-[1200px] mx-auto p-[3rem]">
            <div className="w-full justify-center items-center gap-[1.6rem] inline-flex mb-[2rem]">
                <img className="h-[25px]" src={images.quality.quality} />
                <div className="text-center text-color text-[3rem] font-bold">Tin tức</div>
            </div>
            <div className="grid grid-cols-4 max-lg:grid-cols-2 max-sm:grid-cols-1 gap-[2rem] mx-auto">
                <CardNews />
                <CardNews />
                <CardNews />
                <CardNews />
            </div>
            <div className="load-more primary-color mt-[2rem] text-center text-[1.4rem]">
                <Link>Xem tất cả</Link>
            </div>
        </div>
    );
}

export default News;
