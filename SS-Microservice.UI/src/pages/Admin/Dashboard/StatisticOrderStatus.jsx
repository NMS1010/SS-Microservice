import { Card, DatePicker } from 'antd';
import React, { PureComponent, useState } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer, Tooltip, Legend } from 'recharts';
import dayjs from 'dayjs';
import { useStatisticOrderStatus } from '../../../hooks/api';

const RADIAN = Math.PI / 180;
const renderCustomizedLabel = ({ cx, cy, midAngle, innerRadius, outerRadius, percent, index }) => {
    const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
    const x = cx + radius * Math.cos(-midAngle * RADIAN);
    const y = cy + radius * Math.sin(-midAngle * RADIAN);

    return (
        <text
            x={x}
            y={y}
            fill="white"
            textAnchor={x > cx ? 'start' : 'end'}
            dominantBaseline="central"
        >
            {`${(percent * 100).toFixed(0)}%`}
        </text>
    );
};
const COLORS = ['#586177', '#ff9966', '#00cdd4', '#e9c6be', '#a2a2a2'];
const replaceMap = {
    NOT_PROCESSED: 'Chưa xử lý',
    PROCESSING: 'Đang xử lý',
    SHIPPED: 'Đã gửi',
    DELIVERED: 'Đã giao',
    CANCELLED: 'Đã hủy',
};

function StatisticOrderStatus() {
    const currentDate = new Date();
    const [daterange, setDaterange] = useState([
        dayjs(new Date(currentDate.getFullYear(), currentDate.getMonth(), 1)),
        dayjs(new Date(currentDate).setDate(currentDate.getDate() + 1)),
    ]);

    const { isLoading, data } = useStatisticOrderStatus({
        startDate: daterange[0].$d.toISOString(),
        endDate: daterange[1].$d.toISOString(),
    });

    const onChange = (value) => {
        if (value == null) {
            setDaterange(daterange);
            return;
        }
        setDaterange(value);
    };

    return (
        <Card bordered={false}>
            <div className="flex flex-col items-center justify-between my-[1rem]">
                <h5 className="font-medium text-center text-[1.6rem]">Đơn hàng theo trạng thái</h5>
                <DatePicker.RangePicker
                    defaultValue={daterange}
                    value={daterange}
                    format="YYYY-MM-DD"
                    onChange={onChange}
                />
            </div>
            <PieChart width={340} height={285}>
                <Pie
                    dataKey="value"
                    data={data?.data.map((item) => ({
                        name: replaceMap[item.name] || item.name,
                        value: item.value,
                    }))}
                    cx="50%"
                    cy="50%"
                    labelLine={false}
                    label={renderCustomizedLabel}
                    outerRadius={80}
                    fill="#8884d8"
                >
                    {data?.data.map((entry, index) => (
                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                    ))}
                </Pie>
                <Tooltip />
                <Legend />
            </PieChart>
        </Card>
    );
}

export default StatisticOrderStatus;
