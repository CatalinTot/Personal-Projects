const mongoose = require('mongoose');
const Campground = require('../models/campgrounds');
const cities = require('./cities');
const {
    places,
    descriptors
} = require('./seedHelpers');
mongoose.connect('mongodb://localhost:27017/YelpCamp', {
    useNewUrlParser: true,
    useCreateIndex: true,
    useUnifiedTopology: true
})
const db = mongoose.connection;
db.on('error', console.error.bind(console, 'connection error:'));
db.once('open', function () {
    console.log("Connected to DataBase!");
});

//return a random name from the seedHelper.js
const sample = array => array[Math.floor(Math.random() * array.length)];

const seedDB = async () => {
    await Campground.deleteMany({})
    for (let i = 0; i < 200; i++) {
        const random1000 = Math.floor(Math.random() * 1000);
        const price = Math.floor(Math.random() * 30) + 1;
        const camp = new Campground({
            author: '5fc0a1fa2c26751b7c875fd9',
            location: `${cities[random1000].city}, ${cities[random1000].state}`,
            title: `${sample(descriptors)} ${sample(places)}`,
            images: [{
                url: 'https://res.cloudinary.com/dzainyxbb/image/upload/v1606670851/YelpCamp/aykayewjwnttae7albas.jpg',
                filename: 'YelpCamp/aykayewjwnttae7albas'
            }],
            geometry: {
                type: 'Point',
                coordinates: [cities[random1000].longitude,cities[random1000].latitude]
            },
            description: 'Lorem, ipsum dolor sit amet consectetur adipisicing elit. Maxime nemo modi, repellendus illo facilis rerum ut magnam consequuntur laborum cum reiciendis perspiciatis et fugiat voluptate ab vero aliquid repudiandae expedita.',
            price: price
        })
        await camp.save();
    }
}

seedDB().then(() => {
    mongoose.connection.close(); //built in method for closing the DB
})