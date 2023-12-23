import { faEdit} from '@fortawesome/free-regular-svg-icons';
import { faSearch, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Input, Table, Tag, notification } from 'antd';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import config from '../../../config';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';
import PaymentMethodDetail from './PaymentMethodDetail';
import { useDeletePaymentMethod, useGetListPaymentMethod } from '../../../hooks/api';

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
        title: 'Ảnh',
        dataIndex: 'image',
    },
    {
        title: 'Tên phương thức',
        dataIndex: 'name',
        sorter: true,
    },
    {
        title: 'Mã',
        dataIndex: 'code',
        sorter: true,
    },
    {
        title: 'Trạng thái',
        dataIndex: 'status',
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
            name: item?.name,
            code: item?.code,
            image: <img className="w-20 h-20 rounded-xl" src={item?.image} />,
            status: (
                <Tag className="w-fit uppercase" color={item?.status ? 'green' : 'red'}>
                    {item?.status ? 'Kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
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
                            navigate(`${config.routes.admin.payment_method_update}/${item?.id}`)
                        }
                    >
                        <FontAwesomeIcon icon={faEdit} />
                    </Button>
                    <Button
                        className="text-red-500 border border-red-500"
                        onClick={() => setIsDisableOpen({ id: item?.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={faTrash} />
                    </Button>
                </div>
            ),
        };
    });
}

function Data({ params, setParams, setPaymentMethodIds }) {
    const navigate = useNavigate();

    const [isDetailOpen, setIsDetailOpen] = useState({
        id: 0,
        isOpen: false,
    });
    const [isDisableOpen, setIsDisableOpen] = useState({
        id: 0,
        isOpen: false,
    });

    const mutationDelete = useDeletePaymentMethod({
        success: () => {
            setIsDisableOpen({ ...isDisableOpen, isOpen: false });
            
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Phương thức thanh toán đã được vô hiệu hoá',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá phương thức thanh toán',
            });
        },
        obj: {
            id: isDisableOpen.id,
            params: params,
        },
    });

    const { data, isLoading } = useGetListPaymentMethod(params);

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
            setPaymentMethodIds(selectedRows.map((item) => item.id));
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
                    x: 'max-content',
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
                <PaymentMethodDetail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}
            {isDisableOpen.id !== 0 && (
                <ConfirmPrompt
                    handleConfirm={onDelete}
                    content="Bạn có muốn vô hiệu hoá đơn vị này ?"
                    isDisableOpen={isDisableOpen}
                    setIsDisableOpen={setIsDisableOpen}
                />
            )}
        </div>
    );
}

export default Data;
