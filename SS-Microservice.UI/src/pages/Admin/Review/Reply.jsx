import { faClock, faStar, faUser } from '@fortawesome/free-regular-svg-icons';
import { faBox, faQuoteLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Image, Input, Modal, Rate, Tag, notification } from 'antd';
import { useGetReview, useReplyReview } from '../../../hooks/api';
import { useEffect, useState } from 'react';

function Reply({ isReplyOpen, setIsReplyOpen }) {
    const onCancel = () => {
        setIsReplyOpen({ ...isReplyOpen, isOpen: false });
    };
    const [processing, setProcessing] = useState(false);
    const { data, isLoading, refetch } = useGetReview(isReplyOpen.id);
    const [reply, setReply] = useState('');
    useEffect(() => {
        if (!data) return;
        setReply(data?.data?.reply);
    }, [data]);
    const mutateReply = useReplyReview({
        success: (data) => {
            notification.success({
                message: 'Thêm phản hồi thành công',
            });
            refetch();
            setIsReplyOpen({ ...isReplyOpen, isOpen: false });
            setReply('');
        },
        error: (err) => {
            notification.error({
                message: 'Thêm phản hồi thất bại',
            });
        },
        mutate: (data) => {
            setProcessing(true);
        },
        settled: (data) => {
            setProcessing(false);
        },
    });

    const onReply = async () => {
        await mutateReply.mutateAsync({
            id: isReplyOpen.id,
            body: {
                reply: reply,
            },
        });
    };

    return (
        <Modal
            title={
                <p className="my-4 font-bold text-[1.6rem]">
                    Phản hồi đánh giá ID {data?.data?.id}
                </p>
            }
            width={800}
            open={isReplyOpen.isOpen}
            onCancel={onCancel}
            footer={[
                <Button
                    type="primary"
                    className="bg-white text-red-500 border border-red-500 border-solid"
                    onClick={onCancel}
                >
                    Huỷ
                </Button>,
                <Button
                    loading={processing}
                    type="primary"
                    className="bg-red-500 text-white"
                    onClick={onReply}
                >
                    Thêm phản hồi
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
                    <p>{data?.data?.content}</p>
                </div>
                <div>
                    {data?.data?.image && (
                        <>
                            <p className='my-3 text-2xl'>Ảnh minh hoạ</p>
                            <Image
                                className="border border-solid"
                                width={200}
                                height={200}
                                src={data?.data?.image}
                            />
                        </>
                    )}
                </div>
                <div className="flex pt-6 gap-[2rem]">
                    <Input.TextArea
                        value={reply}
                        onChange={(e) => setReply(e.target.value)}
                        autoSize={{ minRows: 4, maxRows: 4 }}
                        placeholder="Nhập nội dung phản hồi đánh giá"
                        rows={4}
                    />
                </div>
            </div>
        </Modal>
    );
}

export default Reply;
