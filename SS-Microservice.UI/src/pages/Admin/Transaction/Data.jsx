import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Input, Table } from 'antd';
import { useEffect, useState } from 'react';

import TransactionDetail from './TransactionDetail';
import { useGetListTransaction } from '../../../hooks/api';
import { numberFormatter } from '../../../utils/formatter';

const baseColumns = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
        width: 50,
    },
    {
        title: 'Mã đơn hàng',
        dataIndex: 'code',
        sorter: true,
    },
    {
        title: 'Ngày hoàn tất',
        dataIndex: 'completedAt',
        sorter: true,
    },
    {
        title: 'Ngày thanh toán',
        dataIndex: 'paidAt',
        sorter: true,
    },
    {
        title: 'Phương thức thanh toán',
        dataIndex: 'paymentMethod',
        sorter: true,
    },
    {
        title: 'Tổng tiền',
        dataIndex: 'totalPay',
        sorter: true,
    },
    {
        title: 'Thao tác',
        dataIndex: 'action',
    },
];
function transformData(dt, setIsDetailOpen) {
    return dt?.map((item) => {
        return {
            key: item?.id,
            id: item?.id,
            code: item?.orderCode,
            createdAt: new Date(item?.createdAt)?.toLocaleString(),
            paidAt: item?.paidAt && new Date(item?.paidAt)?.toLocaleString(),
            completedAt: item?.completedAt && new Date(item?.completedAt)?.toLocaleString(),
            paymentMethod: item?.paymentMethod,
            totalPay: numberFormatter(item?.totalPay),
            action: (
                <div className="action-btn flex gap-3">
                    <Button
                        className="text-blue-500 border border-blue-500"
                        onClick={() => setIsDetailOpen({ id: item?.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={faSearch} />
                    </Button>
                </div>
            ),
        };
    });
}
function Data({params, setParams}) {
    const [isDetailOpen, setIsDetailOpen] = useState({
        id: 0,
        isOpen: false,
    });

    const { data, isLoading } = useGetListTransaction(params);

    const [tableParams, setTableParams] = useState({
        pagination: {
            current: params.pageIndex,
            pageSize: params.pageSize,
            total: data?.data?.totalItems,
        },
    });

    const [tdata, setTData] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        let dt = transformData(data?.data?.items, setIsDetailOpen);
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
                columns={baseColumns}
                dataSource={tdata}
                pagination={{ ...tableParams.pagination, showSizeChanger: true }}
                onChange={handleTableChange}
            />
            {isDetailOpen.id !== 0 && (
                <TransactionDetail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}
        </div>
    );
}

export default Data;
