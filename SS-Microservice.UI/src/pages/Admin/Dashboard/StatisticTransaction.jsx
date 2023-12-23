import { Card } from 'antd';
import images from '../../../assets/images';
import { usGetTop5TransactionLatest } from '../../../hooks/api';
import { useEffect, useState } from 'react';
import { numberFormatter } from '../../../utils/formatter';
import { useNavigate } from 'react-router-dom';
import config from '../../../config';

function StatisticTransaction() {
    const navigate = useNavigate();
    const { isLoading, data } = usGetTop5TransactionLatest();
    const [transactions, setTransactions] = useState([]);

    useEffect(() => {
        if (isLoading || !data) return;
        setTransactions(data?.data);
    }, [isLoading, data]);

    console.log(transactions);

    return (
        <Card bordered={false} className="card-container min-h-[382px]">
            <div className="flex items-center justify-between mb-[2rem]">
                <h5 className="font-medium text-center text-[2rem]">Giao dịch mới nhất</h5>
                <span
                    className="text-[#3ea4ff] cursor-pointer"
                    onClick={() => navigate(config.routes.admin.transaction)}
                >
                    Xem thêm
                </span>
            </div>
            {transactions.map((item, index) => (
                <div key={index} className="flex items-center justify-between mt-[1.8rem]">
                    <div className="flex items-center">
                        <div className="w-[28px] h-[28px] mr-[1.5rem]">
                            <img
                                className="rounded-full"
                                src={item?.user?.avatar ? item?.user?.avatar : images.user}
                                alt="avatar"
                            />
                        </div>
                        <div className="flex flex-col">
                            <span className="text-[1.6rem] font-medium text-[--text-color]">
                                {`${item?.user?.lastName}  ${item?.user?.firstName}`}
                            </span>
                            <span className="text-[1.2rem]">
                                {new Date(item?.transaction?.createdAt).toLocaleString()}
                            </span>
                        </div>
                    </div>
                    <div className="text-[1.7rem] text-[--primary-color] font-bold">
                        {numberFormatter(item?.transaction?.totalPay)}
                    </div>
                </div>
            ))}
        </Card>
    );
}

export default StatisticTransaction;
