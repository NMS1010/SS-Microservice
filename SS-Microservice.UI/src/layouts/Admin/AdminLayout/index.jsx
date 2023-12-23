import './adminlayout.scss';
import Header from '../components/Header';
import Navbar from '../components/Navbar';

function AdminLayout({ children }) {
    return (
        <>
            <Header />
            <div className="main pt-[58px]">
                <div className="grid grid-cols-12">
                    <div className="col-span-2">
                        <Navbar />
                    </div>
                    <div className="w-full min-h-screen col-span-10 container mx-auto bg-[--background-color-content-admin]">
                        <div className="p-[1.5rem] h-full">{children}</div>
                        <div className="lg:px-[36rem] pt-3 pb-2.5 flex-col bg-white text-center">
                            <div className="text-slate-700 text-[1.6rem] font-normal leading-[2.1rem]">
                                © Bản quyền thuộc về The Green Craze
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default AdminLayout;
