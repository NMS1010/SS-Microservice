import { Button, Modal, Upload } from 'antd';
import { useRef, useState } from 'react';
import { PlusOutlined } from '@ant-design/icons';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};
function UploadImage({ setImage, imageUrl, setImageUrl }) {
    const inputRef = useRef(null);
    const handleChange = (info) => {
        if (info.file) {
            getBase64(info.file, (url) => {
                setImageUrl(url);
                setImage(info.file);
            });
        }
    };

    return (
        <div className="flex flex-col item-center text-center mb-[3rem]">
            <Upload
                listType="picture-circle"
                showUploadList={false}
                beforeUpload={() => false}
                onChange={handleChange}
            >
                {imageUrl ? (
                    <img src={imageUrl} alt="image" className="w-full h-full rounded-full" />
                ) : (
                    <div ref={inputRef}>
                        <PlusOutlined />
                        <div className="mt-[0.8rem]">Tải ảnh lên</div>
                    </div>
                )}
            </Upload>
            <div>
                <Button
                    onClick={() => {
                        setImageUrl(null);
                        setImage(null);
                    }}
                    className="w-1/4 text-red-400 mr-4 hover:border-red-400 border-red-400"
                >
                    Xoá ảnh
                </Button>
            </div>
        </div>
    );
}

export default UploadImage;
