import { faEdit, faEye, faEyeSlash } from '@fortawesome/free-regular-svg-icons';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Input, Table, Tag } from 'antd';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import config from '../../../config';
import { numberFormatter } from '../../../utils/formatter';
import OrderDetail from './OrderDetail';
import { useGetListOrder } from '../../../hooks/api';
import { getOrderStatus } from '../../../utils/constants';

const baseColumns = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
        width: 50,
    },
    {
        title: 'Ngày đặt hàng',
        dataIndex: 'createdAt',
        sorter: true,
    },
    {
        title: 'Mã đơn hàng',
        dataIndex: 'code',
        sorter: true,
    },
    {
        title: 'Phương thức',
        dataIndex: 'paymentMethod',
        sorter: true,
    },
    {
        title: 'Thanh toán',
        dataIndex: 'paymentStatus',
        sorter: true,
    },
    {
        title: 'Trạng thái',
        dataIndex: 'status',
        sorter: true,
    },
    {
        title: 'Tổng tiền',
        dataIndex: 'totalAmount',
        sorter: true,
    },
    {
        title: 'Thao tác',
        dataIndex: 'action',
    },
];

function transformData(dt, navigate, setIsDetailOpen) {
    return dt?.map((item) => {
        return {
            key: '1',
            id: item?.id,
            createdAt: new Date(item?.createdAt)?.toLocaleString(),
            code: item?.code,
            paymentStatus: (
                <Tag className="w-fit uppercase" color={`${item?.paymentStatus ? 'green' : 'red'}`}>
                    {item?.paymentStatus ? 'Đã thanh toán' : 'Chưa thanh toán'}
                </Tag>
            ),
            paymentMethod: (
                <div className="flex justify-center">
                    <Tag className="w-fit uppercase" color="green">
                        {item?.transaction?.paymentMethod}
                    </Tag>
                </div>
            ),
            totalAmount: (
                <div className="font-bold text-red-500">{numberFormatter(item?.totalAmount)}</div>
            ),
            status: (
                <div className="flex justify-center">
                    <Tag className="w-fit uppercase" color={getOrderStatus(item?.status)?.color}>
                        {getOrderStatus(item?.status)?.title}
                    </Tag>
                </div>
            ),
            action: (
                <div className="action-btn flex gap-3">
                    <Button
                        className="text-blue-500 border border-blue-500"
                        onClick={() => setIsDetailOpen({
                            id: item?.id,
                            isOpen: true,
                        })}
                    >
                        <FontAwesomeIcon icon={faSearch} />
                    </Button>
                    <Button
                        className="text-green-500 border border-green-500"
                        onClick={() => navigate(`${config.routes.admin.order_update}/${item?.id}`)}
                    >
                        <FontAwesomeIcon icon={faEdit} />
                    </Button>
                </div>
            ),
        };
    });
}

function Data() {
    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
    });
    const [tdata, setTData] = useState([]);
    const navigate = useNavigate();
    const [isDetailOpen, setIsDetailOpen] = useState({
        id: 0,
        isOpen: false,
    });

    const { data, isLoading } = useGetListOrder(params);

    const [tableParams, setTableParams] = useState({
        pagination: {
            current: params.pageIndex,
            pageSize: params.pageSize,
            total: data?.data?.totalItems,
        },
    });
    useEffect(() => {
        if (isLoading || !data) return;
        let dt = transformData(data?.data?.items, navigate, setIsDetailOpen);
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
                    placeholder="Nhập mã đơn hàng để tìm kiếm"
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
                <OrderDetail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}
        </div>
    );
}

export default Data;
