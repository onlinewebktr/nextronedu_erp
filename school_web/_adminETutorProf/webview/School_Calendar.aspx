<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="School_Calendar.aspx.cs" Inherits="school_web._adminETutorProf.webview.School_Calendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Calendar</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="favicon/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />

    <script src="../../js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        .messbox-sec-h2 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            text-align: center;
            color: #fff;
            background-color: #109be1;
        }

        .fullinfo {
            margin: 0px 0px 0px;
            padding: 10px 5px;
            float: left;
            height: auto;
            width: 100%;
        }

        .textcont1 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 13px;
            line-height: 20px;
            color: #000;
            text-align: left;
        }

        .textcont3 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 12px;
            line-height: 20px;
            color: #000;
            text-align: left;
            font-weight: bold;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 5px;
            left: -23px;
        }

        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }

        .btn {
            padding: 2px 17px 2px 17px !important;
            margin: 5px 0px 0px 0px;
        }
        /******************Notification**********************/
        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 133px !important;
            right: 10px;
            padding: 10px 10px;
            width: 290px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgba(154, 154, 154, 0.8);
        }


        .closenotificationpan {
            position: absolute;
            margin: 0px 0px 0px 0px;
            top: 6px;
            right: 6px;
            cursor: pointer;
        }

        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 999;
        }

        tfoot, th, thead {
            border-color: #e6c6c6 !important;
            border-style: solid !important;
            border-width: 1px !important;
            background: #0d0d0d !important;
            color: #fff7f7 !important;
            padding: 8px 5px 5px 5px !important;
            text-align: center !important;
             font-size: 10px;
        }

        td {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            border: 1px solid #000;
            height: 39px !important;
            width: 148px !important;
            font-size: 10px;
            padding: 5px 3px;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 25px !important;
            font-size: 14px !important;
            padding: 2px 10px;
        }

        .table {
            margin-bottom: 9px !important;
        }

        .scrollbar {
            margin-left: 0px;
            float: left;
            height: auto;
            width: 100%;
            background: #F5F5F5;
            overflow-y: scroll;
            margin-bottom: 5px;
        }

        .clnderType {
            margin: 0px;
            width: 100%;
            float: left;
            font-size: 13px !important;
            font-weight: 500;
        }

        .clnderDesc {
            margin: 2px 0px 0px 0px;
            padding: 2px 0px 0px 0px;
            width: 100%;
            float: left;
            border-top: 1px solid #dddddd3d;
            font-size: 12px !important;
            font-weight: 500;
        }
        .class-cell {
    color: white;
    background-color: green;
}

.holiday-cell {
    color: white;
    background-color: maroon;
}

.events-cell {
    color: white;
    background-color: orange;
}

.examination-cell {
    color: white;
    background-color: orangered;
}

.non-academic-cell {
    color: white;
    background-color: hotpink;
}

.default-cell {
    color: white;
    background-color: maroon;
}
.clnderType {
    margin: 0px;
    width: 100%;
    float: left;
    font-size: 11px !important;
    font-weight: 500;
}
.clnderDesc {
    margin: 2px 0px 0px 0px;
    padding: 2px 0px 0px 0px;
    width: 100%;
    float: left;
    border-top: 1px solid #dddddd3d;
    font-size: 10px !important;
    font-weight: 500;
}
.td1{
    margin:0px!important;padding:5px 0px 5px 10px!important; height:auto!important; width:100%!important; font-size:13px
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="fullinfo">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                    <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                        class="closenotificationpan" alt="" />
                </div>
            </div>


            <div class="col-md-2 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px; display:none" >
                <p class="textcont1 ">Month  </p>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont3">
                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </p>
            </div>
            <div class="col-lg-3 col-md-2 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px; display:none">
                <p class="textcont1 ">Year</p>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont3">
                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </p>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px; display:none">
                <p class="textcont1 ">Class</p>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont3">
                    <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </p>
            </div>
             <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="padding-right: 5px; padding-left: 5px;">
                  <asp:Button ID="btn_submit" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                 </div>

            
            <div class="clearfix"></div>
            <div class="texbox-border">

          
                   <table style="background: #6993c4!important; font-size:13px; margin:0px;padding:0px; float:left; height:auto; width:100%; border: 1px solid #000; padding: 5px 0 8px 0; margin: 0; background: #FF6666 !important; width: 100%; color: #660033; float: left; font-weight: bold; text-transform: uppercase;">
                  <tr style="display:none">
                      <td class="td1" style="text-align:center!important;">
   School Calendar List
                      </td>
                  </tr>
                  <tr>
                      <td class="td1">
                           Month&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<asp:Label ID="lbl_monthname" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#660033" Style="font-size: 14px; line-height: 25px;"></asp:Label>
                      </td>
                  </tr>
                  <tr>
                      <td class="td1">
                           Class&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<asp:Label ID="lbl_class" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#660033" Style="font-size: 14px; line-height: 25px;"></asp:Label>
                      </td>
                  </tr>
                  <tr>
                      <td class="td1">
                           Session :&nbsp;<asp:Label ID="lbl_session" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#660033" Style="font-size: 14px; line-height: 25px;"></asp:Label>
                      </td>
                  </tr>
              </table>

                
                <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" SelectionMode="None" ShowTitle="False" ShowGridLines="True" CellSpacing="1" ShowNextPrevMonth="false" Style="width: 100%; margin: 0px;">
                    <WeekendDayStyle BorderColor="#CC9900" Wrap="True" />
                </asp:Calendar>
                       <div class="wrapper">
                    <div class="scrollbar" id="style-2">
                        <div class="force-overflow">
                            <asp:Calendar ID="Calendar2" runat="server" OnDayRender="Calendar1_DayRender" SelectionMode="None" ShowTitle="False" ShowGridLines="True" CellSpacing="1" ShowNextPrevMonth="false" Style="width: 100%; margin: 0px;">
                                <WeekendDayStyle BorderColor="#CC9900" Wrap="True" />
                            </asp:Calendar>

                        </div>
                    </div>
                    <table style="width:100%; margin:0px;float:left; padding:0px;">
                        <tr>
                            <th style="font-size: 13px;">School calendar color code</th>
                        </tr>
                        <tr>
                            <td class="class-cell td1" title="Class">Class</td>

                        </tr>
                        <tr>
                            <td class="holiday-cell td1" title="Holiday">Holiday</td>

                        </tr>
                        <tr>
                            <td class="events-cell td1" title="Events">Events</td>

                        </tr>
                        <tr>
                            <td class="examination-cell td1" title="Examination">Examination</td>

                        </tr>
                        <tr>
                            <td class="non-academic-cell td1" title="Non-Academic">Non-Academic</td>

                        </tr>
                      <%--  <tr>
                            <td class="default-cell td1" title="Other">Other</td>
                        </tr>--%>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
