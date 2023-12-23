import jwtDecode from 'jwt-decode';

export const clearToken = () => {
    localStorage.removeItem('token');
};

export const isTokenStoraged = () => {
    return !!localStorage.getItem('token');
};

export const saveToken = (token) => {
    localStorage.setItem('token', JSON.stringify(token));
};

export const getRoles = () => {
    let token = JSON.parse(localStorage.getItem('token'));
    if (!token) return;

    let jwtDecodeObj = jwtDecode(token.accessToken);
    let role = Object.keys(jwtDecodeObj).find((val) => val.includes('role'));
    
    return jwtDecodeObj[role];
};
