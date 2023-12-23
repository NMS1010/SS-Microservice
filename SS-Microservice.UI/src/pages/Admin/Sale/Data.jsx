import { faEdit, faEye } from '@fortawesome/free-regular-svg-icons';
import { faArrowRightFromBracket, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Input, Table, Tag, notification } from 'antd';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import config from '../../../config';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';
import SaleDetail from './SaleDetail';
import { useApplySale, useCancelSale, useGetListSale } from '../../../hooks/api';

const baseColumns = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
        width: 50,
    },
    {
        title: 'Tên khuyến mãi',
        dataIndex: 'name',
        sorter: true,
    },
    {
        title: 'Ngày bắt đầu',
        dataIndex: 'startDate',
        sorter: true,
    },
    {
        title: 'Ngày kết thúc',
        dataIndex: 'endDate',
        sorter: true,
    },
    {
        title: 'Phần trăm giảm giá',
        dataIndex: 'promotionalPercent',
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

function transformData(dt, navigate, setIsDetailOpen, onApply, onCancel) {
    return dt?.map((item) => {
        return {
            key: item.id,
            id: item.id,
            name: item.name,
            startDate: new Date(item.startDate).toLocaleString(),
            endDate: new Date(item.endDate).toLocaleString(),
            promotionalPercent: item.promotionalPercent + '%',
            status: (
                <Tag
                    className="w-fit uppercase"
                    color={`${
                        item.status === 'ACTIVE'
                            ? 'green'
                            : item.status === 'INACTIVE'
                            ? 'red'
                            : 'yellow'
                    }`}
                >
                    {item.status === 'ACTIVE'
                        ? 'Đang áp dụng'
                        : item.status === 'INACTIVE'
                        ? 'Vô hiệu lực'
                        : 'Hết hạn'}
                </Tag>
            ),
            action: (
                <div className="action-btn flex gap-3">
                    <Button
                        className="text-blue-500 border border-blue-500"
                        onClick={() => setIsDetailOpen({ id: item.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={faEye} />
                    </Button>
                    <Button
                        className="text-green-500 border border-green-500"
                        onClick={() => navigate(`${config.routes.admin.sale_update}/${item.id}`)}
                    >
                        <FontAwesomeIcon icon={faEdit} />
                    </Button>
                    <Button
                        className="text-yellow-500 border border-yellow-500"
                        disabled={item.status === 'ACTIVE'}
                        onClick={() => onApply(item.id)}
                    >
                        <FontAwesomeIcon icon={faArrowRightFromBracket} />
                    </Button>
                    <Button
                        className="text-red-500 border border-red-500"
                        disabled={item.status !== 'ACTIVE'}
                        onClick={() => onCancel(item.id)}
                    >
                        <FontAwesomeIcon icon={faTrash} />
                    </Button>
                </div>
            ),
        };
    });
}

function Data({ params, setParams }) {
    const navigate = useNavigate();
    const { isLoading, data, refetch } = useGetListSale(params);
    const [tdata, setTData] = useState([]);
    const [tableParams, setTableParams] = useState({
        pagination: {
            current: params.pageIndex,
            pageSize: params.pageSize,
            total: data?.data?.totalItems,
        },
    });
    const [isDetailOpen, setIsDetailOpen] = useState({ id: 0, isOpen: false });
    const [isDisableOpen, setIsDisableOpen] = useState(false);

    const mutationApply = useApplySale({
        success: () => {
            notification.success({
                message: 'Áp dụng thành công',
                description: 'Áp dụng giảm giá cho sản phẩm thành công',
            });
            refetch();
        },
        error: (err) => {
            notification.error({
                message: 'Áp dụng thất bại',
                description: 'Áp dụng giảm giá cho sản phẩm thất bại',
            });
        },
    });

    const mutationCancel = useCancelSale({
        success: () => {
            notification.success({
                message: 'Hủy áp dụng thành công',
                description: 'Hủy áp dụng giảm giá cho sản phẩm thành công',
            });
            refetch();
        },
        error: (err) => {
            notification.error({
                message: 'Hủy áp dụng thất bại',
                description: 'Hủy áp dụng giảm giá cho sản phẩm thất bại',
            });
        },
    });

    const onApply = async (id) => {
        await mutationApply.mutateAsync({ id });
    };

    const onCancel = async (id) => {
        await mutationCancel.mutateAsync({ id });
    };

    useEffect(() => {
        if (isLoading || !data) return;
        setTData(transformData(data?.data?.items, navigate, setIsDetailOpen, onApply, onCancel));
        setTableParams({
            ...tableParams,
            pagination: {
                ...tableParams.pagination,
                total: data?.data?.totalItems,
            },
        });
    }, [isLoading, data]);

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

    const rowSelection = {
        onChange: (selectedRowKeys, selectedRows) => {
            setUnitIds(selectedRows.map((item) => item.id));
        },
        getCheckboxProps: (record) => ({
            name: record.name,
        }),
    };

    const onSearch = (value) => {
        setParams({
            ...params,
            search: value,
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
                    x: 1500,
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
                <SaleDetail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}

            <ConfirmPrompt
                content="Bạn có muốn ẩn đợt khuyến mãi này ?"
                isDisableOpen={isDisableOpen}
                setIsDisableOpen={setIsDisableOpen}
            />
        </div>
    );
}

export default Data;
