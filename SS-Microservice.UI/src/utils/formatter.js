export const numberFormatter = (
    value,
    locale = 'vi-VN',
    options = { style: 'currency', currency: 'VND' },
) => {
    return new Intl.NumberFormat(locale, options).format(value);
};
