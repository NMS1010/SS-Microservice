import './header.scss';
import Head from './Head';
import Navbar from './Navbar';

function Header() {
    return (
        <div className="header-container">
            <Head />
            <Navbar />
        </div>
    );
}

export default Header;
