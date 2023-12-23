import Meta from 'antd/es/card/Meta';
import images from '../../../assets/images';
import Card from 'antd/es/card/Card';
import { Button } from 'antd';

function CardNews() {
    return (
        <Card
            className="shadow-[0px_10px_20px_0_rgba(0,0,0,0.2)] cursor-pointer"
            style={{
                width: '100%',
            }}
            cover={<img className="h-[140px] object-cover" src={images.news.news1} />}
        >
            <Meta title="[KHUYẾN MÃI MỚI] Nhập mã “MUALANDAU” giảm ngay 5% cho khách hàng mới" />
            <div className="text-end mt-[2rem]">
                <Button
                    className="uppercase bg-yellow-500 text-white border-none hover:border-none"
                    shape="round"
                >
                    Đọc tiếp
                </Button>
            </div>
        </Card>
    );
}

export default CardNews;
