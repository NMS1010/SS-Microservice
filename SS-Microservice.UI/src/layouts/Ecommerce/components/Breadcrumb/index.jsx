import { Breadcrumb } from 'antd';
import './breadcrumb.scss';
import config from '../../../../config';

function BreadCrumb({ routes }) {
    let rootRoutes = [
        {
            title: 'Trang chủ',
            href: '/',
        },
    ];

    return (
        <div className="breadcrumb-container bg-gray-200 py-[0.5rem] font-['Roboto'] text-[1.4rem]">
            <Breadcrumb
                className="max-w-[1200px] max-xl:w-[1024px] max-lg:w-[768px] max-md:w-[640px] max-xl:px-[2rem] mx-auto"
                items={[...rootRoutes, ...routes]}
            />
        </div>
    );
}

export default BreadCrumb;
