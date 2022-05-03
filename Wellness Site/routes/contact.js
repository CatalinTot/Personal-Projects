const express = require('express');
const router = express.Router();
const catchAsync = require('../util/catchAsync');
const contactController = require('../controllers/contact');
router.get('/', contactController.renderContactForm)

router.post('/', catchAsync(contactController.sendMessage));

router.get('/sent', contactController.renderSentPage)

module.exports = router;