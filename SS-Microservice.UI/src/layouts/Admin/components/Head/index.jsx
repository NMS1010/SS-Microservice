import { faTrashCan } from '@fortawesome/free-regular-svg-icons';
import { faAdd, faHashtag } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from 'antd';
import { useNavigate } from 'react-router-dom';

function Head({ route, title, isAdd = true, isDisableAll = true, handleDisableAll = () => {} }) {
    const navigate = useNavigate();
    return (
        <div className="flex items-center justify-between">
            <div className="flex items-center gap-[1rem]">
                <FontAwesomeIcon
                    className="text-[2rem] bg-[--primary-color] p-4 rounded-xl text-white"
                    icon={faHashtag}
                />
                <h1 className="font-bold">{title}</h1>
            </div>
            <div className="flex gap-[1rem] text-[1.4rem]">
                {isAdd && (
                    <Button
                        onClick={() => navigate(route)}
                        className="border border-blue-400 text-blue-400"
                        icon={<FontAwesomeIcon icon={faAdd} />}
                    >
                        Thêm mới
                    </Button>
                )}
                {isDisableAll && (
                    <Button
                        onClick={handleDisableAll}
                        className="border border-red-400 text-red-400"
                        icon={<FontAwesomeIcon icon={faTrashCan} />}
                    >
                        Vô hiệu hoá hàng loạt
                    </Button>
                )}
            </div>
        </div>
    );
}

export default Head;
