<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="festival-events.aspx.cs" Inherits="school_web.Student_Profile.webview.festival_events" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <script src="../../assets/Angular/angular.min.js"></script>
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <style>
        :root {
            --background-color: #f7f9fc;
            --card-background: #ffffff;
            --name-color: #0984e3;
            --admission-color: #00b894;
            --dob-color: #fd79a8;
            --address-color: #e17055;
            --label-color: #636e72;
            --border-color-1: #6c5ce7;
            --border-color-2: #00cec9;
            --border-color-3: #e84393;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: var(--background-color);
            margin: 0;
            padding: 20px;
            overflow-x: hidden;
        }

        .header {
            text-align: center;
            color: #6c5ce7;
            font-size: 32px;
            font-weight: bold;
            margin-bottom: 30px;
            animation: fadeSlideDown 1s ease forwards;
        }

        .student-list {
            display: flex;
            flex-direction: column;
            gap: 20px;
            max-width: 600px;
            margin: auto;
        }

        .student-card {
            background: var(--card-background);
            padding: 10px 15px 1px;
            margin: 0px 0px 15px 0px;
            border-left: 6px solid var(--border-color-1);
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.08);
            opacity: 0;
            transform: translateY(50px);
            animation: fadeSlideUp 0.8s ease forwards;
            animation-delay: calc(var(--i) * 0.2s);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

            .student-card:hover {
                transform: scale(1.03);
                box-shadow: 0 12px 24px rgba(0, 0, 0, 0.15);
            }

            /* Different border colors */
            .student-card:nth-child(1) {
                border-left-color: var(--border-color-1);
            }

            .student-card:nth-child(2) {
                border-left-color: var(--border-color-2);
            }

            .student-card:nth-child(3) {
                border-left-color: var(--border-color-3);
            }

        .info {
            margin-bottom: 15px;
        }

        .label {
            font-size: 13px;
            color: var(--label-color);
            text-transform: uppercase;
            margin-bottom: 4px;
            letter-spacing: 0.5px;
        }

        .value-name {
            font-size: 18px;
            font-weight: 600;
            color: var(--name-color);
        }

        .value-admission {
            font-size: 18px;
            font-weight: 600;
            color: var(--admission-color);
        }

        .value-dob {
            font-size: 18px;
            font-weight: 600;
            color: var(--dob-color);
        }

        .value-address {
            font-size: 18px;
            font-weight: 600;
            color: var(--address-color);
        }

        /* Animations */
        @keyframes fadeSlideUp {
            0% {
                opacity: 0;
                transform: translateY(50px);
            }

            100% {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes fadeSlideDown {
            0% {
                opacity: 0;
                transform: translateY(-20px);
            }

            100% {
                opacity: 1;
                transform: translateY(0);
            }
        }


        .ints-loader-wpr {
            display: none;
            margin: 0;
            padding: 0;
            width: 100%;
            height: auto;
            float: left;
            background: #ffffff80;
            position: absolute;
            z-index: 9999999999;
            left: 0;
            bottom: 0;
            top: 0;
        }

        .ints-loader-wpr-inr {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            float: left;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .ints-loader {
            background-color: #fff;
            padding: 6px 15px;
            height: auto;
            -webkit-border-radius: 9px 9px 9px 9px;
            border-radius: 2px;
            border: 1px solid #fff;
            box-shadow: 0 3px 14px 1px rgba(92,91,91,.85);
        }

        .ints-loader-txt {
            color: #414141;
            font-weight: 500;
            font-size: 16px;
            z-index: 999999;
            position: relative;
            letter-spacing: 1px;
            margin: 0;
            text-align: center;
        }

        .ints-loader-img {
            width: 30px;
            margin: 0 8px 0 0;
        }

        .notFound {
            margin: 0px 0px 0px 0px;
            padding: 20px 0px;
            width: 100%;
            float: left;
            background: #fbffc6;
            border: 1px solid #dedfa0;
            border-radius: 4px;
        }

            .notFound p {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                width: 100%;
                float: left;
                text-align: center;
                font-size: 17px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server"> 
        <div class="student-list" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
            <div class="ints-loader-wpr" id="intsLoader">
                <div class="ints-loader-wpr-inr">
                    <div class="ints-loader">
                        <p class="ints-loader-txt">
                            <img src="../../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>

            <div style="width: 100%; float: left" id="listS">
                <!-- Student 1 -->
                <div class="student-card" style="--i: 0; {{x.bdrcolors}}" data-ng-repeat="x in eventsS">
                    <div class="info">
                        <div class="label">Date</div>
                        <div class="value-name">{{x.Dates}} ({{x.Days}})</div>
                    </div>
                    <div class="info {{x.IsFestival}}">
                        <div class="label">Connecting Festival</div>
                        <div class="value-admission">{{x.Festival}}</div>
                    </div>
                    <%--<div class="info">
                    <div class="label">Date of Birth</div>
                    <div class="value-dob">2005-06-15</div>
                </div>--%>
                    <div class="info">
                        <div class="label">Activity</div>
                        <div class="value-address">{{x.Event_details}}</div>
                    </div>
                </div>
            </div>

            <div class="notFound hidden" id="NotFounD">
                <p>No record found.</p>
            </div>
            <!-- Student 2 -->
            <%--<div class="student-card" style="--i: 1;">
                <div class="info">
                    <div class="label">Name</div>
                    <div class="value-name">Jane Smith</div>
                </div>
                <div class="info">
                    <div class="label">Admission No</div>
                    <div class="value-admission">B67890</div>
                </div>
                <div class="info">
                    <div class="label">Date of Birth</div>
                    <div class="value-dob">2006-09-21</div>
                </div>
                <div class="info">
                    <div class="label">Address</div>
                    <div class="value-address">456, Oak Avenue, Riverdale</div>
                </div>
            </div>

            
            <div class="student-card" style="--i: 2;">
                <div class="info">
                    <div class="label">Name</div>
                    <div class="value-name">Michael Brown</div>
                </div>
                <div class="info">
                    <div class="label">Admission No</div>
                    <div class="value-admission">C54321</div>
                </div>
                <div class="info">
                    <div class="label">Date of Birth</div>
                    <div class="value-dob">2004-11-05</div>
                </div>
                <div class="info">
                    <div class="label">Address</div>
                    <div class="value-address">789, Pine Lane, Greentown</div>
                </div>
            </div> --%>
        </div>

        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_adm_no" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var adm_no = $("#<%=hd_adm_no.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();


                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("webService/apis.asmx/fetch_events", { params: { "Session_id": session_id, "Adm_no": adm_no, "Class_id": class_id } }).then(function (response) {
                    $scope.eventsS = response.data;
                    if ($scope.eventsS == "") {
                        $("#intsLoader").addClass("hidden");
                        $("#listS").addClass("hidden");
                        $("#notFound").removeClass("hidden");
                    }
                    else {
                        $("#listS").removeClass("hidden");
                        $("#notFound").addClass("hidden");
                        $("#intsLoader").addClass("hidden");
                    }
                })
            });

            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }
        </script>


        <style>
            .hidden {
                display: none !important;
            }
        </style>
    </form>
</body>
</html>
