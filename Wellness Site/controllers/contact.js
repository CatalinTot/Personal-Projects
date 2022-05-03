const nodemailer = require('nodemailer');

module.exports.renderContactForm = (req, res) => {
    res.render('contact', {
        title: 'Contact'
    });
}

module.exports.sendMessage = async (req, res) => {
    try {
        const {
            name,
            surname,
            email,
            message
        } = req.body;
        await sendContactForm(name, surname, email, message);
        req.flash('info', 'The message has been sent!');
        res.redirect('/contact/sent');
    } catch (e) {
        req.flash('error', e);
        res.redirect('/contact');
    }

}

module.exports.renderSentPage = (req, res) => {
    res.render('sent', {
        title: 'Sent'
    });
}

async function sendContactForm(name, surname, email, message) {
    const smtpTransport = nodemailer.createTransport({
        service: 'Outlook',
        auth: {
            user: 'tot.vi.catalin@utcluj.didatec.ro',
            pass: process.env.OUTLOOKPASS
        }
    });
    const mailOptions = {
        to: 'tot.catalin98@gmail.com',
        from: 'tot.vi.catalin@utcluj.didatec.ro',
        subject: 'New message from Cat24Fit',
        text: `${name + surname}`,
        html: `<p>${email} says:</p>\n\n` +
            `${message}\n\n`
    };
    smtpTransport.sendMail(mailOptions, function (err, info) {
        if (err) {
            console.log(err);
        }
        // console.log(info);
    });
}

