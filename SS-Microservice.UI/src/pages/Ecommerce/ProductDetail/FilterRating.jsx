import { Radio } from 'antd';


function FilterRating({setParams, count}) {
    const onChange = (e) => {
        setParams((prev) => ({...prev, rating: e.target.value}))
    };
    return (
        <div className="filter-rating">
            <Radio.Group onChange={onChange} defaultValue={null}>
                <Radio.Button value={null}>Tất cả ({count[0]})</Radio.Button>
                <Radio.Button value="5">5 Sao ({count[1]})</Radio.Button>
                <Radio.Button value="4">4 Sao ({count[2]})</Radio.Button>
                <Radio.Button value="3">3 Sao ({count[3]})</Radio.Button>
                <Radio.Button value="2">2 Sao ({count[4]})</Radio.Button>
                <Radio.Button value="1">1 Sao ({count[5]})</Radio.Button>
            </Radio.Group>
        </div>
    );
}

export default FilterRating;
