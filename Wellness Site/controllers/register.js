const async = require('async');
const crypto = require('crypto');
const User = require('../models/user');
const nodemailer = require('nodemailer');
const Visitor = require('../models/visitors');
const mongoose = require('mongoose');

module.exports.renderRegisterPage = (req, res) => {
    res.render('user/register', {
        title: 'Register'
    });
}

module.exports.sendVerificationEmail = async (req, res, next) => {
    try {
        const newDate = new Date();
        var month = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        const myDate = `${month[newDate.getMonth()+1]}-${newDate.getFullYear() < 10 ? '0' + newDate.getFullYear() : newDate.getFullYear()}`
        console.log(myDate);
        mongoose.connection.db.collection('users').countDocuments(async function (err, count) {
            res.locals.userCount = count;
        });
        mongoose.connection.db.collection('visitors').countDocuments(async function (err, count) {
            if (count == 0) {
                const newVisitor = new Visitor();
                res.locals.userCount++;
                console.log(res.locals.userCount);
                newVisitor.userCount.push({date:myDate,count:res.locals.userCount});
                await newVisitor.save();
            } else {
                const visitor = await Visitor.findOne({});
                console.log(myDate, visitor.userCount[visitor.userCount.length-1].date);
                console.log(typeof(myDate), typeof(visitor.userCount[visitor.userCount.length-1].date));
                if(myDate != visitor.userCount[visitor.userCount.length-1].date){
                    res.locals.userCount = 0;
                    res.locals.userCount++;
                    console.log(res.locals.userCount);
                    visitor.userCount.push({date:myDate,count:res.locals.userCount})
                    await visitor.save();
                }
                else{
                    res.locals.userCount++;
                    console.log(res.locals.userCount);
                    visitor.userCount.push({date:myDate,count:res.locals.userCount})
                    await visitor.save();
                }
                console.log(visitor);

            }
    });
        const {
            username,
            password
        } = req.body;
        const user = new User({
            username
        })
        const newUser = await User.register(user, password);
        await sendVerificationEmail(newUser, req, res);
        req.flash("info", "A validation e-mail was sent to your email address!");
        return res.redirect('/register/validation');
    } catch (e) {
        req.flash('error', e.message);
        return res.redirect('/register');
    }

}

module.exports.validationRegisterPage = (req, res) => {
    res.render('user/validation', {
        title: 'Validation'
    });
}

module.exports.verifyUserToken = async (req, res) => {
    const user = await User.findOne({
        verifyEmailToken: req.params.token,
        verifyEmailExpires: {
            $gt: Date.now()
        }
    })

    if (!user) {
        req.flash('error', 'Email validation token is invalid or has expired.');
        return res.redirect('/about');
    } else {
        user.verifyEmailToken = undefined;
        user.verifyEmailExpires = undefined;
        user.isVerified = true;
        await user.save();
    }
    if (req.isAuthenticated()) {
        req.flash('success', 'Your e-mail was validated!');
        return res.redirect('/about')
    }
    req.flash('success', 'Your email was validated! You can now login!');
    res.redirect('/login');
}

async function sendVerificationEmail(user, req, res) {
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
                        res.redirect('/forgot');
                    }

                    user.verifyEmailToken = token;
                    user.verifyEmailExpires = Date.now() + 1000*60*60*24*7; // one week hour

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
                    subject: 'Account verification Email',
                    text: 'You are almost done with registering on Cat24Fit website.\n\n' +
                        'Please click on the following link, or paste this into your browser to complete the process:\n\n' +
                        'http://' + req.headers.host + '/register/verify/' + token + '\n\n' +
                        'If you did not request this, please ignore this email.\n'
                };
                smtpTransport.sendMail(mailOptions, function (err) {
                    // req.flash('info', 'A verification e-mail has been sent to ' + user.email + ' with further instructions.');
                    done(err, 'done');
                });
            }
        ],
        function (err) {
            if (err) return next(err);
            res.redirect('/register');
        });
}