import { Card, DatePicker } from 'antd';
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
} from 'recharts';
import dayjs from 'dayjs';
import { useEffect, useState } from 'react';
import { useStatisticTopSellingProductYear } from '../../../hooks/api';

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

const COLORS = ['#a6a6a4', '#7258db', '#e5df88', '#ed5782', '#ff9966'];

function StatisticTopSellingProductYear() {
    const [year, setYear] = useState(new Date().getFullYear().toString());
    const [keys, setKeys] = useState(null);
    const [cdata, setCData] = useState([]);

    const { isLoading, data } = useStatisticTopSellingProductYear({ year });

    useEffect(() => {
        if (isLoading || !data) return;
        setCData(
            data?.data.map((d) => {
                return { date: d.date, ...d.products };
            }),
        );
        setKeys(Object.keys(data?.data[0]?.products));
    }, [isLoading, data]);

    const onChange = (date, dateString) => {
        setYear(dateString);
    };

    return (
        <Card bordered={false} className="min-h-[382px]">
            <div className="flex items-center justify-between my-[1rem]">
                <h5 className="font-medium text-center text-[1.6rem] ml-[4rem]">
                    Top sản phẩm bán chạy nhất năm
                </h5>
                <DatePicker
                    className="mr-[1rem]"
                    defaultValue={dayjs(year)}
                    format="YYYY"
                    onChange={onChange}
                    picker="year"
                    placeholder="Chọn năm"
                />
            </div>
            <BarChart width={700} height={350} data={cdata}>
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
                {keys &&
                    keys.map((key, index) => (
                        <Bar dataKey={key} stackId="a" fill={COLORS[index % COLORS.length]} />
                    ))}
            </BarChart>
        </Card>
    );
}

export default StatisticTopSellingProductYear;
