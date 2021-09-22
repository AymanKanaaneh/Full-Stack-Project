let questions = [];
let score = 0;
let curExamIdx;
let curExam;
let curQuestionObj;
let curQuestionIdx;
let questionsAnsweredCnt = 0;
let studentAnswers = [];

function checkLog() {

    var hostName = $(location).attr('host');
    var userName = sessionStorage.getItem('StudentsName');

    if (!userName) {
        window.location.replace("https://" + hostName + "/" + 'index.html');
    }

}

$(document).ready(function () {


    checkLog();

    var url = window.location.href;
    url = url.split('?');
    curExamIdx = url[1];

    $.ajax({
        url: 'api/Questions/ByExam/' + curExamIdx,
        type: 'GET',
        dataType: 'json',
        success: function (jsonQuestions) {

            objectQuestions(jsonQuestions);
            objectExam(curExamIdx);
            //startExam();
        }
    });

});
function objectExam(examId) {
    $.ajax({
        url: 'api/Exams/' + curExamIdx,
        type: 'GET',
        dataType: 'json',
        success: function (jsonExam) {
            var dur = jsonExam.durationMinutes;
            //curExam.setDuration(dur);
            startExam(dur);
        }
    });

}

function objectQuestions(jsonQuestions) {

    var url = window.location.href;
    url = url.split('?');
    var examId = url[1];

    jsonQuestions.forEach(jsonQuestion => {

        let q = new Question(   

            jsonQuestion.id,
            jsonQuestion.question,
            jsonQuestion.choicesList,
            jsonQuestion.examId,
            jsonQuestion.correct,
            jsonQuestion.points

        );

        questions.push(q);

    })

    questions = shuffle(questions);
    curExam = new Exam(curExamIdx, questions);
    

}

function shuffle(array) {
    var currentIndex = array.length,
        temporaryValue, randomIndex;


    // While there remain elements to shuffle...
    while (0 !== currentIndex) {

        // Pick a remaining element...
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        // And swap it with the current element.
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
    }

    return array;
}

function startTimer(duration, display) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10)
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            timer = duration;
        }
    }, 1000);
}

function startExam(dur) {

    var your_label;
    var text_to_change;
    $("input:radio").prop("checked", false);


    var durationMinutes = dur * 60,
        display = document.querySelector('#time');
    startTimer(durationMinutes, display);
    $('#h4ExamId').text('Exam ID: ' + curExamIdx);
    $('#bar').text('(' + (curExam.getQuestionsIndex()+1) + ' of ' + questions.length + ')');
    $('#question b').text(curExam.getQuestionsByIndex().getQuestion());

    //option1
    your_label = document.getElementById('option1');
    text_to_change = your_label.childNodes[0];
    text_to_change.nodeValue = curExam.getQuestionsByIndex().getChoices()[0];
    $("#option1 input").val(curExam.getQuestionsByIndex().getChoices()[0]);


    //option2
    your_label = document.getElementById('option2');
    text_to_change = your_label.childNodes[0];
    text_to_change.nodeValue = curExam.getQuestionsByIndex().getChoices()[1];
    $("#option2 input").val(curExam.getQuestionsByIndex().getChoices()[1]);

    //option3
    your_label = document.getElementById('option3');
    text_to_change = your_label.childNodes[0];
    text_to_change.nodeValue = curExam.getQuestionsByIndex().getChoices()[2];
    $("#option3 input").val(curExam.getQuestionsByIndex().getChoices()[2]);

    //option4
    your_label = document.getElementById('option4');
    text_to_change = your_label.childNodes[0];
    text_to_change.nodeValue = curExam.getQuestionsByIndex().getChoices()[3];
    $("#option4 input").val(curExam.getQuestionsByIndex().getChoices()[3]);

    $('input:radio[name="radio"][value='+studentAnswers[curExam.getQuestionsIndex()]+']').prop('checked', true);
    //$('input:radio[value="'+studentAnswer[curExam.getQuestionsIndex()]+'"]').attr('checked', true);
    //$('input:radio[value="'+studentAnswer[curExam.getQuestionsIndex()]+'"]').val("ascsa");
    //$('input:radio[name=radio][value=' + studentAnswer[curExam.getQuestionsIndex()]+']').click();
    //$('input:radio[name=radio][value=' + studentAnswer[curExam.getQuestionsIndex()]+']').val("asc");
}


function nextQuestion() {

    if (!atLeastOneRadio()) {
        alert("Youd Didn't choose any answer!");
        return;
    }

    if (curExam.isFinish()) {
        let choiceVal = $("input[name='radio']:checked").val();
        studentAnswers[curExam.getQuestionsIndex()] = choiceVal;
        var d = document.getElementById("submitD");
        d.style.display = "block";
        alert("There is no more questions!");
        startExam();
    } else {

        let choiceVal = $("input[name='radio']:checked").val();
        studentAnswers[curExam.getQuestionsIndex()] = choiceVal;
        questionsAnsweredCnt++;
        curExam.guessChoice(choiceVal);
        curExam.goForward();
        startExam();
    }
}

function showGrade() {

    var score = curExam.getScore();

    alert(`Your score is a ${score} from ${questions.length} questions`);
    return;
}

function prevQuestion() {

    if (curExam.getQuestionsIndex() > 0) {
        questionsAnsweredCnt--;
        curExam.goPrev();
        startExam();
    } else {

        alert("This is the first question");

    }
}

function submitQuestion() {

    //TODO: fix finish btn show after clicked 4 time on submit
    let choiceVal = $("input[name='choises']:checked").val();
    questionsAnsweredCnt++;
    curExam.guessChoise(choiceVal);
    if (questionsAnsweredCnt === curQuestionObj.choises.length - 1) {
        let txt = `<br/>` + `<hr/>`;
        txt += `<button onclick="nextQuestion();">Finish test</button>`;
        $("#testBtn").append(txt);
    }
}

function atLeastOneRadio() {
    return $("input[type=radio]:checked").length > 0;
}

function examSubmit() {

    var studentId = (window.location.href).split('?')[2];
    // alert(studentId);

    var GradeModel = {

        StudentId: studentId,
        ExamId: curExamIdx,
        Score: curExam.getScore()

    }

    $.ajax({
        type: "POST",
        url: "api/Grades",
        data: JSON.stringify(GradeModel),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true,
        success: function (data, status, jqXHR) {
            // alert("success..." + JSON.stringify(data));
            alert("Exam submited");

            var hostName = $(location).attr('host');
            var url = "https://" + hostName + "/studentPage.html" + '?' + studentId;
            $(location).attr('href', url);
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }

    });

   
}