class Exam {

    constructor(id, questions) {
        this.questions = questions;
        this.id = id;
        this.score = 0;
        this.questionIndex = 0;
        this.duration = 0;
    }

    getId() {
        return this.id;
    }

    getDuration() {
        return this.duration;
    }

    setDuration(minuts) {
        this.duration = minuts;
    }

    fromJson(jsonObj) {}

    toJson() {}

    //--Get Question By Current questionIndex--
    getQuestionsByIndex() {
        return this.questions[this.questionIndex];
    }

    getQuestionsIndex() {
        return this.questionIndex;
    }

    //If Answer Correct Add 1 Score
    guessChoice(answer) {
        let theQuestion = this.getQuestionsByIndex();
        if (!theQuestion.isQuestionAnswered()) {
            if (theQuestion.isCorrectAnswer(answer)) {
                this.score++;
                console.log(answer + " GOOD");
            }
            theQuestion.questionAnswered();
        }
        console.log("score: " + this.score);
        //this.questionIndex++;
    }

    getScore() {
        return this.score;
    }

    getQuestion() {
        return this.question;
    }

    goPrev() {
        if (this.questionIndex > 0) this.questionIndex--;
    }

    goForward() {
        if (this.questionIndex < this.questions.length) this.questionIndex++;
    }

    //TIME ENDED
    isFinish() {
        return this.questionIndex === this.questions.length - 1;
    }

    isAllAnswered() {
        //TODO:: iterate all questions and check if they  Answered
    }
}