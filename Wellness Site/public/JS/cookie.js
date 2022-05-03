const parsedVisitor = JSON.parse(visitor);
document.addEventListener('DOMContentLoaded', function () {
    cookieconsent.run({
        "notice_banner_type": "interstitial",
        "consent_type": "express",
        "palette": "light",
        "language": "en"
    });
    document.querySelector('.cc_dialog.interstitial').style.setProperty('top', 'initial')
    document.querySelector('.cc_dialog.interstitial').style.bottom = '3vh';
});

window.onresize = function(){location.reload();}

google.charts.load('current', {
    'packages': ['bar']
});
if(parsedVisitor)
    google.charts.setOnLoadCallback(drawChartVisitors);

function drawChartVisitors() {
    let visitorArray = [];
    parsedVisitor.userCount.forEach((visitor, i) => {
        let newVisitor = [visitor.date.toString(), visitor.count];
        
            if (visitorArray.length > 0 && visitor.date == visitorArray[visitorArray.length - 1][0]) {
                console.log(visitor.date, visitorArray[visitorArray.length - 1][0]);
                visitorArray.splice(visitorArray.length-1, 1, newVisitor);
            }
            else{
            visitorArray.push(newVisitor);
        }
        
        

    })
    visitorArray.unshift(['Date','Visitor']);
    var data = google.visualization.arrayToDataTable(visitorArray);

    var options = {
        chart: {
            title: 'Registered users',
            // subtitle: `From ${visitorArray[1][0]} to ${visitorArray[visitorArray.length-1][0]}`,
        },
        bars: 'vertical',
        vAxis: {
            format: 'decimal'
        },
        height: 400,
        colors: ['#7570b3']
    };

    var chart = new google.charts.Bar(document.getElementById('chart-div-visitors'));

    chart.draw(data, google.charts.Bar.convertOptions(options));
}