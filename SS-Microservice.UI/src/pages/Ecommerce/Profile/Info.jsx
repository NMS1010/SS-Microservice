import { Button, DatePicker, Form, Input, Radio, Upload, notification } from 'antd';
import UploadAvatar from './UploadAvatar';
import { useEffect, useState } from 'react';
import dayjs from 'dayjs';
import { useEditUser, useGetMe } from '../../../hooks/api';
import WebLoading from '../../../layouts/Ecommerce/components/WebLoading';

function Info() {
    const profile = useGetMe();
    const [processing, setProcessing] = useState(false);
    const [avatar, setAvatar] = useState(null);
    const [imageUrl, setImageUrl] = useState(profile?.data?.data?.avatar);
    const [form] = Form.useForm();
    const mutateEditUser = useEditUser({
        success: (data) => {
            notification.success({
                message: 'Cập nhật thông tin thành công!',
            });
            setAvatar(null);
            profile.refetch();
        },
        error: (err) => {
            notification.error({
                message: 'Cập nhật thông tin thất bại!',
            });
        },
        mutate: (data) => {
            setProcessing(true);
        },
        settled: (data) => {
            setProcessing(false);
        },
    });
    useEffect(() => {
        if (!profile?.data || profile?.isLoading) return;
        let user = profile?.data?.data;
        form.setFieldsValue({
            firstName: user?.firstName,
            lastName: user?.lastName,
            phone: user?.phone,
            gender: user?.gender || 'other',
            dob: dayjs(user?.dob, 'YYYY-MM-DD'),
        });
    }, [profile?.data, profile?.isLoading]);

    const onEditUser = async (values) => {
        let data = {
            ...values,
            status: 1,
            avatar: avatar,
            dob: values.dob.format('YYYY-MM-DD'),
        };
        await mutateEditUser.mutateAsync(data);
    };

    if (profile?.isLoading) return <WebLoading />;

    return (
        <div className="md:p-[5rem] sm:p-[1rem] form-container w-full">
            <Form
                form={form}
                onFinish={onEditUser}
                name="my-profile"
                labelCol={{
                    span: 8,
                }}
                wrapperCol={{
                    span: 16,
                }}
                initialValues={{
                    remember: true,
                }}
                autoComplete="off"
                className="grid sm:grid-cols-2 mx-[1.6rem]"
            >
                <div className="">
                    <Form.Item
                        label="Họ"
                        name="firstName"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập họ của bạn!',
                            },
                        ]}
                    >
                        <Input className="text-[1.6rem] h-[3rem] bg-white rounded-[0.3rem] shadow outline-none" />
                    </Form.Item>
                    <Form.Item
                        label="Tên"
                        name="lastName"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập tên của bạn!',
                            },
                        ]}
                    >
                        <Input className="text-[1.6rem] h-[3rem] bg-white rounded-[0.3rem] shadow outline-none" />
                    </Form.Item>
                    <Form.Item label="Email" name="email">
                        <p className="text-black text-[1.6rem] font-normal">
                            {profile?.data?.data?.email}
                        </p>
                    </Form.Item>
                    <Form.Item
                        label="Số điện thoại"
                        name="phone"
                        className=""
                        rules={[
                            {
                                required: true,
                                message: 'Nhập số điện thoại của bạn!',
                            },
                        ]}
                    >
                        <Input className="text-black text-[1.6rem] h-[3rem] bg-white rounded-[0.3rem] shadow" />
                    </Form.Item>
                    <Form.Item label="Giới tính" name="gender">
                        <Radio.Group defaultValue={'MALE'}>
                            <Radio value="MALE">Nam</Radio>
                            <Radio value="FEMALE">Nữ</Radio>
                            <Radio value="OTHER">Khác</Radio>
                        </Radio.Group>
                    </Form.Item>
                    <Form.Item
                        label="Ngày sinh"
                        name="dob"
                        rules={[
                            {
                                required: true,
                                message: 'Nhập ngày sinh của bạn!',
                            },
                        ]}
                    >
                        <DatePicker
                            format={'YYYY-MM-DD'}
                            className="max-w-[14rem] text-[1.6rem] p-3 h-[3rem] bg-white rounded-[0.3rem] shadow"
                        />
                    </Form.Item>
                </div>
                <div className="">
                    <UploadAvatar
                        setAvatar={setAvatar}
                        imageUrl={imageUrl}
                        setImageUrl={setImageUrl}
                    />
                </div>

                <Form.Item className="text-center max-md:flex xl:ml-[8rem] mt-2 max-sm:justify-center">
                    <Button
                        loading={processing}
                        className="submit-btn text-white text-[1.8rem] leading-4 h-[3rem] pb-[0.6rem] px-[4rem] rounded-[5px] border-none"
                        htmlType="submit"
                    >
                        Lưu
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
}

export default Info;
