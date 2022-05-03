const cloudinary = require('cloudinary').v2;
const {CloudinaryStorage} = require('multer-storage-cloudinary');

cloudinary.config({
    cloud_name: process.env.CLOUDINARY_CLOUD_NAME,
    api_key: process.env.CLOUDINARY_KEY,
    api_secret: process.env.CLOUDINARY_SECRET
})

const storage = new CloudinaryStorage({
    cloudinary,
    params:{
        folder: async(req,res)=>{
            if (req.route.path.indexOf('profile') != -1)
                return 'Wellness/Avatar';
            return 'Wellness/Results';
        },
        allowedformats:['jpeg','png','jpg']
    }
})

module.exports = {storage,cloudinary}