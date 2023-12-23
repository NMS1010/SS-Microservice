import { Button, Modal } from 'antd';

function ConfirmPrompt({ isDisableOpen, setIsDisableOpen, content, handleConfirm }) {
    return (
        <Modal
            title={<p className="text-center text-[2rem] mb-6">Xác nhận</p>}
            open={isDisableOpen.isOpen}
            onCancel={() => setIsDisableOpen({ ...isDisableOpen, isOpen: false })}
            footer={[
                <Button
                    type="primary"
                    className="bg-red-500 text-white"
                    onClick={() => setIsDisableOpen(false)}
                >
                    Huỷ
                </Button>,
                <Button
                    onClick={() => handleConfirm(isDisableOpen.id)}
                    type="primary"
                    className="bg-green-500 text-white"
                >
                    Xác nhận
                </Button>,
            ]}
        >
            <p className="text-[1.6rem] border-b-[1px] border-t-[1px] py-[2rem]">{content}</p>
        </Modal>
    );
}

export default ConfirmPrompt;
