import { Carousel, Image } from 'antd';

const contentStyle = {
    margin: 0,
    height: '480px',
    borderRadius: '5px',
    objectFit: 'cover',
    color: '#fff',
    lineHeight: '160px',
    textAlign: 'center',
    // background: '#f8f8f8',
};

function Images({ productImages }) {
    const onChange = (currentSlide) => {
        console.log(currentSlide);
    };
    return (
        <div className="images-product">
            <Carousel afterChange={onChange}>
                {productImages?.map((v) => {
                    return (
                        <div key={v.id}>
                            <Image style={contentStyle} src={v.image} alt="" />
                        </div>
                    );
                })}
            </Carousel>
        </div>
    );
}

export default Images;
