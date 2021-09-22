/*$(document).ready(function () {

    alert("Question");

});

*/


class Question {

    constructor(id, qText, choices, examId, answer, points) {
        this.id = id;
        this.text = qText;
        this.choices = choices;
        this.examId = examId;
        this.answer = answer;
        this.points = points;
        
        this.isAnswered = false;
    }

    getId() {
        return this.id;
    }

    getQuestion() {
        return this.text;
    }

    getChoices() {
        return this.choices;
    }

    getExanId() {
        return this.examId;
    }

    getAnswer() {
        return this.answer;
    }
    getAnswerIdx() {
        return this.choices.indexOf(this.answer);
    }

    getPoints() {
        return this.points;
    }

    isCorrectAnswer(userChoice) {
        return this.answer === userChoice;
    }
    questionAnswered() {
        this.isAnswered = true;
    }
    isQuestionAnswered() {
        return this.isAnswered === true;
    }
   

}