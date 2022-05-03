const express = require('express');
const router = express.Router();
const User = require('../models/user');
const catchAsync = require('../util/catchAsync');
const stripe = require("stripe")(process.env.STRIPE_SECRET_KEY);
const {isLoggedIn,hasPaidSubscribtion,completeProfile, passedDetails} = require('../middleware');
const multer = require('multer');
const {storage} = require('../cloudinary');
const upload = multer({storage});
const userController = require('../controllers/user');


// render the subscription plan page
router.get('/plan', isLoggedIn, userController.subPlan)

// render the gift page
router.get('/gift',isLoggedIn,completeProfile, userController.giftPage);

// render the details page
router.get('/details',isLoggedIn,passedDetails, userController.detailsPage);

// after the user passes the details, update, and set isComplete to true
router.post('/details',isLoggedIn,catchAsync(userController.updateDetails));

// render the user profile page
router.get('/profile', isLoggedIn, completeProfile, userController.renderProgilePage);

// upload the user profile image
router.post('/profile', isLoggedIn, upload.single('image') ,catchAsync(userController.uploadProfileImage));

// render the edit page for the user
router.get('/profile/edit',isLoggedIn,completeProfile, userController.renderEditPage)

// send a put request to update the user profile details
router.put('/profile', isLoggedIn, upload.single('image') ,catchAsync(userController.updateUserProfile))

// delete request for the user profile
router.delete('/profile',isLoggedIn,catchAsync(userController.deleteProfile));

// get the user progress page
router.get('/progress', completeProfile, isLoggedIn, userController.getProgressPage);

// add new measurements to the user profile
router.post('/progress', isLoggedIn, userController.editMeasurements);

// Edit user wiegths array
router.put('/progress/:id', isLoggedIn, userController.editWeight);

// delete user weigths value
router.delete('/progress/:id', isLoggedIn, userController.deleteWeight);

router.use(
    express.json({
        // We need the raw body to verify webhook signatures.
        // Let's compute it only when hitting the Stripe webhook endpoint.
        verify: function (req, res, buf) {
            if (req.originalUrl.startsWith("/webhook")) {
                req.rawBody = buf.toString();
            }
        },
    })
);

// Fetch the Checkout Session to display the JSON result on the success page
router.get("/checkout-session", catchAsync(async (req, res) => {
    const {
        sessionId
    } = req.query;
    const session = await stripe.checkout.sessions.retrieve(sessionId);
    res.send(session);
}));

router.post("/create-checkout-session", hasPaidSubscribtion, catchAsync(async (req, res) => {
    const domainURL = process.env.DOMAIN;
    const {
        priceId
    } = req.body;

    try {
        const session = await stripe.checkout.sessions.create({
            mode: "subscription",
            payment_method_types: ["card"],
            line_items: [{
                price: priceId,
                quantity: 1,
            }, ],
            // ?session_id={CHECKOUT_SESSION_ID} means the redirect will have the session ID set as a query param
            success_url: `${domainURL}/user/success?session_id={CHECKOUT_SESSION_ID}`,
            cancel_url: `${domainURL}/about`,
        });
        
        
        res.send({
            sessionId: session.id,
        });
    } catch (e) {
        res.status(400);
        return res.send({
            error: {
                message: e.message,
            }
        });
    }
}));

router.get('/success', async(req,res)=>{
    try{
        const foundUser = await User.findByIdAndUpdate(req.user._id, {
            subscriptionType: 'premium',
            subscriptionExpires: Date.now() + 1000 * 60 * 60 * 24 * 30,
            isPaid: true,
            isSubscribed: true
        });
        await foundUser.save();
        req.flash('success', 'Your data is up to date')
        res.redirect('/about');
    }catch(e){
        req.flash('error',e);
        res.redirect('/about');
    }
    
})

router.get("/setup", hasPaidSubscribtion, (req, res) => {
    res.send({
        publishableKey: process.env.STRIPE_PUBLISHABLE_KEY,
        proPrice: process.env.PRO_PRICE_ID
    });
});

router.post('/customer-portal', catchAsync(async (req, res) => {
    // For demonstration purposes, we're using the Checkout session to retrieve the customer ID. 
    // Typically this is stored alongside the authenticated user in your database.
    const {
        sessionId
    } = req.body;
    const checkoutsession = await stripe.checkout.sessions.retrieve(sessionId);

    // This is the url to which the customer will be redirected when they are done
    // managing their billing with the portal.
    const returnUrl = process.env.DOMAIN;

    const portalsession = await stripe.billingPortal.sessions.create({
        customer: checkoutsession.customer,
        return_url: returnUrl,
    });

    res.send({
        url: portalsession.url,
    });
}));

// Webhook handler for asynchronous events.
router.post("/webhook", catchAsync(async (req, res) => {
    let eventType;
    // Check if webhook signing is configured.
    if (process.env.STRIPE_WEBHOOK_SECRET) {
        // Retrieve the event by verifying the signature using the raw body and secret.
        let event;
        let signature = req.headers["stripe-signature"];

        try {
            event = stripe.webhooks.constructEvent(
                req.rawBody,
                signature,
                process.env.STRIPE_WEBHOOK_SECRET
            );
        } catch (err) {
            console.log(`⚠️  Webhook signature verification failed.`);
            return res.sendStatus(400);
        }
        // Extract the object from the event.
        data = event.data;
        eventType = event.type;
    } else {
        // Webhook signing is recommended, but if the secret is not configured in `config.js`,
        // retrieve the event data directly from the request body.
        data = req.body.data;
        eventType = req.body.type;
    }

    if (eventType === "checkout.session.completed") {
        console.log(`🔔  Payment received!`);
        
        req.flash('success', 'Your data is up to date!');
    }

    res.sendStatus(200);
}));

module.exports = router;