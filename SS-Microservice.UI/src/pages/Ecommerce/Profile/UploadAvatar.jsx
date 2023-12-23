import { Upload } from 'antd';
import { useRef } from 'react';
import { PlusOutlined } from '@ant-design/icons';

const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
};
function UploadAvatar({setAvatar, imageUrl, setImageUrl}) {
    const inputRef = useRef(null);
    const handleChange = (info) => {
        if (info.file) {
            getBase64(info.file, (url) => {
                setImageUrl(url);
                setAvatar(info.file);
            });
        }
    };
    return (
        <div className="flex flex-col item-center text-center xl:border-l-[0.1rem] xl:ml-[6rem] mb-[3rem]">
            <Upload
                name="avatar"
                listType="picture-circle"
                className="avatar-uploader flex justify-center"
                showUploadList={false}
                beforeUpload={() => false}
                onChange={handleChange}
            >
                {imageUrl ? (
                    <img src={imageUrl} alt="avatar" className="w-full h-full rounded-full" />
                ) : (
                    <div ref={inputRef}>
                        <PlusOutlined />
                        <div
                            className='mt-[0.8rem]'
                        >
                            Tải ảnh lên
                        </div>
                    </div>
                )}
            </Upload>
        </div>
    );
}

export default UploadAvatar;
