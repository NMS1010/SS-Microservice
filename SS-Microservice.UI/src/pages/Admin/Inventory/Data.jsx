import { faUpload } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Input, Table, Tag, notification } from 'antd';
import { useEffect, useState } from 'react';
import Edit from './Edit';
import { useGetListProduct, useImportProduct } from '../../../hooks/api';
import History from './History';

const baseColumns = [
    {
        title: 'Mã sản phẩm',
        dataIndex: 'code',
        sorter: true,
    },
    {
        title: 'Tên sản phẩm',
        dataIndex: 'name',
        sorter: true,
    },
    {
        title: 'Số lượng trong kho',
        dataIndex: 'quantity',
        sorter: true,
    },
    {
        title: 'Có thể bán',
        dataIndex: 'actualInventory',
        sorter: true,
    },
    {
        title: 'Đã bán',
        dataIndex: 'sold',
        sorter: true,
    },
    {
        title: 'Trạng thái',
        dataIndex: 'status',
    },
    {
        title: 'Lịch sử',
        dataIndex: 'history',
    },
    {
        title: 'Thao tác',
        dataIndex: 'action',
    },
];

function transformData(dt, setIsImportProduct, setIsDetailOpen) {
    return dt?.map((item) => {
        return {
            key: item.id,
            code: item.code,
            name: item.name,
            quantity: item.quantity,
            actualInventory: item.actualInventory,
            sold: item.sold,
            status: (
                <Tag className="w-fit uppercase" color={item.quantity > 0 ? 'green' : 'red'}>
                    {item.quantity > 0 ? 'Còn hàng' : 'Hết hàng'}
                </Tag>
            ),
            history: (
                <Button
                    onClick={() => {
                        setIsDetailOpen({
                            productId: item.id,
                            productName: item.name,
                            isOpen: true,
                        });
                    }}
                    className="text-blue-800 border-0 p-0 shadow-none "
                >
                    <span className="underline">Giao dịch</span>
                </Button>
            ),
            action: (
                <div className="action-btn flex gap-3">
                    <Button
                        className="text-green-500 border border-green-500"
                        onClick={() =>
                            setIsImportProduct({
                                docket: {
                                    productId: item.id,
                                    quantity: null,
                                    actualInventory: item.actualInventory,
                                    note: null,
                                },
                                isEdit: true,
                            })
                        }
                    >
                        <FontAwesomeIcon icon={faUpload} />
                    </Button>
                </div>
            ),
        };
    });
}

function Data({ params, setParams }) {
    const { isLoading, data, refetch } = useGetListProduct(params);
    const [tdata, setTData] = useState([]);
    const [tableParams, setTableParams] = useState({
        pagination: {
            current: params.pageIndex + 1,
            pageSize: params.pageSize,
            totalPages: data?.data?.totalItems,
        },
    });
    const [isImportProduct, setIsImportProduct] = useState({
        docket: {
            productId: 0,
            quantity: 0,
            actualInventory: 0,
            note: null,
        },
        isEdit: false,
    });
    const [isDetailOpen, setIsDetailOpen] = useState({
        productId: 0,
        productName: '',
        isOpen: false,
    });
    const [processing, setProcessing] = useState(false);

    useEffect(() => {
        if (isLoading || !data) return;
        setTData(transformData(data?.data?.items, setIsImportProduct, setIsDetailOpen));
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

    const mutationImportProduct = useImportProduct({
        success: () => {
            setIsImportProduct({
                docket: { ...isImportProduct.docket },
                isEdit: false,
            });
            notification.success({
                message: 'Thêm sản phẩm vào kho thành công',
                description: 'Sản phẩm đã được thêm vào kho',
            });
            refetch();
        },
        error: (err) => {
            notification.error({
                message: 'Thêm sản phẩm vào kho thất bại',
                description: 'Có lỗi xảy ra khi thêm sản phẩm đã được thêm vào kho',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
        obj: {
            id: isImportProduct.docket.productId,
        },
    });

    const importProduct = async (isImportProduct) => {
        const request = { ...isImportProduct.docket };
        await mutationImportProduct.mutateAsync(request);
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
                columns={baseColumns}
                dataSource={tdata}
                pagination={{ ...tableParams.pagination, showSizeChanger: true }}
                onChange={handleTableChange}
            />
            {isImportProduct.isEdit && (
                <Edit
                    loading={processing}
                    isImportProduct={isImportProduct}
                    setIsImportProduct={setIsImportProduct}
                    importProduct={importProduct}
                />
            )}
            {isDetailOpen.productId !== 0 && (
                <History isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}
        </div>
    );
}

export default Data;
