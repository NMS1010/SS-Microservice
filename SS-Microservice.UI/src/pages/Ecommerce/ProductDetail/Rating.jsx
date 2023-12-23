import images from '../../../assets/images';
import { Avatar, Image, Rate } from 'antd';

function Rating({ review }) {
    return (
        <div className="rating p-[2rem] border-b-[1px] ">
            <div className="flex">
                <div className="avatar">
                    <Avatar src={images.user} />
                </div>
                <div className="px-[1rem] flex-grow">
                    <div className="text-[1.2rem]">
                        <span>{review?.user?.email}</span>
                        <div className="star-color my-[0.3rem]">
                            <Rate value={review?.rating} disabled />
                        </div>
                        <div>
                            <span className="font-light">
                                {review?.updatedAt && new Date(review?.updatedAt).toLocaleString()}
                            </span>
                            <span className="mx-2">|</span>
                            <span className="font-light">
                                Phân loại hàng: {review?.variantName}
                            </span>
                        </div>
                    </div>
                    <div className="text-[1.4rem]">
                        <div className="my-[0.6rem]">{review?.content}</div>
                        <div className="w-[550px] max-md:w-[350px] max-sm:w-[150px] grid grid-cols-6 max-md:grid-cols-4 max-sm:grid-cols-2 gap-[2rem]">
                            {review?.image && (
                                <Image
                                    className="border border-solid"
                                    width={80}
                                    src={review?.image}
                                />
                            )}
                        </div>
                        {review?.reply && (
                            <div className="my-[0.6rem] bg-[#f5f5f5] p-[1rem] rounded-[2px]">
                                <p className="text-[1.4rem] text-[#000000de] capitalize">
                                    Phản hồi của người bán
                                </p>
                                <p className="text-[1.4rem] mt-[1rem]">{review?.reply}</p>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Rating;
