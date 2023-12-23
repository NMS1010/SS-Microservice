import { Card, DatePicker } from 'antd';
import React, { PureComponent, useState } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer, Tooltip, Legend } from 'recharts';
import dayjs from 'dayjs';
import { useStatisticTopSellingProduct } from '../../../hooks/api';

// const data = [
//     { name: 'Group A', value: 100 },
//     { name: 'Group B', value: 500 },
//     { name: 'Group C', value: 400 },
//     { name: 'Group D', value: 200 },
//     { name: 'Group E', value: 200 },
// ];

const COLORS = ['#91c864', '#8155b2 ', '#64c6ec', '#e9c6be', '#faca51'];

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

function StatisticTopSellingProduct() {
    const currentDate = new Date();
    const [daterange, setDaterange] = useState([
        dayjs(new Date(currentDate.getFullYear(), currentDate.getMonth(), 1)),
        dayjs(currentDate),
    ]);
    const [params, setParams] = useState({
        startDate: daterange[0].format('YYYY-MM-DD'),
        endDate: daterange[1].format('YYYY-MM-DD'),
    });
    const { data, isLoading } = useStatisticTopSellingProduct(params);
    const onChange = (value) => {
        let startDate = value[0].format('YYYY-MM-DD');
        let endDate = value[1].format('YYYY-MM-DD');
        setParams({ startDate, endDate });
    };

    return (
        <Card bordered={false}>
            <div className="flex flex-col items-center justify-between my-[1rem]">
                <h5 className="font-medium text-center text-[1.6rem]">
                    Top sản phẩm bán chạy nhất
                </h5>
                <DatePicker.RangePicker
                    defaultValue={daterange}
                    format="YYYY-MM-DD"
                    onChange={onChange}
                />
                ;
            </div>
            <PieChart width={340} height={300}>
                <Pie
                    data={data?.data}
                    cx="50%"
                    cy="50%"
                    labelLine={false}
                    label={renderCustomizedLabel}
                    outerRadius={80}
                    fill="#8884d8"
                    dataKey="value"
                >
                    {data?.data?.map((entry, index) => (
                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                    ))}
                </Pie>
                <Tooltip />
                <Legend />
            </PieChart>
        </Card>
    );
}

export default StatisticTopSellingProduct;
