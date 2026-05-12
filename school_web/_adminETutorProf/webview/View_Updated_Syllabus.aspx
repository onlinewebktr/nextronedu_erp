<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Updated_Syllabus.aspx.cs" Inherits="school_web._adminETutorProf.webview.View_Updated_Syllabus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            position: relative;
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
            color: #fdb351;
            position: absolute;
            top: 9px;
            right: 5px;
            left: inherit;
        }

        .form-control {
            font-weight: 500;
        }

        td, th {
            padding: 5px 7px;
        }

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
    </style>
    <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>

    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",
            });
        });
    </script>
    <script src="../../Parents_Profile/js/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#datatable').DataTable({
                "pagingType": "full_numbers",
                "lengthMenu": [
                    [10, 25, 50, -1],
                    [10, 25, 50, "All"]
                ],
                responsive: true,
                language: {
                    search: "INPUT",
                    searchPlaceholder: "Search records",
                }

            });

            var table = $('#datatable').DataTable();

            // Edit record
            table.on('click', '.edit', function () {
                $tr = $(this).closest('tr');
                if ($($tr).hasClass('child')) {
                    $tr = $tr.prev('.parent');
                }

                var data = table.row($tr).data();
                alert('You press on Row: ' + data[0] + ' ' + data[1] + ' ' + data[2] + '\'s row.');
            });

            // Delete a record
            table.on('click', '.remove', function (e) {
                $tr = $(this).closest('tr');
                if ($($tr).hasClass('child')) {
                    $tr = $tr.prev('.parent');
                }
                table.row($tr).remove().draw();
                e.preventDefault();
            });

            //Like record
            table.on('click', '.like', function () {
                alert('You clicked on Like button');
            });
        });
        </script>
    <link href="../../Student_Profile/css/customK.css" rel="stylesheet" />
    <link href="../../Student_Profile/css/mediaK.css" rel="stylesheet" />
    <link href="../../Student_Profile/css/black-dashboard.min.css" rel="stylesheet" />
    <style>
        .container {
            width: 100%;
            padding-right: 14px !important;
            padding-left: 12px !important;
        }
        .notificationpan {
    display: none;
    width: 100%;
    position: absolute;
    top: 210px !important;
    right: 0%;
    padding: 10px 10px;
    height: auto;
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

                </div>
            </div>


            <div class="container">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont1 ">Start Date  </p>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont3">
                            <asp:TextBox ID="txt_date" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                        </p>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont1 ">End Date </p>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont3">
                            <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                        </p>
                    </div>

                    <div class="clearfix"></div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;"></div>
                    <div class="col-lg-12 col-md-12 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                        <asp:Button ID="btn_submit" Style="padding: 5px 12px 3px 12px; margin: 2px 0px 15px 0px;"
                            runat="server" Text="Find" class="mt-2 btn btn-primary my-btn my-btn" OnClick="btn_submit_Click" />
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-right: 5px; padding-left: 5px;">




                        <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Subject</th>
                                    <th>Sub-Subject</th>
                                    <th>Chapter</th>
                                    <th>Subchapter</th>
                                    <th>Date</th>
                                    <th>Status</th>
                                    <th>Remarks</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_sub_subject_name" runat="server" Text='<%#Bind("SubSubjName")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Chapter_Name" runat="server" Text='<%#Bind("Chapter_Name")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Subchapter_Name" runat="server" Text='<%#Bind("SubChapterName")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>

                                            </td>


                                            <td>
                                                <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Style="background-color: #e14eca; color: #fff; padding: 2px 5px 2px 5px; width: auto; border-radius: 2px; font-weight: 500; display: inherit;" OnClick="lnkEdit_Click"><span>Edit</span></asp:LinkButton>
                                                <asp:LinkButton ID="lnk_Delete" runat="server" Style="background-color: #e14eca; color: #fff; padding: 2px 5px 2px 5px; width: auto; border-radius: 2px; font-weight: 500; display: inherit;" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><span>Delete</span></asp:LinkButton>
                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>


                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>



                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
