<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Your_testM.aspx.cs" Inherits="school_web.Student_Profile.webview.Your_testM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online Test</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="keywords" content="Online Testing Portal" />
    <meta name="description" content="Online Testing Portal" />
    <meta http-equiv='cache-control' content='no-cache' />
    <meta http-equiv='expires' content='0' />
    <meta http-equiv='pragma' content='no-cache' />

    <script src="js/jquery-1.10.2.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Online_Test_admin/Doc/font-awesome-4.6.3/css/font-awesome.css" rel="stylesheet" />
    <link href="css/bootstrap-theme.min.css" rel="stylesheet" />

    <style>
        body, #form1 {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            font-family: sans-serif, Arial;
        }

        .main {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .main_iner {
            margin: 0px auto;
            padding: 0px;
            height: auto;
            width: 100%;
        }

        .maininercons {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .border {
            margin: 20px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 936px;
            padding: 5px;
            border: 2px solid #070202;
            float: left;
            position: relative;
        }

        .row {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .table {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .grd th {
            padding: 10px;
        }

        .grd td {
            padding: 2px;
            text-align: center;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border-top: 1px solid #ddd;
        }

        @media print {
            .noPrint {
                display: none;
            }
        }

        .pag_num h2 {
            color: #006600;
            font: normal bold 14px Ebrima;
            margin: 5px 0;
            padding: 0 0 0 3px;
            width: 120px;
            float: left;
        }

        .pag_num h3 {
            color: #8e8b8b;
            font: normal bold 14px Ebrima;
            margin: 5px 0;
            padding: 0 0 0 3px;
        }

        .pag_num h4 {
            color: #e60909;
            font: normal bold 14px Ebrima;
            margin: 11px 0 0 0px;
            padding: 0 0 0 3px;
            font-size: 12px;
        }

        .pag_num h5 {
            color: purple;
            font: normal bold 14px Ebrima;
            margin: 11px 0 0 0px;
            padding: 0 0 0 3px;
            font-size: 12px;
        }

        .nav-tabs {
            border-bottom: 0px solid #ddd;
        }

        #circle_main {
            width: 100%;
            height: 135px;
            margin: 0px 0px 0px 0px;
            padding: 0px;
            float: left;
        }

        #circle {
            width: 90px;
            height: 90px;
            margin: 0px auto;
            padding: 0px;
            float: none;
            background: #ececec;
            border-radius: 100%;
            border: solid 1px #999;
            font: normal 18px Ebrima;
            color: #333;
            line-height: 34px;
            text-align: center;
        }



        .glyphicon-ok:before {
            /*color: purple;*/
            color: green;
        }
    </style>

    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }

        function changeHashOnLoad() {
            window.location.href += "#";
            setTimeout("changeHashAgain()", "50");
        }

        function changeHashAgain() {
            window.location.href += "1";
        }

        var storedHash = window.location.hash;
        window.setInterval(function () {
            if (window.location.hash != storedHash) {
                window.location.hash = storedHash;
            }
        }, 50);


        //document.onkeydown = function () {
        //    switch (event.keyCode) {
        //        case 116: //F5 button
        //            event.returnValue = false;
        //            event.keyCode = 0;
        //            return false;
        //        case 82: //R button
        //            if (event.ctrlKey) {
        //                event.returnValue = false;
        //                event.keyCode = 0;
        //                return false;
        //            }
        //        case 117: //F6 button
        //            if (event.ctrlKey) {
        //                event.returnValue = false;
        //                event.keyCode = 0;
        //                return false;
        //            }
        //        case 123: //F12 button
        //            if (event.ctrlKey) {
        //                event.returnValue = false;
        //                event.keyCode = 0;
        //                return false;
        //            }
        //        case 67: //C button
        //            if (event.ctrlKey) {
        //                event.returnValue = false;
        //                event.keyCode = 0;
        //                return false;
        //            }
        //        case 85: //U button
        //            if (event.ctrlKey) {
        //                event.returnValue = false;
        //                event.keyCode = 0;
        //                return false;
        //            }
        //        case 86: //V button
        //            if (event.ctrlKey) {
        //                event.returnValue = false;
        //                event.keyCode = 0;
        //                return false;
        //            }
        //        case 17: //ctrl button
        //            if (event.ctrlKey) {
        //                event.returnValue = false;
        //                event.keyCode = 0;
        //                return false;
        //            }
        //    }
        //}

        //window.history.forward();
        //function noBack() {
        //    window.history.forward();
        //}

        //function disableF5(e) {
        //    if ((e.which || e.keyCode) == 116) e.preventDefault();
        //};
        //document.onkeydown = disableF5;


        //document.onmousedown = disableclick;
        //function disableclick(event) {
        //    if (event.button == 2) {
        //        alert("Right Click Disabled");
        //        return false;
        //    }
        //}
    </script>

    <style>
        .left_section {
            width: 73%;
            margin: 0px;
            padding: 0px;
            float: left;
        }

        .right_section {
            width: 27%;
            margin: 0px;
            padding: 0px;
            border: 1px solid #d4eaf5;
            background-color: #edf8fd;
            float: left;
            min-height: 522px;
        }

            .right_section .user {
                width: 100%;
                margin: 0px;
                padding: 4px 5px 4px 5px;
                border-bottom: 1px solid #d4eaf5;
                float: left;
                height: 37px;
            }

            .right_section .question_nubering {
                width: 100%;
                margin: 0px;
                padding: 5px;
                float: left;
            }

        .head_tab {
            width: 100%;
            margin: 0px;
            padding: 0px;
            float: left;
        }

            .head_tab ul.tab {
                list-style-type: none;
                margin: 0;
                padding: 0;
                overflow: hidden;
                border: 1px solid #ccc;
                background-color: #f1f1f1;
                border-right: none;
            }

                /* Float the list items side by side */
                .head_tab ul.tab li {
                    float: left;
                }

                    /* Style the links inside the list items */
                    .head_tab ul.tab li a {
                        display: inline-block;
                        color: black;
                        text-align: center;
                        padding: 7px 5px;
                        text-decoration: none;
                        transition: 0.3s;
                        font-size: 15px;
                    }

                        .head_tab ul.tab li a span {
                            border-right: 1px solid #ccc;
                            padding-right: 10px;
                        }
                        /* Change background color of links on hover */
                        .head_tab ul.tab li a:hover {
                            background-color: #ddd;
                            color: black;
                        }

                        /* Create an active/current tablink class */
                        .head_tab ul.tab li a:focus, .active {
                            background-color: #0284ab;
                            color: #fff !important;
                        }


        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 0px solid #ccc;
            border-top: none;
        }

        .marks_p {
            background-color: #0893f5;
            color: #fff;
            padding: 2px 8px;
            border-radius: 30% 30%;
        }

        .marks_n {
            background-color: #e60a0a;
            color: #fff;
            padding: 2px 8px;
            border-radius: 30% 30%;
        }


        .nav-tabs > li {
            margin: 5px;
            margin-bottom: 0px;
            border-bottom-width: 1px;
            border-radius: 0px;
            background-color: #8e8b8b;
            color: #fff;
            border: 1px solid #FCF6F6;
        }

            .nav-tabs > li.active {
                border: 1px solid #bcff08;
            }

                .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
                    color: #fff;
                    cursor: pointer;
                    background-color: #8e8b8b;
                    border: 0px solid #ddd;
                    font-weight: bold;
                    text-shadow: 1px 2px #000;
                }

        .nav > li > a:focus, .nav > li > a:hover {
            background: none;
        }

        .nav-tabs > li > a {
            border: 0px solid transparent;
            border-radius: 0px 0px 0 0;
            margin-right: 0px;
            color: #fff;
            padding: 3px;
            width: 40px;
            text-align: center;
            font-size: 14px;
            border: 0px;
        }

        .pag_num h2 {
            color: #006600;
            font: normal bold 14px Ebrima;
            margin: 5px 0;
            padding: 0 0 0 3px;
            width: 120px;
            float: left;
        }

        .pag_num h3 {
            color: #000;
            font: normal bold 14px Ebrima;
            margin: 5px 0;
            padding: 0 0 0 3px;
        }

        .pag_num h4 {
            color: #e60909;
            font: normal bold 14px Ebrima;
            margin: 11px 0 0 0px;
            padding: 0 0 0 3px;
            font-size: 12px;
        }

        .nav-tabs {
            border-bottom: 0px solid #ddd;
        }

        p {
            margin: 0 10px 10px;
            color: black !important;
        }

        .tab-content > .active {
            background: none;
        }

        .right_menu_scroll {
            width: 100%;
            float: left;
            height: 350px;
            margin: 0px;
            overflow-y: auto;
            padding-right: 10px;
        }

        .show {
            display: block;
        }

        .hide {
            display: none;
        }

        .not-active {
            pointer-events: none;
            cursor: default;
            text-decoration: none;
            color: black;
        }
    </style>

    <script src="../js/jquery.blockUI.js"></script>
   
    <script src="../js/Cookie_datas.js"></script>

    <%--  <script type="text/ecmascript">
        var validNavigation = false;
        function wireUpEvents() {
            if (!validNavigation) {
                window.onbeforeunload = function () {
                    myfun();
                    return 'Are you sure you want to leave?';
                };
            }
              
        }
        function unhook() {
            validNavigation = true;
        }
        function myfun() {
            alert("00");
        }
        // Wire up the events as soon as the DOM tree is ready
        $(document).ready(function () {
            wireUpEvents();
        });
    </script>--%>

    <script type="text/javascript">
        var hook = true;
        window.onbeforeunload = function () {

            if (hook) {

                Insertandupdatepausedata();//for resume status
                return "Thank you for visit."
            }
        }
        function unhook() {
            hook = false;
        }
        function Insertandupdatepausedata() {

            var studentid = $("#<%=hd_studentid.ClientID%>").val();
            var testid = $("#<%=hd_testid.ClientID%>").val();
            var examcode = $("#<%=hd_examcode.ClientID%>").val();
            var hd_entry_id = $("#<%=hd_entry_id.ClientID%>").val();
            var hd_package_id = $("#<%=hd_package_id.ClientID%>").val();
            var hd_attempt_id = $("#<%=hd_attempt_id.ClientID%>").val();

            var jsondata = JSON.stringify(myarray);
            var browser = get_browser_info();
            var json_not_visit_ar = JSON.stringify(not_visit_ar);
            var json_attempt_ar = JSON.stringify(attempt_ar);
            var json_not_attempt = JSON.stringify(not_attempt);
            var json_review_mark = JSON.stringify(review_mark);
            var json_review_ar = JSON.stringify(review_ar);
            var json_selected_review = JSON.stringify(selected_review);
            var json_marked = JSON.stringify(marked);
            var json_review = JSON.stringify(review);

            $.ajax({

                type: "POST",
                url: "Onlinetestapi/Test_taking_code.asmx/browser_cute_savepause_data",
                data: "{'entryid':" + hd_entry_id + ",'packageid':'" + hd_package_id + "','sectionid':'" + cur_sec_id + "','testid':'" + testid + "','studentid':'" + studentid + "','jsondata':'" + jsondata + "','examcode':'" + examcode + "','seconds':'" + seconds + "','browser':'" + browser.name + "','curqueid':'" + cur_que_id + "','curqueno':'" + cur_que_no + "','_notvisitar':'" + json_not_visit_ar + "','_attemptar':'" + json_attempt_ar + "','_notattempt':'" + json_not_attempt + "','_reviewmark':'" + json_review_mark + "','_reviewar':'" + json_review_ar + "','_selectedreview':'" + json_selected_review + "' ,'_marked':'" + json_marked + "','_review':'" + json_review + "','hdattemptid':'" + hd_attempt_id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == "0") {
                    }
                }
            });
        }

        window.setInterval(function () {
            var data = $("#<%=hd_start_test.ClientID%>").val();

            if (data != "0") {
                Insertandupdatepausedata();//for resume status
            }
        }, 10000);
    </script>

    <style>
        @media screen and (max-width: 992px) {

            .hideimg {
                display: none;
            }

            .testname {
                display: none;
            }

            .user {
                display: none;
            }

            .right_section {
                width: 100%;
            }

            .left_section {
                width: 100%;
            }

            #menu_en1 {
                height: auto !important;
            }

            .questionview {
                width: 100%;
                float: left;
                height: auto !important;
                color: #000;
                margin: 0px;
                overflow-y: auto;
                padding: 0px;
            }
        }
    </style>
</head>
<body onload="changeHashOnLoad();">


    <form id="form1" runat="server">
        <asp:HiddenField ID="hd_df_lang_con" runat="server" />
        <div id="domMessage" style="display: none;">
            <h1 style="font-size: 20px;">

                <img src="../../Online_Test_admin/Doc/ld_img.gif" style="width: 50px" /><br />
                <span>We are preparing your result</span><br />
                Please Wait...</h1>
        </div>
        <div class="main">
            <div class="main_iner">
                <div class="maininercons">
                    <div class="row" style="color: red">
                        <div class=" col-md-6 col-lg-6 col-sm-12 col-xs-12 ">
                            <asp:Image ID="Image2" runat="server" class="img-responsive hideimg" Style="height: 36px; float: left;" />
                            <%--<img src="../images/logo.png" class="img-responsive" style="height: 76px">--%>
                        </div>
                        <div class=" col-md-6 col-lg-6 col-sm-12 col-xs-12 ">
                            <div class=" col-md-6 col-lg-6 col-sm-12 col-xs-12 ">
                                <h2 style="margin: 0px; font-weight: bold;">
                                    <span style="font-size: 15px;"><i class="fa fa-clock-o" aria-hidden="true"></i>Total Time  : </span>
                                    <asp:Label ID="lbl_time" runat="server" Style="font-size: 15px;"></asp:Label>
                                </h2>
                            </div>
                            <div class=" col-md-6 col-lg-6 col-sm-12 col-xs-12 ">
                                <h2 style="margin: 0px; font-weight: bold">
                                    <span style="font-size: 15px;"><i class="fa fa-clock-o" aria-hidden="true"></i>Section Time  : </span>
                                    <span style="font-size: 15px;" id="countdown"></span>
                                </h2>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField5" runat="server" />
                        <asp:HiddenField ID="hd_studentid" runat="server" />
                        <asp:HiddenField ID="hd_section" runat="server" />
                        <asp:HiddenField ID="hd_time" runat="server" />
                        <asp:HiddenField ID="hd_time_type" runat="server" />
                    </div>
                    <div class="row" style="border-top: 1px solid #e8e5e5">
                        <div class=" col-md-3 col-lg-3 col-sm-12 col-xs-12 " style="padding: 10px 0px 10px 10px;">
                            <div style="display: none">
                                Change Default Language -
                            <asp:DropDownList ID="ddl_default_language" runat="server">
                                <asp:ListItem Value="0">English</asp:ListItem>
                                <asp:ListItem Value="1">Hindi</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-6 col-sm-12 col-xs-12 testname " style="padding: 10px 0px 10px 0px; text-align: center;">
                            <asp:Label ID="lbl_testname" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                        </div>
                        <div class=" col-md-3 col-lg-3 col-sm-12 col-xs-12 " style="padding: 10px 10px 10px 10px;">
                            <a href="javascript:" id="stop" class="btn btn-warning" style="float: left; display: none"><i class="fa fa-pause" aria-hidden="true"></i>&nbsp;Pause</a>

                            <a href="javascript:" onclick="end_test()" class="btn btn-info" style="float: left;">Final Submit</a>

                        </div>
                    </div>
                    <script src="../js/Browser_details.js"></script>

                    <%-- <script  type="text/javascript" async src="../MathJax275/MathJax-2.7.5/MathJax.js?config=MML_HTMLorMML-full"></script>--%>
                    <script type="text/javascript" async src="https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.1/MathJax.js?config=MML_HTMLorMML-full">
                    </script>



                    <div class="row">

                        <script type="text/javascript">


                            //1. user id var - u1
                            //2. test id -u2
                            //3. question answer array u3
                            //	a. sec_id
                            //	b. q_id
                            //	c. ans_id	
                            //	d.status = ( notvi-0,not_att-1,review-2,atte-3,revi_mar-4)
                            //  e. time_duration


                            var resume_data = "";
                            var resume_status = "";

                            var countdownTimer = "0";
                            $(document).ready(function () {

                                //FOR RESUME DATA

                                var my_loc_data = localStorage.getItem('my_local_storge');
                                //alert(my_loc_data);

                                resume_status = $("#<%=hd_resume_status.ClientID%>").val();
                                if (resume_status == "1") {
                                    resume_data = $("#<%=hd_resume_data.ClientID%>").val();
                                    myarray = JSON.parse(resume_data);

                                    featch_data_from_cookie();
                                }
                                cur_sec_id = $("#<%=hd_cur_secid.ClientID%>").val();
                                cur_que_id = $("#<%=hd_cur_queid.ClientID%>").val();
                                cur_que_no = $("#<%=hd_cur_queno.ClientID%>").val();
                                //FOR RESUME DATA



                                var test_no = $("#<%=HiddenField5.ClientID%>").val();

                                var studentid = $("#<%=hd_studentid.ClientID%>").val();

                                //Bank,SSC,RAILWAY
                                var examname = $("#<%=hd_exam_category.ClientID%>").val();


                                //PT,MAINS
                                var exam_type = $("#<%=hd_exam_type.ClientID%>").val();


                                if (examname == "0") {
                                    bind_heading_for_bank();
                                    start_section_wise_timing_for_bank();
                                }
                                else {
                                    bind_heading_for_other();
                                    start_section_wise_timing_for_other();
                                }

                                $("#stop").click(function () {

                                    $.blockUI({
                                        message: $('#pauseblock'),
                                        css: {
                                            border: 'none',
                                            backgroundColor: '#fff',
                                        }
                                    });

                                    clearInterval(countdownTimer);
                                    countdownTimer = null;

                                    $("#<%=hd_start_test.ClientID%>").val("0");
                                    $("#<%=HiddenField4.ClientID%>").val("0");
                                    Insert_and_update_pause_data("0");// )0 for resume status

                                });

                                function featch_data_from_cookie() {

                                    var cooki_data = getCookie("Pause_and_play");
                                    //alert(cooki_data);
                                    if (JSON.parse(cooki_data.split("&")[0]) != "0") {
                                        not_visit_ar = JSON.parse(cooki_data.split("&")[0]);
                                    }
                                    if (JSON.parse(cooki_data.split("&")[1]) != "0") {
                                        attempt_ar = JSON.parse(cooki_data.split("&")[1]);
                                    }
                                    if (JSON.parse(cooki_data.split("&")[2]) != "0") {
                                        not_attempt = JSON.parse(cooki_data.split("&")[2]);
                                    }
                                    if (JSON.parse(cooki_data.split("&")[3]) != "0") {
                                        review_mark = JSON.parse(cooki_data.split("&")[3]);
                                    }
                                    if (JSON.parse(cooki_data.split("&")[4]) != "0") {
                                        review_ar = JSON.parse(cooki_data.split("&")[4]);
                                    }
                                    if (JSON.parse(cooki_data.split("&")[5]) != "0") {
                                        selected_review = JSON.parse(cooki_data.split("&")[5]);
                                    }
                                    if (JSON.parse(cooki_data.split("&")[6]) != "0") {
                                        marked = JSON.parse(cooki_data.split("&")[6]);
                                    }
                                    if (JSON.parse(cooki_data.split("&")[7]) != "0") {
                                        review = JSON.parse(cooki_data.split("&")[7]);
                                    }

                                }
                                function Insert_and_update_pause_data(type) {

                                    var studentid = $("#<%=hd_studentid.ClientID%>").val();
                                    var testid = $("#<%=hd_testid.ClientID%>").val();
                                    var examcode = $("#<%=hd_examcode.ClientID%>").val();
                                    var hd_entry_id = $("#<%=hd_entry_id.ClientID%>").val();
                                    var hd_package_id = $("#<%=hd_package_id.ClientID%>").val();
                                    var hd_attempt_id = $("#<%=hd_attempt_id.ClientID%>").val();

                                    var jsondata = JSON.stringify(myarray);

                                    var browser = get_browser_info();

                                    if (type == "0") {

                                        var json_not_visit_ar = JSON.stringify(not_visit_ar);
                                        var json_attempt_ar = JSON.stringify(attempt_ar);
                                        var json_not_attempt = JSON.stringify(not_attempt);
                                        var json_review_mark = JSON.stringify(review_mark);
                                        var json_review_ar = JSON.stringify(review_ar);
                                        var json_selected_review = JSON.stringify(selected_review);
                                        var json_marked = JSON.stringify(marked);
                                        var json_review = JSON.stringify(review);

                                        $.ajax({
                                            type: "POST",
                                            url: "Onlinetestapi/Test_taking_code.asmx/save_pause_data",
                                            data: "{'entryid':'" + hd_entry_id + "','packageid':'" + hd_package_id + "','sectionid':'" + cur_sec_id + "','testid':'" + testid + "','studentid':'" + studentid + "','jsondata':'" + jsondata + "','examcode':'" + examcode + "','seconds':'" + seconds + "','browser':'" + browser.name + "','curqueid':'" + cur_que_id + "','curque_no':'" + cur_que_no + "','_notvisitar':'" + json_not_visit_ar + "','_attemptar':'" + json_attempt_ar + "','_notattempt':'" + json_not_attempt + "','_reviewmark':'" + json_review_mark + "','_reviewar':'" + json_review_ar + "','_selectedreview':'" + json_selected_review + "' ,'_marked':'" + json_marked + "','_review':'" + json_review + "','hdattemptid':'" + hd_attempt_id + "'}",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (response) {
                                                if (response.d == "0") {
                                                }
                                            }
                                        });
                                    } else {
                                        $.ajax({
                                            type: "POST",
                                            url: "Onlinetestapi/Test_taking_code.asmx/update_pause_data",
                                            data: "{'entryid':'" + hd_entry_id + "','packageid':'" + hd_package_id + "','sectionid':'" + cur_sec_id + "','testid':'" + testid + "','studentid':'" + studentid + "','jsondata':'" + jsondata + "','examcode':'" + examcode + "','seconds':'" + seconds + "','browser':'" + browser.name + "','curqueid':'" + cur_que_id + "','curqueno':'" + cur_que_no + "'}",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (response) {
                                                if (response.d == "0") {
                                                }
                                            }
                                        });
                                    }
                                }
                                $("#btn_start").click(function () {
                                    $.unblockUI({ message: $('#pauseblock') });
                                    $("#<%=HiddenField4.ClientID%>").val("0");
                                    Insert_and_update_pause_data("1");// )1 for play status
                                    $("#<%=hd_start_test.ClientID%>").val("1");

                                    //Bank,SSC,RAILWAY
                                    var examname = $("#<%=hd_exam_category.ClientID%>").val();
                                    if (examname == "0") {
                                        start_section_wise_timing_for_bank();
                                    }
                                    else {
                                        start_section_wise_timing_for_other();
                                    }
                                });
                            });

                            //CODE FOR BANKING
                            function start_section_wise_timing_for_bank() {
                                countdownTimer = window.setInterval(function () {
                                    secondPassed();
                                }, 999);

                                function secondPassed() {

                                    if (seconds != 0) {
                                        seconds--;

                                        var minutes = parseInt(seconds / 60);
                                        var remainingSeconds = parseInt(seconds % 60);
                                        if (remainingSeconds < 10) {
                                            remainingSeconds = "0" + parseInt(remainingSeconds);
                                        }


                                        document.getElementById('countdown').innerHTML = minutes + " mins : " + remainingSeconds + " secs";
                                        if (parseInt(seconds) === 0) {

                                            clearInterval(countdownTimer);
                                            document.getElementById('countdown').innerHTML = "Time is Over for this section.";

                                            if (parseInt(sec_index) == parseInt(tot_sec)) {
                                                end_test();
                                            } else {

                                                //FOR SECTION ACTIVE & DE-ACTIVE
                                                var pre_sec_id = store_sectionid[parseInt(sec_index) - 1];
                                                $("#head_" + pre_sec_id).removeClass("active");
                                                $("#head_" + pre_sec_id).addClass("not-active");

                                                var sec_id = store_sectionid[sec_index];
                                                $("#head_" + sec_id).removeClass("not-active");
                                                $("#head_" + sec_id).addClass(" active");

                                                //FRO RIGHT SIDE QUESTION NUMBERING SHOW & HIDE
                                                $("#rg" + pre_sec_id).removeClass("show");
                                                $("#rg" + pre_sec_id).addClass("hide");
                                                $("#rg" + sec_id).removeClass("hide");
                                                $("#rg" + sec_id).addClass("show");

                                                // SECTION WAISE TIMING 
                                                $("#<%=hd_time.ClientID%>").val(store_section_time[sec_index]);
                                                $("#<%=lbl_time.ClientID%>").text(store_section_time[sec_index] + " " + sec_time_type);
                                                sec_index = parseInt(sec_index) + 1;

                                                var selector = "#head_" + sec_id;
                                                $(selector).click();
                                            }
                                        }
                                    }
                                }
                            }

                            //DEFAULT LANGUAGE
                            var df_language = $("#ddl_default_language").find("option:selected").text();


                            //GLOBAL VARIABLE
                            var tme_count = 0;
                            var cur_sec_id = "";
                            var cur_que_id = "";
                            var cur_que_no = "";
                            var myarray = [];
                            var mystring = "";
                            var seconds = 0;
                            var sec_time_type = "";
                            var section_id = "";
                            var default_enid = "";//use for Hindi language
                            var default_hnid = "";//use for English language
                            var n_qid = "";
                            var pre_qid = "";
                            var visit_qid = "";
                            var pre_liid = "";
                            var new_tot_myans = "";
                            var tot_ct = 0;
                            var pre_secid = "";
                            var s_qid = 0;
                            var s_ansid = 0;
                            var set_qtime = 0;
                            var set_mtime = 0;
                            var set_stime = 0;
                            var selected_value = [];
                            var pre5_qid = [];//SECTION WISE FIRST QUSTION NO 

                            //GLOBAL ARRAY FOR SECTION ID
                            var store_sectionid = [];
                            var sec_index = 0;
                            var tot_sec = 0;
                            //GLOBAL ARRAY FRO SECTION WISE TIME
                            var store_section_time = [];
                            var questions = [];


                            function bind_heading_for_bank() {
                                alert(1);
                                $.blockUI({ message: $('#pleasewait') });
                                var i = 0;
                                var ii = 0;
                                var test_mode = $("#<%=hd_testmode.ClientID%>").val();
                                var examcode = $("#<%=hd_examcode.ClientID%>").val();
                                var examtype_code = $("#<%=hd_examtype_code.ClientID%>").val();

                                var testid = $("#<%=hd_testid.ClientID%>").val();
                                var studentid = $("#<%=hd_studentid.ClientID%>").val();

                                if (resume_status == "0") {
                                    mystring = studentid + "#" + testid + "#";
                                    myarray.push(mystring);
                                    //alert(myarray);
                                }
                                $.ajax({

                                    type: "POST",
                                    url: "Onlinetestapi/WebService1.asmx/bind_top_heading_tabbing",
                                    data: "{'testid':'" + testid + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {
                                        setTimeout($.unblockUI, 1000);
                                        var json = response.d;

                                        if (json != "[]") {
                                            $('#head_tab').empty();
                                            $('#question_nubering').empty();

                                            var maindiv = $('<div></div>').addClass('row');
                                            var section_head = $('<ul></ul>').addClass('tab');
                                            var my_secarray = [];
                                            $.each($.parseJSON(json), function (idx, obj) {

                                                var ik = 0;
                                                //Sections design coding START
                                                ii++;
                                                var sec_id = obj.id;
                                                var sec_name = obj.sec_name;

                                                var sec_time = obj.time;
                                                sec_time_type = obj.time_type;

                                                tot_sec = obj.tot_sec;
                                                store_sectionid.push(sec_id);
                                                store_section_time.push(sec_time);


                                                var header = "";
                                                if (ii == 1) {
                                                    section_id = sec_id;
                                                    header = $('<li></li>').html('<a href="javascript:void(0)" class="tablinks active" id="head_' + sec_id + '"  onclick="open_section_for_bank(event, ' + sec_id + ',' + ii + ')" ><span> ' + sec_name + '</span></a>');

                                                    //FIRST SECTION TIME SET
                                                    $("#<%=hd_time.ClientID%>").val(sec_time);
                                                    $("#<%=hd_time_type.ClientID%>").val(sec_time_type);

                                                    $("#<%=lbl_time.ClientID%>").text(sec_time + " " + sec_time_type);

                                                    sec_index = 1;

                                                }
                                                else {

                                                    header = $('<li></li>').html('<a href="javascript:void(0)"  class="tablinks not-active" id="head_' + sec_id + '" onclick="open_section_for_bank(event, ' + sec_id + ',' + ii + ')" ><span> ' + sec_name + '</span></a>');
                                                }

                                                section_head.append(header);
                                                $('#head_tab').append(section_head);

                                                MathJax.Hub.Queue(["Typeset", MathJax.Hub, "head_tab"]);
                                                //Sections design coding END


                                                //Sections wise container design coding START
                                                var div_tabcontent = "";
                                                if (ii == 1) {
                                                    div_tabcontent = $('<div id=' + sec_id + ' style="display: block; float:left; width:100%;"></div>').addClass('tabcontent');

                                                }
                                                else {
                                                    div_tabcontent = $('<div id=' + sec_id + ' style="float:left; width:100%;"></div>').addClass('tabcontent');
                                                }

                                                //LOOP FOR QUESTION DESIGNING START
                                                var question_block = $('<div id=lf' + sec_id + ' style="float:left;"></div>').addClass("row");
                                                var tab_cnt_div = $('<div></div>').addClass('tab-content');


                                                //FOR RIGHT SIDE QUESTION NUMBERING AND BUTTON DESIGN
                                                var tab_sec = "";
                                                if (ii == 1) {
                                                    tab_sec = $('<div id=rg' + sec_id + ' class="show"  style="width:100%;  float:left;"></div>').html('<p>Total Question- ' + obj.ques.length + '</p>');
                                                } else {
                                                    tab_sec = $('<div id=rg' + sec_id + ' class="hide" style="width:100%;  float:left;"></div>').html('<p>Total Question- ' + obj.ques.length + '</p>');
                                                }
                                                var tot_qs = parseInt(obj.ques.length);//NOT VISIT SECTION WISE TOTAL QUESTION


                                                //COUNTING Attempted,Not Visited,Not Attempted, Review,Review & Answer
                                                var attempted = attempt_ar.length;
                                                if (attempt_ar.length == undefined) { attempted = 0; }

                                                var notvisited = not_visit_ar.length;
                                                if (not_visit_ar.length == undefined) { notvisited = tot_qs; }
                                                else { notvisited = 0; }

                                                var notattempted = not_attempt.length;
                                                if (not_attempt.length == undefined) { notattempted = 0; }

                                                var review = review_ar.length;
                                                if (review_ar.length == undefined) { review = 0; }

                                                var reviewmark = review_mark.length;
                                                if (review_mark.length == undefined) { reviewmark = 0; }

                                                var bottom_cnt = $('<div class="row" style="margin: 0px 0px 5px 0px; border-top: 1px solid #f1f1f1; border-bottom: 1px solid #f1f1f1; padding: 5px;"></div>').html('<table style="width: 100%;"><tr class="pag_num"><td style="vertical-align: top"><h4><div id=atp' + sec_id + ' style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: #060; float: left; margin-right: 5px;  color:#fff">' + attempted + '</div> Attempted</h4></td><td colspan="2"><h4><div id=nv' + sec_id + '  style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: #8e8b8b; float: left; margin-right: 5px; color:#fff">' + notvisited + '</div>Not Visited</h4></td></tr><tr class="pag_num"> <td><h4><div id=nat' + sec_id + ' style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: red; float: left; margin-right: 5px;  color:#fff">' + notattempted + '</div>Not Attempted</h4></td><td><h4><div id=rev' + sec_id + ' style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: purple; float: left; margin-right: 5px;  color:#fff">' + review + '</div>Review</h4></td><td> <h4> <div id=revm' + sec_id + '  class="glyphicon glyphicon-ok" style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: purple; float: left; margin-right: 5px;  color:#fff">' + reviewmark + '</div>Review & Answer</h4></td></tr></table>');
                                                tab_sec.append(bottom_cnt);//NOT VISIT SECTION WISE TOTAL QUESTION

                                                var ul = $('<ul></ul>').addClass('nav nav-tabs right_menu_scroll');

                                                //question_nubering
                                                var my_ques = "";
                                                for (var k = 0; k < obj.ques.length; k++) {

                                                    i++;
                                                    ik++;//USING FOR FIRST QUESTION IS VISIBLE

                                                    //for question numbering
                                                    var qid = obj.ques[k].qid;
                                                    questions.push(qid);

                                                    var path = "#menu" + i;
                                                    var li = "";


                                                    my_ques = my_ques + sec_id + "$" + qid + "$" + "0" + "$" + "0" + "$" + "0";
                                                    my_secarray.push(my_ques);
                                                    my_ques = "";
                                                    ////SECTION WISE FIRST QUSTION NO 
                                                    if (ik == 1) {
                                                        pre5_qid.push(i);
                                                    }
                                                    //if (i == 1) {

                                                    //    n_qid = qid;
                                                    //    pre_qid = i;
                                                    //    pre_liid = i;
                                                    //    $('#que_no').append('1');
                                                    //    li = $('<li id=li' + i + '></li>').addClass('active').html('<a data-toggle="tab" href="' + path + '" id=ls' + i + ' onclick="find_question_for_bank(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');

                                                    //}
                                                    //else {
                                                    //    li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab" href="' + path + '" id=ls' + i + ' onclick="find_question_for_bank(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '   mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                    //}
                                                    if (i == 1) {
                                                        n_qid = qid;
                                                        pre_qid = i;
                                                        pre_liid = i;
                                                        $('#que_no').append('1');
                                                    }

                                                    if (resume_status == "0") {
                                                        li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab" href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '   mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                        not_visit_ar.push(qid);
                                                    }
                                                    else {
                                                        var myar = myarray.length;
                                                        var data = "";
                                                        for (var c = 0; c < myar; c++) {

                                                            if (c == 1) {
                                                                data = myarray[c];

                                                                for (var cc = 0; cc < data.length; cc++) {
                                                                    var sp_data = data[cc];
                                                                    var sp_secid = sp_data.split("$")[0];
                                                                    var sp_qid = sp_data.split("$")[1];
                                                                    var sp_ansid = sp_data.split("$")[2];
                                                                    var sp_status = sp_data.split("$")[3];

                                                                    if (sp_secid == sec_id && sp_qid == qid) {

                                                                        if (sp_status == "1") {

                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab"  style="background:red;" href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }
                                                                        else if (sp_status == "2") {
                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab"  style="background:Purple;"    href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }
                                                                        else if (sp_status == "3") {
                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab"  style="background:Green;"    href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }
                                                                        else if (sp_status == "4") {
                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab"  style="background:purple;"  class="glyphicon glyphicon-ok"  href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }

                                                                        else {
                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab" href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '   mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }

                                                                    }


                                                                }
                                                            }
                                                        }
                                                    }




                                                    $(ul).append(li);
                                                    ////////////////////////


                                                    //FOR QUESTION LISTING
                                                    var qid = obj.ques[k].qid;
                                                    var divid = "menu" + i;
                                                    var divid_en = "menu_en" + i;
                                                    var divid_hn = "menu_hn" + i;

                                                    var default_enid = divid_en;//use for Hindi language
                                                    var default_hnid = divid_hn;//use for English language

                                                    var div = "";

                                                    if (ik == 1) {


                                                        if (cur_sec_id != sec_id) {

                                                            div = $('<div id=' + divid + '  ></div>').addClass('tab-pane fade in active');
                                                        }
                                                        else {
                                                            div = $('<div id=' + divid + '></div>').addClass('tab-pane fade');
                                                        }


                                                        //div = $('<div id=' + divid + '  ></div>').addClass('tab-pane fade in active');
                                                    }
                                                    else {
                                                        div = $('<div id=' + divid + '></div>').addClass('tab-pane fade');
                                                    }


                                                    var qs_content_eng = "";
                                                    var qs_content_hnd = "";


                                                    if (df_language == "English") {

                                                        qs_content_eng = $('<div style="width:100%; float:left; height: auto; margin-bottom:10px;  padding-right: 10px;" class="show" id="' + divid_en + '" ></div>');
                                                        qs_content_hn = $('<div style="width:100%; float:left; height: auto; margin-bottom:10px;  padding-right: 10px;" class="hide" id="' + divid_hn + '" lngtype=' + obj.ques[k].Language_Itype + '></div>');

                                                        p_en = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px; display:none"><input type="radio" style="display:none" name=hin' + divid_hn + '  id=hin' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px; display:none">View Question In Hindi </span>  Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span style="display:none" class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');
                                                        p_hn = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px; display:none"><input type="radio" name=eng' + divid_en + '  id=eng' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In English </span>      Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');

                                                        qs_content_eng.append(p_en);
                                                        qs_content_hn.append(p_hn);

                                                    }
                                                    else {

                                                        //LANGUAGE TYPE SINGLE =0 & DOUBLE =1
                                                        var language_type = obj.ques[k].Language_Itype;

                                                        if (language_type == "1") {


                                                            qs_content_eng = $('<div style="width:100%; float:left;  height: auto; margin-bottom:10px;  padding-right: 10px;" class="hide" id="' + divid_en + '" ></div>');
                                                            qs_content_hn = $('<div style="width:100%; float:left;  height: auto; margin-bottom:10px;  padding-right: 10px;" class="show" id="' + divid_hn + '" lngtype=' + obj.ques[k].Language_Itype + '></div>');

                                                            p_en = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px; display:none"><input type="radio" name=hin' + divid_hn + '  id=hin' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In Hindi </span>  Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');
                                                            p_hn = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px; display:none"><input type="radio" name=eng' + divid_en + '  id=eng' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In English </span>     Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');

                                                            qs_content_eng.append(p_en);
                                                            qs_content_hn.append(p_hn);
                                                        }
                                                        else {

                                                            qs_content_eng = $('<div style="width:100%; float:left;  height: auto; margin-bottom:10px;  padding-right: 10px;" class="show" id="' + divid_en + '" ></div>');
                                                            qs_content_hn = $('<div style="width:100%; float:left;  height: auto; margin-bottom:10px;  padding-right: 10px;" class="hide" id="' + divid_hn + '" lngtype=' + obj.ques[k].Language_Itype + '></div>');


                                                            p_en = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px; display:none"><input type="radio" name=hin' + divid_hn + '  id=hin' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In Hindi </span>  Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');
                                                            p_hn = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px;"><input type="radio" name=eng' + divid_en + '  id=eng' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In English </span>    Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');

                                                            qs_content_eng.append(p_en);
                                                            qs_content_hn.append(p_hn);

                                                        }
                                                    }

                                                    //START DESIGN FOR -DIRECTION,PHRAGES,QUESTION IN ENGLISH 
                                                    var div_blc_eng = $('<div style="width:100%; float:left; height: auto; color:#000; margin:0px; overflow-y:auto; padding: 0px;"></div>');

                                                    //THIS IS FOR DIRECTION
                                                    if (obj.ques[k].Direction != "") {
                                                        var Direction = $('<p style="margin:0px; padding:0px; height:auto;width:100%; float:left;font-weight:bold;color:black;"></p>').html("Direction : " + obj.ques[k].Direction);
                                                        div_blc_eng.append(Direction);
                                                    }

                                                    //THIS IS FOR PHRAGES
                                                    var figure = "";
                                                    if (obj.ques[k].cnt_type == "Img") {

                                                        figure = $('<figure></figure>').html(obj.ques[k].Di);
                                                    }
                                                    else {
                                                        figure = $('<figure></figure>').html(obj.ques[k].Di);
                                                    }

                                                    if (figure == null || figure == "") {
                                                    }
                                                    else {
                                                        div_blc_eng.append(figure);
                                                    }

                                                    //THIS IS FOR QUESTION
                                                    var question = $('<p></p>').html('<span style="float:left">' + obj.ques[k].Question_no + ').</span>' + obj.ques[k].question);
                                                    div_blc_eng.append(question);
                                                    //END DESIGN FOR -DIRECTION,PHRAGES,QUESTION IN ENGLISH 


                                                    //START DESIGN FOR -DIRECTION,PHRAGES,QUESTION IN HINDI
                                                    var div_blc_hn = $('<div class="questionview"></div>');

                                                    //THIS IS FOR DIRECTION
                                                    if (obj.ques[k].Direction_HN != "") {
                                                        var Direc_hn = '<span class="font_hindi_normal"> ' + obj.ques[k].Direction_HN + '</span>';
                                                        var Direction_hn = $('<p style="margin:0px; padding:0px; height:auto;width:100%; float:left;font-weight:bold;color:black;"></p>').html("Direction : " + Direc_hn);
                                                        div_blc_hn.append(Direction_hn);
                                                    }
                                                    //THIS IS FOR PHRAGES
                                                    var figure_hn = "";
                                                    if (obj.ques[k].cnt_type == "Img") {
                                                        figure_hn = $('<figure></figure>').html(obj.ques[k].DI_HN);
                                                    }
                                                    else {
                                                        figure_hn = $('<figure></figure>').html(obj.ques[k].DI_HN);
                                                    }
                                                    if (figure_hn == null || figure_hn == "") {
                                                    }
                                                    else {
                                                        div_blc_hn.append(figure_hn);
                                                    }


                                                    //THIS IS FOR QUESTION
                                                    var ques_hn = '<span class="font_hindi_normal"> ' + obj.ques[k].Question_name_HN + '</span>';
                                                    var question_hn = $('<p></p>').html('<span style="float:left">' + obj.ques[k].Question_no + ').</span>' + ques_hn);
                                                    div_blc_hn.append(question_hn);

                                                    //END DESIGN FOR -DIRECTION,PHRAGES,QUESTION IN HINDI


                                                    //START DESIGN FOR - QUESTION WISE ANSWER IN ENGLISH & HINDI
                                                    var ans = $('<div style="width:100%; float:left;" id=' + qid + '></div>');
                                                    var ans_hn = $('<div style="width:100%; float:left;" id=' + qid + '></div>');

                                                    var numbring = ["A", "B", "C", "D", "E"];
                                                    var numbring_hn = ["A", "B", "C", "D", "E"];//["क", "ख", "ग", "घ", "ङ"];
                                                    for (var j = 0; j < obj.ques[k].answers.length; j++) {
                                                        //ENGLISH OPTIONS
                                                        var radio_btnid = divid + "_" + obj.ques[k].answers[j].opt_id;
                                                        var opt = $('<p></p>').html('<input type="radio" name=' + divid + ' asnno=' + obj.ques[k].answers[j].opt_id + ' id=' + radio_btnid + ' value=' + i + ' onclick="select_answer(this.id,' + qid + ',' + sec_id + ')"> <span> ' + numbring[j] + ') ' + obj.ques[k].answers[j].opt1 + ' </span> ');
                                                        ans.append(opt);

                                                        //HINDI OPTIONS
                                                        var radio_btnid_hn = divid + "hn_" + obj.ques[k].answers[j].opt_id;
                                                        var rd_name = divid + "hn";
                                                        var opt_hn = $('<p></p>').html('<input type="radio" name=' + rd_name + ' asnno=' + obj.ques[k].answers[j].opt_id_hn + ' id=' + radio_btnid_hn + ' value=' + i + ' onclick="select_answer(this.id,' + qid + ',' + sec_id + ')"> <span class="font_hindi_normal"> ' + numbring_hn[j] + ') ' + obj.ques[k].answers[j].opt1_hn + ' </span> ');
                                                        ans_hn.append(opt_hn);
                                                    }

                                                    div_blc_eng.append(ans);//for English
                                                    div_blc_hn.append(ans_hn);//for Hindi


                                                    qs_content_eng.append(div_blc_eng);//for English
                                                    qs_content_hn.append(div_blc_hn);//for Hindi
                                                    //END DESIGN FOR - QUESTION WISE ANSWER IN ENGLISH & HINDI

                                                    div.append(qs_content_eng);//English Code
                                                    div.append(qs_content_hn);//Hindi Code



                                                    // BUTTON DESIGN

                                                    var btns = $('<div></div>');
                                                    var ne_pre_btn = $('<div style="width:auto; float: left;"></div>');
                                                    var next = i + 1;
                                                    var next_path = "#menu" + next;
                                                    var next_btn = "";
                                                    var pre_btn = "";
                                                    if (ik == 1) {

                                                        next_btn = $('<a data-toggle="tab" href="' + next_path + '" id=ne' + next + ' onclick="find_question_for_bank(this.id,' + qid + ',' + sec_id + ')" style="margin: 0px 10px;" qid=' + qid + '   mrks=' + obj.ques[k].marks + ' >Next</a>').addClass("btn btn-info");

                                                    }
                                                    else if (parseInt(obj.ques[k].rcount) == ik) {

                                                        var pre = i - 1;
                                                        var pre_path = "#menu" + pre;
                                                        pre_btn = $('<a data-toggle="tab" href="' + pre_path + '" id=pr' + pre + ' onclick="find_question_for_bank(this.id,' + qid + ',' + sec_id + ')" style="margin: 0px 0px 0px 10px;" qid=' + qid + '   mrks=' + obj.ques[k].marks + '> Prev</a>').addClass("btn btn-info");

                                                    }
                                                    else {

                                                        var pre = i - 1;
                                                        var pre_path = "#menu" + pre;

                                                        var next_btn = $('<a data-toggle="tab" href="' + next_path + '" id=ne' + next + ' onclick="find_question_for_bank(this.id,' + qid + ',' + sec_id + ')" style="margin: 0px 0px 0px 10px;" qid=' + qid + '   mrks=' + obj.ques[k].marks + '>Next</a>').addClass("btn btn-info");
                                                        pre_btn = $('<a data-toggle="tab" href="' + pre_path + '" id=pr' + pre + ' onclick="find_question_for_bank(this.id,' + qid + ',' + sec_id + ')" style="margin: 0px 0px 0px 10px;"  qid=' + qid + '   mrks=' + obj.ques[k].marks + '> Prev</a>').addClass("btn btn-info");

                                                    }
                                                    var end_btn = $('<a style="background:red; margin: 0px 0px 0px 10px; display:none;" data-toggle="tab" href="#" id=en' + qid + ' onclick="end_test()">End Test</a>').addClass("btn btn-info");
                                                    var btn_review = $('<a style="background:purple; margin: 0px 0px 0px 10px;" data-toggle="tab" href="#" id=en' + qid + ' onclick="review_question(this.id,' + qid + ',' + sec_id + ')">Review</a>').addClass("btn btn-info");
                                                    var btn_clear = $('<a style="background:#232123; margin: 0px 0px 0px 10px;" data-toggle="tab" href="#" id=cls' + qid + ' onclick="uncheck(this.id,' + sec_id + ')">Clear Data</a>').addClass("btn btn-info");


                                                    // btns.append(ne_pre_btn);
                                                    btns.append(pre_btn);

                                                    btns.append(end_btn);
                                                    btns.append(btn_review);
                                                    btns.append(btn_clear);
                                                    btns.append(next_btn);
                                                    div.append(btns);

                                                    tab_cnt_div.append(div);
                                                    question_block.append(tab_cnt_div);
                                                }
                                                tab_sec.append(ul);
                                                $('#question_nubering').append(tab_sec);

                                                //LOOP FOR QUESTION DESIGNING END
                                                div_tabcontent.append(question_block);
                                                if (resume_status == "0") {
                                                    myarray.push(my_secarray);
                                                }
                                                maindiv.append(div_tabcontent);

                                                MathJax.Hub.Queue(["Typeset", MathJax.Hub, "head_tab"]);
                                                $('#head_tab').append(maindiv);


                                                if (cur_sec_id != "0") {
                                                    $("#head_" + cur_sec_id).addClass(" active");
                                                }
                                                if (cur_que_no != "0") {
                                                    cur_que_no = cur_que_no.substring(2)
                                                    $("#ls" + cur_que_no).addClass(" active");
                                                    $("#ls" + cur_que_no).css("background-color", "red");
                                                    $("#menu" + cur_que_no).addClass("active in");
                                                    $("#li" + cur_que_no).addClass("active");
                                                    document.getElementById(cur_sec_id).style.display = "block";

                                                    var de_enid = "menu_en" + cur_que_no;
                                                    var de_hnid = "menu_hn" + cur_que_no;

                                                    var selectedText = $("#ddl_default_language").find("option:selected").text()
                                                    if (selectedText == "English") {
                                                        $("#" + de_enid).removeClass("hide");
                                                        $("#" + de_hnid).removeClass("show");

                                                        $("#" + de_enid).addClass("show");
                                                        $("#" + de_hnid).addClass("hide");
                                                    }

                                                    section_id = cur_sec_id;
                                                    pre_secid = cur_sec_id;

                                                    n_qid = cur_que_id;
                                                    pre_qid = cur_que_no;
                                                    pre_liid = cur_que_no;

                                                    var myar = myarray.length;
                                                    var data = "";
                                                    for (var c = 0; c < myar; c++) {
                                                        if (c == 1) {
                                                            data = myarray[c];
                                                            var jj = 1;
                                                            for (var cc = 0; cc < data.length; cc++) {

                                                                var sp_data = data[cc];
                                                                var sp_secid = sp_data.split("$")[0];
                                                                var sp_qid = sp_data.split("$")[1];
                                                                var sp_ansid = sp_data.split("$")[2];
                                                                var sp_status = sp_data.split("$")[3];
                                                                //alert(sp_status);
                                                                if (sp_status == "3" || sp_status == "4") {

                                                                    var eng_id = "menu" + jj + "_" + sp_ansid;
                                                                    var hn_id = "menu" + jj + "hn_" + sp_ansid;
                                                                    //alert(eng_id);
                                                                    //alert(hn_id);
                                                                    document.getElementById(eng_id).checked = true;
                                                                    document.getElementById(hn_id).checked = true;
                                                                }
                                                                jj++;
                                                            }
                                                        }
                                                    }



                                                } else {
                                                    document.getElementById(section_id).style.display = "block";
                                                    $("#ls" + pre_qid).css("background-color", "red");

                                                    $("#menu" + pre_qid).addClass("active in");
                                                    $("#li" + pre_qid).addClass("active");

                                                }

                                                //$("#ls" + pre_qid).css("background-color", "red");


                                                exam_time_start();
                                                start_time_counting();
                                                $("#<%=hd_start_test.ClientID%>").val("1");
                                                //Sections wise container design coding END

                                            });

                                        }
                                    }
                                });
                            }
                            //CHANGE LANGUAGE FOR EVERY SECTION
                            function change_language(radio_id) {

                                var divid_en = "menu_en" + radio_id.substring(3);
                                var divid_hn = "menu_hn" + radio_id.substring(3);

                                var language_type = $("#" + divid_hn).attr("lngtype");

                                if (language_type == "1") {

                                    if (radio_id.substring(0, 3) == "eng") {

                                        $("#" + divid_en).removeClass("hide");
                                        $("#" + divid_en).addClass("show");

                                        $("#" + divid_hn).removeClass("show");
                                        $("#" + divid_hn).addClass("hide");
                                    } else {
                                        $("#" + divid_en).removeClass("show");
                                        $("#" + divid_en).addClass("hide");

                                        $("#" + divid_hn).removeClass("hide");
                                        $("#" + divid_hn).addClass("show");
                                    }
                                    document.getElementById(radio_id).checked = false;
                                }
                            }

                            //START CODE FOR CHANGE DEFAULT LANGUAGE
                            $("#ddl_default_language").change(function () {

                                var lan_val = $("#ddl_default_language").find("option:selected").val();

                                $("#<%=hd_language.ClientID%>").val(lan_val);

                                dydefault_language(section_id);

                                update_default_language()

                            });

                            function dydefault_language(section_id) {

                                var ulChildren = document.getElementById(section_id).children;
                                var childrenLength = ulChildren.length;

                                for (var i = 0; i < childrenLength; i++) {
                                    var div_id = "lf" + section_id;


                                    if (ulChildren[i].id === div_id) {
                                        var curr_child = document.getElementById(div_id).getElementsByClassName('tab-pane fade active in');
                                        var curr_child_id = curr_child[0].id;
                                        default_enid = "menu_en" + curr_child_id.substring(4);
                                        default_hnid = "menu_hn" + curr_child_id.substring(4);
                                    }



                                    select_language();

                                    default_enid = "";
                                    default_hnid = "";
                                }
                            }
                            function select_language() {

                                var selectedText = $("#ddl_default_language").find("option:selected").text();
                                df_language = selectedText;


                                var language_type = "";
                                if (selectedText == "English") {


                                    $("#" + default_enid).removeClass("hide");
                                    $("#" + default_hnid).removeClass("show");

                                    $("#" + default_enid).addClass("show");
                                    $("#" + default_hnid).addClass("hide");

                                }
                                else {

                                    var language_type = $("#" + default_hnid).attr("lngtype");

                                    if (language_type != "0") {
                                        $("#" + default_enid).removeClass("show");
                                        $("#" + default_hnid).removeClass("hide");

                                        $("#" + default_enid).addClass("hide");
                                        $("#" + default_hnid).addClass("show");
                                    }

                                }

                            }

                            function update_default_language() {
                                var studentid = $("#<%=hd_studentid.ClientID%>").val();
                                var testid = $("#<%=hd_testid.ClientID%>").val();
                                var examcode = $("#<%=hd_examcode.ClientID%>").val();
                                var examtype_code = $("#<%=hd_examtype_code.ClientID%>").val();
                                var exam_category = $("#<%=hd_exam_category.ClientID%>").val();
                                var df_language = $("#<%=hd_language.ClientID%>").val();
                                var attempt_id = $("#<%=hd_attempt_id.ClientID%>").val();

                                var entry_id = $("#<%=hd_entry_id.ClientID%>").val();
                                var package_id = $("#<%=hd_package_id.ClientID%>").val();


                                $.ajax({
                                    type: "POST",
                                    url: "Onlinetestapi/Test_taking_code.asmx/update_default_language",
                                    data: "{'studentid':'" + studentid + "','testid':'" + testid + "','examcode':'" + examcode + "','examtypecode':'" + examtype_code + "','examcategory':'" + exam_category + "','dflanguage':'" + df_language + "','attemptid':'" + attempt_id + "','entryid':'" + entry_id + "','packageid':'" + package_id + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {

                                        if (response.d == "0") {

                                        }
                                    }
                                });

                            }

                            //END CODE FOR CHANGE DEFAULT LANGUAGE


                            //START TIMING FOR EVERY SECTION
                            function exam_time_start() {
                                var hd_time_type = $("#<%=hd_time_type.ClientID%>").val();

                                if (hd_time_type == "Minutes") {
                                    seconds = parseInt($("#<%=hd_time.ClientID%>").val()) * 60;
                                }
                            }

                            //START TIMING FOR EVERY QUESTION
                            function start_time_counting() {
                                var set_qtime = setInterval(function () {
                                    var tme = tme_count + 1;
                                    $("#<%=HiddenField4.ClientID%>").val(tme);

                                    var set_mtime = Math.floor(tme / 60);
                                    var set_stime = tme - set_mtime * 60;

                                }, 998);

                            }
                            //END TIMING FOR EVERY SECTION


                            function open_section_for_bank(evt, sec_id, value) {

                                section_id = sec_id;
                                cur_sec_id = sec_id;
                                pre_qid = pre5_qid[value - 1];
                                //$("#ls" + pre_qid).css("background-color", "red");


                                var kk, tabcontent, tablinks;
                                tabcontent = document.getElementsByClassName("tabcontent");
                                for (kk = 0; kk < tabcontent.length; kk++) {
                                    tabcontent[kk].style.display = "none";
                                }
                                document.getElementById(sec_id).style.display = "block";

                                var tme = 0;
                                $("#<%=HiddenField4.ClientID%>").val(tme);

                                exam_time_start();
                                start_time_counting();
                                start_section_wise_timing_for_bank();
                            }


                            //FIND NEXT QUESTION
                            var visit_qid = "";
                            var pre_liid = "";
                            function find_question_for_bank(id, qid, scid) {

                                cur_que_no = id;
                                cur_que_id = qid;
                                cur_sec_id = scid;
                                var tme = 0;
                                var liid = id.substring(2);//
                                default_enid = "menu_en" + liid;//ENGLISH QUESTION BLOCK
                                default_hnid = "menu_hn" + liid;//HINDI QUESTION BLOCK

                                visit_qid = $("#ls" + liid).attr("qid");//VISITED QUESTION ID

                                var li_pre = "#li" + pre_liid;//RIGHT SIDE BUTTON ID
                                var li_curr = "#li" + liid;//RIGHT SIDE BUTTON ID

                                //FOR NEXT QUESTION & PREVIOUS QUESTION ACTIVE & DE-ACTIVE CODE
                                if (id.substring(0, 2) == "ne") {
                                    $(li_curr).addClass("active");
                                    $(li_pre).removeClass("active");
                                    visit_qid = $("#ls" + liid).attr("qid");
                                }
                                else if (id.substring(0, 2) == "pr") {
                                    $(li_curr).addClass("active");
                                    $(li_pre).removeClass("active");
                                }


                                pre_liid = liid;

                                id = id.substring(2);
                                n_qid = $("#ls" + id).attr("qid");
                                if (pre_qid != id) {

                                    if (pre_qid != "") {

                                        if (s_ansid == 0) {

                                            var text, fLen, i;
                                            text = 0;
                                            fLen = selected_value.length;
                                            for (i = 0; i < fLen; i++) {
                                                var value = selected_value[i];
                                                if (value == pre_qid) {
                                                    text = pre_qid;
                                                }
                                            }
                                            if (text == 0) {
                                                var vv = "";
                                                var rLen = selected_review.length;
                                                for (k = 0; k < rLen; k++) {
                                                    if (selected_review[k] == pre_qid) {
                                                        vv = pre_qid;
                                                    }
                                                }
                                                if (vv == 0) {
                                                    $("#ls" + pre_qid).css("background-color", "red").removeClass("glyphicon glyphicon-ok");

                                                    //NOT VISITED
                                                    Not_Attempted("NAT", scid, n_qid);
                                                }
                                            }
                                        }
                                        else {
                                            var not_attem = false;
                                            var vv = "";
                                            var rLen = selected_review.length;
                                            for (k = 0; k < rLen; k++) {
                                                if (selected_review[k] == pre_qid) {
                                                    vv = pre_qid;
                                                }
                                            }
                                            if (vv == 0) {
                                                $("#ls" + pre_qid).css("background-color", "green");
                                                s_ansid = 0;
                                                s_qid = 0;
                                                not_attem = true;
                                                Not_Attempted("ATP", scid, n_qid);

                                            }
                                        }
                                    }

                                    pre_qid = id;

                                    var btnid = $("#ls" + id).attr("ano");//id;


                                    $('#que_no').empty('');
                                    $('#que_no').append(btnid);


                                    $("#<%=HiddenField4.ClientID%>").val(tme);
                                    start_time_counting();
                                }

                                //FOR LOCAL STORAGE
                                var myar = myarray.length;
                                var data = "";
                                for (i = 0; i < myar; i++) {

                                    if (i == 1) {
                                        data = myarray[i];
                                        data = data.map(function (v) {

                                            var sp_data = v;

                                            var sp_secid = sp_data.split("$")[0];
                                            var sp_qid = sp_data.split("$")[1];
                                            var sp_ansid = sp_data.split("$")[2];
                                            var sp_status = sp_data.split("$")[3];
                                            var sp_time = sp_data.split("$")[4];


                                            var toreturn = "0";

                                            if (sp_secid == scid && sp_qid == n_qid) {

                                                if (not_attem == true) {
                                                    toreturn = sp_secid + "$" + n_qid + "$" + x + "$1" + "$" + parseInt(tme) + parseInt(sp_time);
                                                }
                                                else {
                                                    toreturn = sp_secid + "$" + n_qid + "$" + sp_ansid + "$" + sp_status + "$" + parseInt(tme) + parseInt(sp_time);
                                                }
                                            } else {
                                                toreturn = v;
                                            }
                                            return toreturn;
                                        });
                                    }
                                }

                                myarray[1] = data;
                                localStorage.removeItem('my_local_storge');
                                localStorage.setItem('my_local_storge', JSON.stringify(myarray));
                                save_other_data_to_local_storage();
                                ///////////////////


                                selected_ans_id = "0";
                                selected_ans_hn_id = "0";
                                selected_ans_en_id = "0";
                            }
                            //////////////////////
                            function save_other_data_to_local_storage() {

                                var data = JSON.stringify(not_visit_ar) + "$" + JSON.stringify(attempt_ar) + "$" + JSON.stringify(not_attempt) + "$" + JSON.stringify(review_mark) + "$" + JSON.stringify(review_ar) + "$" + JSON.stringify(selected_review) + "$" + JSON.stringify(marked) + "$" + JSON.stringify(review);

                                localStorage.removeItem('my_otherlocal_storge');
                                localStorage.setItem('my_otherlocal_storge', data);
                            }

                            // SELECT OPTION OF A QUESTION & SAVE SELECTED ANSWER
                            function select_answer(id, quid, scid) {

                                visit_qid = quid;
                                selected_ans_id = id;

                                //HINDI & ENGLISH OPTION CHECK
                                if (id.includes("hn")) {
                                    var hn_id = id.replace("hn", "");
                                    document.getElementById(hn_id).checked = true;
                                    selected_ans_en_id = hn_id;
                                    selected_ans_hn_id = id;
                                }
                                else {
                                    var hn_id = id.replace("_", "hn_");
                                    document.getElementById(hn_id).checked = true;
                                    selected_ans_hn_id = hn_id;
                                    selected_ans_en_id = id;
                                }
                                ////////////////


                                var x = $("#" + id).attr("asnno");
                                s_ansid = x;
                                s_qid = quid;

                                var xx = $("#" + id).attr("value");//quid;
                                $("#ls" + pre_qid).css("background-color", "green");

                                //CHECK QUESTION IS SELECT AS REVIEW OR NOT
                                var bool = false;
                                var rLen = review.length;
                                for (i = 0; i < rLen; i++) {
                                    var value = review[i];
                                    if (value == quid) {
                                        bool = true;
                                        break;
                                    }
                                }

                                if (bool == false) {

                                    Not_Attempted("ATP", scid, quid);
                                }
                                //alert(marked);
                                marked.push(quid);
                                ///////////////////////



                                // QUESTION ATTEMPT TIME CALCULATION
                                var date = new Date();
                                var ans_hh = date.getHours();
                                var ans_mm = date.getMinutes();
                                var ans_ss = date.getSeconds();
                                var asn_itime = ans_hh + "" + ans_mm + "" + ans_ss;

                                var tme = $("#<%=HiddenField4.ClientID%>").val();
                                var ans_block = s_qid + "," + s_ansid + "," + tme + "," + asn_itime + "," + section_id;
                                if (new_tot_myans == "") {
                                    new_tot_myans = ans_block;
                                }
                                else {
                                    new_tot_myans = new_tot_myans + "$" + ans_block;
                                }
                                ////////////////////


                                var text = 0;
                                var fLen = selected_value.length;
                                for (i = 0; i < fLen; i++) {
                                    if (selected_value[i] == xx) {
                                        text = xx;
                                    }
                                }
                                if (text == 0) {
                                    selected_value.push(xx);
                                }

                                var mrk_atmp = false;
                                var rLen = selected_review.length;
                                for (k = 0; k < rLen; k++) {
                                    if (selected_review[k] == xx) {
                                        $("#ls" + pre_qid).css("background-color", "purple").addClass("glyphicon glyphicon-ok");

                                        mrk_atmp = true;
                                        //REVIEW & MARK
                                        Review_and_mark_counting("MRK", scid, quid);
                                        break;
                                    }
                                }
                                //ATTEMPT COUNTING UPDATING
                                attempted_counting("ADD", scid, s_qid);
                                save_data(s_ansid, s_qid, tme, asn_itime, scid);

                                //FOR LOCAL STORAGE
                                var tme = $("#<%=HiddenField4.ClientID%>").val();
                                var myar = myarray.length;
                                var data = "";
                                for (i = 0; i < myar; i++) {

                                    if (i == 1) {
                                        data = myarray[i];

                                        data = data.map(function (v) {

                                            var sp_data = v;

                                            var sp_secid = sp_data.split("$")[0];
                                            var sp_qid = sp_data.split("$")[1];
                                            var sp_ansid = sp_data.split("$")[2];
                                            var sp_status = sp_data.split("$")[3];
                                            var sp_time = sp_data.split("$")[4];


                                            var toreturn = "0";

                                            if (sp_secid == scid && sp_qid == s_qid) {

                                                if (mrk_atmp == true) {

                                                    toreturn = sp_secid + "$" + s_qid + "$" + x + "$4" + "$" + parseInt(tme) + parseInt(sp_time);

                                                }
                                                else {

                                                    toreturn = sp_secid + "$" + s_qid + "$" + x + "$3" + "$" + parseInt(tme) + parseInt(sp_time);
                                                }


                                            } else {

                                                toreturn = v;
                                            }

                                            return toreturn;
                                        });
                                    }
                                }

                                myarray[1] = data;
                                localStorage.removeItem('my_local_storge');
                                localStorage.setItem('my_local_storge', JSON.stringify(myarray));
                                save_other_data_to_local_storage();
                                ///////////////////

                                s_ansid = 0;
                                s_qid = 0;


                            }

                            function save_data(s_ansid, s_qid, tme, asn_itime, scid) {

                                var studentid = $("#<%=hd_studentid.ClientID%>").val();

                                var testid = $("#<%=hd_testid.ClientID%>").val();
                                var test_no = $("#<%=HiddenField5.ClientID%>").val();
                                var examcode = $("#<%=hd_examcode.ClientID%>").val();

                                var neg_mark = $("#<%=hd_neg_mark.ClientID%>").val();
                                var neg_mark_type = $("#<%=hd_neg_mark_type.ClientID%>").val();

                                var df_language = $("#<%=hd_language.ClientID%>").val();
                                var attempt_id = $("#<%=hd_attempt_id.ClientID%>").val();

                                var hd_entry_id = $("#<%=hd_entry_id.ClientID%>").val();
                                var hd_package_id = $("#<%=hd_package_id.ClientID%>").val();


                                if (neg_mark_type == "%") {
                                    neg_mark_type = "Percentage";
                                }

                                $.ajax({
                                    type: "POST",
                                    url: "Onlinetestapi/Test_taking_code.asmx/save_data",
                                    data: "{'entryid':'" + hd_entry_id + "','packageid':'" + hd_package_id + "','Section':'" + section_id + "','testid':'" + testid + "','studentid':'" + studentid + "','sqid':'" + s_qid + "','sansid':'" + s_ansid + "','tme':'" + tme + "','testno':'" + test_no + "','examcode':'" + examcode + "','negmark':'" + neg_mark + "','negmarktype':'" + neg_mark_type + "','language':'" + df_language + "','attemptid':'" + attempt_id + "','asnitime':'" + asn_itime + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {

                                        if (response.d == "0") {

                                        }
                                    }
                                });

                            }
                            ////////////////////////////////////////////////////////////

                            //UNCHECK SELECTED OPTION
                            function uncheck(id, scid) {
                                var qid = id.substring(3);

                                if (visit_qid != "") {

                                    var ulChildren = document.getElementById(visit_qid).children;
                                    var childrenLength = ulChildren.length;

                                    for (var i = 0; i < childrenLength; i++) {
                                        if (ulChildren[i].children[0].nodeName.toLowerCase() === 'input') {
                                            if (document.getElementById(ulChildren[i].children[0].id).checked) {
                                                selected_ans_id = ulChildren[i].children[0].id;
                                                break;
                                            }
                                        }
                                    }

                                    //English option clear
                                    var en_id = selected_ans_id.replace("hn", "");
                                    selected_ans_en_id = en_id;

                                    document.getElementById(en_id).checked = false;

                                    //Hindi option clear
                                    var hn_id = selected_ans_id.replace("_", "hn_");
                                    selected_ans_hn_id = hn_id;
                                    document.getElementById(hn_id).checked = false;

                                    $("#ls" + pre_qid).css("background-color", "red").removeClass("glyphicon glyphicon-ok");

                                    var studentid = $("#<%=hd_studentid.ClientID%>").val();
                                    var testid = $("#<%=hd_testid.ClientID%>").val();
                                    var test_no = $("#<%=HiddenField5.ClientID%>").val();
                                    var examcode = $("#<%=hd_examcode.ClientID%>").val();
                                    var ansid = $("#" + selected_ans_id).attr("asnno"); //document.getElementById("#" + id).getAttribute("qno");
                                    var attempt_id = $("#<%=hd_attempt_id.ClientID%>").val();


                                    var hd_entry_id = $("#<%=hd_entry_id.ClientID%>").val();
                                    var hd_package_id = $("#<%=hd_package_id.ClientID%>").val();


                                    var date = new Date();
                                    var ans_hh = date.getHours();
                                    var ans_mm = date.getMinutes();
                                    var ans_ss = date.getSeconds();
                                    var asn_itime = ans_hh + "" + ans_mm + "" + ans_ss;

                                    var tme = $("#<%=HiddenField4.ClientID%>").val();

                                    var ans_block = qid + ",0," + tme + "," + asn_itime + "," + section_id;
                                    if (new_tot_myans == "") {
                                        new_tot_myans = ans_block;
                                    }
                                    else {
                                        new_tot_myans = new_tot_myans + "$" + ans_block;
                                    }

                                    $.ajax({
                                        type: "POST",
                                        url: "Onlinetestapi/Test_taking_code.asmx/clear_data",
                                        data: "{'entryid':'" + hd_entry_id + "','packageid':'" + hd_package_id + "','Section':'" + section_id + "','testid':'" + testid + "','studentid':'" + studentid + "','sqid':'" + qid + "','sansid':'" + ansid + "','examcode':'" + examcode + "','attemptid':'" + attempt_id + "'}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (response) {
                                        }
                                    });

                                    selected_ans_id = "0";
                                    selected_ans_hn_id = "0";
                                    selected_ans_en_id = "0";


                                    selected_review.splice(pre_qid, 1);

                                    var fLen = marked.length;
                                    for (i = 0; i < fLen; i++) {
                                        var value = marked[i];
                                        if (value == qid) {
                                            marked.splice(i, 1);
                                            break;
                                        }
                                    }

                                    var rLen = review.length;
                                    for (i = 0; i < rLen; i++) {
                                        var value = review[i];
                                        if (value == qid) {
                                            review.splice(i, 1);
                                            break;
                                        }
                                    }

                                    Review_counting("CLR", scid, qid);
                                    Review_and_mark_counting("CLR", scid, qid);
                                    Not_Attempted("CLR", scid, qid);
                                    attempted_counting("MN", scid, qid);


                                    //FOR LOCAL STORAGE
                                    var myar = myarray.length;
                                    var data = "";
                                    for (i = 0; i < myar; i++) {

                                        if (i == 1) {
                                            data = myarray[i];

                                            data = data.map(function (v) {

                                                var sp_data = v;

                                                var sp_secid = sp_data.split("$")[0];
                                                var sp_qid = sp_data.split("$")[1];
                                                var sp_ansid = sp_data.split("$")[2];
                                                var sp_status = sp_data.split("$")[3];
                                                var sp_time = sp_data.split("$")[4];


                                                var toreturn = "0";

                                                if (sp_secid == scid && sp_qid == qid) {
                                                    toreturn = sp_secid + "$" + qid + "$" + x + "$0" + "$0";

                                                } else {

                                                    toreturn = v;
                                                }

                                                return toreturn;
                                            });
                                        }
                                    }

                                    myarray[1] = data;
                                    localStorage.removeItem('my_local_storge');
                                    localStorage.setItem('my_local_storge', JSON.stringify(myarray));
                                    save_other_data_to_local_storage();
                                    ///////////////////



                                }
                            }

                            //////////////////////////////////////////////////////////



                            //NOT VISIT COUNTING
                            var not_visit_ar = [];
                            function not_visit_counting(type, scid, quid) {


                                var nv = "#nv";

                                var examname = $("#<%=hd_exam_category.ClientID%>").val();
                                if (examname == "0")//ZERO FOR BANK ONE FOR OTHER
                                {
                                    nv = nv + scid;
                                } else { nv = nv + "1"; }

                                var count = parseInt($(nv).text());

                                if (type == "MN") {

                                    if (count > 0) {
                                        for (i = 0; i < not_visit_ar.length; i++) {
                                            var value = not_visit_ar[i];
                                            if (value == quid) {
                                                not_visit_ar.splice(i, 1);
                                                count -= 1;
                                            }
                                        }
                                    }
                                }
                                else {
                                    not_visit_ar.push(quid);
                                    count += 1;
                                }
                                $(nv).text(count);

                                //Local Storage not visited array
                                localStorage.removeItem('not_visit_ar');
                                localStorage.setItem('not_visit_ar', JSON.stringify(not_visit_ar));
                            }

                            //ATTEMPT COUNTING
                            var attempt_ar = [];
                            function attempted_counting(type, scid, quid) {
                                var nv = "#atp";

                                var examname = $("#<%=hd_exam_category.ClientID%>").val();
                                if (examname == "0")//ZERO FOR BANK ONE FOR OTHER
                                {
                                    nv = nv + scid;
                                } else { nv = nv + "1"; }


                                var count = parseInt($(nv).text());

                                var rLen = attempt_ar.length;
                                var added_qid = 0;


                                if (type == "MN") {

                                    if (count > 0) {

                                        for (i = 0; i < rLen; i++) {
                                            var value = attempt_ar[i];
                                            if (value == quid) {
                                                attempt_ar.splice(i, 1);
                                                count -= 1;
                                                tot_ct -= 1;
                                                break;
                                            }
                                        }


                                    } else {
                                        count = 0;

                                    }
                                }
                                else {
                                    if (rLen == 0) {
                                        attempt_ar.push(quid);
                                        count += 1;
                                        tot_ct += 1;

                                    }
                                    else {

                                        if (jQuery.inArray(quid, attempt_ar) == -1) {
                                            attempt_ar.push(quid);
                                            count += 1;
                                            tot_ct += 1;

                                        }
                                    }
                                }

                                $(nv).text(count);

                                //Local Storage Attempt array
                                localStorage.removeItem('attempt_ar');
                                localStorage.setItem('attempt_ar', JSON.stringify(attempt_ar));


                            }

                            //NOT ATTEMPT COUNTING
                            var not_attempt = [];
                            function Not_Attempted(type, scid, quid) {
                                var nv = "#nat";

                                var examname = $("#<%=hd_exam_category.ClientID%>").val();
                                if (examname == "0")//ZERO FOR BANK ONE FOR OTHER
                                {
                                    nv = nv + scid;
                                } else { nv = nv + "1"; }

                                var count = parseInt($(nv).text());
                                var rLen = not_attempt.length;


                                if (type == "ATP") {
                                    if (count > 0) {

                                        var bool = false;
                                        for (i = 0; i < rLen; i++) {
                                            var value = not_attempt[i];


                                            if (value == quid) {
                                                not_attempt.splice(i, 1);
                                                count -= 1;
                                                tot_ct -= 1;
                                                bool = true;
                                                break;
                                            }
                                        }
                                        if (bool == false) {
                                            not_visit_counting("MN", scid, quid);
                                        }

                                    }
                                    else {

                                        count = 0;
                                        not_visit_counting("MN", scid, quid);
                                    }
                                }
                                else if (type == "CLR") {

                                    var bool = false;
                                    for (i = 0; i < rLen; i++) {
                                        var value = not_attempt[i];
                                        if (value == quid) {
                                            bool = true;
                                            break;
                                        }
                                    }
                                    if (bool == false) {
                                        not_attempt.push(quid);
                                        count += 1;
                                    }
                                    not_visit_counting("ADD", scid, quid);
                                }
                                else {

                                    if (rLen == 0) {
                                        not_attempt.push(quid);
                                        count += 1;
                                        tot_ct += 1;
                                        not_visit_counting("MN", scid, quid);
                                    }
                                    else {
                                        var bool = false;
                                        for (i = 0; i < rLen; i++) {
                                            var value = not_attempt[i];
                                            if (value == quid) {
                                                bool = true;
                                                break;
                                            }
                                        }
                                        if (bool == false) {
                                            not_attempt.push(quid);
                                            count += 1;
                                            not_visit_counting("MN", scid, quid);
                                        }
                                    }
                                }
                                $(nv).text(count);

                                //Local Storage Not Attempt array
                                localStorage.removeItem('not_attempt');
                                localStorage.setItem('not_attempt', JSON.stringify(not_attempt));

                            }

                            //REVIEW & MARK COUNTING
                            var review_mark = [];
                            function Review_and_mark_counting(type, scid, quid) {

                                var nv = "#revm";

                                var examname = $("#<%=hd_exam_category.ClientID%>").val();
                                if (examname == "0")//ZERO FOR BANK ONE FOR OTHER
                                {
                                    nv = nv + scid;
                                } else { nv = nv + "1"; }


                                var count = parseInt($(nv).text());
                                var rLen = review_mark.length;

                                if (type == "MRK") {

                                    if (rLen == 0) {
                                        review_mark.push(quid);
                                        count += 1;
                                        $(nv).text(count);

                                    }
                                    else {


                                        var bool = false;
                                        for (i = 0; i < rLen; i++) {
                                            var value = review_mark[i];
                                            if (value == quid) {
                                                bool = true;
                                                break;
                                            }
                                        }
                                        if (bool == false) {
                                            review_mark.push(quid);
                                            count += 1;
                                            $(nv).text(count);
                                        }

                                    }
                                }
                                else if (type == "CLR") {
                                    for (i = 0; i < rLen; i++) {

                                        var value = review_mark[i];
                                        if (value == quid) {
                                            review_mark.splice(i, 1);
                                            count -= 1;
                                            $(nv).text(count);
                                            break;
                                        }
                                    }
                                }
                                else {
                                    // do nothing
                                }
                                //Local Storage Review mark array
                                localStorage.removeItem('review_mark');
                                localStorage.setItem('review_mark', JSON.stringify(review_mark));

                            }

                            //REVIEW COUNTING
                            var review_ar = [];
                            function Review_counting(type, scid, quid) {

                                var nv = "#rev";

                                var examname = $("#<%=hd_exam_category.ClientID%>").val();
                                if (examname == "0")//ZERO FOR BANK ONE FOR OTHER
                                {
                                    nv = nv + scid;
                                } else { nv = nv + "1"; }


                                var count = parseInt($(nv).text());
                                var rLen = review_ar.length;


                                if (type == "CLR") {

                                    for (i = 0; i < rLen; i++) {

                                        var value = review_ar[i];
                                        if (value == quid) {

                                            // review
                                            count -= 1;
                                            review_ar.splice(i, 1);
                                            break;
                                        }
                                    }
                                }

                                else {

                                    if (rLen == 0) {
                                        review_ar.push(quid);
                                        count += 1;
                                    }
                                    else {

                                        var bool = false;
                                        for (i = 0; i < rLen; i++) {
                                            var value = review_ar[i];
                                            if (value == quid) {
                                                bool = true;
                                                break;
                                            }
                                        }
                                        if (bool == false) {
                                            review_ar.push(quid);
                                            count += 1;
                                        }
                                    }
                                }
                                $(nv).text(count);

                                //Local Storage Review
                                localStorage.removeItem('review_ar');
                                localStorage.setItem('review_ar', JSON.stringify(review_ar));
                            }

                            //REVIEW QUESTION
                            var selected_review = [];
                            var marked = [];
                            var review = [];
                            function review_question(id, quid, scid) {


                                var xx = $("#" + id).attr("value");//quid;

                                var bool = false;
                                var fLen = marked.length;
                                for (i = 0; i < fLen; i++) {
                                    var value = marked[i];
                                    if (value == quid) {

                                        $("#ls" + pre_qid).css("background-color", "purple").addClass("glyphicon glyphicon-ok");
                                        Review_counting("REV", scid, quid);
                                        Review_and_mark_counting("MRK", scid, quid);

                                        bool = true;
                                        break;
                                    }
                                }
                                if (bool == false) {
                                    $("#ls" + pre_qid).css("background-color", "purple")
                                    Review_counting("REV", scid, quid);
                                }

                                selected_review.push(pre_qid);
                                review.push(quid);

                                var bool1 = false;
                                var rLen = attempt_ar.length;
                                for (i = 0; i < rLen; i++) {
                                    var value = attempt_ar[i];
                                    if (value == quid) {
                                        bool1 = true;
                                        break;
                                    }
                                }
                                if (bool1 == false) {
                                    Not_Attempted("ATP", scid, quid);
                                }


                                //FOR LOCAL STORAGE
                                var tme = $("#<%=HiddenField4.ClientID%>").val();
                                var myar = myarray.length;
                                var data = "";
                                for (i = 0; i < myar; i++) {
                                    if (i == 1) {
                                        data = myarray[i];
                                        data = data.map(function (v) {

                                            var sp_data = v;
                                            var sp_secid = sp_data.split("$")[0];
                                            var sp_qid = sp_data.split("$")[1];
                                            var sp_ansid = sp_data.split("$")[2];
                                            var sp_status = sp_data.split("$")[3];
                                            var sp_time = sp_data.split("$")[4];


                                            var toreturn = "0";

                                            if (sp_secid == scid && sp_qid == quid) {

                                                if (bool == true) {
                                                    toreturn = sp_secid + "$" + quid + "$0" + "$4" + "$" + parseInt(tme) + parseInt(sp_time);
                                                }
                                                else {
                                                    toreturn = sp_secid + "$" + quid + "$0" + "$2" + "$" + parseInt(tme) + parseInt(sp_time);
                                                }
                                            } else {

                                                toreturn = v;
                                            }

                                            return toreturn;
                                        });
                                    }
                                }

                                myarray[1] = data;
                                localStorage.removeItem('my_local_storge');
                                localStorage.setItem('my_local_storge', JSON.stringify(myarray));
                                save_other_data_to_local_storage();
                                ///////////////////




                            }
                            var selected_ans_id = "";
                            var selected_ans_hn_id = "";
                            var selected_ans_en_id = "";


                            function end_test() {
                                unhook();
                                var studentid = $("#<%=hd_studentid.ClientID%>").val();

                                var test_id = $("#<%=hd_testid.ClientID%>").val();
                                var test_no = $("#<%=HiddenField5.ClientID%>").val();
                                var examcode = $("#<%=hd_examcode.ClientID%>").val();

                                var examtype_code = $("#<%=hd_examtype_code.ClientID%>").val();
                                var testmode = $("#<%=hd_testmode.ClientID%>").val();

                                var attempt_id = $("#<%=hd_attempt_id.ClientID%>").val();
                                var exam_category = $("#<%=hd_exam_category.ClientID%>").val();

                                var myanswes = new_tot_myans;
                                var df_language = $("#<%=hd_language.ClientID%>").val();
                                var neg_mark_type = $("#<%=hd_neg_mark_type.ClientID%>").val();
                                var neg_mark = $("#<%=hd_neg_mark.ClientID%>").val();

                                var hd_entry_id = $("#<%=hd_entry_id.ClientID%>").val();
                                var hd_package_id = $("#<%=hd_package_id.ClientID%>").val();
                                var hd_df_lang_con = $("#<%=hd_df_lang_con.ClientID%>").val();
                                var modepage = $("#<%=hd_modepage.ClientID%>").val();
                                if (neg_mark_type == "%") {
                                    neg_mark_type = "Percentage";
                                }
                                //alert();
                                $.blockUI({ message: $('#domMessage') });
                                $.ajax({

                                    type: "POST",
                                    url: "Onlinetestapi/Test_taking_code.asmx/update_data",
                                    data: "{'entryid':'" + hd_entry_id + "','packageid':'" + hd_package_id + "','examcode':'" + examcode + "','testid':'" + test_id + "','studentid':'" + studentid + "','attemptid':'" + attempt_id + "','myanswes':'" + myanswes + "','language':'" + df_language + "' ,'negmarktype':'" + neg_mark_type + "','negmark':'" + neg_mark + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {

                                        if (response.d == "DONE") {

                                            setTimeout($.unblockUI, 5000);

                                            //var path = "OnlineResult.aspx?testid=" + test_id + "&attemptid=" + attempt_id + "&studentid=" + studentid + "&mode=" + modepage;
                                            var path = "OnlineResult.aspx";
                                            window.location = path;
                                        }
                                        else {
                                            alert("Sorry you have not submitted yet, because test date has been expired")
                                        }
                                    }
                                });

                            }
                            //////////////////////////


                            // CODE FOR OTHER -SSC, RAILWAY
                            function start_section_wise_timing_for_other() {
                                countdownTimer = window.setInterval(function () {
                                    secondPassed();
                                }, 999);

                                function secondPassed() {

                                    if (seconds != 0) {
                                        seconds--;

                                        var minutes = parseInt(seconds / 60);
                                        var remainingSeconds = parseInt(seconds % 60);
                                        if (remainingSeconds < 10) {
                                            remainingSeconds = "0" + parseInt(remainingSeconds);
                                        }


                                        document.getElementById('countdown').innerHTML = minutes + " mins : " + remainingSeconds + " secs";
                                        if (parseInt(seconds) === 0) {
                                            clearInterval(countdownTimer);
                                            document.getElementById('countdown').innerHTML = "Time is Over for this section.";
                                            end_test();

                                        }
                                    }
                                }

                            }
                            // SSC, RAILWAY and ALL OTHER EXAMS
                            function bind_heading_for_other() {

                                $.blockUI({ message: $('#pleasewait') });
                                var i = 0;
                                var ii = 0;

                                var testid = $("#<%=hd_testid.ClientID%>").val();
                                var studentid = $("#<%=hd_studentid.ClientID%>").val();


                                if (resume_status == "0") {
                                    mystring = studentid + "#" + testid + "#";
                                    myarray.push(mystring);

                                }
                                $.ajax({

                                    type: "POST",
                                    url: "Onlinetestapi/Test_taking_code.asmx/bind_top_heading_tabbing",
                                    data: "{'testid':'" + testid + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {

                                        setTimeout($.unblockUI, 1000);

                                        var json = response.d;

                                        if (json != "[]") {
                                            $('#head_tab').empty();
                                            $('#question_nubering').empty();
                                            var tabul = $('<ul></ul>').addClass('tab');
                                            var maindiv = $('<div></div>').addClass('row');

                                            var tot_qs = 0;
                                            //FOR RIGHT SIDE QUESTION NUMBERING AND BUTTON DESIGN
                                            var tab_sec = $('<div id="rg1" class="show"  style="width:100%;  float:left;"></div>');
                                            var ul = $('<ul></ul>').addClass('nav nav-tabs right_menu_scroll');
                                            var ik = 0;
                                            var my_secarray = [];
                                            $.each($.parseJSON(json), function (idx, obj) {

                                                var kk = 0;
                                                ii++;

                                                //Sections design coding START
                                                var id = obj.id;
                                                var sec_id = obj.id;
                                                var sec_name = obj.sec_name;

                                                var li = "";
                                                if (ii == 1) {
                                                    section_id = sec_id;
                                                    pre_secid = sec_id;
                                                    li = $('<li></li>').html('<a href="javascript:void(0)" class="tablinks" id="head_' + sec_id + '" onclick="open_section_for_other(event, ' + sec_id + ',' + ii + ')" >' + sec_name + '</a>');
                                                }
                                                else {
                                                    li = $('<li></li>').html('<a href="javascript:void(0)" class="tablinks" id="head_' + sec_id + '" onclick="open_section_for_other(event, ' + sec_id + ',' + ii + ')" >' + sec_name + '</a>');
                                                }

                                                tabul.append(li);
                                                $('#head_tab').append(tabul);

                                                MathJax.Hub.Queue(["Typeset", MathJax.Hub, "head_tab"]);
                                                //Sections design coding END

                                                //Sections wise container design coding START
                                                var div_tabcontent = "";
                                                div_tabcontent = $('<div id=' + sec_id + ' style="float:left; width:100%;"></div>').addClass('tabcontent');

                                                //LOOP FOR QUESTION DESIGNING START
                                                var question_block = $('<div id=lf' + sec_id + ' style="float:left;"></div>').addClass("row");
                                                var tab_cnt_div = $('<div></div>').addClass('tab-content');

                                                //FOR RIGHT SIDE QUESTION NUMBERING AND BUTTON DESIGN
                                                var my_ques = "";
                                                tot_qs = parseInt(tot_qs) + parseInt(obj.ques.length);

                                                for (var k = 0; k < obj.ques.length; k++) {
                                                    i++;
                                                    ik++;
                                                    kk++;
                                                    //for question numbering
                                                    var qid = obj.ques[k].qid;
                                                    questions.push(qid);
                                                    //alert(qid);
                                                    if (resume_status == "0") {
                                                        my_ques = my_ques + sec_id + "$" + qid + "$" + "0" + "$" + "0" + "$" + "0";
                                                        my_secarray.push(my_ques);
                                                    }
                                                    my_ques = "";
                                                    var path = "#menu" + i;
                                                    var li = "";

                                                    ////SECTION WISE FIRST QUSTION NO 
                                                    if (kk == 1) {
                                                        pre5_qid.push(i);
                                                    }
                                                    if (i == 1) {
                                                        n_qid = qid;
                                                        pre_qid = i;
                                                        pre_liid = i;
                                                        $('#que_no').append('1');
                                                    }

                                                    if (resume_status == "0") {

                                                        li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab" href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '   mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                        not_visit_ar.push(qid);
                                                    }
                                                    else {
                                                        var myar = myarray.length;
                                                        var data = "";
                                                        for (var c = 0; c < myar; c++) {

                                                            if (c == 1) {
                                                                data = myarray[c];

                                                                for (var cc = 0; cc < data.length; cc++) {
                                                                    var sp_data = data[cc];
                                                                    var sp_secid = sp_data.split("$")[0];
                                                                    var sp_qid = sp_data.split("$")[1];
                                                                    var sp_ansid = sp_data.split("$")[2];
                                                                    var sp_status = sp_data.split("$")[3];

                                                                    if (sp_secid == sec_id && sp_qid == qid) {

                                                                        if (sp_status == "1") {

                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab"  style="background:red;" href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }
                                                                        else if (sp_status == "2") {
                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab"  style="background:Purple;"    href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }
                                                                        else if (sp_status == "3") {
                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab"  style="background:Green;"    href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }
                                                                        else if (sp_status == "4") {
                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab"  style="background:purple;"  class="glyphicon glyphicon-ok"  href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '  mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }

                                                                        else {
                                                                            li = $('<li id=li' + i + '></li>').html('<a data-toggle="tab" href="' + path + '" id=ls' + i + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" ano=' + i + ' qid=' + qid + '   mrks=' + obj.ques[k].marks + '>' + ik + '</a>');
                                                                            break;
                                                                        }

                                                                    }


                                                                }
                                                            }
                                                        }
                                                    }

                                                    $(ul).append(li);
                                                    //////////////////////////

                                                    //FOR QUESTION LISTING
                                                    var divid = "menu" + i;
                                                    var divid_en = "menu_en" + i;
                                                    var divid_hn = "menu_hn" + i;

                                                    var default_enid = divid_en;//use for Hindi language
                                                    var default_hnid = divid_hn;//use for English language


                                                    var div = "";

                                                    if (kk == 1) {

                                                        if (cur_sec_id != sec_id) {

                                                            div = $('<div id=' + divid + '  ></div>').addClass('tab-pane fade in active');
                                                        }
                                                        else {
                                                            div = $('<div id=' + divid + '></div>').addClass('tab-pane fade');
                                                        }

                                                    }
                                                    else {
                                                        div = $('<div id=' + divid + '></div>').addClass('tab-pane fade');
                                                    }

                                                    var qs_content_eng = "";
                                                    var qs_content_hnd = "";

                                                    if (df_language == "English") {
                                                        qs_content_eng = $('<div style="width:100%; float:left; height: auto; margin-bottom:10px;  padding-right: 10px;" class="show" id="' + divid_en + '" ></div>');
                                                        qs_content_hn = $('<div style="width:100%; float:left; height: auto; margin-bottom:10px;  padding-right: 10px;" class="hide" id="' + divid_hn + '" lngtype=' + obj.ques[k].Language_Itype + '></div>');

                                                        p_en = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px;display:none"><input type="radio" name=hin' + divid_hn + '  id=hin' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In Hindi </span>  Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');
                                                        p_hn = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px;"><input type="radio" name=eng' + divid_en + '  id=eng' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In English </span>      Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');

                                                        qs_content_eng.append(p_en);
                                                        qs_content_hn.append(p_hn);
                                                    }
                                                    else {

                                                        //LANGUAGE TYPE SINGLE =0 & DOUBLE =1
                                                        var language_type = obj.ques[k].Language_Itype;

                                                        if (language_type == "1") {


                                                            qs_content_eng = $('<div style="width:100%; float:left;  height: auto; margin-bottom:10px;  padding-right: 10px;" class="hide" id="' + divid_en + '" ></div>');
                                                            qs_content_hn = $('<div style="width:100%; float:left;  height: auto; margin-bottom:10px;  padding-right: 10px;" class="show" id="' + divid_hn + '" lngtype=' + obj.ques[k].Language_Itype + '></div>');

                                                            p_en = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px;display:none"><input type="radio" name=hin' + divid_hn + '  id=hin' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In Hindi </span>  Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');
                                                            p_hn = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px;"><input type="radio" name=eng' + divid_en + '  id=eng' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In English </span>     Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');

                                                            qs_content_eng.append(p_en);
                                                            qs_content_hn.append(p_hn);
                                                        }
                                                        else {

                                                            qs_content_eng = $('<div style="width:100%; float:left;  height: auto; margin-bottom:10px;  padding-right: 10px;" class="show" id="' + divid_en + '" ></div>');
                                                            qs_content_hn = $('<div style="width:100%; float:left;  height: auto; margin-bottom:10px;  padding-right: 10px;" class="hide" id="' + divid_hn + '" lngtype=' + obj.ques[k].Language_Itype + '></div>');


                                                            p_en = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px;display:none"><input type="radio" name=hin' + divid_hn + '  id=hin' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In Hindi </span>  Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');
                                                            p_hn = $('<p style="Text-align:right;border-bottom: 1px solid #ecf1f3;padding-bottom: 7px;"><input type="radio" name=eng' + divid_en + '  id=eng' + i + ' onclick="change_language(this.id)"> <span style="margin-right: 40px;">View Question In English </span>    Marks- <span class="marks_p" id=mrksp' + sec_id + '>+' + obj.ques[k].marks + '</span> <span class="marks_n" id=mrksn' + sec_id + '>-' + obj.ques[k].negative_mrk + '</span> </p>');

                                                            qs_content_eng.append(p_en);
                                                            qs_content_hn.append(p_hn);

                                                        }


                                                    }


                                                    //START DESIGN FOR -DIRECTION,PHRAGES,QUESTION IN ENGLISH 
                                                    var div_blc_eng = $('<div style="width:100%; float:left; height: auto; color:#000; margin:0px; overflow-y:auto; padding: 0px;"></div>');

                                                    //THIS IS FOR DIRECTION
                                                    if (obj.ques[k].Direction != "") {
                                                        var Direction = $('<p style="margin:0px; padding:0px; height:auto;width:100%; float:left;font-weight:bold;color:black;"></p>').html("Direction : " + obj.ques[k].Direction);
                                                        div_blc_eng.append(Direction);
                                                    }


                                                    //THIS IS FOR PHRAGES
                                                    var figure = "";
                                                    if (obj.ques[k].cnt_type == "Img") {

                                                        figure = $('<figure></figure>').html(obj.ques[k].Di);
                                                    }
                                                    else {
                                                        figure = $('<figure></figure>').html(obj.ques[k].Di);
                                                    }

                                                    if (figure == null || figure == "") {
                                                    }
                                                    else {
                                                        div_blc_eng.append(figure);
                                                    }


                                                    //THIS IS FOR QUESTION
                                                    var question = $('<p></p>').html('<span style="float:left">' + obj.ques[k].Question_no + ').</span>' + obj.ques[k].question);
                                                    div_blc_eng.append(question);
                                                    //END DESIGN FOR -DIRECTION,PHRAGES,QUESTION IN ENGLISH 


                                                    //START DESIGN FOR -DIRECTION,PHRAGES,QUESTION IN HINDI
                                                    var div_blc_hn = $('<div style="width:100%; float:left; height: auto; color:#000;  margin:0px; overflow-y:auto; padding: 0px;"></div>');

                                                    //THIS IS FOR DIRECTION
                                                    if (obj.ques[k].Direction_HN != "") {
                                                        var Direc_hn = '<span class="font_hindi_normal"> ' + obj.ques[k].Direction_HN + '</span>';
                                                        var Direction_hn = $('<p style="margin:0px; padding:0px; height:auto;width:100%; float:left;font-weight:bold;color:black;"></p>').html("Direction : " + Direc_hn);
                                                        div_blc_hn.append(Direction_hn);
                                                    }

                                                    //THIS IS FOR PHRAGES
                                                    var figure_hn = "";
                                                    if (obj.ques[k].cnt_type == "Img") {
                                                        figure_hn = $('<figure></figure>').html(obj.ques[k].DI_HN);
                                                    }
                                                    else {
                                                        figure_hn = $('<figure></figure>').html(obj.ques[k].DI_HN);
                                                    }
                                                    if (figure_hn == null || figure_hn == "") {
                                                    }
                                                    else {
                                                        div_blc_hn.append(figure_hn);
                                                    }

                                                    //THIS IS FOR QUESTION
                                                    var ques_hn = '<span class="font_hindi_normal"> ' + obj.ques[k].Question_name_HN + '</span>';
                                                    var question_hn = $('<p></p>').html('<span style="float:left">' + obj.ques[k].Question_no + ').</span>' + ques_hn);
                                                    div_blc_hn.append(question_hn);

                                                    //END DESIGN FOR -DIRECTION,PHRAGES,QUESTION IN HINDI

                                                    //START DESIGN FOR - QUESTION WISE ANSWER IN ENGLISH & HINDI
                                                    var ans = $('<div style="width:100%; float:left;" id=' + qid + '></div>');
                                                    var ans_hn = $('<div style="width:100%; float:left;" id=' + qid + '></div>');

                                                    var numbring = ["A", "B", "C", "D", "E"];
                                                    var numbring_hn = ["A", "B", "C", "D", "E"];//["क", "ख", "ग", "घ", "ङ"];
                                                    for (var j = 0; j < obj.ques[k].answers.length; j++) {

                                                        //ENGLISH OPTIONS
                                                        var radio_btnid = divid + "_" + obj.ques[k].answers[j].opt_id;
                                                        var opt = $('<p></p>').html('<input type="radio" name=' + divid + ' asnno=' + obj.ques[k].answers[j].opt_id + ' id=' + radio_btnid + ' value=' + i + ' onclick="select_answer(this.id,' + qid + ',' + sec_id + ')"> <span> ' + numbring[j] + ') ' + obj.ques[k].answers[j].opt1 + ' </span> ');
                                                        ans.append(opt);

                                                        //HINDI OPTIONS
                                                        var radio_btnid_hn = divid + "hn_" + obj.ques[k].answers[j].opt_id;
                                                        var rd_name = divid + "hn";
                                                        var opt_hn = $('<p></p>').html('<input type="radio" name=' + rd_name + ' asnno=' + obj.ques[k].answers[j].opt_id_hn + ' id=' + radio_btnid_hn + ' value=' + i + ' onclick="select_answer(this.id,' + qid + ',' + sec_id + ')"> <span class="font_hindi_normal"> ' + numbring_hn[j] + ') ' + obj.ques[k].answers[j].opt1_hn + ' </span> ');
                                                        ans_hn.append(opt_hn);

                                                    }

                                                    div_blc_eng.append(ans);//for English
                                                    div_blc_hn.append(ans_hn);//for Hindi


                                                    qs_content_eng.append(div_blc_eng);//for English
                                                    qs_content_hn.append(div_blc_hn);//for Hindi
                                                    //END DESIGN FOR - QUESTION WISE ANSWER IN ENGLISH & HINDI

                                                    div.append(qs_content_eng);//English Code
                                                    div.append(qs_content_hn);//Hindi Code;

                                                    // BUTTON DESIGN
                                                    var btns = $('<div></div>');
                                                    var ne_pre_btn = $('<div style="width:auto; float: left;"></div>');
                                                    var next = i + 1;
                                                    var next_path = "#menu" + next;
                                                    var next_btn = "";
                                                    var pre_btn = "";
                                                    if (kk == 1) {

                                                        next_btn = $('<a data-toggle="tab" href="' + next_path + '" id=ne' + next + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" style="margin: 0px 10px;" qid=' + qid + '   mrks=' + obj.ques[k].marks + ' >Next</a>').addClass("btn btn-info");

                                                    }
                                                    else if (parseInt(obj.ques[k].rcount) == kk) {

                                                        var pre = i - 1;
                                                        var pre_path = "#menu" + pre;
                                                        pre_btn = $('<a data-toggle="tab" href="' + pre_path + '" id=pr' + pre + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" style="margin: 0px 0px 0px 10px;" qid=' + qid + '   mrks=' + obj.ques[k].marks + '> Prev</a>').addClass("btn btn-info");
                                                    }
                                                    else {
                                                        var pre = i - 1;
                                                        var pre_path = "#menu" + pre;

                                                        var next_btn = $('<a data-toggle="tab" href="' + next_path + '" id=ne' + next + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" style="margin: 0px 0px 0px 10px;" qid=' + qid + '   mrks=' + obj.ques[k].marks + '>Next</a>').addClass("btn btn-info");
                                                        pre_btn = $('<a data-toggle="tab" href="' + pre_path + '" id=pr' + pre + ' onclick="find_question_for_other(this.id,' + qid + ',' + sec_id + ')" style="margin: 0px 0px 0px 10px;"  qid=' + qid + '   mrks=' + obj.ques[k].marks + '> Prev</a>').addClass("btn btn-info");

                                                    }
                                                    var end_btn = $('<a style="background:red; margin: 0px 0px 0px 10px; display:none;" data-toggle="tab" href="#" id=en' + qid + ' onclick="end_test()">End Test</a>').addClass("btn btn-info");
                                                    var btn_review = $('<a style="background:purple; margin: 0px 0px 0px 10px;" data-toggle="tab" href="#" id=en' + qid + ' onclick="review_question(this.id,' + qid + ',' + sec_id + ')">Review</a>').addClass("btn btn-info");
                                                    var btn_clear = $('<a style="background:#232123; margin: 0px 0px 0px 10px;" data-toggle="tab" href="#" id=cls' + qid + ' onclick="uncheck(this.id,' + sec_id + ')">Clear Data</a>').addClass("btn btn-info");


                                                    // btns.append(ne_pre_btn);
                                                    btns.append(pre_btn);

                                                    btns.append(end_btn);
                                                    btns.append(btn_review);
                                                    btns.append(btn_clear);
                                                    btns.append(next_btn);
                                                    div.append(btns);

                                                    tab_cnt_div.append(div);
                                                    question_block.append(tab_cnt_div);
                                                }



                                                //LOOP FOR QUESTION DESIGNING END
                                                div_tabcontent.append(question_block);
                                                maindiv.append(div_tabcontent);

                                            });
                                            if (resume_status == "0") {
                                                myarray.push(my_secarray);
                                            }

                                            //COUNTING Attempted,Not Visited,Not Attempted, Review,Review & Answer
                                            var attempted = attempt_ar.length;
                                            if (attempt_ar.length == undefined) { attempted = 0; }

                                            var notvisited = not_visit_ar.length;
                                            if (not_visit_ar.length == undefined || not_visit_ar.length == 0) { notvisited = tot_qs; }


                                            var notattempted = not_attempt.length;
                                            if (not_attempt.length == undefined) { notattempted = 0; }

                                            var review = review_ar.length;
                                            if (review_ar.length == undefined) { review = 0; }

                                            var reviewmark = review_mark.length;
                                            if (review_mark.length == undefined) { reviewmark = 0; }

                                            var bottom_cnt = $('<div class="row" style="margin: 0px 0px 5px 0px; border-top: 1px solid #f1f1f1; border-bottom: 1px solid #f1f1f1; padding: 5px;"></div>').html('<table style="width: 100%;"><tr class="pag_num"><td style="vertical-align: top"><h4><div id="atp1" style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: #060; float: left; margin-right: 5px;  color:#fff">' + attempted + '</div> Attempted</h4></td><td colspan="2"><h4><div id="nv1"  style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: #8e8b8b; float: left; margin-right: 5px; color:#fff">' + notvisited + '</div>Not Visited</h4></td></tr><tr class="pag_num"> <td><h4><div id="nat1" style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: red; float: left; margin-right: 5px;  color:#fff">' + notattempted + '</div>Not Attempted</h4></td><td><h4><div id="rev1" style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: purple; float: left; margin-right: 5px;  color:#fff">' + review + '</div>Review</h4></td><td> <h4> <div id="revm1"  class="glyphicon glyphicon-ok" style="min-width: 25px; text-align: center; padding: 0px 5px 0px 5px; font-weight: normal; background: purple; float: left; margin-right: 5px;  color:#fff">' + reviewmark + '</div>Review & Answer</h4></td></tr></table>');
                                            tab_sec.append(bottom_cnt);//NOT VISIT SECTION WISE TOTAL QUESTION
                                            tab_sec.append(ul);
                                            $('#question_nubering').append(tab_sec);


                                            $('#head_tab').append(maindiv);
                                            MathJax.Hub.Queue(["Typeset", MathJax.Hub, "head_tab"]);


                                            if (cur_sec_id != "0") {
                                                $("#head_" + cur_sec_id).addClass(" active");
                                            }
                                            if (cur_que_no != "0") {
                                                cur_que_no = cur_que_no.substring(2)
                                                $("#ls" + cur_que_no).addClass(" active");
                                                //$("#ls" + cur_que_no).css("background-color", "red");
                                                $("#menu" + cur_que_no).addClass("active in");
                                                $("#li" + cur_que_no).addClass("active");
                                                document.getElementById(cur_sec_id).style.display = "block";


                                                var de_enid = "menu_en" + cur_que_no;
                                                var de_hnid = "menu_hn" + cur_que_no;

                                                var selectedText = $("#ddl_default_language").find("option:selected").text()
                                                if (selectedText == "English") {
                                                    $("#" + de_enid).removeClass("hide");
                                                    $("#" + de_hnid).removeClass("show");

                                                    $("#" + de_enid).addClass("show");
                                                    $("#" + de_hnid).addClass("hide");
                                                }

                                                section_id = cur_sec_id;
                                                pre_secid = cur_sec_id;

                                                n_qid = cur_que_id;
                                                pre_qid = cur_que_no;
                                                pre_liid = cur_que_no;

                                                var myar = myarray.length;
                                                var data = "";
                                                for (var c = 0; c < myar; c++) {
                                                    if (c == 1) {
                                                        data = myarray[c];
                                                        var jj = 1;
                                                        for (var cc = 0; cc < data.length; cc++) {

                                                            var sp_data = data[cc];
                                                            var sp_secid = sp_data.split("$")[0];
                                                            var sp_qid = sp_data.split("$")[1];
                                                            var sp_ansid = sp_data.split("$")[2];
                                                            var sp_status = sp_data.split("$")[3];
                                                            //alert(sp_status);
                                                            if (sp_status == "3" || sp_status == "4") {

                                                                var eng_id = "menu" + jj + "_" + sp_ansid;
                                                                var hn_id = "menu" + jj + "hn_" + sp_ansid;
                                                                //alert(eng_id);
                                                                //alert(hn_id);
                                                                document.getElementById(eng_id).checked = true;
                                                                document.getElementById(hn_id).checked = true;
                                                            }
                                                            jj++;
                                                        }
                                                    }
                                                }





                                            } else {
                                                document.getElementById(section_id).style.display = "block";
                                                $("#ls" + pre_qid).css("background-color", "red");

                                                $("#menu" + pre_qid).addClass("active in");
                                                $("#li" + pre_qid).addClass("active");

                                            }

                                            exam_time_start();
                                            start_time();
                                            $("#<%=hd_start_test.ClientID%>").val("1");
                                            //Sections wise container design coding END


                                        }

                                    }


                                });


                            }

                            function open_section_for_other(evt, sec_id, value) {

                                section_id = sec_id;
                                cur_sec_id = sec_id;

                                //pre_qid = pre5_qid[value - 1];
                                //$("#ls" + pre_qid).css("background-color", "red");

                                var kk, tabcontent, tablinks;
                                tabcontent = document.getElementsByClassName("tabcontent");
                                for (kk = 0; kk < tabcontent.length; kk++) {
                                    tabcontent[kk].style.display = "none";
                                }
                                tablinks = document.getElementsByClassName("tablinks");
                                for (kk = 0; kk < tablinks.length; kk++) {
                                    tablinks[kk].className = tablinks[kk].className.replace(" active", "");
                                }
                                document.getElementById(sec_id).style.display = "block";
                                //evt.currentTarget.className += " active";
                                $("#head_" + section_id).addClass(" active");

                                dydefault_language(section_id);

                                var tme = 0;
                                $("#<%=HiddenField4.ClientID%>").val(tme);
                                pre_secid = section_id;


                                // start_time();

                            }


                            function exam_time_start() {

                                var hd_time_type = $("#<%=hd_time_type.ClientID%>").val();
                                if (hd_time_type == "Minutes") {
                                    seconds = parseInt($("#<%=hd_time.ClientID%>").val()) * 60;
                                }
                            }

                            function start_time() {

                                var set_qtime = setInterval(function () {
                                    var tme = parseInt($("#<%=HiddenField4.ClientID%>").val()) + 1;
                                    $("#<%=HiddenField4.ClientID%>").val(tme);

                                }, 998);
                            }

                            //FIND NEXT QUESTION
                            function find_question_for_other(id, qid, scid) {

                                cur_que_no = id;
                                cur_que_id = qid;
                                cur_sec_id = scid;
                                var tme = 0;
                                var liid = id.substring(2);
                                default_enid = "menu_en" + liid;//ENGLISH QUESTION BLOCK
                                default_hnid = "menu_hn" + liid;//HINDI QUESTION BLOCK

                                visit_qid = $("#ls" + liid).attr("qid");//VISITED QUESTION ID

                                var li_pre = "#li" + pre_liid;//RIGHT SIDE BUTTON ID
                                var li_curr = "#li" + liid;//RIGHT SIDE BUTTON ID

                                //FOR NEXT QUESTION & PREVIOUS QUESTION ACTIVE & DE-ACTIVE CODE
                                if (id.substring(0, 2) == "ne") {
                                    $(li_curr).addClass("active");
                                    $(li_pre).removeClass("active");
                                    visit_qid = $("#ls" + liid).attr("qid");
                                }
                                else if (id.substring(0, 2) == "pr") {
                                    $(li_curr).addClass("active");
                                    $(li_pre).removeClass("active");
                                }


                                pre_liid = liid;

                                id = id.substring(2);
                                n_qid = $("#ls" + id).attr("qid");

                                if (pre_qid != id) {
                                    if (pre_qid != "") {
                                        if (s_ansid == 0) {

                                            var text, fLen, i;
                                            text = 0;
                                            fLen = selected_value.length;
                                            for (i = 0; i < fLen; i++) {

                                                var value = selected_value[i];
                                                if (value == pre_qid) {
                                                    text = pre_qid;
                                                }
                                            }
                                            if (text == 0) {

                                                var vv = "";
                                                var rLen = selected_review.length;
                                                for (k = 0; k < rLen; k++) {
                                                    if (selected_review[k] == pre_qid) {
                                                        vv = pre_qid;
                                                    }
                                                }
                                                if (vv == 0) {
                                                    $("#ls" + pre_qid).css("background-color", "red").removeClass("glyphicon glyphicon-ok");

                                                    not_attem = true;
                                                    Not_Attempted("NAT", scid, n_qid);
                                                }
                                            }
                                        }
                                        else {
                                            var not_attem = false;
                                            var vv = "";
                                            var rLen = selected_review.length;
                                            for (k = 0; k < rLen; k++) {
                                                if (selected_review[k] == pre_qid) {
                                                    vv = pre_qid;
                                                }
                                            }
                                            if (vv == 0) {
                                                $("#ls" + pre_qid).css("background-color", "green");
                                                s_ansid = 0;
                                                s_qid = 0;
                                                not_attem = true;
                                                Not_Attempted("ATP", scid, n_qid);

                                            }
                                        }
                                    }

                                    pre_qid = id;

                                    var btnid = $("#ls" + id).attr("ano");//id;


                                    $('#que_no').empty('');
                                    $('#que_no').append(btnid);


                                    $("#<%=HiddenField4.ClientID%>").val(tme);
                                    //start_time();
                                }


                                //FOR LOCAL STORAGE
                                var myar = myarray.length;
                                var data = "";

                                for (i = 0; i < myar; i++) {

                                    if (i == 1) {
                                        data = myarray[i];

                                        data = data.map(function (v) {

                                            var sp_data = v;

                                            var sp_secid = sp_data.split("$")[0];
                                            var sp_qid = sp_data.split("$")[1];
                                            var sp_ansid = sp_data.split("$")[2];
                                            var sp_status = sp_data.split("$")[3];
                                            var sp_time = sp_data.split("$")[4];


                                            var toreturn = "0";

                                            if (sp_secid == scid && sp_qid == qid) {

                                                if (not_attem == true) {
                                                    //alert("1");
                                                    toreturn = sp_secid + "$" + qid + "$0" + "$1" + "$" + parseInt(tme) + parseInt(sp_time);
                                                    //alert(toreturn);
                                                }
                                                else {
                                                    toreturn = sp_secid + "$" + qid + "$" + sp_ansid + "$" + sp_status + "$" + parseInt(tme) + parseInt(sp_time);
                                                }
                                            } else {
                                                toreturn = v;
                                            }

                                            return toreturn;
                                        });
                                        //alert(data);
                                    }
                                }
                                //alert(data);
                                myarray[1] = data;
                                localStorage.removeItem('my_local_storge');
                                localStorage.setItem('my_local_storge', JSON.stringify(myarray));
                                save_other_data_to_local_storage();
                                ///////////////////


                                selected_ans_id = "0";
                                selected_ans_hn_id = "0";
                                selected_ans_en_id = "0";

                                if (pre_secid != scid) {
                                    var selector = "#head_" + scid;
                                    $(selector).click();
                                } else {
                                    select_language();
                                }
                            }

                        </script>

                        <div class="left_section">


                            <div class="head_tab" id="head_tab">
                                <div id="pleasewait" style="display: none;">
                                    <h1 style="font-size: 20px; text-align: center;">
                                        <img src="../../Online_Test_admin/Doc/ld_img.gif" style="width: 50px" /><br />
                                        <span>We are preparing your questions</span><br />
                                        Please Wait...</h1>
                                </div>

                            </div>


                            <div id="pauseblock" style="display: none; padding: 10px; position: absolute; top: -183px; right: -150px;">
                                <a href="javascript:" id="btn_start" class="btn btn-success" style="padding: 10px 17px"><i class="fa fa-play" aria-hidden="true"></i>&nbsp; Play</a>
                            </div>

                        </div>
                        <div class="right_section">

                            <div class="user">

                                <asp:Image ID="Image1" runat="server" Style="border-radius: 50%; margin-right: 10px; width: 11%; height: 108%" ImageUrl="../../Online_Test_admin/Doc/icon.png" />
                                <asp:Label ID="lbl_user_name" runat="server"> </asp:Label>
                            </div>

                            <div class="question_nubering" id="question_nubering">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>





        <asp:HiddenField ID="hd_resume_status" runat="server" />
        <asp:HiddenField ID="hd_resume_data" runat="server" />
        <asp:HiddenField ID="hd_cur_secid" runat="server" Value="0" />
        <asp:HiddenField ID="hd_cur_queid" runat="server" Value="0" />
        <asp:HiddenField ID="hd_cur_queno" runat="server" Value="0" />

        <asp:HiddenField ID="HiddenField4" runat="server" Value="1" />
        <asp:HiddenField ID="hd_neg_mark" runat="server" Value="1" />
        <asp:HiddenField ID="hd_neg_mark_type" runat="server" Value="1" />
        <asp:HiddenField ID="HiddenField1" runat="server" Value="1" />
        <asp:HiddenField ID="hd_attempt_id" runat="server" Value="1" />
        <asp:HiddenField ID="HiddenField2" runat="server" Value="1" />


        <asp:HiddenField ID="hd_testid" runat="server" />
        <asp:HiddenField ID="hd_examtype_code" runat="server" />
        <asp:HiddenField ID="hd_examcode" runat="server" />
        <asp:HiddenField ID="hd_testmode" runat="server" />
        <asp:HiddenField ID="HiddenField6" runat="server" />

        <asp:HiddenField ID="hd_package_id" runat="server" />
        <asp:HiddenField ID="hd_entry_id" runat="server" />
        <asp:HiddenField ID="hd_language" runat="server" />

        <%--Bank,SSC,RAILWAY--%>
        <asp:HiddenField ID="hd_exam_category" runat="server" />

        <%--PT,MAINS--%>
        <asp:HiddenField ID="hd_exam_type" runat="server" />

        <%--section wise = 0, Overall=1--%>
        <asp:HiddenField ID="hd_numbering" runat="server" />

        <%--time wise=0	, open all=1--%>
        <asp:HiddenField ID="hd_tab_open" runat="server" />

        <%--individual section wise=0,	Overall=1--%>
        <asp:HiddenField ID="hd_timing" runat="server" />

        <asp:HiddenField ID="hd_userid" runat="server" />
        <asp:HiddenField ID="hd_start_test" runat="server" Value="0" />
        <asp:HiddenField ID="hd_modepage" runat="server" />
    </form>
</body>
</html>
