import { Card, DatePicker } from 'antd';
import { useEffect, useState } from 'react';
import {
    BarChart,
    Bar,
    Rectangle,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    Legend,
    ResponsiveContainer,
    Label,
    LineChart,
    Line,
} from 'recharts';
import dayjs from 'dayjs';
import { useStatisticRevenue } from '../../../hooks/api';
import { numberFormatter } from '../../../utils/formatter';

const monthTickFormatter = (tick) => {
    const date = new Date(tick);

    return date.getMonth() + 1;
};

const renderQuarterTick = (tickProps) => {
    const { x, y, payload } = tickProps;
    const { value, offset } = payload;
    const date = new Date(value);
    const month = date.getMonth();
    const quarterNo = Math.floor(month / 3) + 1;
    const isMidMonth = month % 3 === 1;

    if (month % 3 === 1) {
        return <text x={x} y={y - 4} textAnchor="middle">{`Q${quarterNo}`}</text>;
    }

    const isLast = month === 11;

    if (month % 3 === 0 || isLast) {
        const pathX = Math.floor(isLast ? x + offset : x - offset) + 0.5;

        return <path d={`M${pathX},${y - 4}v${-35}`} stroke="red" />;
    }
    return null;
};

const CustomTooltip = ({ active, payload, label }) => {
    if (active && payload && payload.length) {
        return (
            <div className="custom-tooltip">
                <p className="label">{`${label} : ${payload[0].value} ${numberFormatter(
                    payload[0].value,
                )}`}</p>
            </div>
        );
    }

    return null;
};

function StatisticRevenue() {
    const [year, setYear] = useState(new Date().getFullYear().toString());
    const { isLoading, data } = useStatisticRevenue({ year });

    const onChange = (date, dateString) => {
        setYear(dateString);
    };

    return (
        <Card bordered={false} className="min-h-[382px]">
            <div className="flex items-center justify-between my-[1rem]">
                <h5 className="font-medium text-center text-[1.6rem] ml-[4rem]">
                    Thống kê doanh thu và chi phí theo năm
                </h5>
                <DatePicker
                    defaultValue={dayjs(year)}
                    format="YYYY"
                    className="mr-[1rem]"
                    onChange={onChange}
                    picker="year"
                    placeholder="Chọn năm"
                />
            </div>
            <LineChart width={700} height={300} data={data?.data}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="date" tickFormatter={monthTickFormatter} />
                <XAxis
                    dataKey="date"
                    axisLine={false}
                    tickLine={false}
                    interval={0}
                    tick={renderQuarterTick}
                    height={1}
                    scale="band"
                    xAxisId="quarter"
                />
                <YAxis />
                <Tooltip />
                <Legend />
                <Line
                    name="Doanh thu"
                    type="monotone"
                    dataKey="revenue"
                    stroke="#8884d8"
                    activeDot={{ r: 8 }}
                />
                <Line name="Chi phí" type="monotone" dataKey="expense" stroke="#82ca9d" />
            </LineChart>
        </Card>
    );
}

export default StatisticRevenue;
