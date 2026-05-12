<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/webview/Site2.Master" AutoEventWireup="true" CodeBehind="Hostel_Out_Pass_Request_list.aspx.cs" Inherits="school_web.Student_Profile.webview.Hostel_Out_Pass_Request_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Hostel Out Pass Request List 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        .clndr-dv-wpr {
            margin: 0px;
            padding: 0px;
            float: left;
            width: 100%;
            position: relative;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
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
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 12px !important;
            color: #e14eca;
            position: absolute;
            top: 8px;
            right: 6px;
            left: auto;
        }

        .modal.show .modal-dialog {
            transform: translateY(0%);
        }

        .modal {
            background: rgb(0 0 0 / 48%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translateY(0px) !important;
            }

        .modal-header {
            display: flex;
            align-items: flex-start;
            justify-content: space-between;
            padding: 12px 9px 10px 5px !important;
            border-bottom: 1px solid #e9ecef;
            border-top-left-radius: .2857rem;
            border-top-right-radius: .2857rem;
        }

        .taskside {
            padding: 1px 0px 0px 0px !important;
        }

            .taskside h4 {
                font-size: 18px;
                margin: 0px 0px 0px 0px !important;
            }

        @media only screen and (max-width: 800px) {
            .stdlst-count-bx-dv {
                width: 33% !important;
            }
        }

        .stdlst-count-bx-dv-ico i {
            padding: 5px 5px;
            background: #7a70ba;
            color: #fff;
            height: 17px;
            font-size: 14px;
            width: 22px;
            margin: 0 auto 1px;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-align: center;
            -ms-flex-align: center;
            align-items: center;
            -webkit-box-pack: center;
            -ms-flex-pack: center;
            justify-content: center;
            border-radius: 50%;
        }

        .stdlst-count-bx-name {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-weight: 500;
            color: #404040;
            letter-spacing: 0.6px;
            font-size: 11px;
        }

        .stdlst-count-bx-dv {
            margin: 0px 0px 4px 0px;
            padding: 5px 0px 4px;
            width: 20%;
            float: left;
            text-align: center;
            background-color: rgb(122 112 186 / 15%) !important;
        }

        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translateY(0px);
        }
        .notificationpan {
    
   width: 100%!important;
    background-color: rgb(231 231 231 / 0%);
    position: fixed;
    top: 133px !important;
      right: 1px;
    padding: 10px 10px;
   
    height: auto;
    border: 0px solid rgb(162, 162, 162);
    box-shadow:none;
}
            .alert {
    position: relative;
    padding: .9rem 1.25rem;
    margin-bottom: 1rem;
    border: .0625rem solid transparent;
    border-radius: .2857rem;
    width: 100% !important;
}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">

                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>

                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">

                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>

                    </div>
                </div>
            </div>


            <asp:HiddenField ID="HdID" runat="server" />



            <div class="headingtablee card" style="margin: 14px 0px 0px 0px;">
                <div class="row">
                    <div class="boxmargmm">


                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4 paddrght">
                            <label for="validationCustom01" class="lebelheadpp">Date From</label>
                            <div class="clndr-dv-wpr">
                                <asp:TextBox ID="txt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4 paddlft">
                            <label for="validationCustom01" class="lebelheadpp">Date To</label>
                            <div class="clndr-dv-wpr">
                                <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-4 col-xs-4">
                            <asp:Button ID="btn_find_by_date" OnClick="btn_find_by_date_Click" runat="server" Style="margin: 29px 2px 0px 0px !important;" Text="Find" class="btnfindaa btnfindaa2a" />
                        </div>




                    </div>
                </div>

                <div class="row" style="display: none">

                    <div class="stdlst-count-bx-dv" style="background-color: rgb(122 112 186 / 15%) !important;">
                        <div class="stdlst-count-bx-dv-ico">
                            <i class="material-symbols-outlined" style="background: #48a3d7;">person_check</i>
                        </div>
                        <h2 class="stdlst-count-bx-name">Total On Pending</h2>
                        <p class="stdlst-count-bx-count" id="P1" runat="server">0</p>
                    </div>

                    <div class="stdlst-count-bx-dv" style="background-color: rgb(122 112 186 / 15%) !important;">
                        <div class="stdlst-count-bx-dv-ico">
                            <i class="material-symbols-outlined" style="background: #48a3d7;">person_check</i>
                        </div>
                        <h2 class="stdlst-count-bx-name">Total Forward/Approved To Parents</h2>
                        <p class="stdlst-count-bx-count" id="P2" runat="server">0</p>
                    </div>

                    <div class="stdlst-count-bx-dv" style="background-color: rgb(72 163 215 / 15%) !important;">
                        <div class="stdlst-count-bx-dv-ico">
                            <i class="material-symbols-outlined" style="background: #48a3d7;">person_check</i>
                        </div>
                        <h2 class="stdlst-count-bx-name">Total Assined/On Going</h2>
                        <p class="stdlst-count-bx-count" id="lbl_total_ongoing" runat="server">0</p>
                    </div>
                    <div class="stdlst-count-bx-dv" style="background-color: rgb(32 234 139 / 15%) !important;">
                        <div class="stdlst-count-bx-dv-ico">
                            <i class="material-symbols-outlined" style="background: #6c757d;">person_check</i>
                        </div>
                        <h2 class="stdlst-count-bx-name">Total Reject</h2>
                        <p class="stdlst-count-bx-count" id="lbl_total_reject" runat="server">0</p>
                    </div>

                    <div class="stdlst-count-bx-dv" style="background-color: rgb(143 255 0 / 15%) !important;">
                        <div class="stdlst-count-bx-dv-ico">
                            <i class="material-symbols-outlined" style="background: #6c757d;">person_check</i>
                        </div>
                        <h2 class="stdlst-count-bx-name">Total Complete</h2>
                        <p class="stdlst-count-bx-count" id="lbl_total_Complite" runat="server">0</p>
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                        <div class="row">

                            <div class="grd-wpr">
                                <div id="content">



                                    <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                        <thead>
                                            <tr>
                                                <th>SL.No. </th>

                                                <th>Request ID </th>


                                                <th>Request Date</th>
                                                <th>Out Pass Date</th>
                                                <th>Returen Date</th>
                                                <th>No. of Day </th>
                                                <th>Status</th>
                                                <th>Last Remarks</th>
                                                <th>Last Reply Date</th>

                                                <th class="hiddenOnPrint">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RPDetails" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Request_id" runat="server" Text='<%#Bind("Request_id")%>'></asp:Label>
                                                            <asp:Label ID="lbl_Adm_No" runat="server" Text='<%#Bind("Adm_No")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_studentname" Visible="false" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                            <asp:Label ID="lbl_course" Visible="false" runat="server" Text='<%#Bind("classname")%>'></asp:Label>
                                                            <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>' Visible="false"></asp:Label>
                                                        </td>






                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_request_date" runat="server" Text='<%#Bind("applydate")%>'></asp:Label>
                                                        </td>


                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_outpass_date" runat="server" Text='<%#Bind("Start_datetime")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Return_Pass_Date_time" runat="server" Text='<%#Bind("End_Datetime")%>'></asp:Label>
                                                        </td>


                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_no_of_date" runat="server" Text='<%#Bind("No_of_day")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Last_status")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_remarks" Style="word-break: break-all;" runat="server" Text='<%#Bind("Last_remarks")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Last_reply_date_time" Style="word-break: break-all;" runat="server" Text='<%#Bind("Last_replydatetime")%>'></asp:Label>
                                                        </td>



                                                        <td>
                                                            <asp:LinkButton ID="lnk_view" runat="server" Style="background-color: #e14eca; color: #fff; padding: 2px 5px 2px 5px; width: auto; border-radius: 2px; font-weight: 500;" OnClick="lnk_view_Click"><i class="dropdown-icon lnr-inbox"></i><span>Process</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>


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

                </div>
            </div>
        </div>

    </div>
    <!--end row-->


    <script type="text/javascript">

        function openModal2() {

            $('#myModal').modal('show');
        }
    </script>
    <style>
        .mdl-frm-row {
            margin: 2px 0px 6px 0px !important;
            float: left !important;
            width: 100%;
        }

        .col-eq {
            flex: 1;
            background: #f4f4f4;
        }

        .taskside {
            padding: 10px;
        }

            .taskside h4 {
                margin-bottom: 0;
                font-size: 18px;
            }

        .taskseparator {
            border-top: 1px solid #a39999;
            -webkit-box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
            box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
            float: left;
            width: 100%;
            margin: 0px;
        }

        .taskside hr {
            margin-top: 9px;
            margin-bottom: 9px;
            border: 0;
            border-top: 1px solid #655f5f !important;
        }

        .task-info {
            float: left;
            width: 100%;
        }

            .task-info h5 {
                font-size: 13px;
            }

        .text-dark {
            font-weight: bold;
        }

        .fa.pull-left {
            margin-right: 0.3em;
        }

        .task-info h5 span {
        }

        @media (min-width: 360px) {
            .col-sm-10 {
                width: 80%;
                float: left;
                padding-right: 0px;
            }

            .col-sm-2 {
                width: 20%;
                float: left;
                padding-left: 0px;
            }
        }

        h5, h5 {
            line-height: 1.4em;
            margin-bottom: 5px;
        }
    </style>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
    <%-- <link href="../Stepper/stepper_demo.css" rel="stylesheet" />--%>
    <link href="../Stepper/stepper_style.css" rel="stylesheet" />
    <div id="myModal" class="modal fade" role="dialog" style="padding-left: 0px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="float: left; width: 100%;">Hostel Out Pass Process</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" Style="margin: -5px 0px 0px 0px; padding: 4px 10px 4px 7px; font-size: 12px; background: #f00; font-weight: 600; border-radius: 3px;" />
                </div>
                <div class="modal-body md-bdy" style="overflow: auto; padding: 0px 1rem; background: #f4f4f4;">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="mdl-frm-row">

                                    <div class="taskside">
                                        <div class="task-info task-single-inline-wrap ptt10">
                                            <h5><span class="text-dark">Request id:</span>
                                                <asp:Label ID="lbl_request_Id" runat="server"></asp:Label>
                                            </h5>
                                            <h5><span class="text-dark">Status:</span>
                                                <asp:Label ID="lbl_status1" runat="server"></asp:Label>
                                            </h5>
                                            <h5><span class="text-dark">Admission No:</span>
                                                <asp:Label ID="lbl_admission_no" runat="server"></asp:Label>
                                            </h5>
                                            <h5><span class="text-dark">Created By:</span>
                                                <asp:Image ID="img_created_by" runat="server" Style="height: 25px; width: 25px; padding: 2px; border: 1px solid #000;" />
                                                <asp:Label ID="lbl_created_by" runat="server" Text="xxxx"></asp:Label>
                                            </h5>
                                            <h5><span class="text-dark">Assined to:</span>
                                                <asp:Image ID="img_assined_to" runat="server" Style="height: 25px; width: 25px; padding: 2px; border: 1px solid #000;" />
                                                <asp:Label ID="lbl_assined_to" runat="server" Text="xxxx"></asp:Label>
                                            </h5>


                                            <h5><span class="text-dark">Hostel Room No.:</span>
                                                <asp:Label ID="lbl_hostel_romm_no" runat="server"></asp:Label>
                                            </h5>
                                            <h5><span class="text-dark">Hostel Bed No.:</span>
                                                <asp:Label ID="lbl_hostel_bed_no" runat="server"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>





                                </div>
                                <hr class="taskseparator" />
                                <div class="mdl-frm-row" style="height: 287px;margin-top: 7px !important;">
                                    <section>
                                        <div class="rt-container">
                                            <div class="col-rt-12">
                                                <div class="Scriptcontent">

                                                    <!-- Stepper HTML -->
                                                    <div class="step" runat="server" id="pronumS1">
                                                        <div>
                                                            <div class="circle">1</div>
                                                        </div>
                                                        <div>
                                                            <div class="title">Created</div>
                                                            <div class="caption">
                                                                <asp:Label ID="lbl_created" runat="server" style="font-weight: bold;"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="step step-active" runat="server" id="pronumS2">
                                                        <div>
                                                            <div class="circle">2</div>
                                                        </div>
                                                        <div>
                                                            <div class="title">Day Outpass: <asp:Label ID="lblstatus" runat="server" style="font-weight: bold;"></asp:Label></div>
                                                            <div class="caption">
                                                               
                                                                <asp:Label ID="lbl_approved" runat="server" style="font-weight: bold;"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="step" runat="server" id="pronumS3">
                                                        <div>
                                                            <div class="circle">3</div>
                                                        </div>
                                                        <div>
                                                            <div class="title">Day Outpass On Going</div>
                                                            <div class="caption">
                                                                <asp:Label ID="lbl_on_going" runat="server" style="font-weight: bold;"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="step" runat="server" id="pronumS4">
                                                        <div>
                                                            <div class="circle">4</div>
                                                        </div>
                                                        <div>
                                                            <div class="title">Day Outpass On Yet to Start</div>
                                                            <div class="caption">
                                                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                                    <table id="datatable1" class="table table-bordered dataTable" role="grid" aria-describedby="example2_info" style="margin: 0px !important;">
                                                                        <thead>
                                                                            <tr>


                                                                                <th style="padding: 0px !important;">Reply Date</th>

                                                                                <th style="padding: 0px !important;">Remarks</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="Repeater1_yet_to_start" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr>

                                                                                        <td style="text-align: left;padding: 3px 0px 3px 5px !important;">
                                                                                            <asp:Label ID="lblRequest_id" runat="server" Text='<%#Bind("Datetimechat")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;padding: 3px 0px 3px 5px !important;">
                                                                                            <asp:Label ID="lbl_Remarks" Style="word-break: break-all" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                                                        </td>

                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="step" style="margin-top: 12px;" runat="server" id="pronumS5">
                                                        <div>
                                                            <div class="circle">5</div>
                                                        </div>
                                                        <div>
                                                            <div class="title">Back of the hostel</div>
                                                            <div class="caption">
                                                                <asp:Label ID="lbl_end" runat="server" style="font-weight: bold;">Not End</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- End Stepper HTML -->


                                                </div>

                                            </div>
                                        </div>
                                    </section>




                                </div>

                                <hr class="taskseparator" />

                                <div class="row" runat="server" visible="true" style="margin: 0px; padding: 0px;" id="row1">

                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-sm-12">
                                            <label for="validationCustom01" class="find-dv-lbl">
                                                <sup>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Required Field."
                                                        ControlToValidate="txt_remarks_floup" ValidationGroup="b">
                                                    </asp:RequiredFieldValidator></sup>
                                            </label>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txt_remarks_floup" runat="server" class="form-control" placeholder="Comment here" TextMode="MultiLine" Style="height: 100px !important; border-bottom: 1px solid #000; background: #cfcbcb0a;"></asp:TextBox>
                                        </div>




                                        <div class="col-sm-2" style="text-align: center;">

                                            <asp:ImageButton ID="imgbutton" runat="server" ImageUrl="../images/send_button.png" Style="float: none; margin-top: 53px; background: #eb0202; border-radius: 50px; height: 50px; width: 50px; padding: 9px;"
                                                ValidationGroup="b"  OnClick="imgbutton_Click"/>

                                        </div>
                                    </div>
                                </div>


                            </div>


                        </div>



                    </div>




                </div>
            </div>
        </div>
    </div>

    <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../../Autocomplete/jquery-ui.js"></script>
    <div class="input-group input-group-icon">
    </div>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });

    </script>
    <link href="../assets/css/calender-modified.css" rel="stylesheet" />

</asp:Content>
