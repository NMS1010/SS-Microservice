import images from '../../assets/images';

function NotFound() {
    return (
        <div style={{ height: '100vh', width: '100vw' }}>
            <img src={images.not_found} alt="" style={{ height: '100%', width: '100%' }} />
        </div>
    );
}

export default NotFound;
