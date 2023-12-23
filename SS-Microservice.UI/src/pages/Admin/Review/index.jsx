import './review.scss';
import Data from './Data';
import ReviewHead from './ReviewHead';
import { useState } from 'react';

function ReviewPage() {
    const [reviewIds, setReviewIds] = useState([]);

    const [params, setParams] = useState({
        pageIndex: 1,
        pageSize: 5,
        isSortAscending: false,
        columnName: 'updatedAt',
    });
    return (
        <div className="review-container">
            <ReviewHead params={params} reviewIds={reviewIds} />
            <Data params={params} setParams={setParams} setReviewIds={setReviewIds} />
        </div>
    );
}

export default ReviewPage;
