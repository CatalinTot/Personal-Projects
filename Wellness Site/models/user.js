const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const passportLocalMongoose = require('passport-local-mongoose');
const ImageSchema = new Schema({
    url: String,
    filename: String
})
const MeasureSchema = new Schema({
    date:{
        type:String,
        required:true,
    },
    neck:{
        type:Number,
        required:true,
    },
    chin:{
        type:Number,
        required:true
    },
    arm:{
        type:Number,
        required:true
    },
    bust:{
        type:Number,
        required:true
    },
    waist:{
        type:Number,
        required:true
    },
    stomach:{
        type:Number,
        required:true
    },
    hip:{
        type:Number,
        required:true
    },
    thighs:{
        type:Number,
        required:true
    },
    knee:{
        type:Number,
        required:true
    },
})
const weightSchema = new Schema({
    date:{
        type:String
    },
    weight:{
        type:Number
    }
})
ImageSchema.virtual('thumbnail').get(function () {
    return this.url.replace('/upload', '/upload/c_crop,g_face,h_500,w_500,x_0,y_0');
});
const userSchema = Schema({
    username: {
        type: String,
        required: true,
        unique: true
    },
    image:ImageSchema,
    newMeasure:MeasureSchema,
    firstMeasure:MeasureSchema,
    weightArray:[weightSchema],
    subscriptionType: {
        type: String,
        required:false,
        default:'standard'
    },
    isSubscribed:{
        type:Boolean,
        value: false,
        required:false
    },
    isPaid:{
        type:Boolean,
        value:false,
        required:false
    },
    isAdmin:{
        type:Boolean,
        value:true,
        required:false
    },
    isComplete:{
        type:Boolean,
        value:false,
        required:false
    },
    age:{
        type:Number,
        required:false
    },
    weight:{
        type:Number,
        required: false
    },
    heigth:{
        type:Number,
        required: false
    },
    gender:{
        type:String,
        required: false
    },
    goal:{
        type:String,
        required: false
    },
    subscriptionExpires:{
        type:Date
    },
    isVerified:{
        type:Boolean,
        default:false
    },
    verifyEmailToken: String,
    verifyEmailExpires: Date,
    resetPasswordToken: String,
    resetPasswordExpires: Date
})

userSchema.plugin(passportLocalMongoose);

module.exports = mongoose.model('User', userSchema);