<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="school_web.Student_Profile.home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Home
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .model-fltr {
            top: 0px !important;
            bottom: 0px;
        }

        .model-dialog-fltr {
            width: 250px;
            margin: 0px;
            top: 0px;
            background: #fff;
            bottom: 0px;
            position: fixed;
            overflow: auto;
        }

        .modal-header-fltr {
            padding: 5px 25px 7px 25px;
            border-bottom: 0px solid #fff;
            background: #fff;
            border-radius: 0;
            color: white;
            float: left;
            width: 100%;
            margin: -1px 0px 0px 0px;
        }

        .modal a {
            color: #fff;
            background: #fff;
            padding: 0px 5px 2px 5px;
        }

        .popup-fltr-h {
            color: #333;
            text-transform: capitalize;
            font-size: 21px;
            margin: 0px 0px 30px 0px;
            padding: 0;
            font-weight: 500;
            letter-spacing: 1px;
            float: left;
            width: 100%;
        }

        .mdl-close-btn {
            display: none;
        }

        .modal.left .modal-dialog,
        .modal.right .modal-dialog {
            background: #fff2ce;
            position: fixed;
            margin: auto;
            width: 290px;
            height: 100%;
            -webkit-transform: translate3d(0%, 0, 0);
            -ms-transform: translate3d(0%, 0, 0);
            -o-transform: translate3d(0%, 0, 0);
            transform: translate3d(0%, 0, 0);
        }

        .modal.left .modal-content,
        .modal.right .modal-content {
            height: 100%;
            overflow-y: auto;
        }

        .modal.left .modal-body,
        .modal.right .modal-body {
            padding: 0px;
        }

        /*Left*/
        .modal.left.fade .modal-dialog {
            right: -250px;
            -webkit-transition: opacity 0.3s linear, left 0.3s ease-out;
            -moz-transition: opacity 0.3s linear, left 0.3s ease-out;
            -o-transition: opacity 0.3s linear, left 0.3s ease-out;
            transition: opacity 0.3s linear, left 0.3s ease-out;
        }



        /*Right*/
        .modal.right.fade .modal-dialog {
            right: -250px;
            -webkit-transition: opacity 0.3s linear, right 0.3s ease-out;
            -moz-transition: opacity 0.3s linear, right 0.3s ease-out;
            -o-transition: opacity 0.3s linear, right 0.3s ease-out;
            transition: opacity 0.3s linear, right 0.3s ease-out;
        }

        .modal.right.fade.show .modal-dialog {
            right: 0;
        }

        .modal.left.fade.show .modal-dialog {
            right: 0;
        }

        .modal {
            background: rgb(0 0 0 / 33%);
        }

            .modal.fade:not(.show).right .modal-dialog {
                -webkit-transform: translate3d(25%, 0, 0);
                transform: translate3d(25%, 0, 0);
            }
    </style>

    <style>
        .p-menu-sec {
            margin: 0px;
            padding: 10px 10px;
            width: 100%;
            float: left;
        }

        .p-menu-img-sec {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .p-menu-img-sec img {
                margin: 0px;
                padding: 0px;
                border-radius: 8px;
            }


        .p-menu-ul {
            margin: 10px 0px 5px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .p-menu-ul li {
                margin: 5px 0px 5px 0px;
                padding: 0px;
                width: 100%;
                float: left;
                list-style-type: none;
            }

                .p-menu-ul li a {
                    margin: 0px;
                    padding: 0px;
                    width: 100%;
                    float: left;
                    background: #fff;
                    border-radius: 4px;
                    align-items: center;
                    display: flex;
                }

                    .p-menu-ul li a img {
                        margin: 0px;
                        padding: 7px;
                        width: 60px;
                        float: left;
                        background: #a6f3f7;
                        border-radius: 4px 0px 0px 4px;
                    }

                    .p-menu-ul li a span {
                        margin: 0px;
                        padding: 0px 0px 0px 10px;
                        width: 75%;
                        float: left;
                        color: #2e2e2e;
                        font-weight: 500;
                        letter-spacing: 0.5px;
                        font-size: 14px;
                    }

        .hidden {
            display: none !important;
        }
    </style>

    <script src="assets/js/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh" data-ng-app="MenuApp" data-ng-controller="MenuCtrl">
        <div class="container-fluid for-desktop">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="hom-bx-sec">
                        <h2 class="hom-bx-activity-h">Today's Activity 
                            <asp:Label ID="lbl_today_activity_desk" runat="server" Text="Label"></asp:Label></h2>

                        <div class="hom-bx-wpr-sec">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                    <a href="Attandance_Class_Wise.aspx">
                                        <div class="hom-bx-wpr gbgcolor1">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Attendance</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #a4ca63, #e5e6aa);">
                                                    <img src="assets/images/icons/attandance.png" />
                                                </span>
                                                <%--<i class="far fa-clipboard-user color-secondary"></i>--%>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-secondary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                    <a href="class-routing.aspx">
                                        <div class="hom-bx-wpr gbgcolor2">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>My Class</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                                                    <img src="assets/images/icons/live_classes.png" />
                                                </span>
                                                <%--<i class="far fa-users-class color-success"></i>--%>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-success" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                    <a href="today-notice-board.aspx">
                                        <div class="hom-bx-wpr gbgcolor3">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Notice</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                                                    <img src="assets/images/icons/notice_board.png" />
                                                </span>
                                                <%--<i class="far fa-clipboard-list color-primary"></i>--%>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                    <a href="birthday.aspx">
                                        <div class="hom-bx-wpr gbgcolor4">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Birthday</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                                                    <img src="assets/images/icons/birthday_cake.png" /></span>
                                                <%--<i class="far fa-birthday-cake color-danger"></i>--%>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-danger" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>


                        <h2 class="hom-bx-activity-h">Activity</h2>
                        <div class="hom-bx-wpr-sec">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(6)">
                                        <div class="hom-bx-wpr gbgcolor16">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Fees</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                                                    <img src="assets/images/icons/fee_pay.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a href="school-calendar.aspx">
                                        <div class="hom-bx-wpr gbgcolor11">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Calendar</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #ffc843, #fb7224);">
                                                    <img src="assets/images/icons/sch_calender.png" />
                                                </span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(5)">
                                        <div class="hom-bx-wpr  gbgcolor9">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Attendance</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                                                    <img src="assets/images/icons/attendance.png" />
                                                </span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(4)">
                                        <div class="hom-bx-wpr gbgcolor8">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Certificate</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                                                    <img src="assets/images/icons/immigration.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(3)">
                                        <div class="hom-bx-wpr gbgcolor7">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>LMS</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                                                    <img src="assets/images/icons/lms.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(7)">
                                        <div class="hom-bx-wpr gbgcolor17">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Examination</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                                                    <img src="assets/images/icons/assesment.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(2)">
                                        <div class="hom-bx-wpr gbgcolor6">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Leave</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                                                    <img src="assets/images/icons/aply_leave.png" />
                                                </span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>




                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a href="coming-soon.aspx">
                                        <div class="hom-bx-wpr gbgcolor10">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>PTM</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #815cc5, #e5d9ed);">
                                                    <img src="assets/images/icons/online_meet.png" />
                                                </span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a href="events.aspx">
                                        <div class="hom-bx-wpr gbgcolor12">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Events</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #a5cb64, #e7e8ac);">
                                                    <img src="assets/images/icons/events.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a href="message.aspx">
                                        <div class="hom-bx-wpr gbgcolor13">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Message</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                                                    <img src="assets/images/icons/msg.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                    <a href="today-notice-board.aspx">
                                        <div class="hom-bx-wpr gbgcolor15">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Notice</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #ffc843, #fb7224);">
                                                    <img src="assets/images/icons/notice_board.png" />
                                                </span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a href="complain.aspx">
                                        <div class="hom-bx-wpr gbgcolor14">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Complain</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                                                    <img src="assets/images/icons/complain.png" />
                                                </span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>


                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(1)">
                                        <div class="hom-bx-wpr gbgcolor18">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Library</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                                                    <img src="assets/images/icons/library.png" />
                                                </span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a href="bus-location.aspx">
                                        <div class="hom-bx-wpr gbgcolor5">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Transport</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #a4ca63, #e5e6aa);">
                                                    <img src="assets/images/icons/school_bus.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a href="CLass_log_book.aspx">
                                        <div class="hom-bx-wpr gbgcolor8">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Class Log-Book</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                                                    <img src="assets/images/icons/immigration.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a href="class_Activity.aspx">
                                        <div class="hom-bx-wpr gbgcolor6">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Class Activity</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                                                    <img src="assets/images/icons/sylabus.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(8)">
                                        <div class="hom-bx-wpr gbgcolor8">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Online Purchase</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                                                    <img src="assets/images/icons/ico-apply-book.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                 <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4 custom-col" style="display:none">
                                    <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(9)">
                                        <div class="hom-bx-wpr gbgcolor8">
                                            <div class="hom-bx-wpr-contnt">
                                                <p>Online Test</p>
                                            </div>
                                            <div class="hom-bx-wpr-ico-sec">
                                                <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                                                    <img src="assets/images/icons/ico-apply-book.png" /></span>
                                            </div>
                                            <div class="hmm-dd">
                                                <div class="hom-bx-wpr-btm-arow progress-animate">
                                                    <div class="progress-gradient-primary" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"><span class="animate-circle"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="for-mobile">

            <h2 class="hom-bx-activity-h">Today's Activity
                <asp:Label ID="lbl_today_activity_mob" runat="server" Text="Label"></asp:Label></h2>
            <div class="mb-box-wpr" style="width: 25%">
                <a href="Attandance_Class_Wise.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #a4ca63, #e5e6aa);">
                            <img src="assets/images/icons/attandance.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>Attendance</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr" style="width: 25%">
                <a href="class-routing.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                            <img src="assets/images/icons/live_classes.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>My Class</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr" style="width: 25%">
                <a href="today-notice-board.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                            <img src="assets/images/icons/notice_board.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>Notice</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr" style="width: 25%">
                <a href="birthday.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                            <img src="assets/images/icons/birthday_cake.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Birthday</p>
                    </div>
                </a>
            </div>


            <h2 class="hom-bx-activity-h">Activity</h2>
            <div class="mb-box-wpr">
                <a href="bus-location.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #a4ca63, #e5e6aa);">
                            <img src="assets/images/icons/school_bus.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Transport</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(2)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                            <img src="assets/images/icons/aply_leave.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>Leave</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(3)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                            <img src="assets/images/icons/lms.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>LMS</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(4)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                            <img src="assets/images/icons/immigration.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Certificate</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(5)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                            <img src="assets/images/icons/attendance.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>Attendance</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a href="coming-soon.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #815cc5, #e5d9ed);">
                            <img src="assets/images/icons/online_meet.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>PTM</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a href="school-calendar.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #ffc843, #fb7224);">
                            <img src="assets/images/icons/sch_calender.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>Calendar</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a href="events.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #a5cb64, #e7e8ac);">
                            <img src="assets/images/icons/events.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Events</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a href="message.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #b13097, #ea4e3f);">
                            <img src="assets/images/icons/msg.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Message</p>
                    </div>
                </a>
            </div>

            <div class="mb-box-wpr">
                <a href="today-notice-board.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #ffc843, #fb7224);">
                            <img src="assets/images/icons/notice_board.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>Notice</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a href="complain.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                            <img src="assets/images/icons/complain.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>Complain</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(6)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                            <img src="assets/images/icons/fee_pay.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Fees</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(7)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                            <img src="assets/images/icons/assesment.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Examination</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(1)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #fdd9d9, #e26c5e);">
                            <img src="assets/images/icons/library.png" />
                        </span>
                    </div>
                    <div class="mb-box-content">
                        <p>Library</p>
                    </div>
                </a>
            </div>

            <div class="mb-box-wpr">
                <a href="CLass_log_book.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                            <img src="assets/images/icons/immigration.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Class Log-Book</p>
                    </div>
                </a>
            </div>
            <div class="mb-box-wpr">
                <a href="class_Activity.aspx">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                            <img src="assets/images/icons/sylabus.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Class Activity</p>
                    </div>
                </a>
            </div>

            <div class="mb-box-wpr">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(8)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                            <img src="assets/images/icons/ico-apply-book.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Online Purchase</p>
                    </div>
                </a>
            </div>
              <div class="mb-box-wpr" style="display:none">
                <a data-toggle="modal" href="#myModalFltr" data-ng-click="ButtonClick(9)">
                    <div class="mb-box-icon-sec">
                        <span style="background-image: linear-gradient(to right, #32cbd9, #cef1f5);">
                            <img src="assets/images/icons/ico-apply-book.png" /></span>
                    </div>
                    <div class="mb-box-content">
                        <p>Online Test</p>
                    </div>
                </a>
            </div>

        </div>



        <div class="ints-loader-wpr" id="intsLoader">
            <div class="ints-loader-wpr-inr">
                <div class="ints-loader">
                    <p class="ints-loader-txt">
                        <img src="assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

        <div class="modal left fade model-fltr right" id="myModalFltr" role="dialog" style="z-index: 9999">
            <div class="modal-dialog model-dialog-fltr">
                <%--<div class="modal-header-fltr">
                <button type="button" class="close mdl-close-btn" data-dismiss="modal"><i class="fa fa-times-circle" aria-hidden="true"></i></button>
            </div>--%>
                <div class="modal-body">
                    <div class="p-menu-sec">
                        <div class="p-menu-img-sec">
                            <img src="{{menuS[0].Banner}}" />
                        </div>


                        <ul class="p-menu-ul">
                            <li data-ng-repeat="x in menuS"><a href="{{x.Page}}">
                                <img src="{{x.Icon}}" /><span>{{x.Name}}</span> </a></li>


                            <%--<li><a href="#!">
                            <img src="assets/images/icons/ico-apply-book.png" /><span>Apply Book</span> </a></li>
                        <li><a href="#!">
                            <img src="assets/images/icons/issue-book.png" /><span>Issue Book</span> </a></li>
                        <li><a href="#!">
                            <img src="assets/images/icons/ico-pending-book.png" /><span>Pending Book</span> </a></li>--%>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        var app = angular.module('MenuApp', []);
        app.controller('MenuCtrl', function ($scope, $http) {

            $scope.ButtonClick = function (groupId) {
                //alert(groupId);
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("WebService1.asmx/fetch_menus", { params: { "Group_id": groupId } }).then(function (response) {
                    $scope.menuS = response.data;
                    $("#intsLoader").addClass("hidden");
                })
            }
        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>

</asp:Content>
