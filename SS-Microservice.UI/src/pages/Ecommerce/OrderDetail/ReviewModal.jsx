import { Button, Form, Input, Modal, Rate, Spin, notification } from 'antd';
import { useEffect, useState } from 'react';
import UploadImage from './UploadImage';
import { useCreateReview, useUpdateReview } from '../../../hooks/api';

const desc = ['Tệ', 'Không hài lòng', 'Bình thường', 'Hài lòng', 'Tuyệt vời'];
function ReviewModal({ isReviewModalOpen, setIsReviewModalOpen, data }) {
    const [form] = Form.useForm();
    const [processing, setProcessing] = useState(false);
    const [image, setImage] = useState(null);
    const [rate, setRate] = useState(data?.rating || 1);
    const [imageUrl, setImageUrl] = useState(null);

    useEffect(() => {
        if (!data) return;

        form.setFieldsValue({
            title: data?.title,
            content: data?.content,
        });
        setImageUrl(data?.image);
    }, [data]);
    const mutateCreateReview = useCreateReview({
        success: (data) => {
            notification.success({
                message: 'Đánh giá đơn hàng thành công',
                description: 'Đánh giá của bạn đã được ghi nhận',
            });
            setIsReviewModalOpen(false);
            form.resetFields();
        },
        error: (err) => {
            let description = 'Có lỗi xảy ra khi đánh giá, vui lòng thử lại sau';
            notification.error({
                message: 'Đánh giá đơn hàng thất bại',
                description: description,
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });
    const mutateUpdateReview = useUpdateReview({
        success: (data) => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Đánh giá của bạn đã được ghi nhận',
            });
            setIsReviewModalOpen(false);
            form.resetFields();
        },
        error: (err) => {
            let description = 'Có lỗi xảy ra khi đánh giá, vui lòng thử lại sau';
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: description,
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });
    const onConfirm = async () => {
        try {
            await form.validateFields();
        } catch (err) {
            return;
        }
        const values = form.getFieldsValue();
        const orderItem = isReviewModalOpen?.item;
        let obj = {
            rating: rate,
            title: values?.title,
            content: values?.content,
            image: image,
            orderItemId: orderItem?.id,
            productId: orderItem?.productId,
        };
        if (data) {
            await mutateUpdateReview.mutateAsync({
                id: data?.id,
                body: {
                    ...obj,
                    isDeleteImage: !imageUrl
                },
            });
        } else {
            await mutateCreateReview.mutateAsync(obj);
        }
    };
    const onClose = () => {
        form.resetFields();
        setIsReviewModalOpen({ item: null, isOpen: false });
    };
    return (
        <Modal
            title={
                <p className="text-center text-[2rem] mb-6">
                    {data ? 'Chỉnh sửa đánh giá' : 'Đánh giá sản phẩm'}
                </p>
            }
            open={isReviewModalOpen.isOpen}
            onCancel={onClose}
            footer={[
                <Button
                    loading={processing}
                    onClick={onConfirm}
                    type="primary"
                    className="bg-red-500 text-white"
                >
                    {data ? 'Chỉnh sửa' : 'Đánh giá'}
                </Button>,
            ]}
        >
            <div className={`flex gap-[2.2rem] mx-[2.1rem] py-[1.2rem]`}>
                <img
                    className="w-[7.9rem] h-[7.9rem] border border-solid"
                    src={isReviewModalOpen?.item?.productImage}
                />
                <div className="">
                    <p className="text-black text-[1.5rem] font-normal">
                        {isReviewModalOpen?.item?.productName}
                    </p>
                    <p className="text-[1.2rem] mb-[1rem] mt-[.6rem]">
                        Phân loại: {isReviewModalOpen?.item?.variantName}
                    </p>
                </div>
            </div>
            <Form form={form} name="review-form" layout="vertical">
                <div className="flex gap-[3rem] items-center my-2">
                    <span>Chất lượng sản phẩm</span>
                    <div className="flex items-center gap-[1rem]">
                        <Rate
                            className="text-[2.6rem]"
                            tooltips={desc}
                            value={rate}
                            onChange={(v) => {
                                setRate(v < 1 ? 1 : v);
                            }}
                        />
                        {rate ? <span className="ant-rate-text">{desc[rate - 1]}</span> : ''}
                    </div>
                </div>
                <Form.Item
                    name="title"
                    label="Tiêu đề"
                    rules={[
                        {
                            required: true,
                            message: 'Vui lòng nhập tiêu đề đánh giá',
                        },
                    ]}
                >
                    <Input.TextArea
                        rows={2}
                        autoSize={{ minRows: 2, maxRows: 2 }}
                        className="w-full border border-solid border-gray-300 rounded-lg px-4 py-2"
                        placeholder="Tiêu đề đánh giá của bạn"
                    />
                </Form.Item>
                <Form.Item
                    name="content"
                    label="Nội dung đánh giá"
                    rules={[
                        {
                            required: true,
                            message: 'Vui lòng nhập nội dung đánh giá',
                        },
                    ]}
                >
                    <Input.TextArea
                        rows={4}
                        autoSize={{ minRows: 4, maxRows: 4 }}
                        className="w-full border border-solid border-gray-300 rounded-lg px-4 py-2"
                        placeholder="Hãy chia sẻ những điều bạn thích về sản phẩm này với những người mua khác nhé."
                    />
                </Form.Item>
                <UploadImage setImage={setImage} imageUrl={imageUrl} setImageUrl={setImageUrl} />
            </Form>
        </Modal>
    );
}

export default ReviewModal;
