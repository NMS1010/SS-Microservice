import { faEye, faEyeSlash } from '@fortawesome/free-regular-svg-icons';
import { faReply, faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Input, Rate, Table, Tag, notification } from 'antd';
import { useEffect, useState } from 'react';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';
import ReviewDetail from './ReviewDetail';
import Reply from './Reply';
import { useDeleteReview, useGetListReview, useToggleReview } from '../../../hooks/api';

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
    },
    {
        title: 'Tiêu đề',
        dataIndex: 'title',
        sorter: true,
    },
    {
        title: 'Sản phẩm',
        dataIndex: 'product',
        sorter: true,
    },
    {
        title: 'Số sao',
        dataIndex: 'rating',
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

function transformData(dt, setIsDetailOpen, setIsDisableOpen, setIsReplyOpen) {
    return dt?.map((item) => {
        return {
            key: item?.id,
            id: item?.id,
            createdAt: new Date(item?.createdAt)?.toLocaleString(),
            title: item?.title,
            product: item?.product?.name,
            rating: <Rate className="text-2xl" disabled defaultValue={item?.rating} />,
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
                        onClick={() => setIsReplyOpen({ id: item?.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={faReply} />
                    </Button>
                    <Button
                        className={`border ${
                            item?.status
                                ? ' text-red-500  border-red-500'
                                : 'text-green-500 border-green-500'
                        }`}
                        onClick={() => setIsDisableOpen({ id: item?.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={item?.status ? faEyeSlash : faEye} />
                    </Button>
                </div>
            ),
        };
    });
}

function Data({ params, setParams, setReviewIds }) {
    const [isReplyOpen, setIsReplyOpen] = useState({
        id: 0,
        isOpen: false,
    });

    const [isDetailOpen, setIsDetailOpen] = useState({
        id: 0,
        isOpen: false,
    });

    const [isDisableOpen, setIsDisableOpen] = useState({
        id: 0,
        isOpen: false,
    });

    const { data, isLoading, refetch } = useGetListReview(params);

    const mutationDelete = useToggleReview({
        success: () => {
            setIsDisableOpen({ ...isDisableOpen, isOpen: false });
            refetch();
            notification.success({
                message: 'Thao tác thành công',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Ẩn thất bại',
            });
        },
        obj: {
            id: isDisableOpen.id,
            params: params,
        },
    });

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
            setReviewIds(selectedRows.map((item) => item.id));
        },
        getCheckboxProps: (record) => ({
            name: record.name,
        }),
    };

    useEffect(() => {
        if (isLoading || !data) return;
        let dt = transformData(
            data?.data?.items,
            setIsDetailOpen,
            setIsDisableOpen,
            setIsReplyOpen,
        );
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
        await mutationDelete.mutateAsync({
            id: id,
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
            columnName: !sorter.column ? 'updatedAt' : sorter.field,
            isSortAscending: sorter.order === 'descend' || !sorter.order ? false : true,
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
                <ReviewDetail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}
            {isReplyOpen.id !== 0 && (
                <Reply isReplyOpen={isReplyOpen} setIsReplyOpen={setIsReplyOpen} />
            )}
            {isDisableOpen.id !== 0 && (
                <ConfirmPrompt
                    handleConfirm={onDelete}
                    content="Bạn có muốn ấn đánh giá này ?"
                    isDisableOpen={isDisableOpen}
                    setIsDisableOpen={setIsDisableOpen}
                />
            )}
        </div>
    );
}

export default Data;
