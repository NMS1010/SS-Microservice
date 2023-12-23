import { faHashtag } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import StatisticTotal from './StatisticTotal';
import './dashboard.scss';
import { Col, Row } from 'antd';
import StatisticRevenue from './StatisticRevenue';
import StatisticTopSellingProductYear from './StatisticTopSellingProductYear';
import StatisticRating from './StatisticRating';
import StatisticOrderStatus from './StatisticOrderStatus';
import StatisticTopSellingProduct from './StatisticTopSellingProduct';
import StatisticTransaction from './StatisticTransaction';
import StatisticOrderLatest from './StatisticOrderLatest';
import StatisticSale from './StatisticSale';
import StatisticReviewLatest from './StatisticReviewLatest';
import Map from './Map';

function DashboardPage() {
    return (
        <div className="dashboard-container">
            <div className="flex items-center gap-[1rem] mb-[2rem]">
                <FontAwesomeIcon
                    className="text-[2rem] bg-[--primary-color] p-4 rounded-xl text-white"
                    icon={faHashtag}
                />
                <h1 className="font-bold">Dashboard</h1>
            </div>
            <Row gutter={[16, 40]} className="mb-[3rem]">
                <Col span={24}>
                    <StatisticTotal />
                </Col>
            </Row>
            <Row gutter={[16, 40]} className="mb-[2rem]">
                <Col span={16}>
                    <StatisticRevenue />
                </Col>
                <Col span={8}>
                    <StatisticTransaction />
                </Col>
            </Row>
            <Row gutter={[16, 40]} className="mb-[2rem]">
                <Col span={16}>
                    <StatisticTopSellingProductYear />
                </Col>
                <Col span={8}>
                    <StatisticTopSellingProduct />
                </Col>
            </Row>
            <Row gutter={[16, 40]} className="mb-[2rem]">
                <Col span={16}>
                    <StatisticOrderLatest />
                </Col>
                <Col span={8}>
                    <StatisticOrderStatus />
                </Col>
            </Row>
            <Row gutter={[16, 40]} className="mb-[2rem]">
                <Col span={16}>
                    <StatisticReviewLatest />
                </Col>
                <Col span={8}>
                    <StatisticRating />
                </Col>
            </Row>
            <Row gutter={[16, 40]} className="mb-[2rem]">
                <Col span={8}>
                    <StatisticSale />
                </Col>
                <Col span={16}>
                    <Map />
                </Col>
            </Row>
        </div>
    );
}

export default DashboardPage;
