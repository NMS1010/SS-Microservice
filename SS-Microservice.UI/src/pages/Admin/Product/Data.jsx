import { faEdit, faEyeSlash } from '@fortawesome/free-regular-svg-icons';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Input, Table, Tag, Dropdown, Image, notification } from 'antd';
import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import config from '../../../config';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';
import { useDeleteProduct, useGetListProduct } from '../../../hooks/api';
import ProductDetail from './ProductDetail';

const baseColumns = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
        width: 50,
    },
    {
        title: 'Tên sản phẩm',
        dataIndex: 'name',
        sorter: true,
    },
    {
        title: 'Mã sản phẩm',
        dataIndex: 'code',
        sorter: true,
    },
    {
        title: 'Hình đại diện',
        dataIndex: 'image',
        width: 100,
    },
    {
        title: 'Tên danh mục',
        dataIndex: 'category',
        sorter: true,
    },
    {
        title: 'Trạng thái',
        dataIndex: 'status',
        sorter: true,
    },
    {
        title: 'Dạng bán ra',
        dataIndex: 'variant',
    },
    {
        title: 'Thao tác',
        dataIndex: 'action',
    },
];

function transformData(dt, navigate, setIsDetailOpen, setIsDisableOpen) {
    return dt?.map((item) => {
        return {
            key: item.id,
            id: item.id,
            name: item.name,
            code: item.code,
            image: (
                <>
                    <Image
                        width={80}
                        src={item.images.filter((image) => image.isDefault === true)[0].image}
                    />
                </>
            ),
            category: item.category.name,
            status: (
                <Tag
                    className="w-fit uppercase"
                    color={
                        item.status === 'ACTIVE'
                            ? 'green'
                            : item.status === 'INACTIVE'
                            ? 'red'
                            : 'yellow'
                    }
                >
                    {item.status === 'ACTIVE'
                        ? 'Kích hoạt'
                        : item.status === 'INACTIVE'
                        ? 'Vô hiệu hóa'
                        : 'Hết hàng'}
                </Tag>
            ),
            variant: (
                <div className="flex flex-col gap-[1rem]">
                    {item.variants.map(
                        (variant) =>
                            variant.status === 'ACTIVE' && (
                                <Tag className="w-fit uppercase" color="magenta">
                                    {variant.name} - {variant.quantity} {item.unit.name}
                                </Tag>
                            ),
                    )}
                </div>
            ),
            action: (
                <div className="action-btn flex gap-3">
                    <Button
                        className="text-blue-500 border border-blue-500"
                        onClick={() => setIsDetailOpen({ id: item.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={faSearch} />
                    </Button>
                    <Dropdown
                        menu={{
                            items: [
                                {
                                    key: '1',
                                    label: (
                                        <Link
                                            to={`${config.routes.admin.product_update}/${item.id}`}
                                        >
                                            Thông tin cơ bản
                                        </Link>
                                    ),
                                },
                                {
                                    key: '2',
                                    label: (
                                        <Link
                                            to={`${config.routes.admin.product_variant}/${item.id}`}
                                        >
                                            Dạng bán ra
                                        </Link>
                                    ),
                                },
                                {
                                    key: '3',
                                    label: (
                                        <Link
                                            to={`${config.routes.admin.product_image}/${item.id}`}
                                        >
                                            Hình ảnh
                                        </Link>
                                    ),
                                },
                            ],
                        }}
                        placement="bottom"
                        arrow
                    >
                        <Button className="text-green-500 border border-green-500">
                            <FontAwesomeIcon icon={faEdit} />
                        </Button>
                    </Dropdown>
                    <Button
                        className={
                            item.status === 'ACTIVE'
                                ? 'text-red-500 border border-red-500'
                                : 'text-yellow-500 border '
                        }
                        disabled={item.status !== 'ACTIVE'}
                        onClick={() => setIsDisableOpen({ id: item.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={faEyeSlash} />
                    </Button>
                </div>
            ),
        };
    });
}

function Data({ setProductIds, params, setParams }) {
    const { isLoading, data } = useGetListProduct(params);
    const navigate = useNavigate();
    const [tdata, setTData] = useState([]);
    const [tableParams, setTableParams] = useState({
        pagination: {
            current: params.pageIndex,
            pageSize: params.pageSize,
            total: data?.data?.totalItems,
        },
    });
    const [isDetailOpen, setIsDetailOpen] = useState({ id: 0, isOpen: false });
    const [isDisableOpen, setIsDisableOpen] = useState({ id: 0, isOpen: false });

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
    });

    const rowSelection = {
        onChange: (selectedRowKeys, selectedRows) => {
            setProductIds(selectedRows.map((item) => item.id));
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

    const mutationDelete = useDeleteProduct({
        success: () => {
            setIsDisableOpen({ ...isDisableOpen, isOpen: false });
            notification.success({
                message: 'Vô hiệu hoá thành công',
                description: 'Sản phẩm đã được vô hiệu hoá',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hoá thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hoá sản phẩm',
            });
        },
        obj: {
            id: isDisableOpen.id,
            params: params,
        },
    });

    const onDelete = async (id) => {
        await mutationDelete.mutateAsync(id);
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
                <ProductDetail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}
            {isDisableOpen.id !== 0 && (
                <ConfirmPrompt
                    handleConfirm={onDelete}
                    content="Bạn có muốn vô hiệu hoá sản phẩm này ?"
                    isDisableOpen={isDisableOpen}
                    setIsDisableOpen={setIsDisableOpen}
                />
            )}
        </div>
    );
}

export default Data;
