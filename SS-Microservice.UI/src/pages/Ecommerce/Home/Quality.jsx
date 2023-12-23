import images from '../../../assets/images';

function Quality() {
    return (
        <div className="quality max-w-[1200px] mx-auto p-[3rem] pb-[5rem]">
            <div className="w-full justify-center items-center gap-[1.6rem] inline-flex mb-[2rem]">
                <img className="h-[25px]" src={images.quality.quality} />
                <div className="text-center text-color text-[3rem] font-bold">
                    Tiêu chuẩn chất lượng
                </div>
            </div>
            <div className="grid grid-cols-4 max-md:grid-cols-2 gap-[2rem] mx-auto">
                <div className="image-list h-[150px] flex justify-center rounded-[10px] p-[3rem] bg-white">
                    <img className="h-full" src={images.quality.quality1} alt="" />
                </div>
                <div className="image-list h-[150px] flex justify-center rounded-[10px] p-[3rem] bg-white">
                    <img className="h-full" src={images.quality.quality2} alt="" />
                </div>
                <div className="image-list h-[150px] flex justify-center rounded-[10px] p-[3rem] bg-white">
                    <img className="h-full" src={images.quality.quality3} alt="" />
                </div>
                <div className="image-list h-[150px] flex justify-center rounded-[10px] p-[3rem] bg-white">
                    <img className="h-full" src={images.quality.quality4} alt="" />
                </div>
            </div>
        </div>
    );
}

export default Quality;
