$(document).ready(function () {


    /*==================================================================
    [ Focus input ]*/
    $('.input100').each(function () {
        $(this).on('blur', function () {
            if ($(this).val().trim() != "") {
                $(this).addClass('has-val');
            }
            else {
                $(this).removeClass('has-val');
            }
        })
    })

    /*==================================================================
   [ Show pass ]*/
    var showPass = 0;
    $('.btn-show-pass').on('click', function () {
        if (showPass == 0) {
            $(this).next('input').attr('type', 'text');
            $(this).addClass('active');
            showPass = 1;
        }
        else {
            $(this).next('input').attr('type', 'password');
            $(this).removeClass('active');
            showPass = 0;
        }

    });

    $('.validate-form .input100').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });
});

function atLeastOneRadio() {
    return $("input[type=radio]:checked").length > 0;
}


function logIn() {  

    var input = $('.validate-input .input100');
    if (check(input) == false) {
        return false;
    }

    if (!atLeastOneRadio()) {
        alert("You Didn't choose what you are Student or Teacher ?");
        return;
    }

    let userType = document.querySelector('input[name="radio"]:checked').value;
    var userName = $('#userName').val();
    var userPass = $('#userPassword').val();
    var getUser;
    var pageName;

    if (userType == 'Students') {

        pageName = 'studentPage.html';
        getUser = 'GetByStudentUserName';
        
    } else {

        pageName = 'teacherPage.html';
        getUser = 'GetByTeacherUserName';

    }

    $.ajax({
        url: 'api/' + userType + '/' + getUser+'/' + userName,
        type: 'GET',
        dataType: 'json',
        success: function (user) {

            var hostName = $(location).attr('host');

            if (user) {

                if (user.password == userPass) {

                    sessionStorage.setItem(userType + 'Name', userName);
                    var url = "https://" + hostName + "/" + pageName + '?' + user.id;
                    $(location).attr('href', url);
                }
                else {
                    alert("User name or password is wrong");
                }

            } else {
                alert("User name or password is wrong");
            }

        },
        error: function (request, message, error) {
            // handleException(request, message, error);
        }
    })

}

function check(input) {

    var check = true;

    for (var i = 0; i < input.length; i++) {
        if (validate(input[i]) == false) {
            showValidate(input[i]);
            check = false;
        }
    }

    return check;

}

function validate(input) {
    if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {

        if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
            return false;
        }
    }
    else {
        if ($(input).val().trim() == '') {
            return false;
        }
    }
}

function showValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).addClass('alert-validate');
}

function hideValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).removeClass('alert-validate');
}

function logAsTeacher(userName, userPass) {

    $.ajax({
        url: 'api/Teachers/GetByTeacherUserName/' + userName,
        type: 'GET',
        dataType: 'json',
        success: function (teacher) {

            var hostName = $(location).attr('host');

            if (teacher) {

                if (teacher.password == userPass) {

                    sessionStorage.setItem("username", userName);
                    var url = "https://" + hostName + "/htmlPage.html";
                    $(location).attr('href', url);

                }

            } else {
                alert("User name or password is wrong");
            }

        },
        error: function (request, message, error) {
            // handleException(request, message, error);
        }
    })

}

function logAsStudent(userName, userPass) {

    $.ajax({
        url: 'api/Teachers/GetByTeacherUserName/' + userName,
        type: 'GET',
        dataType: 'json',
        success: function (teacher) {

            var hostName = $(location).attr('host');

            if (teacher) {

                if (teacher.password == userPass) {

                    sessionStorage.setItem("username", userName);
                    var url = "https://" + hostName + "/htmlPage.html";
                    $(location).attr('href', url);

                }

            } else {
                alert("User name or password is wrong");
            }

        },
        error: function (request, message, error) {
            // handleException(request, message, error);
        }
    })

}