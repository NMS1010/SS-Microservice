import { ArrowUpOutlined, ArrowDownOutlined } from '@ant-design/icons';
import { IconPackages, IconUsersGroup } from '@tabler/icons-react';
import { IconCreditCard } from '@tabler/icons-react';
import { IconBrandCashapp } from '@tabler/icons-react';
import { Card, Col, Row, Statistic, Tag } from 'antd';
import { useStatisticTotal } from '../../../hooks/api';
import { numberFormatter } from '../../../utils/formatter';

function StatisticTotal() {
    const { isLoading, data } = useStatisticTotal();

    return (
        <div>
            <Row gutter={16}>
                <Col span={6}>
                    <Card className="bg-[rgb(209,231,221)]" bordered={false}>
                        <Statistic
                            title="Doanh thu"
                            value={numberFormatter(data?.data?.revenue || 0)}
                            precision={2}
                            valueStyle={{
                                color: '#0f5132',
                            }}
                            prefix={
                                <div className="w-[30px] h-[30px] flex items-center justify-center rounded-[5px] bg-[rgb(25,135,84)]">
                                    <IconBrandCashapp className="text-white" />
                                </div>
                            }
                        />
                    </Card>
                </Col>
                <Col span={6}>
                    <Card bordered={false} className="bg-[rgb(248,215,218)]">
                        <Statistic
                            title="Chi phí"
                            value={numberFormatter(data?.data?.expense || 0)}
                            precision={2}
                            valueStyle={{
                                color: '#842029',
                            }}
                            prefix={
                                <div className="w-[30px] h-[30px] flex items-center justify-center rounded-[5px] bg-[rgb(220,53,69)]">
                                    <IconCreditCard className="text-white" />
                                </div>
                            }
                        />
                    </Card>
                </Col>
                <Col span={6}>
                    <Card bordered={false} className="bg-[rgb(255,243,205)]">
                        <Statistic
                            title="Khách hàng"
                            value={data?.data?.users}
                            valueStyle={{
                                color: '#055160',
                            }}
                            prefix={
                                <div className="w-[30px] h-[30px] flex items-center justify-center rounded-[5px] bg-[rgb(255,193,7)]">
                                    <IconUsersGroup className="text-white" />
                                </div>
                            }
                        />
                    </Card>
                </Col>
                <Col span={6}>
                    <Card bordered={false} className="bg-[rgb(207,244,252)]">
                        <Statistic
                            title="Đơn hàng đã giao"
                            value={data?.data?.orders}
                            valueStyle={{
                                color: '#664d03',
                            }}
                            prefix={
                                <div className="w-[30px] h-[30px] flex items-center justify-center rounded-[5px] bg-[rgb(13,202,240)]">
                                    <IconPackages className="text-white" />
                                </div>
                            }
                        />
                    </Card>
                </Col>
            </Row>
        </div>
    );
}

export default StatisticTotal;
