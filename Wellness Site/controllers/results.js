const Result = require('../models/result');
const {cloudinary} = require('../cloudinary');
const sanitizer = require('sanitize-html');
module.exports.renderResultsPage = async (req, res) => {
    const results = await Result.find({}).populate({
        path: 'images'
    });
    res.render('results', {
        title: 'Results',
        results
    });

}

module.exports.createNewResult = async (req, res) => {
    try {
        req.body.result.surname= sanitizer(req.body.result.surname, {
            allowedTags: ['b', 'i', 'em', 'strong', 'a'],
        });
        console.log(req.body.result.surname);
        
        const newResult = new Result(req.body.result);
        
        newResult.images = req.files.map(f => ({
            url: f.path,
            filename: f.filename
        }));
        await newResult.save();
        req.flash('success', 'Successfully made a new result');
        res.redirect(`/results`);
    } catch (e) {
        req.flash('error', e);
        res.redirect('/results')
    }
}

module.exports.editResult = async (req, res) => {
    try {
        const imgs = [];
        const {
            id
        } = req.params;
        const foundResult = await Result.findByIdAndUpdate(id, {
            ...req.body.result
        });
        if (req.files.length !== 0) {
            for (let image of foundResult.images) {
                await cloudinary.uploader.destroy(image.filename)
                imgs.push(image.filename);
            }
            await foundResult.updateOne({
                $pull: {
                    images: {
                        filename: {
                            $in: imgs
                        }
                    }
                }
            })
            foundResult.images = req.files.map(f => ({
                url: f.path,
                filename: f.filename
            }))
        }
        await foundResult.save();
        req.flash('success', 'Successfuly updated result');
        res.redirect('/results');
    } catch (e) {
        req.flash('error', e.reason.message);
        res.redirect('/results');
    }
}

module.exports.deletedResult = async (req, res) => {
    try {
        const {
            id
        } = req.params;
        const deletedResult = await Result.findByIdAndDelete(id);
        for (let image of deletedResult.images) {
            await cloudinary.uploader.destroy(image.filename)
        }
        req.flash('success', 'The result was deleted!');
        res.redirect('/results');
    } catch (e) {
        // console.log(e);
        req.flash('error', e.message);
        res.redirect('/results');
    }
}