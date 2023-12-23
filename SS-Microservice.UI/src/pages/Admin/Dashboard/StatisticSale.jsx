import { Card, Image, Tag } from 'antd';
import { ArrowDownOutlined } from '@ant-design/icons';
import images from '../../../assets/images';
import { useGetSaleLatest } from '../../../hooks/api';
import { data } from 'autoprefixer';

function StatisticSale() {
    const { isLoading, data } = useGetSaleLatest();

    return (
        <Card bordered={false} className="card-container min-h-[340px]">
            <div className="flex items-center justify-between mb-[2rem]">
                <h5 className="font-medium text-center text-[2rem]">Đợi Sale hiện tại</h5>
            </div>
            <div className='text-center'>
                <Image
                    className="object-fill rounded-[5px] shadow-md"
                    width={300}
                    height={150}
                    src={data?.data?.image}
                />
                <div className="flex flex-col items-center">
                    <h3 className="text-[1.6rem] font-medium">Sale lễ 30/4</h3>
                    <div className="w-full flex flex-col items-center text-[1.4rem]">
                        <span>Bắt đầu: {new Date(data?.data?.startDate).toLocaleString()}</span>
                        <span>Kết thúc: {new Date(data?.data?.endDate).toLocaleString()}</span>
                    </div>
                    <Tag
                        bordered={false}
                        color="red"
                        className="w-full py-[0.5rem] text-center text-[1.8rem]"
                    >
                        <ArrowDownOutlined />
                        10%
                    </Tag>
                </div>
            </div>
        </Card>
    );
}

export default StatisticSale;
