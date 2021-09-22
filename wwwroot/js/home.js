$(document).ready(function () {

    var hostName = $(location).attr('host');  
    alert(hostName);
     

    if (sessionStorage.getItem("username") == null)
        window.location.replace("https://"+hostName + "/index.html");


    alert(sessionStorage.getItem("username"));
});
