import { faClock, faStar, faUser } from '@fortawesome/free-regular-svg-icons';
import { faBox, faQuoteLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Image, Modal, Rate, Tag } from 'antd';
import { useGetReview } from '../../../hooks/api';

function ReviewDetail({ isDetailOpen, setIsDetailOpen }) {
    const onCancel = () => {
        setIsDetailOpen({ ...isDetailOpen, isOpen: false });
    };
    const { data, isLoading } = useGetReview(isDetailOpen.id);

    return (
        <Modal
            title={
                <p className="my-4 font-bold text-[1.6rem]">
                    Chi tiết đánh giá ID {data?.data?.id}
                </p>
            }
            width={800}
            open={isDetailOpen.isOpen}
            onCancel={onCancel}
            footer={[
                <Button type="primary" className="bg-red-500 text-white" onClick={onCancel}>
                    OK
                </Button>,
            ]}
        >
            <div>
                <div className="flex items-center gap-[1.5rem]">
                    <div className="flex items-center">
                        <Tag color="green" className="text-2xl px-4 py-2">
                            <FontAwesomeIcon icon={faClock} />
                        </Tag>
                        <p>
                            {data?.data?.updatedAt
                                ? new Date(data?.data?.updatedAt).toLocaleString()
                                : new Date(data?.data?.createdAt).toLocaleString()}
                        </p>
                    </div>
                    <div className="flex items-center">
                        <Tag color="green" className="text-2xl px-4 py-2">
                            <FontAwesomeIcon icon={faUser} />
                        </Tag>
                        <p>{data?.data?.user?.firstName + ' ' + data?.data?.user?.lastName}</p>
                    </div>
                    <div className="flex items-center">
                        <Tag color="green" className="text-2xl px-4 py-2">
                            <FontAwesomeIcon icon={faBox} />
                        </Tag>
                        <p>{data?.data?.product?.name}</p>
                    </div>
                    <div className="flex items-center">
                        <Tag color="green" className="text-2xl px-4 py-2">
                            <FontAwesomeIcon icon={faStar} />
                        </Tag>
                        <Rate className="text-2xl" disabled value={data?.data?.rating} allowHalf />
                    </div>
                </div>
                <div className="flex p-6 gap-[2rem]">
                    <FontAwesomeIcon className="text-5xl text-gray-500" icon={faQuoteLeft} />
                    <div className="flex flex-col p-6 gap-[2rem]">
                        <p>{data?.data?.content}</p>
                        {data?.data?.image && (
                            <Image
                                className="border border-solid"
                                width={80}
                                src={data?.data?.image}
                            />
                        )}
                    </div>
                </div>
            </div>
        </Modal>
    );
}

export default ReviewDetail;
