import { Button, Col, Form, Input, Row } from 'antd';

function SendContact() {
    return (
        <div className="send-contact-container">
            <h2 className="text-[2.5rem] font-bold mb-[2rem]">Gửi thắc mắc cho chúng tôi</h2>
            <div className="form-container w-full">
                <Form
                    name="contact-form"
                    layout="vertical"
                    initialValues={{
                        remember: true,
                    }}
                    labelCol={{
                        span: 5,
                    }}
                >
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item
                                name="name"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập tên của bạn!',
                                    },
                                ]}
                            >
                                <Input placeholder="Nhập tên của bạn" />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={14}>
                            <Form.Item
                                name="email"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập email của bạn!',
                                    },
                                ]}
                            >
                                <Input placeholder="Nhập email của bạn" />
                            </Form.Item>
                        </Col>
                        <Col span={10}>
                            <Form.Item
                                name="phone"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập số điện thoại của bạn',
                                    },
                                ]}
                            >
                                <Input placeholder="Nhập số điện thoại của bạn" />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item
                                name="content"
                                rules={[
                                    {
                                        required: true,
                                        message: 'Nhập nội dung',
                                    },
                                ]}
                            >
                                <Input.TextArea
                                    showCount
                                    maxLength={1000}
                                    style={{
                                        height: 150,
                                        resize: 'none',
                                    }}
                                    placeholder="Nội dung . . . ."
                                />
                            </Form.Item>
                        </Col>
                    </Row>
                    <div className="flex justify-end items-center gap-[1rem]">
                        <Button
                            className="bg-[--primary-color] text-white min-w-[10%] uppercase border-none px-[3rem]"
                            size="large"
                        >
                            Gửi cho chúng tôi
                        </Button>
                    </div>
                </Form>
            </div>
        </div>
    );
}

export default SendContact;
