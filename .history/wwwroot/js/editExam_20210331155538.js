function checkLog() {
	var hostName = $(location).attr("host");
	var userName = sessionStorage.getItem("TeachersName");

	if (!userName) {
		window.location.replace("https://" + hostName + "/" + "index.html");
	}
}

$(document).ready(function () {
	checkLog();
	loadExamData();
	loadQuestionsData();
});
//https://localhost:44327/editExam.html?examId=1008&teacherId=3

function loadExamData() {
	var url = window.location.href;
	url = url.split("?");//["https://localhost:44327/editExam.html","examId=1008&teacherId=3"]

	url = url[1].split("&"); //["examId=1008","teacherId=3"]

	url = url[0].split("="); //["examId","1008"]
	var examId = url[1];

	$.ajax({
		url: "api/exams/" + examId,
		type: "GET",
		dataType: "json",
		success: function (exam) {
			appendExam(exam);
		},
		error: function (request, message, error) {
			// handleException(request, message, error);
		}
	});
}

function loadQuestionsData() {
	var url = window.location.href;
	url = url.split("?");//["https://localhost:44327/editExam.html","examId=1008&teacherId=3"]

	url = url[1].split("&"); //["examId=1008","teacherId=3"]

	url = url[0].split("="); //["examId","1008"]
	var examId = url[1];
	var id = url[1];

	$.ajax({
		url: "api/Questions/ByExam/" + examId,
		type: "GET",
		dataType: "json",
		success: function (questions) {
			appendQuestions(questions);
		},
		error: function (request, message, error) {
			// handleException(request, message, error);
		}
	});
}

function appendExam(exam) {
	if ($("#examDetail tbody").length == 0) {
		$("#examDetail tbody").append("<tbody></tbody>");
	}

	var h_exam =
		"<tr>" +
		'<th scope = "row" >1' +
		//'<button type="button"' +
		//    'onclick="examUpdate(this)" ' +
		//    'class="btn btn-default" ' +
		//    'data-id="' + exam.id + '">' +
		//    '<span class="glyphicon glyphicon-edit" />' +
		//'</button>' +
		"</th>" +
		"<td>" +
		'<div class="input-group input-group-lg">' +
		'<input id="examName"  value = "' +
		exam.title +
		'" type="text" class="form-control" aria-label="Large" aria-describedby="inputGroup-sizing-sm">' +
		"</div>" +
		"</td>" +
		"<td>" +
		' <div class="input-group input-group-lg">' +
		'<input id="examDate" type="text"  value = "' +
		exam.dateStarted +
		'" class="form-control" aria-label="Large" aria-describedby="inputGroup-sizing-sm">' +
		"</div>" +
		"</td>" +
		"<td>" +
		'<div class="input-group input-group-lg">' +
		'<input id="examDuration" type="text"  value = "' +
		exam.durationMinutes +
		'" class="form-control" aria-label="Large" aria-describedby="inputGroup-sizing-sm">' +
		"</div>" +
		"</td>" +
		"</tr>";

	$("#examDetail tbody").append(h_exam);
}

function appendQuestions(questions) {
	var h_question = "";

	if ($("#questionsDetail tbody").length == 0) {
		$("#questionsDeatil tbody").append("<tbody></tbody>");
	}

	$.each(questions, function (index, question) {
		var numRow = 1 + index;
		h_question =
			"<tr>" +
			'<th scope = "row" >' +
			numRow +
			//                <button type="button" onclick="examUpdate(this)" class="btn btn-primary btn-sm btn-block">Update Qustion</button >
			'<button type="button"' +
			'onclick="questionUpdate(this);" ' +
			'class="btn btn-primary btn-sm" ' +
			'data-id="' +
			question.id +
			'">' +
			'Update Qustion' +
			"</button>" +
			'<button type="button"' +
			'onclick="removeQuestion(this);" ' +
			'class="btn btn-default" ' +
			'data-id="' +
			question.id +
			'">' +
			// '<span class="glyphicon glyphicon-remove" />' +
			
			"</button>" +
			"</th >" +
			"<td>" +
			'<textarea id = "question' +
			question.id +
			'" class="form-control" id="exampleFormControlTextarea1" rows="3">' +
			question.question +
			"</textarea>" +
			"</td>" +
			"<td>" +
			'<textarea id = "choice1' +
			question.id +
			'" class="form-control" id="exampleFormControlTextarea1" rows="3">' +
			question.choicesList[0] +
			"</textarea>" +
			"</td>" +
			"<td>" +
			'<textarea id = "choice2' +
			question.id +
			'" class="form-control" id="exampleFormControlTextarea1" rows="3">' +
			question.choicesList[1] +
			"</textarea>" +
			"</td>" +
			"<td>" +
			'<textarea id = "choice3' +
			question.id +
			'" class="form-control" id="exampleFormControlTextarea1" rows="3">' +
			question.choicesList[2] +
			"</textarea>" +
			"</td>" +
			'<td><textarea id = "choice4' +
			question.id +
			'" class="form-control" id="exampleFormControlTextarea1" rows="3">' +
			question.choicesList[3] +
			"</textarea></td>" +
			"<td>" +
			'<textarea id = "answer' +
			question.id +
			'" class="form-control" id="exampleFormControlTextarea1" rows="3">' +
			question.correct +
			"</textarea>" +
			"</td>" +
			"<td>" +
			'<div class="input-group input-group-lg">' +
			'<input id = "points' +
			question.id +
			'" type="text"  value = "' +
			question.points +
			'" class="form-control input-lg" aria-label="Large" aria-describedby="inputGroup-sizing-sm">' +
			"</div>" +
			"</td>" +
			"</tr >";

		$("#questionsDetail tbody").append(h_question);
	});
}

function examUpdate(examB) {
	var url = window.location.href;
	url = url.split("?");//["https://localhost:44327/editExam.html","examId=1008&teacherId=3"]

	var urlSplitUnp = url[1].split("&"); //["examId=1008","teacherId=3"]

	//url = url[1].split("&"); //["examId=1008","teacherId=3"]

	var examIdUrl = urlSplitUnp[0].split("="); //["examId","1008"]
	var examId = examIdUrl[1];

	var teacherIdUrl = urlSplitUnp[1].split("="); //["examId","1008"]
	var teacherId = teacherIdUrl[1];

	//var id = $(examB).data("id");
	var examName = $("#examName").val();
	var examDate = $("#examDate").val();
	var examDuration = $("#examDuration").val();

	var ExamModel = {
		Id: examId,
		Title: examName,
		TeachrId: teacherId,
		DateStarted: examDate,
		DurationMinutes: examDuration
	};

	/*var ExamModel = new Object();
    ExamModel.Id = 1;
    ExamModel.Title = "";
    ExamModel.TeachrId = 1;
    ExamModel.DateStarted = "";
    ExamModel.DurationMinutes = "";*/

	/*var exam = new Object();
    exam.Id = 1;
    exam.Ttile = 'csd';
    exam.TeacherId = 1;
    exam.DateTime = new Date(2008, 5, 1, 8, 30, 52);
    exam.DurationMinutes = 5;*/

	$.ajax({
		type: "PUT",
		url: "api/exams/" + examId,
		data: JSON.stringify(ExamModel),
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		processData: true,
		success: function (data, status, jqXHR) {
			// alert("success..." + JSON.stringify(data));
			alert("Exam submited");
		},
		error: function (xhr) {
			alert(xhr.responseText);
		}
	});
}

function questionUpdate(questionB) {
	var url = window.location.href;
	url = url.split("?");//["https://localhost:44327/editExam.html","examId=1008&teacherId=3"]

	url = url[1].split("&"); //["examId=1008","teacherId=3"]

	url = url[0].split("="); //["examId","1008"]
	var examId = url[1];

	var id = $(questionB).data("id");
	var questionVal = $("#question" + id).val();
	var choice1Val = $("#choice1" + id).val();
	var choice2Val = $("#choice2" + id).val();
	var choice3Val = $("#choice3" + id).val();
	var choice4Val = $("#choice4" + id).val();
	var answerVal = $("#answer" + id).val();
	var pointsVal = $("#points" + id).val();

	var choicesVal =
		choice1Val + "," + choice2Val + "," + choice3Val + "," + choice4Val;
	var QuestionModel = {
		Id: id,
		Question: questionVal,
		Choices: choicesVal,
		Correct: answerVal,
		Points: pointsVal,
		ExamId: examId
	};

	$.ajax({
		type: "PUT",
		url: "api/Questions/" + id,
		data: JSON.stringify(QuestionModel),
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		processData: true,
		success: function (data, status, jqXHR) {
			// alert("success..." + JSON.stringify(data));
			alert("Question submited");
		},
		error: function (xhr) {
			alert(xhr.responseText);
		}
	});
}

function addQuestion(questionB) {
	var url = window.location.href;
	url = url.split("?");//["https://localhost:44327/editExam.html","examId=1008&teacherId=3"]

	url = url[1].split("&"); //["examId=1008","teacherId=3"]

	url = url[0].split("="); //["examId","1008"]
	var examId = url[1];

	var id = $(questionB).data("id");
	var questionVal = $("#question" + id).val();
	var choice1Val = $("#choice1" + id).val();
	var choice2Val = $("#choice2" + id).val();
	var choice3Val = $("#choice3" + id).val();
	var choice4Val = $("#choice4" + id).val();
	var answerVal = $("#answer" + id).val();
	var pointsVal = $("#points" + id).val();

	var choicesVal =
		choice1Val + "," + choice2Val + "," + choice3Val + "," + choice4Val;

	var QuestionModel = {
		Question: questionVal,
		Choices: choicesVal,
		Correct: answerVal,
		Points: pointsVal,
		ExamId: examId
	};

	$.ajax({
		type: "POST",
		url: "api/Questions",
		data: JSON.stringify(QuestionModel),
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		processData: true,
		success: function (data, status, jqXHR) {
			// alert("success..." + JSON.stringify(data));
			alert("Question added");
		},
		error: function (xhr) {
			alert(xhr.responseText);
		}
	});
}
//'<button type="button"' +
//	'onclick="questionUpdate(this);" ' +
//	'class="btn btn-primary btn-sm" ' +
//	'data-id="' +
//	question.id +
//	'">' +
//	'Update Qustion' +
//	"</button>" +
function appendQuestion() {
	// alert("scasc");
	var h_question =
		"<tr>" +
		'<th scope = "row" >' +
		
		'<button type="button"' +
		'onclick="addQuestion(this);" ' +
		'class="btn btn-primary btn-sm" ' +
		'data-id="' +
		"new" +
		'">' +
		'Update Qustion' +
		"</button>" +
		"   New Question" +
		"</th >" +
		"<td>" +
		'<textarea id = "question' +
		"new" +
		'" class="form-control" id="exampleFormControlTextarea1" rows="3">Deafult Question</textarea>' +
		"</td>" +
		"<td>" +
		'<textarea id = "choice1' +
		"new" +
		'" class="form-control" id="exampleFormControlTextarea1" rows="3">Deafult Choice 1</textarea>' +
		"</td>" +
		"<td>" +
		'<textarea id = "choice2' +
		"new" +
		'" class="form-control" id="exampleFormControlTextarea1" rows="3">Deafult Choice 2</textarea>' +
		"</td>" +
		"<td>" +
		'<textarea id = "choice3' +
		"new" +
		'" class="form-control" id="exampleFormControlTextarea1" rows="3">Deafult Choice 3</textarea>' +
		"</td>" +
		'<td><textarea id = "choice4' +
		"new" +
		'" class="form-control" id="exampleFormControlTextarea1" rows="3">Deafult Choice 4</textarea></td>' +
		"<td>" +
		'<textarea id = "answer' +
		"new" +
		'" class="form-control" id="exampleFormControlTextarea1" rows="3">Deafult Answer</textarea>' +
		"</td>" +
		"<td>" +
		'<div class="input-group input-group-lg">' +
		'<input id = "points' +
		"new" +
		'" type="text"  value = "0" class="form-control input-lg" aria-label="Large" aria-describedby="inputGroup-sizing-sm">' +
		"</div>" +
		"</td>" +
		"</tr >";

	$("#questionsDetail tbody").append(h_question);
}

function removeQuestion(removeB) {
	var questionIdId = $(removeB).data("id");

	$.ajax({
		url: "api/Questions/" + questionIdId,
		type: "DELETE",
		dataType: "text",
		success: function (msg) {
			alert(msg);
			$(removeB).parents("tr").remove(); //Delete
		},
		error: function (request, message, error) {
			// handleException(request, message, error);
		}
	});
}

function handleException(request, message, error) {
	var msg = "";
	msg += "Code: " + request.status + "\n";
	msg += "Text: " + request.statusText + "\n";
	if (request.responseJSON != null) {
		msg += "Message" + request.responseJSON.Message + "\n";
	}
	alert(msg);
}
