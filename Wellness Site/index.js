if (process.env.NODE_ENV !== "production") {
    require('dotenv').config();
}

const express = require('express');
const app = express();
const path = require('path');
const ejsMate = require('ejs-mate');
const mongoose = require('mongoose');
const passport = require('passport');
const User = require('./models/user');
const PORT = 3000 || process.env.PORT;
const flash = require('connect-flash');
const session = require('express-session');
const userRoutes = require('./routes/user');
const contactRoutes = require('./routes/contact');
const resultsRoutes = require('./routes/results');
const loginRoutes = require('./routes/login');
const LocalStrategy = require('passport-local');
const expressError = require('./util/expressError');
const registerRoutes = require('./routes/register');
const server = require('http').createServer(app);
const io = require('socket.io')(server);
const methodOverride = require('method-override');
const Visitor = require('./models/visitors');


mongoose.connect('mongodb://localhost:27017/Wellness', {
        useNewUrlParser: true,
        useUnifiedTopology: true,
        useFindAndModify: false,
        useCreateIndex: true
    })
    .then(() => {
        console.log("Successfully connected to DB!");
    })
    .catch((e) => {
        console.log(e);
    })

const sessionConfig = {
    name: 'session',
    secret: 'secret',
    resave: false,
    saveUninitialized: true,
    cookie: {
        httpOnly: true,
        expires: Date.now() + 1000 * 60 * 60 * 24 * 7,
        maxAge: 1000 * 60 * 60 * 24 * 7
    }
}


app.engine('ejs', ejsMate);
app.set('view engine', 'ejs');
app.use(session(sessionConfig));
app.use(methodOverride('_method'));
app.use(flash());
app.set('views', path.join(__dirname, 'views'));
app.use(express.urlencoded({extended: true}));
app.use(express.static(path.join(__dirname, 'public')));

app.use(passport.initialize());
app.use(passport.session());
passport.use(new LocalStrategy(User.authenticate()));
passport.deserializeUser(User.deserializeUser());
passport.serializeUser(User.serializeUser());

app.use(async (req, res, next) => {
    res.locals.currentUser = req.user;
    res.locals.success = req.flash('success');
    res.locals.error = req.flash('error');
    res.locals.info = req.flash('info');
    
    next();
})

app.use('/register',registerRoutes);
app.use('/login',loginRoutes);
app.use('/user',userRoutes);
app.use('/results',resultsRoutes);
app.use('/contact',contactRoutes);

app.get('/', (req, res) => {
    res.render('home');
})

app.get('/about', async (req, res) => {
    const visitor = await Visitor.findOne({});
    res.render('about',{title:'About',visitor});
    
})

// next routes go here



app.get('/validation',(req,res)=>{
    res.render('validation',{title:'validation'});
})

app.get('/logout', (req, res) => {
    req.logout();
    req.flash('success', 'You were logged out!');
    return res.redirect('/about');
})

io.on('connection', (socket) => {
    // when the client emits 'new message', this listens and executes
    socket.on('new message', (data) => {
        // we tell the client to execute 'new message'
        socket.broadcast.emit('new message', {
            username: socket.username,
            message: data
        });
    });

    // when the client emits 'typing', we broadcast it to others
    socket.on('typing', () => {
        socket.broadcast.emit('typing', {
            username: socket.username
        });
    });

    // when the client emits 'stop typing', we broadcast it to others
    socket.on('stop typing', () => {
        socket.broadcast.emit('stop typing', {
            username: socket.username
        });
    });

});

app.all('*', (req, res, next) => {
    next(new expressError('Page Not Found', 404))
})

app.use((err, req, res, next) => {
    const {
        statusCode = 500
    } = err;
    if (!err.message) err.message = 'Oh No, Something Went Wrong!'
    res.status(statusCode).render('error', {
        err,
        title: 'Error'
    });
})

server.listen(PORT, () => {
    console.log('Server listening at port %d', 3000);
});



