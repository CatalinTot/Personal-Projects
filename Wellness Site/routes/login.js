const express = require('express');
const router = express.Router();
const passport = require('passport');
const catchAsync = require('../util/catchAsync');
const loginControllers = require('../controllers/login');

// render the login page
router.get('/', loginControllers.renderLoginPage);

// login the user - post request
router.post('/',
    passport.authenticate('local', {
    failureRedirect: '/login',
    failureFlash: true,
    successFlash: true
}), loginControllers.loginUser);

// render the page to reset the password where the user enters the e-mail
router.get('/forgot', loginControllers.forgotPasswordForm);
// send the e-mail with the link to reset the password
router.post('/forgot', catchAsync(loginControllers.resetPasswordEmail))
//render the page with the form where the user will reset the password and can login
router.get('/reset/:token', catchAsync(loginControllers.newPasswordForm))
// after the user resets the password, send a confirmation e-mail
router.post('/reset/:token', catchAsync(loginControllers.sendConfirmationEmail));

module.exports = router;