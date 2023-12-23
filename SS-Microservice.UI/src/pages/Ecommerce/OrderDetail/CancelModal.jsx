import { Button, Form, Input, Modal, Select, notification } from 'antd';
import { useGetListOrderCancellationReason } from '../../../hooks/api/useOrderCancellationReasonApi';
import { useUpdateOrder } from '../../../hooks/api/useOrderApi';
import { useEffect, useState } from 'react';
import { ORDER_STATUS } from '../../../utils/constants';

function CancelModal({ isCancelModelOpen, setIsCancelModelOpen, orderId, orderRefetch }) {
    const { data, isLoading } = useGetListOrderCancellationReason({
        status: true,
    });
    const [reasonOptions, setReasonOptions] = useState([]);
    useEffect(() => {
        if (!data || isLoading) return;
        let dt = data?.data?.items?.map((item) => {
            return {
                label: item?.name,
                value: item?.id,
            };
        });
        setReasonOptions([
            ...dt,
            {
                label: 'Lý do khác',
                value: null,
            },
        ]);
    }, [data, isLoading]);

    const [reason, setReason] = useState(null);
    const [form] = Form.useForm();
    const [processing, setProcessing] = useState(false);

    const mutateUpdateOrder = useUpdateOrder({
        success: (data) => {
            // notification.success({
            //     message: 'Huỷ đơn hàng thành công',
            //     description: 'Đơn hàng của bạn đã được huỷ thành công',
            // })
            orderRefetch();
            setIsCancelModelOpen(false);
            form.resetFields();
            setReason(null);
        },
        error: (err) => {
            let description = 'Có lỗi xảy ra khi huỷ, vui lòng thử lại sau';
            notification.error({
                message: 'Huỷ đơn hàng thất bại',
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
    const onConfirmCancellation = async () => {
        await mutateUpdateOrder.mutateAsync({
            id: orderId,
            body: {
                status: ORDER_STATUS.CANCELLED,
                orderCancellationReasonId: reason,
                otherCancellation: form.getFieldValue('otherReason'),
            }
        });
    };

    return (
        <Modal
            title={<p className="text-center text-[2rem] mb-6">Huỷ đơn hàng</p>}
            open={isCancelModelOpen}
            onCancel={() => setIsCancelModelOpen(false)}
            footer={[
                <Button
                    type="primary"
                    className="bg-transparent border border-red-400 border-solid text-black"
                    onClick={() => setIsCancelModelOpen(false)}
                >
                    Thoát
                </Button>,
                <Button
                    loading={processing}
                    onClick={onConfirmCancellation}
                    type="primary"
                    className="bg-red-500 text-white"
                >
                    Xác nhận huỷ
                </Button>,
            ]}
        >
            <Form form={form} name="cancel-form" layout="vertical">
                <Form.Item name="reason" label="Lý do huỷ đơn hàng">
                    <Select
                        defaultValue={reason}
                        onChange={(id) => {
                            setReason(id);
                            form.setFieldValue('reason', id);
                        }}
                        showSearch
                        filterOption={(input, option) =>
                            (option?.label ?? '').toLowerCase().includes(input.toLowerCase())
                        }
                        options={reasonOptions}
                    />
                </Form.Item>
                {!reason && (
                    <Form.Item name="otherReason" label="Lý do khác">
                        <Input.TextArea
                            rows={4}
                            className="w-full h-[4.2rem] border border-solid border-gray-300 rounded-lg px-4 py-2"
                            placeholder="Nhập lý do huỷ của bạn"
                        />
                    </Form.Item>
                )}
            </Form>
        </Modal>
    );
}

export default CancelModal;
