
/**
 * Module dependencies.
 */

var express = require('express')
  , routes = require('./routes')
  , moment = require('moment');

var app = module.exports = express();

// Configuration

app.configure(function ()
{
    app.set('views', __dirname + '/views');
    app.set('view engine', 'jade');
    app.use(express.bodyParser());
    app.use(express.cookieParser('piOfTheTiger@pass'));
    app.use(express.methodOverride());
    app.use(app.router);
    app.use(express.static(__dirname + '/public'));
});

app.configure('development', function(){
  app.use(express.errorHandler({ dumpExceptions: true, showStack: true }));
});

app.configure('production', function(){
  app.use(express.errorHandler());
});

//global

app.locals.appSettings = { date: new moment().calendar() };

global.applicationTile= "PiOfTheTiger";

// Routes

app.get('/', routes.index);
app.get('/Login', routes.login);


app.listen(process.env.port || 3000);
//console.log("Express server listening on port %d in %s mode", app.address().port, app.settings.env);


function checkAuth(req, res, next) {
  if (!req.session.user_id) {
    res.send('You are not authorized to view this page');
  } else {
    next();
  }
}

app.get('/logout', function (req, res) {
  delete req.session.user_id;
  res.redirect('/login');
});


app.post('/login', checkAuth, function (req, res)
{

});