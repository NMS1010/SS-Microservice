export const encodeQueryData = (data, name = '') => {
    const ret = [];
    for (let d in data)
        ret.push((name || encodeURIComponent(d)) + '=' + encodeURIComponent(data[d]));
    return ret.join('&');
};
