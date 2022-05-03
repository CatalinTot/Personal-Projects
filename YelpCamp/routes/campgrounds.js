const express = require('express');
const router = express.Router();
const catchAsync = require('../util/catchAsync');
const campgroundController = require('../controllers/campgrounds');
const multer = require('multer');
const {
    storage
} = require('../cloudinary');
var upload = multer({
    storage
});
const {
    isLoggedIn,
    isAuthor,
    validateCampground
} = require('../middleware')
router.route('/')
    .get(catchAsync(campgroundController.homePage))
    .post(isLoggedIn, upload.array('image'), validateCampground,
        catchAsync(campgroundController.createCampground))
router.get('/new', isLoggedIn, campgroundController.newCampgroundForm)

router.route('/:id')
    .get(catchAsync(campgroundController.showCampground))
    .put(isLoggedIn, isAuthor, upload.array('image'), validateCampground,
        catchAsync(campgroundController.updateCampground))
    .delete(isLoggedIn, isAuthor,
        catchAsync(campgroundController.deleteCampground))

router.get('/:id/edit', isLoggedIn, isAuthor,
    catchAsync(campgroundController.editCampground))

module.exports = router;