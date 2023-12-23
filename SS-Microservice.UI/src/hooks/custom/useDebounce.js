import { useEffect, useState } from 'react';

const useDebounce = (value, delay = 500) => {
    const [debouncedValue, setDebouncedValue] = useState(value);
    useEffect(() => {
        const timeoutId = setTimeout(() => {
            setDebouncedValue(value);
        }, delay);
        return () => {
            clearTimeout(timeoutId);
        };
    }, [value]);
    return debouncedValue;
};

export default useDebounce;