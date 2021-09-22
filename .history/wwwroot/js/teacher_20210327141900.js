function checkLog() {
	var hostName = $(location).attr("host");
	var userName = sessionStorage.getItem("TeachersName");

	if (!userName) {
		window.location.replace("https://" + hostName + "/" + "index.html");
	}
}

$(function () {
	$('[data-toggle="tooltip"]').tooltip();
});
$(document).ready(function () {
	checkLog();
	loadExamsData();
});
///---------------------------------------
let addExamBtn = document.getElementById("addExamBtn");
addExamBtn.addEventListener("click", (e) => {
	btn.innerHTML = "OK";
});
$("#addExamBtn").click(function () {
	// select by Class $(".mark")

	// td class="title-column2" style="display:none;">
	//                                 <input type="text" id="title" />
	//                             </td>
	//                             <td class="startDate-column3" style="display:none;">
	//                                 <input type="date" id="startDate" />
	//                             </td>
	//                             <td class="duration-column4" style="display:none;">
	//                                 <input type="number" id="duration" />
	//                             </td>

	//                             <td class="TeacherId-column5" style="display:none;">
	//                                 <input type="text" id="TeacherId" />
	//                             </td>
	//                             <td class="empty-column6" style="display:none;">

	//                             </td>
	//                             <td class="submit-column7" style="display:none;">
	//                                 <input type="submit" onclick="return fillTable();" />
	//                             </td>
	$(".title-column2").style.display = "block";
	$(".startDate-column3").style.display = "block";
	$(".duration-column4").style.display = "block";
	$(".TeacherId-column5").style.display = "block";
	$(".empty-column6").style.display = "block";
	$(".submit-column7").style.display = "block";

	// var QuestionModel = {
	//     Question: questionVal,
	//     Choices: choicesVal,
	//     Correct: answerVal,
	//     Points: pointsVal,
	//     ExamId: examId
	// }

	// $.ajax({
	//     type: "POST",
	//     url: "api/Questions",
	//     data: JSON.stringify(QuestionModel),
	//     contentType: "application/json; charset=utf-8",
	//     dataType: "json",
	//     processData: true,
	//     success: function (data, status, jqXHR) {
	//         alert("success..." + JSON.stringify(data));
	//     },
	//     error: function (xhr) {
	//         alert(xhr.responseText);
	//     }

	// });
});

function loadExamsData() {
	var url = window.location.href;
	var teacherId = url.split("?")[1];

	$.ajax({
		url: "api/exams/GetByTeacher/" + teacherId,
		type: "GET",
		dataType: "json",
		success: function (exams) {
			examsListing(exams);
		},
		error: function (request, message, error) {
			// handleException(request, message, error);
		}
	});
}

//------------------------------------------

function onAddExamBtnClick() {
	// select by Class $(".mark")

	// td class="title-column2" style="display:none;">
	//                                 <input type="text" id="title" />
	//                             </td>
	//                             <td class="startDate-column3" style="display:none;">
	//                                 <input type="date" id="startDate" />
	//                             </td>
	//                             <td class="duration-column4" style="display:none;">
	//                                 <input type="number" id="duration" />
	//                             </td>

	//                             <td class="TeacherId-column5" style="display:none;">
	//                                 <input type="text" id="TeacherId" />
	//                             </td>
	//                             <td class="empty-column6" style="display:none;">

	//                             </td>
	//                             <td class="submit-column7" style="display:none;">
	//                                 <input type="submit" onclick="return fillTable();" />
	//                             </td>
	$(".title-column2").style.display = "block";
	$(".startDate-column3").style.display = "block";
	$(".duration-column4").style.display = "block";
	$(".TeacherId-column5").style.display = "block";
	$(".empty-column6").style.display = "block";
	$(".submit-column7").style.display = "block";

	// var QuestionModel = {
	//     Question: questionVal,
	//     Choices: choicesVal,
	//     Correct: answerVal,
	//     Points: pointsVal,
	//     ExamId: examId
	// }

	// $.ajax({
	//     type: "POST",
	//     url: "api/Questions",
	//     data: JSON.stringify(QuestionModel),
	//     contentType: "application/json; charset=utf-8",
	//     dataType: "json",
	//     processData: true,
	//     success: function (data, status, jqXHR) {
	//         alert("success..." + JSON.stringify(data));
	//     },
	//     error: function (xhr) {
	//         alert(xhr.responseText);
	//     }

	// });
}

//------------------------------------------

function examsListing(exams) {
	$.each(exams, function (index, exam) {
		addExamRow(exam);
	});
}

//-------------------------------------------
function addExamRow(exam) {
	if ($("#examTable tbody").length == 0) {
		$("#examTable").append("<tbody></tbody>");
	}

	$("#examTable tbody").append(examBuildTableRow(exam));
}

//---------------------------------------------
function examBuildTableRow(exam) {
	var ret =
		"<tr>" +
		'<td class="column1">' +
		"<button type='button' " +
		"onclick='examGet(this);' " +
		"class='btn btn-outline-info' " +
		"data-id='" +
		exam.id +
		"'>" +
		"</button>" +
		"</td>" +
		'<td class="column2">' +
		exam.title +
		"</td>" +
		'<td class="column3">' +
		exam.dateStarted +
		"</td>" +
		'<td class="column4">' +
		exam.durationMinutes +
		"</td>" +
		'<td class="column5">' +
		exam.teachrId +
		"</td>" +
		'<td class="column6">' +
		"<button type='button' " +
		"onclick='examDelete(this);' " +
		"class='btn btn-outline-danger' " +
		"data-id='" +
		exam.id +
		"'>" +
		"</button>" +
		"</td>" +
		'<td class="column7">' +
		"<button type='button' " +
		"onclick='getGraph(this);' " +
		"class='btn btn-outline-primary' " +
		"data-id='" +
		exam.id +
		"'>" +
		"</button>" +
		"</td>" +
		"</tr>";

	return ret;
}
//--------------------------------------------------------------

function examGet(examEditButton) {
	var id = $(examEditButton).data("id");
	var hostName = $(location).attr("host");
	var url = "https://" + hostName + "/editExam.html?" + id;
	$(location).attr("href", url);
}

function getGraph(examGradesButton) {
	var id = $(examGradesButton).data("id");
	var hostName = $(location).attr("host");
	var url = "https://" + hostName + "/examGraph.html?" + id;
	$(location).attr("href", url);
}

function examDelete(ctl) {
	var id = $(ctl).data("id");

	$.ajax({
		url: "api/exams/" + id,
		type: "DELETE",
		dataType: "text",
		success: function (msg) {
			alert(msg);
			$(ctl).parents("tr").remove(); //Delete
		},
		error: function (request, message, error) {
			// handleException(request, message, error);
		}
	});
}

//CRUD UI
//- List Of Exams
//- EACH ROW WILL HAVE 2 Buttons DELETE/UPDATE
//- Set Add Button
//-  FORM FOR EACH DATA
//-  BUILD JSON FIELDS AND SEND TO SERVER
//-----------

//ComboBOX
//- LIST OF EXAMS
//- CHOOSE EXAM ITEM
//- SHOW ALL QUESTIONS OF THIS EXAM
