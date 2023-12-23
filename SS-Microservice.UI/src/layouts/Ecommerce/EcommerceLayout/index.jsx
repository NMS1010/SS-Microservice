import Header from '../components/Header';
import Footer from '../components/Footer';
import './ecommercelayout.scss';

function EcommerceLayout({ children }) {
    return (
        <>
            <Header />
            <div className="main">
                <div className="min-h-screen mx-auto">{children}</div>
            </div>
            <Footer />
        </>
    );
}

export default EcommerceLayout;
