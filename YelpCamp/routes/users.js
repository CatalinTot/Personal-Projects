const express = require('express');
const router = express.Router();
const catchAsync = require('../util/catchAsync');
const userController = require('../controllers/users');
const passport = require('passport');
router.route('/register')
    .get(userController.registerForm)
    .post(catchAsync(userController.createNewUser))
router.route('/login')
    .get(userController.loginForm)
    .post(passport.authenticate('local', {
        failureRedirect: '/login',
        failureFlash: true,
        successFlash: true

    }), userController.loginUser)

router.get('/logout', userController.logout)

module.exports = router;