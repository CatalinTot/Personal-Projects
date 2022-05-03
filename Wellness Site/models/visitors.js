const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const visitorSchema = new Schema({
    date: {
        type: String,
        required: false,
    },
    count: {
        type: Number,
        required: false,
    }
})
const dataSchema = Schema({
    userCount:[visitorSchema]
})

module.exports = mongoose.model('Visitor', dataSchema);