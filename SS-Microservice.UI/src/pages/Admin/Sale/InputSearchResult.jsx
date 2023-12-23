import { faCheck } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

function InputSearchResult({ itemList, add, chosenList, Item }) {
    return (
        <div className="bg-white max-h-[250px] overflow-y-auto w-full absolute p-3 shadow transition-all">
            {itemList.map((item, idx) => {
                let isChosen = chosenList.some((x) => x.id === item.id);
                return (
                    <div
                        key={item.id}
                        onClick={() => add(item)}
                        className={`${
                            isChosen ? 'cursor-not-allowed' : 'cursor-pointer'
                        } p-4 flex items-center justify-between hover:bg-gray-100 transition-all rounded-xl`}
                    >
                        <Item item={item} />
                        {isChosen && <FontAwesomeIcon className="text-green-500" icon={faCheck} />}
                    </div>
                );
            })}
        </div>
    );
}

export default InputSearchResult;
