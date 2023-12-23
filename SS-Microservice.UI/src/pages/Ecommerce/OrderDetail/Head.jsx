import { faArrowLeft, faChevronLeft } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { NavLink } from "react-router-dom";
import config from "../../../config";
import { getOrderStatus } from "../../../utils/constants";
import { Tag } from "antd";


function Head({code, status}) {
    return <div className="flex justify-between p-[2rem] border-b-[0.1rem]">
        <NavLink to={config.routes.web.order} className="flex items-center gap-[0.3rem] text-[1.6rem] cursor-pointer hover:text-red-300 transition-all">
            <FontAwesomeIcon icon={faChevronLeft}/>
            <p className="mb-0">Trở lại</p>
        </NavLink>
        <div className="flex items-center text-[1.4rem]">
            <p className="uppercase">Mã đơn hàng <p className="font-bold text-green-800">{code}</p></p>
            <span className="mx-[1.5rem] font-normal block h-[2.5rem] border-l-[0.01rem] border-gray-600"></span>
            <Tag style={{
                backgroundColor: getOrderStatus(status)?.color,
                color: 'white'
            }} className="uppercase">{getOrderStatus(status)?.title}</Tag>
        </div>
    </div>
}

export default Head;