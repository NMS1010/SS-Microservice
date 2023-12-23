import { faEdit, faEye, faEyeSlash } from '@fortawesome/free-regular-svg-icons';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Input, Table, Tag, notification } from 'antd';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import config from '../../../config';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';
import EmployeeDetail from './EmployeeDetail';
import { useDeleteEmployee, useGetListEmployee } from '../../../hooks/api';

const baseColumns = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
        width: 50,
    },
    {
        title: 'Ngày tạo',
        dataIndex: 'createdAt',
        sorter: true,
        ellipsis: true,
        width: 200,
    },
    {
        title: 'Ảnh đại diện',
        dataIndex: 'avatar',
    },
    {
        title: 'Email',
        dataIndex: 'email',
        sorter: true,
        width: 100,
    },
    {
        title: 'Họ',
        dataIndex: 'firstName',
        sorter: true,
    },
    {
        title: 'Tên',
        dataIndex: 'lastName',
        sorter: true,
    },
    {
        title: 'Trạng thái',
        dataIndex: 'status',
        sorter: true,
    },
    {
        title: 'Vai trò',
        dataIndex: 'roles',
    },
    {
        title: 'Loại nhân viên',
        dataIndex: 'type',
        sorter: true,
    },
    {
        title: 'Thao tác',
        dataIndex: 'action',
    },
];
function transformData(dt, navigate, setIsDetailOpen, setIsDisableOpen) {
    return dt?.map((item) => {
        return {
            key: item?.id,
            id: item?.id,
            createdAt: new Date(item?.createdAt)?.toLocaleString(),
            email: item?.user?.email,
            firstName: item?.user?.firstName,
            lastName: item?.user?.lastName,
            avatar: <img className="w-20 h-20 rounded-xl" src={item?.user?.avatar} />,
            status: (
                <Tag className="w-fit uppercase" color={item?.user?.status ? 'green' : 'red'}>
                    {item?.user?.status ? 'Kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
            type: (
                <div className="flex flex-col gap-[1rem]">
                    <Tag className="w-fit uppercase">{item?.type}</Tag>
                </div>
            ),
            roles: (
                <div className="flex flex-col gap-[1rem]">
                    {item?.user?.roles.map((r) => (
                        <Tag key={r} className="w-fit uppercase">
                            {r}
                        </Tag>
                    ))}
                </div>
            ),
            action: (
                <div className="action-btn flex gap-3">
                    <Button
                        className="text-blue-500 border border-blue-500"
                        onClick={() => setIsDetailOpen({ id: item?.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={faSearch} />
                    </Button>
                    <Button
                        className="text-green-500 border border-green-500"
                        onClick={() =>
                            navigate(`${config.routes.admin.employee_update}/${item?.id}`)
                        }
                    >
                        <FontAwesomeIcon icon={faEdit} />
                    </Button>
                    <Button
                        className={`border ${
                            item?.user?.status
                                ? ' text-red-500  border-red-500'
                                : 'text-green-500 border-green-500'
                        }`}
                        onClick={() => setIsDisableOpen({ id: item?.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={item?.user?.status ? faEyeSlash : faEye} />
                    </Button>
                </div>
            ),
        };
    });
}
function Data({ params, setParams, setEmployeeIds }) {
    const navigate = useNavigate();

    const [isDetailOpen, setIsDetailOpen] = useState({
        id: 0,
        isOpen: false,
    });
    const [isDisableOpen, setIsDisableOpen] = useState({
        id: 0,
        isOpen: false,
    });

    const mutationDelete = useDeleteEmployee({
        success: () => {
            setIsDisableOpen({ ...isDisableOpen, isOpen: false });
            notification.success({
                message: 'Đổi trạng thái thành công',
                description: 'Nhân viên đã được thay đổi trạng thái',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Đổi trạng thái thất bại',
                description: 'Có lỗi xảy ra khi thay đổi trạng thái nhân viên',
            });
        },
        obj: {
            id: isDisableOpen.id,
            params: params,
        },
    });

    const { data, isLoading } = useGetListEmployee(params);

    const [tableParams, setTableParams] = useState({
        pagination: {
            current: params.pageIndex,
            pageSize: params.pageSize,
            total: data?.data?.totalItems,
        },
    });

    const [tdata, setTData] = useState([]);

    const rowSelection = {
        onChange: (selectedRowKeys, selectedRows) => {
            setEmployeeIds(selectedRows.map((item) => item.id));
        },
        getCheckboxProps: (record) => ({
            name: record.name,
        }),
    };

    useEffect(() => {
        if (isLoading || !data) return;
        let dt = transformData(data?.data?.items, navigate, setIsDetailOpen, setIsDisableOpen);
        setTData(dt);
        setTableParams({
            ...tableParams,
            pagination: {
                ...tableParams.pagination,
                total: data?.data?.totalItems,
            },
        });
    }, [isLoading, data]);

    const onSearch = (value) => {
        setParams({
            ...params,
            search: value,
        });
    };

    const onDelete = async (id) => {
        await mutationDelete.mutateAsync(id);
    };

    const handleTableChange = (pagination, filters, sorter) => {
        setTableParams({
            ...tableParams,
            pagination,
            ...sorter,
        });
        setParams({
            ...params,
            pageIndex: pagination.current,
            pageSize: pagination.pageSize,
            columnName: !sorter.column ? 'id' : sorter.field,
            isSortAscending: sorter.order === 'ascend' || !sorter.order ? true : false,
        });
    };

    return (
        <div>
            <div className="search-container p-4 bg-white mb-3 flex items-center rounded-lg">
                <Input.Search
                    className="xl:w-1/4 md:w-1/2"
                    allowClear
                    enterButton
                    placeholder="Nhập từ khoá tìm kiếm"
                    onSearch={onSearch}
                />
            </div>
            <Table
                loading={isLoading}
                scroll={{
                    x: 1300,
                }}
                rowSelection={{
                    type: 'checkbox',
                    ...rowSelection,
                }}
                columns={baseColumns}
                dataSource={tdata}
                pagination={{ ...tableParams.pagination, showSizeChanger: true }}
                onChange={handleTableChange}
            />
            {isDetailOpen.id !== 0 && (
                <EmployeeDetail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}
            {isDisableOpen.id !== 0 && (
                <ConfirmPrompt
                    handleConfirm={onDelete}
                    content="Bạn có muốn vô hiệu hoá nhân viên này ?"
                    isDisableOpen={isDisableOpen}
                    setIsDisableOpen={setIsDisableOpen}
                />
            )}
        </div>
    );
}

export default Data;
