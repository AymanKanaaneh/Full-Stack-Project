function checkLog() {

    var hostName = $(location).attr('host');
    var userName = sessionStorage.getItem('StudentsName');

    if (!userName) {
        window.location.replace("https://" + hostName + "/" + 'index.html');
    }

}

$(document).ready(function () {
    checkLog();

    loadExamsData();

});
///---------------------------------------

function loadExamsData() {


    $.ajax({
        url: 'api/exams/',
        type: 'GET',
        dataType: 'json',
        success: function (exams) {
            examsListing(exams);
        },
        error: function (request, message, error) {
            // handleException(request, message, error);
        }
    })
}

//------------------------------------------

function examsListing(exams) {
    $.each(exams, function (index, exam) {

        addExamRow(index,exam)
    });
}


//-------------------------------------------
function addExamRow(index,exam) {


    if ($("#examTable tbody").length == 0) {
        $("#examTable").append("<tbody></tbody>");
    }

    $("#examTable tbody").append(
        examBuildTableRow(index,exam));


}

//---------------------------------------------
function examBuildTableRow(index, exam) {

    var strnigButton;

    if (executedExam(exam.id) == exam.id) {
        strnigButton = '<button  onclick="executeExam(this)" data-id="' + exam.id + '" type="button" class="btn btn-outline-secondary ml-auto ml-sm-5">Start Exam</button>';
    } else {
        strnigButton = '<button onclick="showGrade(this)" type="button" data-id="' + exam.id + '" class="btn btn-outline-secondary ml-auto ml-sm-5 ">Show Grade</button>';
    }

    var ret = '<tr>'+
                    '<td class="column1" >'+index+'</td >'+
                    '<td class="column2">'+exam.title+'</td>'+
                    '<td class="column3">'+exam.id+'</td>'+
                    '<td class="column4">'+exam.dateStarted+'</td>'+
                    '<td class="column5">'+exam.durationMinutes+'</td>'+
                    '<td>'+strnigButton+'</td>'+
             '</tr>';

    return ret;

}

function executedExam(examId) {

    var studentId = ((window.location.href).split('?'))[1];
    var ret = examId;

    $.ajax({

        url: 'api/grades/' + studentId + '/' + examId,
        type: 'GET',
        dataType: 'json',
        async: false,
        success: function (info) {
            ret = info;
        },
        error: function (request, message, error) {

            // handleException(request, message, error);
        }
    })

    return ret;
}

async function showGrade(executeB) {

    var examId = String($(executeB).data("id"));
    //api/grades/2/7'
    var studentId = ((window.location.href).split('?'))[1];
    var ret;
    var url = 'api/grades/' + studentId + '/' + examId;


    try {
        ret = await getData(url);
    } catch (error) {
        console.error(error);
    }

 
    if (ret.score != undefined) {
        var str2Show = "Your grade in this exam is: " + ret.score;
        alert(str2Show);
    }




    return ret;
}

function executeExam(executeB) {

    var studentId = ((window.location.href).split('?'))[1];
    var examId = $(executeB).data("id");
    var hostName = $(location).attr('host');
    var url = "https://" + hostName + "/executeExam.html?" + examId + '?' + studentId;
    $(location).attr('href', url);
    //window.location.replace("https://" + hostName + "/executeExam.html?" + examId);
}


function handleException(request, message, error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message: " + request.responseJSON.Message + "\n";
    }//api/grades/2/7'
    alert(msg);
}




async function getData(url = "") {
	// Default options are marked with *
	const response = await fetch(url, {
		method: "GET", // *GET, POST, PUT, DELETE, etc.
		mode: "cors", // no-cors, *cors, same-origin
		cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
		credentials: "same-origin", // include, *same-origin, omit
		headers: {
		"Content-Type": "application/json",
		// 'Content-Type': 'application/x-www-form-urlencoded',
	},
	redirect: "follow", // manual, *follow, error
	referrer: "no-referrer", // no-referrer, *client
});
    return await response.json(); // parses JSON response into native JS objects
}