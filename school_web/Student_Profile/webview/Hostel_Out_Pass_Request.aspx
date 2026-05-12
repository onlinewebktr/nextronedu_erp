<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/webview/Site2.Master" AutoEventWireup="true" CodeBehind="Hostel_Out_Pass_Request.aspx.cs" Inherits="school_web.Student_Profile.webview.Hostel_Out_Pass_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Hostel Out Pass Request
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

        .gridcss th {
            font-size: 14px !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #000 !important;
            position: absolute;
            top: 6px !important;
            left: 130px !important;
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
    <script src="../../Content/js/my.js"></script>

    <script src="../../Content/js/sweetalert2@11.min.js"></script>

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

            <div class="headingtablee">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-md-12 col-xs-12 paddrght">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Select Date and Time</p>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-xs-6 paddrght">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">From Date</p>
                                            <div class="clndr-dv-wpr">
                                                <asp:TextBox ID="txt_from_date" runat="server" CssClass="form-control"  ></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-xs-6 paddleft">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Time</p>
                                            <div class="clndr-dv-wpr">




                                                <asp:TextBox ID="txt_time_from" CssClass="form-control timepicker"  runat="server" data-format="hh:mm:ss tt" AutoCompleteType="None" TextMode="Time"  ></asp:TextBox>


                                                <script>
                                                    $(function () {
                                                        $('.timepicker').timepicker({
                                                            timeFormat: 'h:mm p',
                                                            interval: 30,
                                                            minTime: '9:00am',
                                                            maxTime: '6:00pm',
                                                            defaultTime: '9:00am',
                                                            dynamic: false,
                                                            dropdown: true,
                                                            scrollbar: true
                                                        });
                                                    });
                                                </script>


                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-12 col-xs-12 paddrght">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Return Date and Time</p>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-xs-6 paddrght">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">To Date</p>
                                            <div class="clndr-dv-wpr">
                                                <asp:TextBox ID="txt_todate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-xs-6 paddleft">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">To Time</p>
                                            <div class="clndr-dv-wpr">




                                                <asp:TextBox ID="txt_to_time" CssClass="form-control timepicker" runat="server" data-format="hh:mm:ss tt" AutoCompleteType="None" TextMode="Time"></asp:TextBox>





                                            </div>
                                        </div>
                                    </div>


                                     <div class="col-md-12 col-xs-12 paddleft">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Remarks</p>
                                            <div class="clndr-dv-wpr">
                                                <asp:TextBox ID="txt_remarks" style="height: 109px !important;
    border: 1px solid #000!important;" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="btns-dv-wpr" style="text-align: center;">
                                        <asp:Button ID="btn_submit" OnClientClick="Confirm()" runat="server" Text="Submit" class="mt-2 btn btn-primary" OnClick="btn_submit_Click" />
                                         
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
    <script>
        $(function () {
            $("#<%=txt_from_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2024:2050"
            });
        });
        $(function () {
            $("#<%=txt_todate.ClientID %>").datepicker({
                  dateFormat: "dd/mm/yy",
                  changeMonth: true,
                  changeYear: true,
                  yearRange: "2024:2050"
              });
          });

        


    </script>
     <script type="text/javascript">
         function Confirm() {
             var confirm_value
             var isSubmitted = false;
             confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Do you want to submit final?")) {
                 confirm_value.value = "Yes";
                 if (!isSubmitted) {
                     $('#<%=btn_submit.ClientID %>').val('Submitting.. Please Wait..');
                     isSubmitted = true;
                 }
                 else {
                     alert("Please Wait.. due to process is running");
                 }
             }
             else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
     </script>

</asp:Content>
