import images from '../../../../assets/images';
import config from '../../../../config';
import { Link, NavLink, useLocation, useNavigate, useSearchParams } from 'react-router-dom';
import {
    faBars,
    faCartShopping,
    faMagnifyingGlass,
    faUserCircle,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useGetCart, useGetListSearchingProduct, useGetMe } from '../../../../hooks/api';
import { Badge, Dropdown, Input, Spin } from 'antd';
import { clearToken, isTokenStoraged } from '../../../../utils/storage';
import { useContext, useEffect, useState } from 'react';
import NotificationItem from '../NotificationItem';
import WebLoading from '../WebLoading';
import { useDebounce } from '../../../../hooks/custom';
import { encodeQueryData } from '../../../../utils/queryParams';
import { MAX_PRICE, MIN_PRICE } from '../../../../utils/constants';
import { NotificationContext } from '../../../../context/NotificationContext';

function Head() {
    const { countNotify, refetchNotification, notifications } = useContext(NotificationContext);

    const [searchParams, setSearchParams] = useSearchParams();
    const navigate = useNavigate();
    const [searchVal, setSearchValue] = useState(searchParams.get('search') || '');
    const debouncedSearchValue = useDebounce(searchVal);
    const location = useLocation();

    const { data: searchdt, isLoading: isSearchLoading } = useGetListSearchingProduct({
        search: debouncedSearchValue,
        pageSize: 5,
    });

    useEffect(() => {
        if (location.pathname === '/') {
            setSearchValue('');
        }
    }, [location.pathname]);

    const [user, setUser] = useState(null);
    const { isLoading, data, refetch: refetchMe } = useGetMe();
    const { isLoading: isLoadingCart, data: dCart, refetch: refetchCart } = useGetCart({
        all: true
    });

    useEffect(() => {
        if (isLoading || isLoadingCart) return;
        setUser(data?.data);
    }, [isLoading, isLoadingCart, data, dCart]);

    const onLogout = () => {
        clearToken();
        setUser(null);
        localStorage.removeItem('chosenCartItems');
        window.location.href = config.routes.web.login;
    };

    const onSearch = (e) => {
        setSearchValue(e.target.value);
    };

    if (localStorage.getItem('token')){
        if((!data?.data || !dCart?.data)){
            refetchMe();
            refetchCart();
            return <WebLoading />;
        }
    } 

    return (
        <div className="head-container">
            <div className="container grid grid-cols-12 lg:gap-[5rem] mx-auto bg-white h-[68px] my-[1rem] sm:px-[3.2rem] px-[1rem] items-center justify-between">
                <label for="active" className="lg:hidden col-span-4 text-[2.4rem] cursor-pointer">
                    <FontAwesomeIcon icon={faBars} />
                </label>
                <Link to={'/'} className="lg:col-span-2 col-span-4">
                    <div className="logo md:w-[130px] md:h-[68px] mx-auto">
                        <img src={images.logo} alt="logo" />
                    </div>
                </Link>
                <div className="search xl:col-span-5 lg:col-span-4 col-span-12 max-lg:order-1 flex-1 h-[40px] pl-[1.5rem] pr-[0.5rem] py-2.5 rounded-full  justify-between items-center inline-flex">
                    <Dropdown
                        menu={{
                            items: [
                                {
                                    key: '-1',
                                    type: 'group',
                                    label: 'Kết quả tìm kiếm',
                                },
                                ...(searchdt?.data?.items || [])?.map((item, idx) => {
                                    return {
                                        key: item?.slug,
                                        label: item?.name.toLowerCase(),
                                    };
                                }),
                                searchdt?.data?.items?.length > 0 && {
                                    key: '-3',
                                    type: 'group',
                                    label: (
                                        <div className="flex justify-center">
                                            <NavLink
                                                reloadDocument={location.pathname.includes('-')}
                                                className={'text-green-700'}
                                                to={
                                                    config.routes.web.search +
                                                    '?' +
                                                    encodeQueryData({
                                                        search: searchVal,
                                                        minPrice: MIN_PRICE,
                                                        maxPrice: MAX_PRICE,
                                                        rating: 1,
                                                        pageIndex: 1,
                                                        pageSize: 50,
                                                        isSortAscending: true,
                                                        columnName: 'name',
                                                    })
                                                }
                                            >
                                                Xem tất cả
                                            </NavLink>
                                        </div>
                                    ),
                                },
                            ],
                            onClick: (e) => {
                                setSearchValue('');
                                navigate(`${config.routes.web.product_detail}/${e.key}`);
                            },
                        }}
                        trigger={['click']}
                        placement="bottom"
                        arrow={{
                            pointAtCenter: true,
                        }}
                    >
                        <div className="flex justify-between w-full items-center">
                            <input
                                onChange={onSearch}
                                value={searchVal}
                                className="bg-[--search-main] w-full p-[0.5rem] border-hidden text-black text-[1.4rem] placeholder:text-[1.4rem] ring-0"
                                type="search"
                                name="search"
                                id="search-main"
                                placeholder="Nhập tên sản phẩm . . . ."
                            />
                            <Link
                                reloadDocument={location.pathname.includes('-')}
                                className={
                                    'w-[40px] h-[40px] text-[1.4rem] flex items-center justify-center'
                                }
                                to={
                                    config.routes.web.search +
                                    '?' +
                                    encodeQueryData({
                                        search: searchVal,
                                        minPrice: MIN_PRICE,
                                        maxPrice: MAX_PRICE,
                                        rating: 1,
                                        pageIndex: 1,
                                        pageSize: 50,
                                        isSortAscending: true,
                                        columnName: 'name',
                                    })
                                }
                            >
                                <FontAwesomeIcon icon={faMagnifyingGlass} />
                            </Link>
                        </div>
                    </Dropdown>
                </div>
                <div className="xl:col-span-5 lg:col-span-6 col-span-4 flex items-center lg:justify-center justify-end">
                    <Link>
                        <div className="max-lg:hidden flex items-center">
                            <Dropdown
                                onOpenChange={() => refetchNotification()}
                                menu={{
                                    items:
                                        isTokenStoraged() && user
                                            ? [
                                                  {
                                                      key: '-1',
                                                      type: 'group',
                                                      label: 'Thông báo mới nhận',
                                                  },
                                                  ...notifications?.map((item, idx) => {
                                                      return {
                                                          key: item?.id,
                                                          label: (
                                                              <NotificationItem
                                                                  key={item?.id}
                                                                  notification={item}
                                                                  isRead={item?.status}
                                                              />
                                                          ),
                                                      };
                                                  }),
                                                  {
                                                      key: '-3',
                                                      type: 'group',
                                                      label: (
                                                          <div className="flex justify-center">
                                                              <NavLink
                                                                  className={'text-green-700'}
                                                                  to={
                                                                      config.routes.web.notification
                                                                  }
                                                              >
                                                                  Xem tất cả
                                                              </NavLink>
                                                          </div>
                                                      ),
                                                  },
                                              ]
                                            : [
                                                  {
                                                      key: '-1',
                                                      type: 'group',
                                                      label: (
                                                          <div className="min-h-[10rem] flex justify-center items-center">
                                                              <p>Đăng nhập để xem thông báo</p>
                                                          </div>
                                                      ),
                                                  },
                                                  {
                                                      key: '-3',
                                                      type: 'group',
                                                      label: (
                                                          <div className="flex justify-between items-center gap-[1rem]">
                                                              <Link
                                                                  className={
                                                                      'text-green-700 text-center min-w-[10rem] border border-solid border-[--primary-color] hover:bg-[--primary-color]  hover:text-white py-[0.5rem] rounded-[5px]'
                                                                  }
                                                                  to={config.routes.web.login}
                                                              >
                                                                  Đăng nhập
                                                              </Link>
                                                              <Link
                                                                  className={
                                                                      'text-green-700 text-center min-w-[10rem] border border-solid border-[--primary-color]  hover:bg-[--primary-color]  hover:text-white py-[0.5rem] rounded-[5px]'
                                                                  }
                                                                  to={config.routes.web.register}
                                                              >
                                                                  Đăng ký
                                                              </Link>
                                                          </div>
                                                      ),
                                                  },
                                              ],
                                }}
                                placement="bottom"
                                arrow={{
                                    pointAtCenter: true,
                                }}
                            >
                                <div className="flex items-center">
                                    <Badge
                                        overflowCount={99}
                                        count={isTokenStoraged() && user ? countNotify : 0}
                                        className=" mr-[1.5rem]"
                                    >
                                        <div className="w-[28px] h-[28px]">
                                            <img src={images.bell} alt="bell" />
                                        </div>
                                    </Badge>
                                    <span className="text-[1.4rem]">Thông báo</span>
                                </div>
                            </Dropdown>
                        </div>
                    </Link>
                    <div className="max-lg:hidden flex items-center mx-[3rem]">
                        {isTokenStoraged() && user ? (
                            <Dropdown
                                className="cursor-pointer"
                                menu={{
                                    items: [
                                        {
                                            key: '1',
                                            label: (
                                                <Link to={config.routes.web.profile}>
                                                    Tài khoản của Tôi
                                                </Link>
                                            ),
                                        },
                                        {
                                            key: '2',
                                            label: (
                                                <Link to={config.routes.web.order}>Đơn mua</Link>
                                            ),
                                        },
                                        {
                                            key: '3',
                                            label: <div onClick={() => onLogout()}>Đăng xuất</div>,
                                        },
                                    ],
                                }}
                                placement="bottom"
                                arrow
                            >
                                <div className="flex items-center">
                                    <div className="w-[28px] h-[28px] mr-[1.5rem]">
                                        <img
                                            className="rounded-full"
                                            src={user?.avatar ? user?.avatar : images.user}
                                            alt="avatar"
                                        />
                                    </div>
                                    <div className="text-[1.4rem]">{`${user?.lastName} ${user?.firstName}`}</div>
                                </div>
                            </Dropdown>
                        ) : (
                            <div className="flex items-center">
                                <div className="w-[28px] h-[28px] mr-[1.5rem]">
                                    <img
                                        className="rounded-full"
                                        src={
                                            isTokenStoraged() && user?.avatar
                                                ? user?.avatar
                                                : images.user
                                        }
                                        alt="bell"
                                    />
                                </div>
                                <div className="flex-col">
                                    <Link
                                        to={config.routes.web.login}
                                        className="block text-[1.6rem]"
                                    >
                                        Đăng nhập
                                    </Link>
                                    <Link
                                        to={config.routes.web.register}
                                        className="block text-[1.2rem]"
                                    >
                                        Đăng ký
                                    </Link>
                                </div>
                            </div>
                        )}
                    </div>
                    <Link to={config.routes.web.cart}>
                        <div className="cart max-md:border-none w-[130px] h-[40px] bg-white p-[0.8rem] flex items-center justify-start max-md:justify-end max-md:relative">
                            <FontAwesomeIcon className="text-[2.4rem]" icon={faCartShopping} />
                            <span className="max-md:hidden text-[1.4rem] mx-[0.4rem]">
                                Giỏ hàng
                            </span>
                            <span className="max-md:absolute bottom-[2rem] right-[-0.2rem] top text-[1.2rem] px-[0.5rem] py-[0.3rem] bg-yellow-400 rounded-[5px]">
                                {dCart?.data?.currentItemCount || 0}
                            </span>
                        </div>
                    </Link>
                </div>
            </div>
            <input className="hidden" type="checkbox" id="active" />
            <div className="wrapper hidden h-screen w-screen fixed top-0 left-0 justify-between z-10">
                <div className="menu bg-white min-w-[300px]">
                    <div className="top py-[1rem] px-[1.5rem]">
                        <div className="flex items-center text-white">
                            <div className="text-[2.4rem] mr-[1.5rem]">
                                <FontAwesomeIcon icon={faUserCircle} />
                            </div>
                            <div className="flex-col">
                                <Link className="block text-[1.6rem]">Đăng nhập</Link>
                                <Link className="block text-[1.2rem]">Đăng ký</Link>
                            </div>
                        </div>
                    </div>
                    <ul className="flex-col text-[1.6rem] uppercase">
                        <li>
                            <Link>Trang chủ</Link>
                        </li>
                        <li>
                            <Link>Sản phẩm</Link>
                        </li>
                        <li>
                            <Link>Liên hệ</Link>
                        </li>
                        <li>
                            <Link>Về chúng tôi</Link>
                        </li>
                        <li>
                            <Link>Cộng đồng</Link>
                        </li>
                    </ul>
                </div>
                <label for="active" className="h-full w-full bg-black opacity-[0.2]"></label>
            </div>
        </div>
    );
}

export default Head;
