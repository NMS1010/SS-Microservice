import { Button, Col, DatePicker, Form, Input, Row, Select, Spin, notification } from 'antd';
import { useNavigate, useParams } from 'react-router-dom';
import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import dayjs from 'dayjs';

import './employee.scss';
import config from '../../../config';
import { useCreateEmployee, useGetEmployee, useUpdateEmployee } from '../../../hooks/api';
import { useEffect, useState } from 'react';
import {
    useGetListDistrict,
    useGetListProvince,
    useGetListWard,
} from '../../../hooks/api/useAddressApi';

function EmployeeFormPage() {
    let { id } = useParams();
    const navigate = useNavigate();

    const [processing, setProcessing] = useState(false);
    const { data, isLoading } = id ? useGetEmployee(id) : { data: null, isLoading: null };

    const provinceApi = useGetListProvince();
    const [chosenProvince, setChosenProvince] = useState(1);

    const districtApi = useGetListDistrict(chosenProvince);
    const [chosenDistrict, setChosenDistrict] = useState(1);

    const wardApi = useGetListWard(chosenDistrict);

    const [form] = Form.useForm();
    const mutationCreate = useCreateEmployee({
        success: () => {
            notification.success({
                message: 'Thêm thành công',
                description: 'Nhân viên mới đã được tạo',
            });
            navigate(config.routes.admin.employee);
        },
        error: (err) => {
            notification.error({
                message: 'Thêm thất bại',
                description: err?.response?.data?.detail,
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });
    const mutationUpdate = useUpdateEmployee({
        success: () => {
            notification.success({
                message: 'Chỉnh sửa thành công',
                description: 'Nhân viên đã được chỉnh sửa',
            });
            navigate(config.routes.admin.employee);
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa thất bại',
                description: err?.response?.data?.detail,
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
    });

    useEffect(() => {
        if (isLoading || !data) return;
        let employee = data.data;
        let user = employee?.user;
        let address = user?.addresses?.find((item) => item?.isDefault);
        form.setFieldsValue({
            firstName: user?.firstName,
            lastName: user?.lastName,
            email: user?.email,
            phone: user?.phone,
            dob: dayjs(user?.dob, 'YYYY-MM-DD'),
            gender: user?.gender,
            status: user?.status,
            province: address?.province?.id,
            district: address?.district?.id,
            ward: address?.ward?.id,
            street: address?.street,
            type: employee?.type,
        });
        setChosenProvince(address?.province?.id);
        setChosenDistrict(address?.district?.id);
    }, [isLoading, data]);

    const onAdd = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        await mutationCreate.mutateAsync({
            firstName: form.getFieldValue('firstName'),
            lastName: form.getFieldValue('lastName'),
            password: form.getFieldValue('password'),
            email: form.getFieldValue('email'),
            phone: form.getFieldValue('phone'),
            gender: form.getFieldValue('gender'),
            status: form.getFieldValue('status'),
            type: form.getFieldValue('type'),
            dob: form.getFieldValue('dob'),
            address: {
                provinceId: form.getFieldValue('province'),
                districtId: form.getFieldValue('district'),
                wardId: form.getFieldValue('ward'),
                street: form.getFieldValue('street'),
                email: form.getFieldValue('email'),
                phone: form.getFieldValue('phone'),
                receiver: form.getFieldValue('firstName') + ' ' + form.getFieldValue('lastName'),
            },
        });
    };
    const onEdit = async () => {
        try {
            await form.validateFields();
        } catch {
            return;
        }
        let address = data?.data?.user?.addresses?.find((item) => item?.isDefault);

        await mutationUpdate.mutateAsync({
            id: id,
            body: {
                firstName: form.getFieldValue('firstName'),
                lastName: form.getFieldValue('lastName'),
                password: form.getFieldValue('password'),
                phone: form.getFieldValue('phone'),
                gender: form.getFieldValue('gender'),
                status: form.getFieldValue('status'),
                type: form.getFieldValue('type'),
                dob: form.getFieldValue('dob'),
                address: {
                    id: address?.id,
                    provinceId: form.getFieldValue('province'),
                    districtId: form.getFieldValue('district'),
                    wardId: form.getFieldValue('ward'),
                    street: form.getFieldValue('street'),
                    email: form.getFieldValue('email'),
                    phone: form.getFieldValue('phone'),
                    receiver:
                        form.getFieldValue('firstName') + ' ' + form.getFieldValue('lastName'),
                },
            },
        });
    };

    if ((isLoading && id))
        return <Spin className="flex justify-center" />;
    return (
        <div className="form-container">
            <div className="flex items-center gap-[1rem]">
                <FontAwesomeIcon
                    onClick={() => navigate(config.routes.admin.employee)}
                    className="text-[1.6rem] bg-[--primary-color] p-4 rounded-xl text-white cursor-pointer"
                    icon={faChevronLeft}
                />
                <h1 className="font-bold">{id ? 'Cập nhật thông tin' : 'Thêm nhân viên'}</h1>
            </div>
            <div className="flex items-center justify-start rounded-xl shadow text-[1.6rem] text-black gap-[1rem] bg-white p-7">
                <div className="flex flex-col gap-[1rem]">
                    <p>ID</p>
                    <code className="bg-blue-100 p-2">{data?.data?.id || '_'}</code>
                </div>
                <div className="flex flex-col gap-[1rem]">
                    <p>Ngày tạo</p>
                    <code className="bg-blue-100 p-2">
                        {data?.data?.createdAt
                            ? new Date(data?.data?.createdAt).toLocaleString()
                            : '__/__/____'}
                    </code>
                </div>
                <div className="flex flex-col gap-[1rem]">
                    <p>Ngày cập nhật</p>
                    <code className="bg-blue-100 p-2">
                        {data?.data?.updatedAt
                            ? new Date(data?.data?.updatedAt).toLocaleString()
                            : '__/__/____'}
                    </code>
                </div>
            </div>
            <div className="bg-white p-7 mt-5 rounded-xl shadow">
                <Form
                    name="employee-form"
                    layout="vertical"
                    form={form}
                    labelCol={{
                        span: 5,
                    }}
                >
                    <Row gutter={16}>
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
                                <Input disabled={id} />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                label="Mật khẩu"
                                name="password"
                                rules={[
                                    {
                                        required: id ? false : true,
                                        message: 'Nhập mật khẩu của bạn!',
                                    },
                                ]}
                            >
                                <Input.Password />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={8}>
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
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={8}>
                            <Form.Item
                                label="Ngày sinh"
                                name="dob"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Chọn ngày sinh của bạn!',
                                    },
                                ]}
                            >
                                <DatePicker format={'YYYY-MM-DD'} className="w-full" />
                            </Form.Item>
                        </Col>
                        <Col span={8}>
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
                                <Input />
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
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item label="Giới tính" name="gender" initialValue={'MALE'}>
                                <Select
                                    defaultValue={'MALE'}
                                    onChange={(v) => form.setFieldValue('gender', v)}
                                >
                                    <Option value="MALE">Nam</Option>
                                    <Option value="FEMALE">Nữ</Option>
                                </Select>
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
                    <Row gutter={16}>
                        <Col span={8}>
                            <Form.Item
                                label="Địa chỉ cụ thể"
                                name="street"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập địa chỉ của bạn!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Col>
                        <Col span={8}>
                            <Form.Item label="Trạng thái" name="status" initialValue={1}>
                                <Select
                                    defaultValue={1}
                                    onChange={(v) => form.setFieldValue('status', v)}
                                >
                                    <Option value={1}>Kích hoạt</Option>
                                    <Option value={0}>Vô hiệu hoá</Option>
                                </Select>
                            </Form.Item>
                        </Col>
                        <Col span={8}>
                            <Form.Item label="Loại nhân viên" name="type" initialValue={'SOCIAL'}>
                                <Select
                                    defaultValue={'SOCIAL'}
                                    onChange={(v) => form.setFieldValue('type', v)}
                                >
                                    <Option value={'SOCIAL'}>Mạng xã hội</Option>
                                    <Option value={'ECOMMERCE'}>Thương mại điện tử</Option>
                                </Select>
                            </Form.Item>
                        </Col>
                    </Row>
                    <div className="flex justify-between items-center gap-[1rem]">
                        <Button className="min-w-[10%]">Đặt lại</Button>
                        <Button
                            loading={processing}
                            htmlType="submit"
                            onClick={id ? onEdit : onAdd}
                            className="bg-blue-500 text-white min-w-[10%]"
                        >
                            {id ? 'Cập nhật' : 'Thêm mới'}
                        </Button>
                    </div>
                </Form>
            </div>
        </div>
    );
}

export default EmployeeFormPage;
