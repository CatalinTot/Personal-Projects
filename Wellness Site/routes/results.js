const express = require('express');
const router = express.Router();
const multer = require('multer');
const {storage} = require('../cloudinary');
const upload = multer({storage});
const catchAsync = require('../util/catchAsync');
const {resultIdLength, isLoggedIn} = require('../middleware');
const resultsController = require('../controllers/results');


// render the result page
router.get('/', resultsController.renderResultsPage);

// create a new result
router.post('/', isLoggedIn, upload.array('image', {folder:'Wellness/Results'}), catchAsync(resultsController.createNewResult))

// edit a result
router.put('/:id', isLoggedIn, resultIdLength, upload.array('image', folder = 'Wellness/Results'), catchAsync(resultsController.editResult))

// delete the result
router.delete('/:id', catchAsync(resultsController.deletedResult));

module.exports = router;