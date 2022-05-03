const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const imageSchema = new Schema({
    url: String,
    filename: String
})
const resultSchema = Schema({
    surname: {
        type: String
    },
    age: {
        type: Number,
        required: true
    },
    result: {
        type: String,
        required: true
    },
    images: [imageSchema]
})

module.exports = mongoose.model('Result', resultSchema);