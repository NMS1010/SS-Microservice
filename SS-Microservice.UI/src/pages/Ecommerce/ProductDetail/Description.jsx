import { Button } from 'antd';
import RecommentProduct from '../../../layouts/Ecommerce/components/RecommentProduct';
import { useState } from 'react';
import { Typography } from 'antd';
import DOMPurify from 'dompurify';

const { Paragraph } = Typography;

function Description({ product }) {
    const [expand, setExpand] = useState(false);
    return (
        <div className="description-product mt-[3rem] pt-[2rem] border-t-[1px]">
            <div className="grid grid-cols-12">
                <div className="col-span-9 max-lg:col-span-12 pr-[1rem] border-r-[1px] max-lg:border-none px-[1rem]">
                    <h2 className="text-[2.6rem] font-medium">Mô tả sản phẩm</h2>
                    <div className="text-[1.6rem] overflow-hidden">
                        <Paragraph
                            ellipsis={
                                !expand
                                    ? {
                                          expandable: false,
                                          rows: 15,
                                      }
                                    : false
                            }
                        >
                            <div
                                className="font-[roboto]"
                                dangerouslySetInnerHTML={{
                                    __html: DOMPurify.sanitize(product?.description),
                                }}
                            />
                        </Paragraph>
                    </div>
                    <div className="text-center my-[2.6rem]">
                        <Button
                            className="shadow-none hover:text-lime-700 hover:border-lime-700"
                            shape="round"
                            onClick={() => setExpand((prev) => !prev)}
                        >
                            {expand ? 'Ẩn bớt' : 'Xem thêm'}
                        </Button>
                    </div>
                </div>
                <div className="col-span-3 max-lg:col-span-12 pl-[1rem]">
                    <RecommentProduct />
                </div>
            </div>
        </div>
    );
}

export default Description;
