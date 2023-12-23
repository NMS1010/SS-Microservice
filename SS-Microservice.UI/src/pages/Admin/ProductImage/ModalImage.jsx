import { PlusOutlined } from '@ant-design/icons';
import { Button, Modal, Upload } from 'antd';
import { useEffect, useRef, useState } from 'react';
import './image.scss';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};

function ModalImage({ modalImage, setModalImage, handleImage, loading }) {
    const inputRef = useRef(null);
    const [imageUrl, setImageUrl] = useState(null);
    const [imageFile, setImageFile] = useState(null);

    const handleSubmit = () => {
        handleImage(modalImage.edit, imageFile);
        setImageUrl(null);
        setImageFile(null);
    };

    const handleChange = (info) => {
        if (info.file) {
            setImageFile(info.file);
            getBase64(info.file, (url) => {
                setImageUrl(url);
            });
        }
    };

    useEffect(() => {
        if (!modalImage.edit.isEdit) return;
        setImageUrl(modalImage.image);
    }, [modalImage]);

    return (
        <Modal
            className="modal-image"
            title={<p className="text-center text-[2rem] mb-6">{modalImage.title}</p>}
            open={modalImage.isOpen}
            onCancel={() => {
                setModalImage({
                    image: null,
                    edit: { index: null, isEdit: false },
                    isOpen: false,
                });
                setImageUrl(null);
                setImageFile(null);
            }}
            footer={[
                <Button
                    onClick={handleSubmit}
                    loading={loading}
                    type="primary"
                    className={`${
                        modalImage.edit.isEdit ? 'bg-yellow-500' : 'bg-[--primary-color]'
                    } text-white px-[4rem]`}
                >
                    {modalImage.edit.isEdit ? 'Cập nhật' : 'Thêm'}
                </Button>,
            ]}
        >
            <div className="form-container mx-auto w-full">
                <Upload
                    name="image"
                    listType="picture-card"
                    className="flex justify-center"
                    showUploadList={false}
                    beforeUpload={() => false}
                    onChange={(info) => handleChange(info)}
                >
                    {imageUrl ? (
                        <img src={imageUrl} alt="category" className="w-full" />
                    ) : (
                        <div ref={inputRef}>
                            <PlusOutlined />
                            <div className="mt-[0.8rem]">Tải ảnh lên</div>
                        </div>
                    )}
                </Upload>
            </div>
        </Modal>
    );
}

export default ModalImage;
