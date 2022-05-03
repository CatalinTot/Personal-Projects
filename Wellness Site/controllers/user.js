const User = require('../models/user');
const {cloudinary} = require('../cloudinary');
const genderTable = ['Male', 'Female', 'Other'];
const goalsTable = ['Weightloss', 'Maintain your weight', 'Grow muscles'];
let userCount = 0;

module.exports.subPlan = (req, res) => {
    res.render('user/plan', {
        title: 'Plan'
    });
}

module.exports.giftPage = (req, res) => {
    res.render('user/gift', {
        title: 'Gift'
    });
}

module.exports.detailsPage = (req, res) => {
    res.render('user/details', {
        title: 'Details'
    });
}

module.exports.updateDetails = async (req, res) => {
    try {
        const newInfo = req.body;
        const user = await User.findByIdAndUpdate(req.user._id, newInfo);
        const newDate = new Date();
        user.weightArray.push({date:`${newDate.getFullYear()}-${newDate.getMonth()<10? '0'+newDate.getMonth():newDate.getMonth()}-${newDate.getDay()<10? '0'+newDate.getDay():newDate.getDay()}`,weight:req.body.weight})
        user.isComplete = true;
        await user.save();
        const redirectUrl = req.session.returnTo || '/about';
        delete req.session.returnTo;
        req.flash('success', 'Your date was updated!');
        res.redirect(redirectUrl);
    } catch (e) {
        req.flash('error', e.message);
        res.redirect('/user/details');
    }

}

module.exports.renderProgilePage = (req, res) => {
    res.render('user/profile', {
        title: 'Profile'
    });
}

module.exports.uploadProfileImage = async (req, res) => {
    try {
        const user = await User.findById(req.user.id);
        user.image = {
            url: req.file.path,
            filename: req.file.filename
        }
        // console.log(user.image);
        await user.save();
        req.flash('success', 'Successfully added photo!');
        res.redirect('/user/profile');
    } catch (e) {
        req.flash('error', e);
        res.redirect('/user/profile');
    }

}

module.exports.renderEditPage = (req, res) => {
    res.render('user/edit', {
        title: 'Edit profile',
        genderTable,
        goalsTable
    });
}

module.exports.updateUserProfile = async (req, res) => {
    try {
        const newData = req.body;
        const user = await User.findByIdAndUpdate(req.user._id, newData);
        const newDate = new Date();
        user.weightArray.push({
            date: `${newDate.getFullYear()}-${newDate.getMonth()<10? '0'+newDate.getMonth():newDate.getMonth()}-${newDate.getDay()<10? '0'+newDate.getDay():newDate.getDay()}`,
            weight: req.body.weight
        })
        user
        if (req.file !== undefined) {
            if (user.image !== undefined)
                await cloudinary.uploader.destroy(req.user.image.filename); //delete the old image
            user.image = { //upload the new image
                url: req.file.path,
                filename: req.file.filename
            }
        }
        await user.save();
        req.flash('success', 'The profile was updated!');
        res.redirect('/user/profile');
    } catch (e) {
        req.flash('error', e.message);
        res.redirect('/user/profile/edit');
    }
}

module.exports.deleteProfile = async (req, res) => {
    try {
        if (req.user.image !== undefined) {
            await cloudinary.uploader.destroy(req.user.image.filename);
        }
        const user = await User.findByIdAndDelete(req.user._id);
        req.flash('success', 'Your account has been deleted!');
        res.redirect('/about');
    } catch (e) {
        req.flash('error', e);
        res.redirect('/user/profile');
    }

}

module.exports.getProgressPage = async(req,res)=>{
    try{
        const date = new Date();
        let day = date.getDay();
        let month = date.getMonth();
        if (day < 10)
            day = '0' + day;
        if (month < 10)
            month = '0' + month;
        const fullDate = `${day}.${month}.${date.getFullYear()}`
        res.render('user/progress', {
            title: 'Progress',
            date: fullDate
        });
    }catch(e){
        req.flash('error', e);
        res.redirect('back');
    }
    
}

module.exports.editMeasurements = async(req,res)=>{
    try {
        if (req.query.action === 'firstMeasure') {
            const currentUser = await User.findById(req.user._id);
            currentUser.firstMeasure = req.body;
            await currentUser.save();
            req.flash('success', 'The date has beed saved!');
            res.redirect('/user/progress');
        }
        if (req.query.action === 'newMeasure') {
            const currentUser = await User.findById(req.user._id);
            currentUser.newMeasure = req.body;
            await currentUser.save();
            req.flash('success', 'The date has beed saved!');
            res.redirect('/user/progress');
        }
        if (req.query.action === 'newWeight') {
            const currentUser = await User.findById(req.user._id);
            if (req.body.weight != currentUser.weight) {
                currentUser.weightArray.push({
                    weight: req.body.weight,
                    date: req.body.date
                });
                currentUser.weight = req.body.weight;
                await currentUser.save();
                req.flash('success', 'The new weight was added!');
                res.redirect('/user/progress');
            } else {
                req.flash('info', 'The weight is the same!');
                res.redirect('/user/progress');
            }

        }
    } catch (e) {
        req.flash('error', e);
        res.redirect('/user/progress');
    }
}

module.exports.editWeight = async(req,res)=>{
    try {
        const user = await User.findById(req.user.id);
        user.weightArray.splice(parseInt(req.params.id), 1, {
            weight: parseInt(req.body.weight),
            date: req.body.date
        });
        await user.save();
        req.flash('success', 'The value has been changed!');
        res.redirect('/user/progress');
    } catch (e) {
        req.flash('error', e);
        res.redirect('/user/progress');
    }
}

module.exports.deleteWeight = async(req,res)=>{
     try {
         const user = await User.findById(req.user.id);
         user.weightArray.splice(parseInt(req.params.id), 1);
         await user.save();
         req.flash('success', 'The value has been deleted!');
         res.redirect('/user/progress');
     } catch (e) {
         req.flash('error', e);
         res.redirect('/user/progress');
     }
}