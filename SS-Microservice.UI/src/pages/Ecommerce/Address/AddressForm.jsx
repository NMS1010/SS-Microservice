import { Button, Col, Form, Input, Modal, Row, Select, notification } from 'antd';
import {
    useCreateAddress,
    useGetAddress,
    useGetListDistrict,
    useGetListProvince,
    useGetListWard,
    useUpdateAddress,
} from '../../../hooks/api/useAddressApi';
import { useEffect, useState } from 'react';

function AddressForm({ isFormOpen, setIsFormOpen }) {
    const { data, isLoading } =
        isFormOpen.id !== 0 ? useGetAddress(isFormOpen.id) : { data: null, isLoading: false };

    const [form] = Form.useForm();

    const [processing, setProcessing] = useState(false);

    const provinceApi = useGetListProvince();
    const [chosenProvince, setChosenProvince] = useState(1);

    const districtApi = useGetListDistrict(chosenProvince);
    const [chosenDistrict, setChosenDistrict] = useState(1);

    const wardApi = useGetListWard(chosenDistrict);
    const mutationCreate = useCreateAddress({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Địa chỉ mới của bạn đã được thêm',
            });
            setIsFormOpen({ ...isFormOpen, isOpen: false });
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: err?.response?.data?.detail,
            });
            setIsFormOpen({ ...isFormOpen, isOpen: false });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
        obj: {
            params: {
                status: true,
            },
        },
    });
    const mutationUpdate = useUpdateAddress({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Địa chỉ của bạn đã được cập nhật',
            });
            setIsFormOpen({ ...isFormOpen, isOpen: false });
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: err?.response?.data?.detail,
            });
            setIsFormOpen({ ...isFormOpen, isOpen: false });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
        obj: {
            params: {
                status: true,
            },
        },
    });
    const onCancel = () => {
        form.resetFields();
        setIsFormOpen({ ...isFormOpen, isOpen: false });
    };
    const onAdd = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        await mutationCreate.mutateAsync({
            provinceId: form.getFieldValue('province'),
            districtId: form.getFieldValue('district'),
            wardId: form.getFieldValue('ward'),
            street: form.getFieldValue('street'),
            email: form.getFieldValue('email'),
            phone: form.getFieldValue('phone'),
            receiver: form.getFieldValue('receiver'),
        });
    };

    const onUpdate = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        await mutationUpdate.mutateAsync({
            id: isFormOpen.id,
            body: {
                provinceId: form.getFieldValue('province'),
                districtId: form.getFieldValue('district'),
                wardId: form.getFieldValue('ward'),
                street: form.getFieldValue('street'),
                email: form.getFieldValue('email'),
                phone: form.getFieldValue('phone'),
                receiver: form.getFieldValue('receiver'),
            },
        });
    };

    useEffect(() => {
        if (isLoading || !data) return;
        let address = data.data;
        form.setFieldsValue({
            receiver: address?.receiver,
            email: address?.email,
            phone: address?.phone,
            province: address?.province?.id,
            district: address?.district?.id,
            ward: address?.ward?.id,
            street: address?.street,
        });
        setChosenProvince(address?.province?.id);
        setChosenDistrict(address?.district?.id);
    }, [isLoading, data]);

    return (
        <Modal
            width={800}
            title={<p className="text-center text-[2rem] mb-6">Địa chỉ của bạn</p>}
            open={isFormOpen.isOpen}
            onCancel={onCancel}
            footer={null}
        >
            <Form name="address-form" layout="vertical" form={form}>
                <Row gutter={16}>
                    <Col span={12}>
                        <Form.Item
                            label="Họ và tên"
                            name="receiver"
                            rules={[
                                {
                                    required: true,
                                    message: 'Nhập tên người nhận!',
                                },
                            ]}
                        >
                            <Input.TextArea
                                rows={1}
                                autoSize={{ maxRows: 1, minRows: 1 }}
                                className="text-[1.6rem]"
                            />
                        </Form.Item>
                    </Col>
                    <Col span={12}>
                        <Form.Item
                            label="Email"
                            name="email"
                            rules={[
                                {
                                    required: true,
                                    message: 'Nhập email của bạn!',
                                },
                                {
                                    type: 'email',
                                    message: 'Nhập đúng định dạng email',
                                },
                            ]}
                        >
                            <Input.TextArea
                                rows={1}
                                autoSize={{ maxRows: 1, minRows: 1 }}
                                className="text-[1.6rem]"
                            />
                        </Form.Item>
                    </Col>
                </Row>
                <Row gutter={16}>
                    <Col span={12}>
                        <Form.Item
                            label="Số điện thoại"
                            name="phone"
                            rules={[
                                {
                                    required: true,
                                    message: 'Nhập số điện thoại của bạn!',
                                },
                            ]}
                        >
                            <Input.TextArea
                                rows={1}
                                autoSize={{ maxRows: 1, minRows: 1 }}
                                className="text-[1.6rem]"
                            />
                        </Form.Item>
                    </Col>
                    <Col span={12}>
                        <Form.Item
                            label="Địa chỉ cụ thể"
                            name="street"
                            rules={[
                                {
                                    required: true,
                                    message: 'Nhập địa chỉ cụ thể của bạn!',
                                },
                            ]}
                        >
                            <Input.TextArea
                                rows={1}
                                autoSize={{ maxRows: 1, minRows: 1 }}
                                className="text-[1.6rem]"
                            />
                        </Form.Item>
                    </Col>
                </Row>
                <Row gutter={16}>
                    <Col span={8}>
                        <Form.Item
                            label="Tỉnh/Thành phố"
                            name="province"
                            rules={[
                                {
                                    required: true,
                                    message: 'Chọn tỉnh thành phố của bạn!',
                                },
                            ]}
                        >
                            <Select
                                className="text-[1.6rem]"
                                onChange={(id) => {
                                    setChosenProvince(id);
                                    form.setFieldValue('province', id);
                                    form.setFieldValue('district', '');
                                    form.setFieldValue('ward', '');
                                }}
                                showSearch
                                filterOption={(input, option) =>
                                    (option?.label ?? '')
                                        .toLowerCase()
                                        .includes(input.toLowerCase())
                                }
                                options={provinceApi?.data?.data?.map((item) => {
                                    return {
                                        label: item?.name,
                                        value: item?.id,
                                    };
                                })}
                            />
                        </Form.Item>
                    </Col>
                    <Col span={8}>
                        <Form.Item
                            label="Quận/Huyện"
                            name="district"
                            rules={[
                                {
                                    required: true,
                                    message: 'Chọn quận huyện của bạn!',
                                },
                            ]}
                        >
                            <Select
                                className="text-[1.6rem]"
                                onChange={(id) => {
                                    setChosenDistrict(id);
                                    form.setFieldValue('district', id);
                                    form.setFieldValue('ward', '');
                                }}
                                showSearch
                                filterOption={(input, option) =>
                                    (option?.label ?? '')
                                        .toLowerCase()
                                        .includes(input.toLowerCase())
                                }
                                options={districtApi?.data?.data?.map((item) => {
                                    return {
                                        label: item?.name,
                                        value: item?.id,
                                    };
                                })}
                            />
                        </Form.Item>
                    </Col>
                    <Col span={8}>
                        <Form.Item
                            label="Phường/Xã"
                            name="ward"
                            rules={[
                                {
                                    required: true,
                                    message: 'Chọn xã phường của bạn!',
                                },
                            ]}
                        >
                            <Select
                                className="text-[1.6rem]"
                                onChange={(id) => {
                                    form.setFieldValue('ward', id);
                                }}
                                showSearch
                                filterOption={(input, option) =>
                                    (option?.label ?? '')
                                        .toLowerCase()
                                        .includes(input.toLowerCase())
                                }
                                options={wardApi?.data?.data?.map((item) => {
                                    return {
                                        label: item?.name,
                                        value: item?.id,
                                    };
                                })}
                            />
                        </Form.Item>
                    </Col>
                </Row>
                <div className="flex justify-end items-center gap-[1rem]">
                    <Button onClick={onCancel} type="primary" className="bg-red-500 text-white">
                        Huỷ
                    </Button>
                    <Button
                        loading={processing}
                        htmlType="submit"
                        onClick={isFormOpen.id ? onUpdate : onAdd}
                        className="bg-blue-500 text-white min-w-[10%]"
                    >
                        {isFormOpen.id ? 'Cập nhật' : 'Thêm mới'}
                    </Button>
                </div>
            </Form>
        </Modal>
    );
}

export default AddressForm;
