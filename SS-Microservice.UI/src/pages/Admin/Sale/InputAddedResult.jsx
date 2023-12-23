import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

function InputAddedResult({ chosenList, remove, Item }) {
    return (
        <div className="mt-[2rem]">
            {chosenList?.map((item, index) => {
                return (
                    <div
                        key={item.id}
                        className="p-4 flex my-2 items-center justify-between bg-gray-100 transition-all rounded-xl"
                    >
                        <Item item={item} />
                        <FontAwesomeIcon
                            onClick={() => remove(item)}
                            className="text-red-500 border-red-300 cursor-pointer p-2 rounded-md border-2"
                            icon={faTrash}
                        />
                    </div>
                );
            })}
        </div>
    );
}

export default InputAddedResult;
