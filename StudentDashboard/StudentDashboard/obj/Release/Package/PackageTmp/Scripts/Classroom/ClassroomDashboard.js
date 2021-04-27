function viewClassroomSharingDetails()
{
    $("#shareModal").modal('show');

}
function requestUploadVideo()
{
    $("#fileUploadModal").modal("show");
}
function requestStartMeeting() {
    $("#requestStartMeeting").modal("show");
}
function copyText() {
    var copyText = document.getElementById("test-url");
    copyText.select();
    document.execCommand("copy");
}
function copyText2() {
    var copyText = document.getElementById("test-url-2");
    copyText.select();
    document.execCommand("copy");
}
function callInsertNewPost(buttonid) {
    debugger
    var post = tinyMCE.activeEditor.getContent();
    if (post == "") { return; }
    $(buttonid).append('<i class="fa fa-spinner fa-spin" id="tempspinner"></i>');
    $(buttonid).disabled = true;

    if (true) {
        var _data =
        {
            "post": post,
            "classroom_id": localStorage.getItem("classroom_id")
        }
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            data: JSON.stringify(_data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/InsertNewPostToClassroom",
            success: function (data) {
                debugger;
                if (data != null && data.response_code == 1) {
                    $("#successModal").modal("show");
                }
                else {
                    $("#errorModal").modal("show");
                }
                $(buttonid).disabled = false;
                $(buttonid).find(":first-child").remove();
            },
            error: function (data) {
                $("errorModal").modal("show");
                $(buttonid).disabled = false;
                $(buttonid).find(":first-child").remove();
            }
        });
    }
    else {
        $(buttonid).disabled = false;
        $(buttonid).find(":first-child").remove();
    }
}
function requestActivateClassroom()
{
    $("#classroomActivateModal").modal("show");
}
function getClassroomWeekDaysSchedule(){

}
function activateClassroom() {
    debugger;
    var classroomJoiningFee = $("#classroomFee").val();
    var demoClasses = $("#demoClasses").val();
    var _data = {
        "classroom_id": localStorage.getItem("classroom_id"),
        "classroom_joining_fee": classroomJoiningFee,
        "classroom_start_date": $("#classroomStartDate").val(),
        "classroom_start_time": $("#classroomDailyStartTime").val(),
        "classroom_weekday_schedule": getSelectedDates(),
        "no_of_demo_classes": demoClasses,
        "public_type": ClassroomPublicType
    }
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data:JSON.stringify(_data),
        url: "/api/v1/instructor/activateclassroom",
        success: function (data) {
            if (data != null && data.response_code == 1) {
                location.reload();
            }
        }
    });
}
        (function () {
            'use strict';
            pageShowEvents();
        })();
        function pageShowEvents()
        {
            //runTimer();
        }
        function onPageHideEvents()
        {
            clearInterval(messageInerval);
        }

        function getMeetingDetails() {
        //debugger;
            $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "/api/v1/instructor/GetAllMeetingsForClassroom?ClassroomId=".concat(localStorage.getItem("classroom_id")),
            success: function (data) {
                debugger;
                var sNo = 0;
                if (data != null && data.response_code == 1 && data.meeting_details != null && data.meeting_details.length>0) {
                    var meetings = data.meeting_details;
                    $('#tableBody').empty();
                    for (var i = 0; i < meetings.length; i++) {
                        sNo++;
                        var rows = '<tr><td class="pr-2"><button class=btn btn-outline-primary" onclick="showClassroomMeetingContent(' + meetings[i].meeting_id + ')"><i class="fa fa-2x fa-play-circle text-primary"></i></button></td></td>'
                        + '<td class="pr-2">Topic Name</td><td class="pr-2">' + meetings[i].meeting_finish_time + '</td>'
                        + '<td class="pr-2"><button class="btn btn-sm btn-primary" onclick="getStudentMeetingDetails('.concat(meetings[i].meeting_id) + ')">' + meetings[i].participants_count + '</button></td>'
                        +''
                            + '</tr>';

                        $('#tableBody').append(rows);
                        $("#spinner").remove();
                        $("#coursesTable").show();
                    }
                    $("#startPreviousMeeting").fadeIn();
                    $("#startPreviousMeeting").attr("href", "StartMeeting?ClassroomId=".concat(localStorage.getItem("classroom_id")));

                }
                else {
                    $("#footer").show();
                    $("#spinner").remove();
                }
            }
    });
        }
    function getStudentDetails() {
        //debugger;
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/GetAllStudnetJoinedToClassroom?ClassroomId=".concat(localStorage.getItem("classroom_id")),
            success: function (data) {
                debugger;
                var sNo = 0;
                if (data != null && data.response_code == 1 && data.student_details != null && data.student_details.length > 0) {
                    var students = data.student_details;
                    $('#tableBodyStudentDetails').empty();
                    for (var i = 0; i < students.length; i++) {
                        sNo++;
                        var rows = '<tr><td scope="row" ><button type="button" class="btn btn-link font-weight-bold" onclick="getCourseIndexDetails('.concat(students[i].student_id) + ')">' + sNo + '</button></td>'
                        + '<td>' + students[i].student_name + '</td><td>' + students[i].date_of_joining + '</td>'
                        + '</tr>';
                        $('#tableBodyStudentDetails').append(rows);
                        $("#spinnerStudentDetails").remove();
                        $("#studentDetailsTable").show();
                    }
                    $('#studentTable').DataTable();

                }
                else {
                    $("#footerStudentsJoined").show();
                    $("#spinnerStudentDetails").remove();
                }
            }
        });
    }
    function getStudentMeetingDetails(meetingId) {
        //debugger;
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
           
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/GetAllStudentsJoinedToMeeting?ClassroomId=".concat(localStorage.getItem("classroom_id")).concat("&MeetingId=").concat(meetingId),
            success: function (data) {
                debugger;
                var sNo = 0;
                if (data != null && data.response_code == 1 && data.student_details != null && data.student_details.length > 0) {
                    var students = data.student_details;
                    $("#footerStudentsMeetingJoined").hide();
                    $('#tableBodyStudentMeetingDetails').empty();
                    $("#spinnerStudentMeetingDetails").remove();
                    $("#studentMeetingTable").show();
                    $("#studentMeetingJoinedModal").modal('show');
                    for (var i = 0; i < students.length; i++) {
                        sNo++;
                        var rows = '<tr><td scope="row" ><button type="button" class="btn btn-link font-weight-bold" onclick="getCourseIndexDetails('.concat(students[i].student_id) + ')">' + sNo + '</button></td>'
                        + '<td>' + students[i].student_name + '</td><td>' + students[i].time_of_joining + '</td>'
                        + '</tr>';
                        $('#tableBodyStudentMeetingDetails').append(rows);

                    }

                    $('#studentMeetingTable').DataTable();

                }
                else {
                    $("#footerStudentsMeetingJoined").show();
                    $("#spinnerStudentMeetingDetails").remove();
                }
            }
        });
    }

    function callSubmitMessage(buttonid) {
        debugger
        syncFlag = true;
        var post = $("#classroomMessage").val();
        if (post == "") { return; }
        $(buttonid).append('<i class="fa fa-spinner fa-spin" id="tempspinner"></i>');
        $(buttonid).disabled = true;
        if (true) {
            var _data =
            {
                "message": post,
                "classroom_id": localStorage.getItem("classroom_id")
            }
            $.ajax({
                headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
                type: "POST",
                data: JSON.stringify(_data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "/api/v1/instructor/InsertNewClassroomMessage",
                success: function (data) {
                    debugger;
                    if (data != null && data.response_code == 1) {
                        $("#classroomMessage").val("")
                        getAllMessagesAsync();
                    }
                    else {
                        $("#errorModal1").modal("show");
                    }
                    $(buttonid).disabled = false;
                    $(buttonid).find(":first-child").remove();
                    syncFlag = false;
                },
                error: function (data) {
                    $("errorModal").modal("show");
                    $(buttonid).disabled = false;
                    $(buttonid).find(":first-child").remove();
                }
            });
        }
        else {
            $(buttonid).disabled = false;
            $(buttonid).find(":first-child").remove();
        }
    }
    var messageInerval
    function runTimer() {
        messageInerval = setInterval(getAllMessagesAsync, 1000);
    }
    function getAllMessages() {
        $.ajax({

            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/GetAllClassroomMessage?ClassroomId=".concat(localStorage.getItem("classroom_id")),
            success: function (data) {
                debugger;
                //stopCourseSpinner();
                if (data != null && data.response_code == 1) {
                    var currentSessionLastMessageId=-1;
                    $("#questionContainer").empty();
                    var row = "";
                    for (var i = 0; i < data.message_details.length; i++) {
                        currentSessionLastMessageId = data.message_details[i].message_id;

                        if (data.message_details[i].is_instructor == false) {

                            row= ' <div class="container-fluid">'
                                + '<p class="messge-sender"><spna><i class="fa fa-user mr-2"></i></span>' + data.message_details[i].student_name + '</p>'
                                          + '<p class="ui-form-chat-instructor">' + data.message_details[i].message + '</p>'
                                          + '<p class="time_date_instructor">' + data.message_details[i].message_creation_time + '</p>'
                                      + '</div>';
                        }
                        else {
                            row='<div class="container-fluid">'

                                          + '<p class="ui-form-chat">' + data.message_details[i].message + '</p>'
                                          + '<p class="time_date">' + data.message_details[i].message_creation_time + '</p>'
                                      + '</div>';

                        }
                        $("#questionContainer").append(row);
                        $("#messagesContainer").scrollTop($("#messagesContainer")[0].scrollHeight);
                    }
                    if (currentSessionLastMessageId != -1) {
                        lastMessageId = currentSessionLastMessageId;
                    }

                }
            }
        });
    }
    function getAllMessagesAsync() {
        if (lastMessageId == -1||syncFlag)
        {
            return;
        }
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/GetAllClassroomMessageAfterLast?ClassroomId=".concat(localStorage.getItem("classroom_id")).concat("&LastMessageId=").concat(lastMessageId),
            success: function (data) {
                //stopCourseSpinner();
                if (data != null && data.response_code == 1) {
                    //$("#questionContainer").empty();
                    for (var i = 0; i < data.message_details.length; i++) {
                        lastMessageId = data.message_details[i].message_id;
                        if (data.message_details[i].is_instructor == false) {
                            var row = ' <div class="container-fluid">'
                                + '<p class="messge-sender"><spna><i class="fa fa-user mr-2"></i></span>' + data.message_details[i].student_name + '</p>'
                                          + '<p class="ui-form-chat-instructor">' + data.message_details[i].message + '</p>'
                                          + '<p class="time_date_instructor">' + data.message_details[i].message_creation_time + '</p>'
                                      + '</div>';
                        }
                        else {
                            var row = ' <div class="container-fluid">'

                                          + '<p class="ui-form-chat">' + data.message_details[i].message + '</p>'
                                          + '<p class="time_date">' + data.message_details[i].message_creation_time + '</p>'
                                      + '</div>';
                        }
                        $("#questionContainer").append(row);
                        $("#messagesContainer").scrollTop($("#messagesContainer")[0].scrollHeight);
                    }
                }
            }
        });
    }
    function showMessages()
    {
        hideAll();
        runTimer();
        $("#navMessagesContainer").show();
        getAllMessages();
    }
    function showUpdateMeeting()
    {
        hideAll();
        $("#navUpdateDetails").show();
    }
    function showInstructorsOfClassrooms() {
        hideAll();
        $("#navInstructors").show();
    }
    function hideAll()
    {
        clearInterval(messageInerval);
        $("#navMessagesContainer").hide();
        $("#navPreviousMeetings").hide();
        $("#navStudentsJoined").hide();
        $("#navUpdateDetails").hide();
        $("#navInstructors").hide();
        $("#navAllAssignments").hide();
        $("#navAllTests").hide();
        $("#classroomDetails").hide();
        $("#navClassroomPost").hide();
        $("#navAddNewAttachment").hide();
        $("#navAllAttachments").hide();
        $("#classroomContaintProgressDetails").hide();
        $("#navHome").hide();
        $("#navUpdateSchedulerDetails").hide();
        $("#updateSyllabusDiv").hide();
        $("#sendClassroomNotification").hide();
    }

    function showPreviousMeetings() {
        hideAll();
        $("#navPreviousMeetings").addClass("active");
        $("#navPreviousMeetings").show();
        getMeetingDetails();
    }
    function showStudentsJoined() {
        hideAll();
        $("#navStudentsJoined").show();
        getStudentDetails();
    }
    var syncFlag = false;
    var lastMessageId = -1;

    function callUpdateClassroomDetails(buttonid) {
        debugger
        $(buttonid).append('<i class="fa fa-spinner fa-spin" id="tempspinner"></i>');
        $(buttonid).disabled = true;
        var classRoomName = $("#classroomName").val();
        var classRoomDescription = $("#classroomDescription").val();
        var BackGroundUrl = $("#classroomBackGroundUrl").val();
        var classroomRegistrationCloseDate = $("#registrationCloseDate").val();
        var classroomStartDate = $("#classroomStartDateSchedule").val();
        var noOfDemoClassrooms = $("#noOfDemoClasses").val();
        if (validateInputField(classroomName) && validateInputField(classroomDescription)) {
            var _data =
            {
                "classroom_id":localStorage.getItem("classroom_id"),
                "classroom_name": classRoomName,
                "classroom_description": classRoomDescription,
                "background_url": BackGroundUrl,
                "classroom_class_start_date": classroomStartDate,
                "classroom_registration_close_date": classroomRegistrationCloseDate,
                "no_of_demo_sessions": noOfDemoClassrooms
            }
            $.ajax({
                headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
                type: "POST",
                data: JSON.stringify(_data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "/api/v1/instructor/UpdateClassroomDetails",
                success: function (data) {
                    debugger;
                    if (data != null && data.response_code == 1) {
                        $("#successModal1").modal("show");

                    }
                    else {
                        $("#errorModal").modal("show");
                    }
                    $(buttonid).disabled = false;
                    $(buttonid).find(":first-child").remove();
                },
                error: function (data) {
                    $("errorModal").modal("show");
                    $(buttonid).disabled = false;
                    $(buttonid).find(":first-child").remove();
                }
            });
        }
        else {
            $(buttonid).disabled = false;
            $(buttonid).find(":first-child").remove();
        }
    }
    function validateInputField(id) {
        if ($(id).val() == "") {
            $(id).addClass("is-invalid");
            return false;
        }
        else {
            $(id).addClass("is-valid").removeClass("is-invalid");
            return true;
        }
    }
    function viewAllClassroomTests()
    {
        hideAll();
        $("#navAllTests").show();
        getAllTestDetails();
    }
    function viewAllClassroomAssignments() {
        hideAll();
        $("#navAllAssignments").show();
        //debugger;
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            //data: {
            //    "MeetingId": meetingId,
            //    "ClassroomId": localStorage.getItem("classroom_id")
            //},
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/GetAllClassroomAssignment?ClassroomId=".concat(localStorage.getItem("classroom_id")),
            success: function (data) {
                debugger;
                var sNo = 0;
                if (data != null && data.response_code == 1 && data.assignment_details != null && data.assignment_details.length > 0) {
                    var assignmentDetails = data.assignment_details;
                    $('#tableAllAssignments').empty();
                    for (var i = 0; i < assignmentDetails.length; i++) {
                        sNo++;
                        var rows = '<tr><td scope="row" colspan="2"><button type="button" class="btn btn-link font-weight-bold" onclick="getAssignmentDetails('.concat(assignmentDetails[i].assignment_id) + ')">' + assignmentDetails[i].assignment_name + '</button></td>'
                        + '<td class="mr-2">   ' + assignmentDetails[i].creation_date + '</td><td class="mr-2">   ' + assignmentDetails[i].no_of_submissions + '</td><td class="mr-2">' + assignmentDetails[i].no_of_submissions + '</td>'
                        + '<td class="mr-2">'
                            + '<a type="button" href="ViewClassroomAssignment?c_id=' + localStorage.getItem("classroom_id")+ '&&ass_id=' + assignmentDetails[i].assignment_id + '" class="btn btn-link" class="list-group-horizontal">'
                                + '<i class="fas fa-edit fa-sm fa-fw mr-2 text-gray-400 m-1"></i>'
                            + '</a>'
                            + '<button type="button" class="btn btn-link" class="list-group-horizontal" onclick="requestDeletAssignment(' + assignmentDetails[i].assignment_id + ')">'
                                + '<i class="fas fa-trash fa-sm fa-fw mr-2 text-gray-400 m-1"></i>'
                            + '</button>'
                        + '</td>'
                        + '</tr>';
                        $("#footerAllAssignments").hide();
                        $('#tableAllAssignments').append(rows);
                        $("#spinnerAllAssignments").remove();
                        $("#studentAllAssignmentsTable").show();
                    }
                    $('#studentAllAssignmentsTable').DataTable();

                }
                else {
                    $("#footerAllAssignments").show();
                    $("#spinnerAllAssignments").remove();
                }
            }
        });
    }
    function requestDeletAssignment(AssignmentId) {
        $("#confirmMessage").text("Are you sure to want to delete the assignment?");
        $("#confirmDeletButtonForCourse").attr("onclick", "deleteAssignment(" + AssignmentId + ")");
        $("#confirmDelete").modal('show');
    }
    function deleteAssignment(AssignmentId) {
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/DeleteClassroomAssignment?ClassroomId=".conact(localStorage.getItem("classroom_id")).concat("&AssignmentId=").concat(AssignmentId),
            success: function (data) {
                if (data != null && data.response_code == 1) {
                    $("#successResponseMessage").text("assignment deleted successfully");
                    $("#successDelete").modal('show');
                    viewAllClassroomAssignments();
                }
                else {
                    $("#errorAlertMessageBody").text("assignment could not be deleted");
                    $("#errorAlertHeader").modal('show');
                }
            }
        });
    }
    function getAssignmentDetails(id) {
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/Course/FetchAssignmentDetails?AssignmentId=".concat(id),
            success: function (data) {
                debugger
                if (data != null && data.response_code == 1) {
                    var questionDetails = data.mcq_questions;
                    if (data.assignment_type == "sub") {
                        data.assignment_type = "Subjective";
                    }
                    AssignmentType = data.assignment_type;
                    var assignmentHtml = '<div class="card shadow mb-4 border-bottom-primary">'
                                + '<div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">'
                                    + '<h6 class="m-0 font-weight-bold text-primary">Assignment Details</h6>'
                                     + '<div class="dropdown no-arrow">'
                                     + '</div>'
                                 + '</div>'
                                + '<div class="card-body">'
                    assignmentHtml += '<dl class="row ml-3">'
                          + '<dt class="col-sm-3">Assignment name:</dt><dd class="col-sm-9">' + data.assignment_name + '</dd>'
                          + '<dt class="col-sm-3">Assignment description:</dt><dd class="col-sm-9">' + data.assignment_description + '</dd>'
                          + '<dt class="col-sm-3">Assignment Creation Date:</dt><dd class="col-sm-9">' + data.creation_date + '</dd>'
                          + '<dt class="col-sm-3">Assignment Updat Date:</dt><dd class="col-sm-9">' + data.updatione_date + '</dd>'
                          + '<dt class="col-sm-3">Assignment Type:</dt><dd class="col-sm-9">' + data.assignment_type + '</dd>'
                          + '</dl></div><h2 class="ml-3">Questions</h2>';
                    if (data.assignment_type == "mcq") {
                        assignmentHtml += '<div class="card-body" style="padding:0">'
                                                + '<div class="table-responsive">'
                                                    + '<table id="coursesTable" class="table table-hover text-xsmall">'
                                                        + '<thead class="thead-dark">'
                                                            + '<tr>'
                                                                + '<th scope="col">S.No</th>'
                                                                + '<th scope="col">Question Statement</th>'
                                                                + '<th scope="col">Option1</th>'
                                                                + '<th scope="col">Option2</th>'
                                                                + '<th scope="col">Option3</th>'
                                                                + '<th scope="col">Option4</th>'
                                                                + '<th scope="col">Correct Option</th>'
                                                            + '</tr>'
                                                        + '</thead>'
                                                        + '<tbody>';
                    }
                    else {
                        assignmentHtml += '<div class="card-body" style="padding:0">'
                                               + '<div class="table-responsive">'
                                                   + '<table id="coursesTable" class="table table-hover text-xsmall">'
                                                       + '<thead class="thead-dark">'
                                                           + '<tr>'
                                                               + '<th scope="col">S.No</th>'
                                                               + '<th scope="col">Question Statement</th>'
                                                               + '<th scope="col">Hint</th>'
                                                               + '<th scope="col">Addition Date</th>'
                                                               //+ '<th scope="col">Actions</th>'
                                                           + '</tr>'
                                                       + '</thead>'
                                                       + '<tbody>';
                    }
                    var rows = '';
                    if (data.assignment_type == "mcq") {
                        if (questionDetails != null) {
                            for (var i = 0; i < questionDetails.length; i++) {
                                var sNo = i + 1;
                                rows += '<tr><th scope="row" >' + sNo + '</th>'
                                + '<td>' + questionDetails[i].question_statement + '</td><td>' + questionDetails[i].option1 + '</td><td>' + questionDetails[i].option2 + '</td><td>' + questionDetails[i].option3 + '</td><td>' + questionDetails[i].option4 + '</td><td>' + questionDetails[i].correct_option + '</td>'
                                + '</tr>';
                            }
                        }
                    }
                    else if (data.assignment_type = "Subjective") {

                        var questionDetails = data.subjective_questions;
                        if (questionDetails != null) {
                            for (var i = 0; i < questionDetails.length; i++) {
                                var sNo = i + 1;
                                rows += '<tr><th scope="row" >' + sNo + '</th>'
                               + '<td>' + questionDetails[i].question_statement + '</td><td>' + questionDetails[i].hint + '</td><td>' + questionDetails[i].creation_date + '</td>'
                               + '</tr>';
                            }
                        }
                    }
                    assignmentHtml += rows;
                    assignmentHtml += '</tbody></table></div></div>';
                }

                $("#responseBodyContainer").html(assignmentHtml);
                $("#assignmentDetailsModal").modal('show');
            }
        });
    }
    function getAllTestDetails() {
        debugger;
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/GetAllClassroomTests?ClassroomId=".concat(localStorage.getItem("classroom_id")),
            success: function (data) {
                var x = $('#tableAllTests').empty();
                debugger;
                if (data != null && data.response_code == 1 && data.classroom_test_details!=null) {
                    var tests = data.classroom_test_details;
                    if (tests.length > 0) {
                        for (var i = 0; i < tests.length; i++) {
                            if (tests[i].test_name.length > 30) {
                                tests[i].test_name = tests[i].test_name.substring(0, 30) + "...";
                            }
                            var rows = '<tr><th scope="row" ><button type="button" class="btn btn-link"  onclick=getTestDetails("' + tests[i].test_id + '") >' + tests[i].test_name + '</button></th>'
                            + '<td>' + tests[i].creation_date + '</td><td>' + tests[i].no_of_questions + '</td>'
                            + '<td><button type="button" class="btn btn-link font-weight-bold" onclick="getTestSubmissions('.concat(tests[i].test_id) + ')">' + tests[i].no_of_submissions + '</button></td>'
                            + '<td>'
                                + '<a type="button" class="btn btn-link" class="list-group-horizontal" href="ViewClassroomTest?t_id='.concat(tests[i].test_id) + '&c_id=' + localStorage.getItem("classroom_id")+'">'
                                    + '<i class="fas fa-edit fa-sm fa-fw mr-2 text-gray-400 m-1"></i>'
                                + '</a>'
                                + '<button type="button" class="btn btn-link" class="list-group-horizontal" onclick=requestDeleteTest("' + tests[i].test_id + '") >'
                                    + '<i class="fas fa-trash fa-sm fa-fw mr-2 text-gray-400 m-1"></i>'
                                + '</button>'
                            + '</td>'
    + '</tr>';
$('#tableAllTests').append(rows);

$("#studentAllTestTable").show();
                        }
                    }
                    else {
    $("#footerAllTests").show();
}
$('#customTableCodePathshala').DataTable();
$("#spinnerAllTests").remove();
                }
                else {
    $("#footerAllTests").show();
    $("#spinnerAllTests").remove();
}
            }
        });
    }
function markClassroomOpen(id) {
    $(id).removeClass("fa-square").addClass("fa-check-square").addClass("text-success").text(" Open").removeClass("text-danger");
    $(id).prop("onclick", markClassroomClosed(id));
}
function markClassroomClosed(id) {
    $(id).removeClass("fa-check-square").addClass("fa-square").addClass("text-danger").text(" Closed").removeClass("text-success");
    $(id).prop("onclick", markClassroomClosed(id));
}
function getTestDetails(id) {
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "/api/v1/Course/FetchTestDetails?id=".concat(id),
        success: function (data) {
            if (data != null && data.response_code == 1) {
                var questionDetails = data.mcq_questions;
                var assignmentHtml = '<div class="card shadow mb-4 border-bottom-primary">'
                    + '<div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">'
                    + '<h6 class="m-0 font-weight-bold text-primary">Test Details</h6>'
                    + '<div class="dropdown no-arrow">'
                    + '<a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">'
                    + '<i class="fas fa-ellipsis-v fa-sm fa-fw text-gray-400"></i>'
                    + '</a>'

                    + '</div>'
                    + '</div>'
                    + '<div class="card-body">'
                assignmentHtml += '<dl class="row ml-3">'
                    + '<dt class="col-sm-3">Test name:</dt><dd class="col-sm-9">' + data.test_name + '</dd>'
                    + '<dt class="col-sm-3">Test description:</dt><dd class="col-sm-9">' + data.test_description + '</dd>'
                    + '<dt class="col-sm-3">Test Creation Date:</dt><dd class="col-sm-9">' + data.test_creation_datetime + '</dd>'
                    + '<dt class="col-sm-3">Test Updat Date:</dt><dd class="col-sm-9">' + data.test_updation_datetime + '</dd>'
                    + '<dt class="col-sm-3">Test Type:</dt><dd class="col-sm-9">Mcq</dd>'
                    + '</dl></div><h2 class="ml-3">Questions</h2>';
                assignmentHtml += '<div class="card-body" style="padding:0">'
                    + '<div class="table-responsive">'
                    + '<table id="coursesTable" class="table table-hover">'
                    + '<thead class="thead-dark">'
                    + '<tr>'
                    + '<th scope="col">S.No</th>'
                    + '<th scope="col">Question Statement</th>'
                    + '<th scope="col">Option1</th>'
                    + '<th scope="col">Option2</th>'
                    + '<th scope="col">Option3</th>'
                    + '<th scope="col">Option4</th>'
                    + '<th scope="col">Correct Option</th>'
                    + '<th scope="col">Time(sec)</th>'
                    + '<th scope="col">Marks</th>'
                    //+ '<th scope="col">Actions</th>'
                    + '</tr>'
                    + '</thead>'
                    + '<tbody>';
                var rows = '';
                if (questionDetails != null) {
                    for (var i = 0; i < questionDetails.length; i++) {
                        var sNo = i + 1;
                        rows += '<tr><th scope="row" >' + sNo + '</th>'
                            + '<td>' + questionDetails[i].question_statement + '</td><td>' + questionDetails[i].option1 + '</td><td>' + questionDetails[i].option2 + '</td><td>' + questionDetails[i].option3 + '</td><td>' + questionDetails[i].option4 + '</td><td>' + questionDetails[i].correct_option + '</td>'
                            + '<td>' + questionDetails[i].question_time_in_seconds + '</td><td>' + questionDetails[i].marks + '</td>'

                            + '</tr>';
                    }
                }
                assignmentHtml += rows;
                assignmentHtml += '</tbody></table></div></div>';

            }
            $("#testDetailsModalBody").html(assignmentHtml);
            $("#testDetailsModal").modal('show');
        }

    });
}
function deleteTest(TestId) {
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "/api/v1/instructor/DeleteClassroomTest?ClassroomId=".concat(localStorage.getItem("classroom_id")).concat("& TestId=").concat(TestId),
        success: function (data) {
            if (data != null && data.response_code == 1) {
                $("#successResponseMessage").text("test deleted successfully");
                $("#successDelete").modal('show');
                viewAllClassroomTests();
            }
            else {
                $("#errorAlertMessageBody").text("test could not be deleted");
                $("#errorAlertHeader").modal('show');
            }
        }
    });
}
function requestDeleteTest(TestId) {
    $("#confirmMessage").text("Are you sure to want to delete the test?");
    $("#confirmDeletButtonForCourse").attr("onclick", "deleteTest(" + TestId + ")");
    $("#confirmDelete").modal('show');
}
function viewClassroomPosts() {
    hideAll();
    $("#navClassroomPost").show();
}
function viewClassroomHome() {
    hideAll();
    $("#classroomDetails").show();
    $("#classroomContaintProgressDetails").show();
    $("#navHome").show();
}
function showUpdateScheduleDetails() {
    hideAll();
    $("#navUpdateSchedulerDetails").show();
    for (var i = 1; i < 8; i++) {
        $('#classroomStartTime'.concat(i)).timepicker();
        $('#classroomFinishTime'.concat(i)).timepicker();
    }
    callGetClassroomTimeTable();
}
function requestNewFileUpload() {
    $("#fileUploadModal").modal('show');
}
function requestUpload() {
    document.getElementById("fileUploadProgressBar").style.width = "0%";
    debugger
    var data = new FormData();
    var file = $('#fileUpload')[0];
    data.append('file', file.files[0]);
    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();

            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    percentComplete = parseInt(percentComplete * 100);
                    document.getElementById("fileUploadProgressBar").style.width = percentComplete + "%";
                    if (percentComplete === 100) {
                        $("#postUploadAlert").show();
                        $("#postUploadAlert").append('<button type="button" class="btn btn-link" onclick="requestPreviewUpload()">View upload&rarr;</button>');
                    }
                }
            }, false);

            return xhr;
        },
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: false,
        contentType: false,
        data: data,
        url: "/api/v1/instructor/UploadImage",
        success: function (data) {
            debugger
            if (data != null && data.response_code == 1) {
                backGroundImagePath = data.file_location;
                smallIconUrl = data.small_size_icon_url;
                mediumSizeIconUrl = data.medium_size_icon_url;
                $("#imagePreview").attr("src", data.file_location);
            }
            else {
                $("#errorAlertMessageBody").text("file could not be added");
                $("#errorAlertHeader").modal('show');
            }
        }
    });
}
function requestPreviewUpload() {
    $("#previewUploadModal").modal("show");
}
function viewAddNewAttachment() {
    hideAll();
    $("#navAddNewAttachment").show();
}
function requestFileUpload() {
    document.getElementById("fileUploadProgressBar").style.width = "0%";
    debugger
    var data = new FormData();
    var file = $('#studyMaterialUpload')[0];
    data.append('file', file.files[0]);
    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();

            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    percentComplete = parseInt(percentComplete * 100);
                    document.getElementById("fileUploadProgressBar").style.width = percentComplete + "%";

                }
            }, false);

            return xhr;
        },
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: false,
        contentType: false,
        data: data,
        url: "/api/v1/instructor/UploadFile",
        success: function (data) {
            debugger
            if (data != null && data.response_code == 1) {
                backGroundImagePath = data.file_location;
                $("#attachmentUrl").val(backGroundImagePath);
                $("#attachmentUrl").prop("disabled", true);
                $("#postFileUploadAlert").show();
                $("#postFileUploadAlert").append('<a type="button" target="_blank" href="' + data.file_location + '" class="btn btn-link" >download upload&rarr;</a>');

            }
            else {

            }
        }
    });
}
function requestFileUploadForStudyMaterial() {
    document.getElementById("fileUploadProgressBarForAttachment").style.width = "0%";
    debugger
    var data = new FormData();
    var file = $('#studyMaterialUpload')[0];
    data.append('file', file.files[0]);
    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();

            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    percentComplete = parseInt(percentComplete * 100);
                    document.getElementById("fileUploadProgressBarForAttachment").style.width = percentComplete + "%";

                }
            }, false);

            return xhr;
        },
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: false,
        contentType: false,
        data: data,
        url: "/api/v1/instructor/UploadFile",
        success: function (data) {
            debugger
            if (data != null && data.response_code == 1) {
                backGroundImagePath = data.file_location;
                $("#attachmentUrl").val(backGroundImagePath);
                $("#attachmentUrl").prop("disabled", true);
                $("#postFileUploadAlert").show();
                $("#postFileUploadAlert").append('<a type="button" target="_blank" href="' + data.file_location + '" class="btn btn-link" >download upload&rarr;</a>');

            }
            else {

            }
        }
    });
}
function validateUrl(id) {
    var re = /^(http[s]?:\/\/){0,1}(www\.){0,1}[a-zA-Z0-9\.\-]+\.[a-zA-Z]{2,5}[\.]{0,1}/;
    if ($(id).val() != "" && re.test($(id).val())) {
        $(id).addClass("is-valid").removeClass("is-invalid");
    }
    else {
        $(id).addClass("is-invalid");
    }
}

function callAddAttachment(buttonid) {
    $(buttonid).append('<i class="fa fa-spinner fa-spin" id="tempspinner"></i>');
    $(buttonid).prop("disabled", true);
    var attachmentNameId = $("#attachmentName");
    var attachmentDescriptionId = $("#attachmentDescription");
    var attachmentUrlId = $("#attachmentUrl");
    if (attachmentNameId.hasClass("is_invalid") || attachmentDescriptionId.hasClass("is-invalid") || attachmentUrlId.hasClass("is_invalid")) {
        $(buttonid).find(":first-child").remove();
        return;
    }
    if (validateInputField(classroomName) && validateInputField(classroomDescription)) {
        var _data =
        {
            "classroom_id": localStorage.getItem("classroom_id"),
            "attachment_name": attachmentNameId.val(),
            "attachment_description": attachmentDescriptionId.val(),
            "attachment_url": attachmentUrlId.val()
        }
        $.ajax({
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
            type: "POST",
            data: JSON.stringify(_data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/v1/instructor/AddClassroomAttachment",
            success: function (data) {
                debugger;
                if (data != null && data.response_code == 1) {
                    $("#postStudyMaterialUploadSucess").show();
                    refreshAddAttachment();
                    $(buttonid).prop("disabled", false)
                }
                else {
                    $("#postStudyMaterialUploadFail").show();
                }
                $(buttonid).disabled = false;
                $(buttonid).find(":first-child").remove();
                $(buttonid).prop("disabled", false)
            },
            error: function (data) {
                $("errorModal").modal("show");
                $(buttonid).prop("disabled", false)
                $(buttonid).find(":first-child").remove();
            }
        });
    }
    else {
        $(buttonid).prop("disabled", false)
        $(buttonid).find(":first-child").remove();
    }
}
function refreshAddAttachment() {
    $("#attachmentName").val("");
    $("#attachmentDescription").val("");
    $("#attachmentUrl").val("");
    $("#attachmentUrl").prop("disabled", false);
}
function viewAllClassroomAttachments() {
    hideAll();
    $("#navAllAttachments").show();
    getAllClassroomAttachments();
}
function getAllClassroomAttachments() {
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "/api/v1/instructor/ClassroomAttachments?ClassroomId=".concat(localStorage.getItem("classroom_id")),
        success: function (data) {
            debugger;
            var sNo = 0;
            if (data != null && data.response_code == 1 && data.attachments != null && data.attachments.length > 0) {
                var attachments = data.attachments;
                $('#tableBodyClassroomAttachments').empty();
                for (var i = 0; i < attachments.length; i++) {
                    sNo++;
                    var rows = '<tr><td scope="row" ><button type="button" class="btn btn-link font-weight-bold" >' + sNo + '</button></td>'
                        + '<td>' + attachments[i].attachment_name + '</td><td>' + attachments[i].date_of_addition + '</td>'
                        + '<td>'
                        + '<a   class="list-group-horizontal" download  href="' + attachments[i].attachment_url + '">'
                        + '<i class="fas fa-download fa-sm fa-fw mr-2 text-gray-600 m-1"></i>'
                        + '</a>'
                        + '<button type="button" class="btn btn-link" class="list-group-horizontal" onclick="deleteClassroomAttachment(' + attachments[i].attachment_id + ')" data-toggle="modal" >'
                        + '<i class="fas fa-trash fa-sm fa-fw mr-2 text-gray-600 m-1"></i>'
                        + '</button>'
                        + '</td>'
                        + '</tr>';
                    $('#tableBodyClassroomAttachments').append(rows);
                    $("#spinnerClassroomAttachments").remove();
                    $("#tableClassroomAttachments").show();
                }
                $("#tableBodyClassroomAttachments").fadeIn();

                $('#coursesTable').DataTable();
            }
            else {
                $("#spinnerClassroomAttachments").remove();
                $("#spinner").remove();
                $("#footerAllAttachments").fadeIn();
            }
        }
    });
}
function deleteClassroomAttachment(AttachmentId) {
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "/api/v1/instructor/DeleteClassroomAttachment?ClassroomId=".concat(localStorage.getItem("classroom_id")).concat("&id=").concat(AttachmentId),
        success: function (data) {
            if (data != null && data.response_code == 1) {

                getAllClassroomAttachments();
            }
            else {

            }
        }
    });
}
function openFullscreen(elem) {
    if (elem.requestFullscreen) {
        elem.requestFullscreen();
    } else if (elem.mozRequestFullScreen) { /* Firefox */
        elem.mozRequestFullScreen();
    } else if (elem.webkitRequestFullscreen) { /* Chrome, Safari and Opera */
        elem.webkitRequestFullscreen();
    } else if (elem.msRequestFullscreen) { /* IE/Edge */
        elem.msRequestFullscreen();
    }
    elem.style.width = "100%";
}
function closeFullscreen() {
    if (document.exitFullscreen) {
        document.exitFullscreen();
    } else if (document.mozCancelFullScreen) { /* Firefox */
        document.mozCancelFullScreen();
    } else if (document.webkitExitFullscreen) { /* Chrome, Safari and Opera */
        document.webkitExitFullscreen();
    } else if (document.msExitFullscreen) { /* IE/Edge */
        document.msExitFullscreen();
    }
}
function changeClassroomOpenStatus(id) {
    if ($(id).prop("checked") == true) {
        $(id)[0].parentNode.children[1].innerText = " Open ";
        $(id)[0].nextElementSibling.classList.remove("text-danger");
        $(id)[0].nextElementSibling.classList.add("text-success");
        $(id)[0].parentElement.parentNode.nextElementSibling.children[0].disabled = false;
        $(id)[0].parentElement.parentNode.nextElementSibling.nextElementSibling.children[0].disabled = false;
    }
    else {
        $(id)[0].parentNode.children[1].innerText = " Closed ";
        $(id)[0].nextElementSibling.classList.remove("text-success");
        $(id)[0].nextElementSibling.classList.add("text-danger");
        $(id)[0].parentElement.parentNode.nextElementSibling.children[0].disabled = true;
        $(id)[0].parentElement.parentNode.nextElementSibling.nextElementSibling.children[0].disabled = true;
    }
}
function setClassroomTimeTable(data) {
    for (var i = 1; i < 8; i++) {
        $("#classroomStartTime".concat(i)).val(data[i - 1].start_time);
        $("#classroomFinishTime".concat(i)).val(data[i - 1].close_time);
        $("#classroomCloseStatus".concat(i)).prop("checked", data[i - 1].is_open);
        var x = $("#classroomCloseStatus".concat(i))[0];
        if (!data[i - 1].is_open) {
            x.parentNode.children[1].innerText = " Closed ";
            x.nextElementSibling.classList.remove("text-success");
            x.nextElementSibling.classList.add("text-danger");
        }
        else {
            x.parentNode.children[1].innerText = " Open ";
            x.nextElementSibling.classList.remove("text-danger");
            x.nextElementSibling.classList.add("text-success");
        }
    }
}
function createClassroomScheduleUpdateRequest() {
    debugger
    var classroomScheduleDetails = [];
    for (var i = 1; i < 8; i++) {
        dayWiseSchedule = {
            "start_time": $("#classroomStartTime".concat(i)).val(),
            "close_time": $("#classroomFinishTime".concat(i)).val(),
            "is_open": $("#classroomCloseStatus".concat(i)).prop("checked")
        };
        classroomScheduleDetails.push(dayWiseSchedule);
    }
    return classroomScheduleDetails;
}
function callUpdateClassroomSchedule() {
    debugger
    var _data = {
        "classroom_id": localStorage.getItem("classroom_id"),
        "schedule_details": createClassroomScheduleUpdateRequest(),
    }
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(_data),
        url: "/api/v1/instructor/InsertClassroomSchedule",
        success: function (data) {
            if (data != null && data.response_code == 1) {
                $("#classroomScheduleUpdateMessage").addClass("text-sucess").removeClass("text-danger");
                $("#classroomScheduleUpdateMessage").text("classroom schedule updated successfully");
            }
            else {
                $("#classroomScheduleUpdateMessage").addClass("text-danger").removeClass("text-success");
                $("#classroomScheduleUpdateMessage").text("classroom schedule could not be updated");
            }
            $("#classroomScheduleUpdateAlert").show();
        }
    });
}
function callGetClassroomTimeTable() {
    debugger
    var _data = {
        "classroom_id": localStorage.getItem("classroom_id")
    }
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "/api/v1/instructor/GetClassroomTimeTable?ClassroomId=".concat(localStorage.getItem("classroom_id")),
        success: function (data) {
            if (data != null && data.response_code == 1 && data.classroom_schedule_details != null) {
                setClassroomTimeTable(data.classroom_schedule_details.schedule_details);
            }
            else {

            }

        }
    });
}
var ClassroomPublicType;
var classroomFee = 0;
function activateClassroomChoose(option) {
    ClassroomPublicType = option;
    switch (option) {
        case 1:
            {
                $("#activateClassroomPrivate").show();
                $("#activateClassroomPublic").hide();
                break;
            }
        case 2:
            {
                $("#activateClassroomPublic").show();
                $("#activateClassroomPrivate").hide();
                break;
            }
    }
    $("#activateClassroomButton").prop("disabled", false);

}
function callClassroomMeeting() {
    debugger
    try {
        AndroidInterface.startClassroomMeeting("@Model.m_strClassRoomName", "@Model.m_strClassroomMeetingName");
    }
    catch (ex) {

    }
}
function validateAmount(id) {
    if ($(id).val() == "-") {
        $(id).val("");
    }
    if (parseInt($(id).val()) < 0) {
        $(id).val("");
    }
}
$(function () {
    $('#classroomStartDate').prop('min', function () {
        return new Date().toJSON().split('T')[0];
    });
    $('#classroomStartDate').prop('value', function () {
        return new Date().toJSON().split('T')[0];
    });
    $('#classroomDailyStartTime').prop('value', function () {
        return new Date().toJSON().split('T')[0];
    });

});
var selectedDates = {
    "monday": 0,
    "tuesday": 0,
    "wednesday": 0,
    "thursday": 0,
    "friday": 0,
    "saturday": 0,
    "sunday": 0
};

function setDate(id, date) {
    if ($(id).hasClass("btn-primary")) {
        $(id).removeClass("btn-primary");
        $(id).addClass("btn-outline-primary");
    } else {
        $(id).addClass("btn-primary");
        $(id).removeClass("btn-outline-primary");
    }
    selectedDates[date] = selectedDates[date] == 0 ? 1 : 0;
    //alert(selectedDates[date])
}
function getSelectedDates() {
    return "" + selectedDates["monday"] +
        selectedDates["tuesday"] + selectedDates["wednesday"] + selectedDates["thursday"] +
        selectedDates["friday"] + selectedDates["saturday"] + selectedDates["sunday"];
}
var smallIconUrl;
var mediumSizeIconUrl;
var backGroundImagePath;
function updateClassroomBgImage() {

    var _data =
    {
        "classroom_id": localStorage.getItem("classroom_id"),
        "image_data": {
            "small_size_url": smallIconUrl,
            "original_url": backGroundImagePath,
            "medium_size_url": mediumSizeIconUrl
        }
    }
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(_data),
        url: "/api/v1/instructor/UpdateClassroomImage",
        success: function (data) {
            if (data != null && data.response_code == 1) {
                location.reload();
            }
            else {

            }

        }
    });

}
var currentMeetingId = 0;
function updateClassroomMeetingDetails() {

    var _data =
    {
        "video_link": $("#liveClassVideoUrl").val(),
        "meeting_id": currentMeetingId,
        "classroom_id": localStorage.getItem("classroom_id"),
        "topic_name": $("#liveClassTopicName").val(),
        "topic_description": $("#liveClassNotes").val(),
    }
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(_data),
        url: "/api/v1/instructor/UpdateLiveClassDetails",
        success: function (data) {
            if (data != null && data.response_code == 1) {
                location.reload();
            }
            else {

            }

        }
    });
}
function uploadClassroomMeetingVideo() {
    document.getElementById("meetingVideoUploadProgressbar").style.width = "0%";
    debugger
    var data = new FormData();
    //var file = $('#fileUpload')[0];
    var file = $("#meetingVideoUpload")[0];
    data.append('file', file.files[0]);
    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    percentComplete = parseInt(percentComplete * 100);
                    document.getElementById("meetingVideoUploadProgressbar").style.width = percentComplete + "%";
                    if (percentComplete === 100) {

                    }
                }
            }, false);

            return xhr;
        },
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: false,
        contentType: false,
        data: data,
        url: "/api/v1/instructor/UploadVideo",
        success: function (data) {
            debugger
            if (data != null && data.response_code == 1 && data.file_location != null && data.file_location != "") {
                awsPath = data.file_location;
                $("#liveClassVideoUrl").val(data.file_location);
                $("#postMeetingVideoUploadAlert").show();
                $("#postMeetingVideoUploadAlert").append('<button type="button" class="btn btn-link" >File uploaded successfully </button>');
            }
            else {
                $("#postUploadAlert").show();
                $("#postUploadAlert").append('<button type="button" class="btn btn-link" >File uploading failed</button>');
            }
        }
    });
}

function showClassroomMeetingContent(meetingId) {
    currentMeetingId = meetingId;
    var _data =
    {
        "meeting_id": meetingId,
        "classroom_id": localStorage.getItem("classroom_id")
    }
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(_data),
        url: "/api/v1/instructor/GetLiveClassDetails",
        success: function (data) {
            debugger
            if (data != null && data.response_code == 1) {
                $("#no-data-present").hide();
                if (data.live_class_details.class_topic != null) {
                    $("#topic-name").text(data.live_class_details.class_topic);
                    $("#liveClassTopicName").val(data.live_class_details.class_topic);

                }
                if (data.live_class_details.video_url != null) {
                    $("#topic-video-palyer-container").show();
                    const player = new Plyr("#topic-video-palyer-container", { captions: { active: true } });
                    window.player = player;
                    $("#liveClassVideoUrl").val(data.live_class_details.video_url);
                    $("#video-url").attr("src", data.live_class_details.video_url);
                    $("#video-url").show();
                    $("#no-video-data-present").hide();
                    $("#no_video_present_img").hide();
                }

                else {
                    $("#topic-video-palyer-container").hide();
                    $("#video-url").hide();
                    $("#no-video-data-present").show();
                    $("#no_video_present_img").show();
                }
            }
            else {
                $("#no-data-present").show();
                //else part of ajax call
            }

        }
    });
}
function viewUpdateClassroomSyallabus() {
    hideAll();
    $("#updateSyllabusDiv").show();
    getClassroomSyllabus();
}
function updateTopic(weekname, topic_no) {
    classroomSyllabus[weekno - 1]["week" + weekno].topics_to_be_covered[topic_no] = $("#updateTopicOfWeek").val();

}
function deleteTopic(weekno, topic_no) {
    debugger
    classroomSyllabus[weekno - 1]["topics_to_be_covered"].splice(topic_no - 1, 1);
    //document.getElementsByClassName("fa-trash")[1].parentNode.parentNode.remove();
    updateClassroomSyllabus();
}
function removeWeek(weekno) {
    classroomSyllabus.splice(weekno - 1, 1);
    updateClassroomSyllabus();
}
var topic_no_to_be_updated;
var week_no_to_be_updated;
function requestUpdateTopic(weekno, topic_no) {
    topic_no_to_be_updated = topic_no;
    week_no_to_be_updated = weekno;
    $("#classroomUpdateTopicModal").modal("show");
    $("#updateCurrentWeekTopic").val(classroomSyllabus[weekno - 1]["topics_to_be_covered"][topic_no - 1]);
}
function updateTopic() {
    if ($("#updateCurrentWeekTopic").val() == "") {
        return;
    }
    classroomSyllabus[week_no_to_be_updated - 1]["topics_to_be_covered"][topic_no_to_be_updated - 1] = $("#updateCurrentWeekTopic").val();
    $("#updateCurrentWeekTopic").val("");   
    updateClassroomSyllabus();
}
function addTopicToWeek() {
    if ($("#currentWeekTopic").val() == "") {
        return;
    }
    var currentTopic = $("#currentWeekTopic").val();
    debugger
    classroomSyllabus[currentWeekNo - 1].topics_to_be_covered.push(currentTopic);
    $("#week" + currentWeekNo + "-body").append('<li class="h4 list-group-item"><button class="btn p-1" onclick="deleteTopic(' + currentWeekNo + ',' + classroomSyllabus[currentWeekNo - 1].topics_to_be_covered.length + ')"><i class="fa fa-trash"></i></button><button class="btn p-1" onclick="requestUpdateTopic(' + currentWeekNo + ',' + classroomSyllabus[currentWeekNo - 1].topics_to_be_covered.length + ')"><i class="fa fa-pencil"></i></button>'

        +currentTopic+ '</li>');
    $("#currentWeekTopic").val("");
    $('#currentWeekTopicPreviewUrl').val("");
}
function getClassroomSyllabus() {

    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "/api/v1/instructor/GetClassroomSyllabus?ClassroomId=".concat(localStorage.getItem("classroom_id")),
        success: function (data) {
            $("#spinnerClassroomClassroomSchedule").remove();
            debugger
            if (data != null && data.response_code == 1) {
                setClassroomSchedule(data.classroom_syllabus_response.week_wise_schedule);
            }
            else {

            }
        }
    });
}
function updateClassroomSyllabus() {

    var _data = {
        "week_wise_schedule": classroomSyllabus,
        "classroom_id": localStorage.getItem("classroom_id")
    };
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(_data),
        url: "/api/v1/instructor/UpdateClassroomSyllabus",
        success: function (data) {
            debugger
            if (data != null && data.response_code == 1) {
                $("#infoMessage").text("classroom details updated successfully");
                $("#successModal").modal("show");
                getClassroomSyllabus();
            }
            else {

            }
        }
    });
}
var classroomSyllabus = [];
var currentWeekNo = 0;

function addNewWeek() {
    debugger
    var week = "week ".concat(classroomSyllabus.length + 1);
    var weekId = "week".concat(classroomSyllabus.length + 1);
    var data = {
        "week_name": week,
        "topics_to_be_covered": new Array()
    }
    classroomSyllabus.push(data);
    console.log(classroomSyllabus);
    var row = '<div class="card-header" id="collapse-week-header' + weekId + '">'
        + '<h2 class="mb-0">'
        + '<button class="btn btn-link text-left" type="button" data-toggle="collapse" data-target="#collapse-week-body' + weekId + '" aria-expanded="true" aria-controls="collapse-week-body' + weekId + '">' +
        week
        + '</button> '
        + '<span><button class="btn btn-outline-primary p-1 btn-sm text-left" onclick="requestAddNewTopicToWeek(' + classroomSyllabus.length + ')"><i class="fa fa-plus-circle"></i>'
        + 'add topic'
        + '</button></span>'
        + '<span><button class="btn btn-outline-danger btn-sm p-1 ml-2  text-left"  onclick="removeWeek(' + classroomSyllabus.length + ')"><i class="fa fa-trash"></i>'
        + ''
        + '</button></span>'
        + '</h2>'
        + '</div>';
    row += ' <div id="collapse-week-body' + weekId + '" class="collapse" aria-labelledby="headingOne" data-parent="#collapse-week-header' + weekId + '">'
        + '<div class="card-body">'
        + '<ul class="list-group" id="' + weekId + '-body">'
        + '</ul>'
        + ' </div>'
        + ' </div>'
        + ' </div>';
    $("#classroom-syllabus-container").append(row);
}
function requestAddNewTopicToWeek(weekNo) {
    currentWeekNo = weekNo;
    $("#classroomTopicModal").modal("show");
}
function setClassroomSchedule(data) {
    $("#classroom-syllabus-container").empty();
    classroomSyllabus = [];
    for (var i = 0; i < data.length; i++) {
        addNewWeek();
        currentWeekNo = i + 1;
        for (var j = 0; j < data[i].topics_to_be_covered.length; j++) {
            $("#currentWeekTopic").val(data[i].topics_to_be_covered[j]);
            addTopicToWeek();
        }
    }
}
function sendClassroomNotification() {
    hideAll();
    $("#sendClassroomNotification").show();
}
function sendClassroomSMSNotificationToAllStudents() {

    if ($("#classroomNotification").val() == null || $("#classroomNotification").val() == "") {
        return;
    }
    var _data = {
        "notification": $("#classroomNotification").val(),
        "classroom_id": localStorage.getItem("classroom_id")
    };
    $.ajax({
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('access_token') },
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(_data),
        url: "/api/v1/instructor/SendClassroomNotificationToStudents",
        success: function (data) {
            debugger
            if (data != null && data.response_code == 1) {
                $("#infoMessage").text("classroom details updated successfully");
                $("#successModal").modal("show");
                getClassroomSyllabus();
            }
            else {

            }
        }
    });
}
function setNavLinkBehavior() {
    $('.nav-link').on('click', event => {
        $('.nav-link').each(function () {
            $(this).removeClass("active");
        });
        const clickedElement = $(event.target);
        clickedElement.addClass("active");
    });
}
setNavLinkBehavior();