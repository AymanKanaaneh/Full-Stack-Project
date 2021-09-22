window.onload = function () {

	var gradeList = getGrades();
	var data = getPureGrades(gradeList);
	var average = getAverage(data);
	alert("AVG is: " + average);
	var sd = getStandardDeviation(average, data).toFixed(2);
	alert("Standard Deviation is: " + sd);

	/*var dataForExample = [
		{ y: 450 },
		{ y: 414 },
		{ y: 520, indexLabel: "\u2191 highest", markerColor: "red", markerType: "triangle" },
		{ y: 460 },
		{ y: 450 },
		{ y: 500 },
		{ y: 480 },
		{ y: 480 },
		{ y: 410, indexLabel: "\u2193 lowest", markerColor: "DarkSlateGrey", markerType: "cross" },
		{ y: 500 },
		{ y: 480 },
		{ y: 510 }
	]*/


var chart = new CanvasJS.Chart("chartContainer", {
	animationEnabled: true,
	theme: "dark2",
	title:{
		text: "Scores Report"
	},
	data: [{        
		type: "line",
      	indexLabelFontSize: 16,
		dataPoints: data
	}]
});
	chart.render();
	var avgStr = '<h3 id="avg-p">The Average is: ' + average +'</h3>'
	$("#avg-div").append(avgStr);

	var sdStr = '<h3 id="sd-p">The Standard Deviation is: ' + sd + '</h3>'
	$("#sd-div").append(sdStr);


}

function getStandardDeviation(avg, gradeList) {
	var res = 0;
	var x = avg;
	var sum = 0;
	$.each(gradeList, function (index, grade) {

		$.each(grade, function (key, c) {
			var dist = c - x;
			var distPow = dist ** 2;
			sum = sum + distPow;
		});

	});
	sum = sum / gradeList.length;
	res = Math.sqrt(sum);
	return res;

}

function getGrades() {

	var examId = ((window.location.href).split('?'))[1];
	var grades;

	$.ajax({

		url: 'api/Grades/GetByExamId/' + examId,
		type: 'GET',
		dataType: 'json',
		async: false,
		success: function (Grades) {
			grades = Grades;
		},
		error: function (request, message, error) {

			// handleException(request, message, error);
		}
	});

	return grades;
}

function getPureGrades(gradeList) {

	let data = [];

	$.each(gradeList, function (index, grade) {

		$.each(grade, function (key, value) {

			if (key == 'score') {
				data.push({y:value})
			}


		});

	});

	return data;
}

function getAverage(gradeList) {

	var sum = 0;

	$.each(gradeList, function (index, grade) {

		$.each(grade, function (key, value) {
			sum = sum + value;
		});

	});

	return (sum / gradeList.length);
}