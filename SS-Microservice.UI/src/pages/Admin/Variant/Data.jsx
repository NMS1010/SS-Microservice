import { faEdit, faEyeSlash } from '@fortawesome/free-regular-svg-icons';
import { faAdd, faArrowLeft, faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Table, Tag, notification } from 'antd';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import ConfirmPrompt from '../../../layouts/Admin/components/ConfirmPrompt';
import ModalVariant from '../../../layouts/Admin/components/ModalVariant';
import {
    useCreateVariant,
    useDeleteVariant,
    useGetListVariant,
    useUpdateVariant,
} from '../../../hooks/api';
import VariantDetail from './VariantDetail';
import config from '../../../config';

const baseColumns = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
        width: 50,
    },
    {
        title: 'Dạng sản phẩm bán ra',
        dataIndex: 'name',
        sorter: true,
        width: 150,
    },
    {
        title: 'Sku',
        dataIndex: 'sku',
        sorter: true,
    },
    {
        title: 'Giá 1 sản phẩm',
        dataIndex: 'itemPrice',
        sorter: true,
    },
    {
        title: 'Giá khuyến mãi',
        dataIndex: 'promotionalItemPrice',
        sorter: true,
    },
    {
        title: 'Số lượng',
        dataIndex: 'quantity',
    },

    {
        title: 'Tổng giá',
        dataIndex: 'totalPrice',
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

function transformData(dt, setIsDetailOpen, setIsDisableOpen, setModalVariant) {
    return dt?.map((item) => {
        return {
            key: item.id,
            id: item.id,
            name: item.name,
            sku: item.sku,
            itemPrice: item.itemPrice,
            promotionalItemPrice: item.promotionalItemPrice,
            quantity: item.quantity,
            totalPrice: item.totalPrice,
            status: (
                <Tag className="w-fit uppercase" color={item.status === 'ACTIVE' ? 'green' : 'red'}>
                    {item.status === 'ACTIVE' ? 'Kích hoạt' : 'Vô hiệu hóa'}
                </Tag>
            ),
            action: (
                <div className="action-btn flex gap-3">
                    <Button
                        className="text-blue-500 border border-blue-500"
                        onClick={() => setIsDetailOpen({ id: item.id, isOpen: true })}
                    >
                        <FontAwesomeIcon icon={faSearch} />
                    </Button>
                    <Button
                        className="text-green-500 border border-green-500"
                        onClick={() =>
                            setModalVariant({
                                title: 'Chỉnh sửa dạng sản phẩm bán ra',
                                variant: item,
                                edit: {
                                    index: item.id,
                                    isEdit: true,
                                },
                                isOpen: true,
                            })
                        }
                    >
                        <FontAwesomeIcon icon={faEdit} />
                    </Button>
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

function Data({ setVariantIds, setProductId }) {
    const navigate = useNavigate();
    const { productId } = useParams();
    const { isLoading, data } = productId
        ? useGetListVariant({ productId })
        : { isLoading: null, data: null };
    const [tdata, setTData] = useState([]);
    const [isDetailOpen, setIsDetailOpen] = useState({ id: 0, isOpen: false });
    const [isDisableOpen, setIsDisableOpen] = useState({ id: 0, isOpen: false });
    const [modalVariant, setModalVariant] = useState({
        title: null,
        variant: null,
        edit: {
            index: null,
            isEdit: false,
        },
        isOpen: false,
    });
    const [processing, setProcessing] = useState(false);

    useEffect(() => {
        if (isLoading || !data) return;
        setProductId(productId);
        setTData(transformData(data?.data, setIsDetailOpen, setIsDisableOpen, setModalVariant));
    }, [isLoading, data]);

    const rowSelection = {
        onChange: (selectedRowKeys, selectedRows) => {
            setVariantIds(selectedRows.map((item) => item.id));
        },
        getCheckboxProps: (record) => ({
            name: record.name,
        }),
    };

    const mutationCreate = useCreateVariant({
        success: () => {
            setModalVariant({
                ...modalVariant,
                edit: {
                    index: null,
                    isEdit: false,
                },
                isOpen: false,
            });
            notification.success({
                message: 'Tạo dạng sản phẩm bán ra thành công',
                description: 'Dạng sản phẩm bán ra đã được tạo',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Tạo dạng sản phẩm bán ra thất bại',
                description: 'Có lỗi xảy ra khi tạo dạng sản phẩm bán ra',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
        obj: {
            id: isDisableOpen.id,
            params: { productId },
        },
    });

    const mutationUpdate = useUpdateVariant({
        success: () => {
            setModalVariant({
                ...modalVariant,
                edit: {
                    index: null,
                    isEdit: false,
                },
                isOpen: false,
            });
            notification.success({
                message: 'Chỉnh sửa dạng sản phẩm bán ra thành công',
                description: 'Dạng sản phẩm bán ra đã được chỉnh sửa',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Chỉnh sửa dạng sản phẩm bán ra thất bại',
                description: 'Có lỗi xảy ra khi chỉnh sửa dạng sản phẩm bán ra',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
        obj: {
            id: isDisableOpen.id,
            params: { productId },
        },
    });

    const mutationDelete = useDeleteVariant({
        success: () => {
            setIsDisableOpen({ ...isDisableOpen, isOpen: false });
            notification.success({
                message: 'Vô hiệu dạng sản phẩm bán ra thành công',
                description: 'Dạng sản phẩm bán ra đã được vô hiệu hóa',
            });
        },
        error: (err) => {
            notification.error({
                message: 'Vô hiệu hóa dạng sản phẩm bán ra thất bại',
                description: 'Có lỗi xảy ra khi vô hiệu hóa dạng sản phẩm bán ra',
            });
        },
        mutate: () => {
            setProcessing(true);
        },
        settled: () => {
            setProcessing(false);
        },
        obj: {
            id: isDisableOpen.id,
            params: { productId },
        },
    });

    const onAdd = async (variant) => {
        const req = { ...variant, productId };
        await mutationCreate.mutateAsync(req);
    };

    const onEdit = async (id, variant) => {
        await mutationUpdate.mutateAsync({
            id: id,
            body: variant,
        });
    };

    const onDelete = async (id) => {
        await mutationDelete.mutateAsync(id);
    };

    const handleVariant = (edit, variant) => {
        if (edit.isEdit) {
            onEdit(edit.index, variant);
        } else {
            onAdd(variant);
        }
    };

    return (
        <div>
            <div className="p-4 bg-white mb-3 flex justify-between rounded-lg">
                <Button
                    onClick={() => navigate(config.routes.admin.product)}
                    className="border border-[--primary-color] text-[--primary-color]"
                    icon={<FontAwesomeIcon icon={faArrowLeft} />}
                >
                    Trở lại
                </Button>
                <Button
                    onClick={() =>
                        setModalVariant({
                            title: 'Thêm dạng sản phẩm bán ra',
                            variant: null,
                            edit: {
                                index: null,
                                isEdit: false,
                            },
                            isOpen: true,
                        })
                    }
                    className="border border-blue-400 text-blue-400"
                    icon={<FontAwesomeIcon icon={faAdd} />}
                >
                    Thêm mới
                </Button>
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
                pagination={false}
            />
            {modalVariant.edit.index !== 0 && (
                <ModalVariant
                    loading={processing}
                    modalVariant={modalVariant}
                    setModalVariant={setModalVariant}
                    handleVariant={handleVariant}
                />
            )}
            {modalVariant.edit.index !== 0 && (
                <VariantDetail isDetailOpen={isDetailOpen} setIsDetailOpen={setIsDetailOpen} />
            )}
            <ConfirmPrompt
                handleConfirm={onDelete}
                content="Bạn có muốn vô hiệu hoá dạng sản phẩm này?"
                isDisableOpen={isDisableOpen}
                setIsDisableOpen={setIsDisableOpen}
            />
        </div>
    );
}

export default Data;
