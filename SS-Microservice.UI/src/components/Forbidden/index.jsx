import images from '../../assets/images';

function Forbidden() {
    return (
        <div style={{ height: '100vh', width: '100vw' }}>
            <img src={images.forbidden} alt="" style={{ height: '100%', width: '100%' }} />
        </div>
    );
}

export default Forbidden;
