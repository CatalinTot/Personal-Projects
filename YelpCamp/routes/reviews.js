const express = require('express');
const router = express.Router({
    mergeParams: true
});
const catchAsync = require('../util/catchAsync');
const {
    validateReview,
    isLoggedIn,
    isReviewAuthor
} = require('../middleware')

const reviewsController = require('../controllers/reviews')


router.post('/', validateReview, isLoggedIn, catchAsync(reviewsController.createReview))

router.delete('/:reviewId', isLoggedIn, isReviewAuthor, catchAsync(reviewsController.deleteReview))

module.exports = router;