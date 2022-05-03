window.onload = () => {
    let user;
    if (window.location.href == "http://localhost:3000/user/progress") {
        user = JSON.parse(currentUser);
    }


    google.charts.load('current', {
        'packages': ['bar']
    });
    google.charts.setOnLoadCallback(drawChartMeasure);

    window.onresize = function () { location.reload(); }

    const scrollableTable = document.querySelector('.scrollable-table');

    if (user.weightArray.length > 10) {
        scrollableTable.style.overflowY = 'scroll';
    }

    function drawChartMeasure() {
        let firstDate, lastDate;
        if (user.firstMeasure === undefined) {
            var data = google.visualization.arrayToDataTable([
                ['Area', '', ''],
                ['Neck', 0, 0],
                ['Chin', 0, 0],
                ['Arm', 0, 0],
                ['Bust', 0, 0],
                ['Waist', 0, 0],
                ['Stomach', 0, 0],
                ['Hip', 0, 0],
                ['Thighs', 0, 0],
                ['Knee', 0, 0],
            ]);
            firstDate = '-';
            lastDate = '-'
        } else if (user.newMeasure === undefined) {
            var data = google.visualization.arrayToDataTable([
                ['Area', '', ''],
                ['Neck', `${user.firstMeasure.neck}`, 0],
                ['Chin', `${user.firstMeasure.chin}`, 0],
                ['Arm', `${user.firstMeasure.arm}`, 0],
                ['Bust', `${user.firstMeasure.bust}`, 0],
                ['Waist', `${user.firstMeasure.waist}`, 0],
                ['Stomach', `${user.firstMeasure.stomach}`, 0],
                ['Hip', `${user.firstMeasure.hip}`, 0],
                ['Thighs', `${user.firstMeasure.thighs}`, 0],
                ['Knee', `${user.firstMeasure.knee}`, 0],
            ]);
            firstDate = user.firstMeasure.date;
            lastDate = '-'
        } else {
            var data = google.visualization.arrayToDataTable([
                ['Area', '', ''],
                ['Neck', `${user.firstMeasure.neck}`, `${user.newMeasure.neck}`],
                ['Chin', `${user.firstMeasure.chin}`, `${user.newMeasure.chin}`],
                ['Arm', `${user.firstMeasure.arm}`, `${user.newMeasure.arm}`],
                ['Bust', `${user.firstMeasure.bust}`, `${user.newMeasure.bust}`],
                ['Waist', `${user.firstMeasure.waist}`, `${user.newMeasure.waist}`],
                ['Stomach', `${user.firstMeasure.stomach}`, `${user.newMeasure.stomach}`],
                ['Hip', `${user.firstMeasure.hip}`, `${user.newMeasure.hip}`],
                ['Thighs', `${user.firstMeasure.thighs}`, `${user.newMeasure.thighs}`],
                ['Knee', `${user.firstMeasure.knee}`, `${user.newMeasure.knee}`],
            ]);
            firstDate = user.firstMeasure.date;
            lastDate = user.newMeasure.date;
        }

        var options = {
            chart: {
                title: 'Your progress',
                subtitle: `From ${firstDate} to ${lastDate}`,
            },
            bars: 'vertical',
            vAxis: {
                format: 'decimal'
            },
            height: 400,
            colors: ['#1b9e77', '#d95f02'],
            legend: { position: 'top', alignment: 'start' },
        };

        options.legend = 'end';

        var chart = new google.charts.Bar(document.getElementById('chart_div_measures'));

        chart.draw(data, google.charts.Bar.convertOptions(options));


    }

    google.charts.load('current', {
        'packages': ['corechart']
    });
    google.charts.setOnLoadCallback(drawChartWeight);

    function drawChartWeight() {
        let newArray = [['Year', 'Weight']];
        user.weightArray.forEach((value, index) => {
            let newData = [value.date, value.weight];
            newArray.push(newData);
        })
        var data = google.visualization.arrayToDataTable(newArray);

        var options = {
            title: 'Weight fluctuation',
            curveType: 'function',
            legend: {
                position: 'bottom'
            },
            hAxix: {
                title: 'Date'
            },
            vAxix: {
                title: 'Weight'
            }
        };

        var chart = new google.visualization.LineChart(document.getElementById('chart_div_weight'));

        chart.draw(data, options);
    }

    const editWeightModal = document.getElementById('editWeight');
    const editWeightButtons = document.querySelectorAll('.fa-pen-square');
    const editWeightForm = document.querySelector('#editWeight form');

    editWeightButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            let weightId = btn.parentElement.parentElement.parentElement.parentElement.id;
            editWeightForm.action = `/user/progress/${weightId}?_method=PUT`;
            console.log(btn.parentElement.parentElement.parentElement.parentElement.firstElementChild.nextElementSibling.innerHTML)
            editWeightForm.firstElementChild.firstElementChild.nextElementSibling.firstElementChild.value = parseInt(btn.parentElement.parentElement.parentElement.parentElement.firstElementChild.nextElementSibling.innerHTML);
        })
    })
}