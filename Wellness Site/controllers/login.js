const async = require('async');
const crypto = require('crypto');
const User = require('../models/user');
const nodemailer = require('nodemailer');

module.exports.renderLoginPage = (req, res) => {
    res.render('user/login', {
        title: 'Login'
    });
}

module.exports.loginUser = (req, res) => {
        req.flash('success', "Welcome back!");
        const redirectUrl = req.session.returnTo || '/about';
        delete req.session.returnTo;
        res.redirect(redirectUrl);
    
}

module.exports.forgotPasswordForm = (req, res) => {
    res.render('user/forgot', {
        title: 'Forgot'
    });
}

module.exports.resetPasswordEmail = async (req, res) => {
    async.waterfall([
            function (done) {
                crypto.randomBytes(20, function (err, buf) {
                    var token = buf.toString('hex');
                    done(err, token);
                });
            },
            function (token, done) {
                User.findOne({
                    username: req.body.username
                }, function (err, user) {
                    if (!user) {
                        req.flash('error', 'No account with that email address exists.');
                        return res.redirect('/login/forgot');
                    }

                    user.resetPasswordToken = token;
                    user.resetPasswordExpires = Date.now() + 3600000; // 1 hour

                    user.save(function (err) {
                        done(err, token, user);
                    });
                });
            },
            function (token, user, done) {
                var smtpTransport = nodemailer.createTransport({
                    service: 'Outlook',
                    auth: {
                        user: 'tot.vi.catalin@utcluj.didatec.ro',
                        pass: process.env.OUTLOOKPASS
                    }
                });
                var mailOptions = {
                    to: user.username,
                    from: 'tot.vi.catalin@utcluj.didatec.ro',
                    subject: 'Node.js Password Reset',
                    text: 'You are receiving this because you (or someone else) have requested the reset of the password for your account.\n\n' +
                        'Please click on the following link, or paste this into your browser to complete the process:\n\n' +
                        'http://' + req.headers.host + '/login/reset/' + token + '\n\n' +
                        'If you did not request this, please ignore this email and your password will remain unchanged.\n'
                };
                smtpTransport.sendMail(mailOptions, function (err) {
                    req.flash('info', 'An e-mail has been sent to ' + user.username + ' with further instructions.');
                    done(err, 'done');
                });
            }
        ],
        function (err) {
            if (err) return next(err);
            res.redirect('/login/forgot');
        });
}

module.exports.newPasswordForm = async (req, res) => {
    await User.findOne({
        resetPasswordToken: req.params.token,
        resetPasswordExpires: {
            $gt: Date.now()
        }
    }, function (err, user) {
        if (!user) {
            req.flash('error', 'Password reset token is invalid or has expired.');
            return res.redirect('/login/forgot');
        }
        res.render('user/reset', {
            token: req.params.token,
            title: 'Reset'
        });
    });
}

module.exports.sendConfirmationEmail = async (req, res) => {
    async.waterfall([
        function (done) {
            User.findOne({
                resetPasswordToken: req.params.token,
                resetPasswordExpires: {
                    $gt: Date.now()
                }
            }, function (err, user) {
                if (!user) {
                    req.flash('error', 'Password reset token is invalid or has expired.');
                    return res.redirect('back');
                }

                if (req.body.password === req.body.confirm) {
                    user.setPassword(req.body.password, function (err) {
                        user.resetPasswordToken = undefined;
                        user.resetPasswordExpires = undefined;

                        user.save(function (err) {
                            req.login(user, function (err) {
                                done(err, user);
                            });
                        });
                    })
                } else {
                    req.flash('error', 'Password do not match!');
                    return res.redirect('back');
                }


            });
        },
        function (user, done) {
            var smtpTransport = nodemailer.createTransport({
                service: 'Outlook',
                auth: {
                    user: 'tot.vi.catalin@utcluj.didatec.ro',
                    pass: process.env.OUTLOOKPASS
                }
            });
            var mailOptions = {
                to: user.username,
                from: 'tot.vi.catalin@utcluj.didatec.ro',
                subject: 'Your password has been changed',
                text: 'Hello,\n\n' +
                    'This is a confirmation that the password for your account ' + user.username + ' has just been changed.\n'
            };
            smtpTransport.sendMail(mailOptions, function (err) {
                req.flash('success', 'Success! Your password has been changed.');
                done(err);
            });
        }
    ], function (err) {
        res.redirect('/about');
    });
}