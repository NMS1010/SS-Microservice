function ProductItem({ item }) {
    return (
        <div>
            <p className="text-[1.6rem]">{item.name}</p>
            <div className="flex text-gray-400 items-center gap-[1.5rem]">
                <p>Mã: {item.code}</p>
                <p>Phân loại: {item.category}</p>
            </div>
        </div>
    );
}

export default ProductItem;
