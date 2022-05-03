module.exports.isLoggedIn = (req, res, next) => {
    if (!req.isAuthenticated()) {
        req.session.returnTo = req.originalUrl
        req.flash('error', 'You must be logged in!');
        return res.redirect('/login')
    }
    next();
}

module.exports.hasSubscription = (req,res,next)=>{
    if(req.user.isSubscribed){
        req.flash('success','You have already a subscribtion active!');
        return res.redirect('/user/plan');
    }
    next();
}

module.exports.isVerified = (req,res,next)=>{
    if (!req.user.isVerified) {
        req.flash('info', 'Please verify your e-mail address!');
        return res.redirect('validation');
    }
    next();
}
module.exports.hasSubscribed = (req,res,next)=>{
    if(!req.user.isSubscribed){
        req.flash('info','Please choose a subscription plan!');
        return res.redirect('/user/plan');
    }
    next();
}

module.exports.hasPaidSubscribtion = (req,res,next)=>{
    if(req.user.isPaid){
        req.flash('success','You have already a paid subscribtion!');
        return res.redirect('/user/plan');
    }
    next();
}

module.exports.completeProfile = (req,res,next)=>{
    if(!req.user.isComplete){
        req.session.returnTo = req.originalUrl
        req.flash('success', 'Please fill your info to have a better experience on this page!');
        return res.redirect('/user/details');
    }
    next();

}

module.exports.completeProfileProgress = (req,res,next)=>{
    if(!req.user.isComplete){
        req.flash('success', 'Please fill your info to have a better experience on this page!');
        return res.redirect('/user/details');
    }
    next();
}

module.exports.passedDetails = (req,res,next)=>{
    if(req.user.isComplete){
         req.flash('success', 'Your profile is already completed, you can now enjoy your gift!');
         return res.redirect('/user/gift');
    }
    next();
}

module.exports.resultIdLength = (req,res,next)=>{
    if (req.params.id.length !== 24){
        req.flash('error','We could not find that result!');
        return res.redirect('/results');
    }
    next();
}
