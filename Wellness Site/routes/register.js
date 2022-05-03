const express = require('express');
const router = express.Router();
const catchAsync = require('../util/catchAsync');
const registerController = require('../controllers/register');

// render the register page
router.get('/', registerController.renderRegisterPage );

// send a post request, and a verification Email to the entered e-mail
router.post('/', catchAsync(registerController.sendVerificationEmail));

// render the validation page, with the message to check the e-mail
router.get('/validation', registerController.validationRegisterPage);

// when the user clicks on the link in the e-mail, he will be redirected to the login page
router.get('/verify/:token', catchAsync(registerController.verifyUserToken))

module.exports = router;