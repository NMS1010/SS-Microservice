import { Col, Row } from 'antd';
import BreadCrumb from '../../../layouts/Ecommerce/components/Breadcrumb';
import Map from './Map';
import './contact.scss';
import ContactInfo from './ContactInfo';
import SendContact from './SendContact';

function ContactPage() {
    return (
        <>
            <BreadCrumb routes={[{ title: 'Liên hệ' }]} />
            <div className="contact-container max-w-[1200px] h-full mx-auto p-[1.2rem]">
                <Map />
                <Row gutter={[40, 40]} className="my-[2rem]">
                    <Col span={10}>
                        <ContactInfo />
                    </Col>
                    <Col span={14}>
                        <SendContact />
                    </Col>
                </Row>
            </div>
        </>
    );
}

export default ContactPage;
