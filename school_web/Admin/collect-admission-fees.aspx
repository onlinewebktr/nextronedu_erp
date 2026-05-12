<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="collect-admission-fees.aspx.cs" Inherits="school_web.Admin.collect_admission_fees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Collect Admission Fees
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        @font-face {
            font-family: 'K2D';
            src: url(../assets/fonts/k2d-cufonfonts/K2D-Bold.ttf);
            font-weight: bold;
        }

        @font-face {
            font-family: 'K2D';
            src: url(../assets/fonts/k2d-cufonfonts/K2D-Medium.ttf);
            font-weight: normal;
        }

        body {
            font-family: 'K2D';
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
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 5px;
            right: 10px;
        }

        @media (min-width: 768px) {
            .col-md-6 {
                flex: 0 0 auto;
                width: 50%;
                padding-top: 0px;
                margin-top: 6px;
            }
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 19px;
            height: 19px;
            position: relative;
            top: 4.6px;
            left: 2px;
            margin: 1px 10px -4px 0px;
            z-index: 1;
        }


        .popup-adm-std-wpr {
            margin: 0px;
            padding: 5px 7px 5px 7px;
            width: 100%;
            float: left;
            background: #fff;
            border-bottom: 1px solid #ddd;
        }

        .popup-adm-std-info {
            margin: 0px;
            padding: 0px;
            width: 85%;
            float: right;
        }

        .popup-adm-std-imgs {
            margin: 0px;
            padding: 0px;
            width: 13%;
            float: left;
            height: 131px;
            overflow: hidden;
            border: 2px dashed #FFBA5F;
            border-radius: 2px;
        }

            .popup-adm-std-imgs img {
                width: 100%;
            }

        .adm-box-wprs1 {
            margin: 0px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

        .popup-adm-fees-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .adm-fees-wprsss {
            margin: 0px;
            padding: 5px;
            width: 100%;
            float: left;
            background: #fff;
        }

        .adm-fees-wprs {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            background: #fff;
            border-radius: 4px;
            border: 1px solid #dee2e6 !important;
        }

        tfoot, th, thead {
            background: #FFBA5F !important;
        }

        tbody, td, tfoot, th, thead, tr {
            font-size: 12px;
        }

        .adm-box-wprs3 {
            margin: 0px 0px 0px -1px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-left: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

        .lblfnts {
            font-size: 12.5px;
        }

        label {
            font-size: 13px;
        }

        .find-dv-txtbx {
            padding: 2px 6px;
            font-size: 12px;
        }

        .button-37 {
            margin: 2px 0px 0px 0px;
            float: left;
            font-weight: 600;
            padding: 3px 25px 5px;
            background-color: #13aa52;
            border: 1px solid #13aa52;
            border-radius: 4px;
            box-shadow: rgba(0, 0, 0, .1) 0 2px 4px 0;
            box-sizing: border-box;
            color: #fff;
            cursor: pointer;
            font-size: 14px;
            text-align: center;
            transform: translateY(0);
            transition: transform 150ms, box-shadow 150ms;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
        }

            .button-37:hover {
                color: #fff;
                box-shadow: rgba(0, 0, 0, .15) 0 3px 9px 0;
                transform: translateY(-2px);
            }

        .adm-box-wprs2 {
            margin: 0px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 43%);
        }
    </style>
    <script type="text/javascript">
        function Confirm() {

            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to print bill?")) {
                confirm_value.value = "Yes"; 
            }
            else {
                confirm_value.value = "No";
            } 
            document.forms[0].appendChild(confirm_value);
        }


        function save_data() {
            var valsubmit = $('#<%=btn_Submit.ClientID %>').val();
            if (valsubmit == "Pay") {
                $('#<%=btn_Submit.ClientID %>').val('Submitting.. Please Wait..');
                Confirm();
                document.getElementById("<%=btn_Submit.ClientID %>").click();
            }
            else {
                alert("Already submitted ")
            } 
        }

        function openChequeAlert() {
            $('#mdlAlertMsgs').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-wrapper">
        <div class="page-content">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-white">Success Alerts</h6>
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-dark">Warning Alerts</h6>
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                </div>
            </div>

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Fees Collection</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Collect Admission Fees</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <ul class="sub-pag-menu-ul sub-pag-menu-ul-mrgn" style="position: absolute; right: 6px; float: none; top: 25px; width: auto;">
                <li><a id="a2" runat="server" class="sub-mnu-p-a-active">Set Discount on Admission Fees</a></li>
                <li><a id="a1" runat="server" class="sub-mnu-p-a-active btn-success" style="background: #05c853; border: 1px solid #001017;">Collect Monthly Fees</a></li>
            </ul>

            <asp:HiddenField ID="HdID" runat="server" />



            <div class="row">
                <div class="col-xl-12">
                    <div class="adm-fees-wprsss">
                        <div class="adm-fees-wprs">
                            <div class="popup-adm-std-wpr">
                                <div class="popup-adm-std-info">
                                    <div class="row">
                                        <div class="col-xl-6 padd-rght-5">
                                            <div class="row">
                                                <div class="col-xl-4 padd-rght-5">
                                                    <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                </div>
                                                <div class="col-xl-8 padd-lft-5">
                                                    <asp:Label ID="lbl_studentname" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-6 padd-rght-5">
                                            <div class="row">
                                                <div class="col-xl-4 padd-rght-5">
                                                    <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                </div>
                                                <div class="col-xl-8 padd-lft-5">
                                                     <asp:Label ID="lbl_admissionno" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                   
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-xl-6 padd-rght-5">
                                            <div class="row">
                                                <div class="col-xl-4 padd-rght-5">
                                                    <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                </div>
                                                <div class="col-xl-8 padd-lft-5">
                                                    <asp:Label ID="lbl_class" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label> 
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-6 padd-rght-5">
                                            <div class="row">
                                                <div class="col-xl-4 padd-rght-5">
                                                    <label for="validationCustom01" class="stdnt-info-fnds">Student Type : </label>
                                                </div>
                                                <div class="col-xl-8 padd-lft-5">
                                                    <asp:Label ID="lbl_student_type" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-6 padd-rght-5">
                                            <div class="row">
                                                <div class="col-xl-4 padd-rght-5">
                                                    <label for="validationCustom01" class="stdnt-info-fnds">Session : </label>
                                                </div>
                                                <div class="col-xl-8 padd-lft-5">
                                                    <asp:Label ID="lbl_session" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-6 padd-rght-5">
                                            <div class="row">
                                                <div class="col-xl-4 padd-rght-5">
                                                    <label for="validationCustom01" class="stdnt-info-fnds">Section : </label>
                                                </div>
                                                <div class="col-xl-8 padd-lft-5">
                                                    <asp:Label ID="lbl_section" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="popup-adm-std-imgs">
                                    <asp:Image ID="Image2" runat="server" />
                                </div>
                            </div>


                            <div class="popup-adm-fees-wpr">
                                <div class="row">
                                    <div class="col-xl-6" style="padding-right: 0px">
                                        <div class="adm-box-wprs1">
                                            <asp:Label ID="Label4" runat="server" class="mb-0 text-uppercase" Style="font-weight: 500; font-size: 1rem;" Text="Admission Fees Deatils"></asp:Label>

                                            <div style="overflow: auto; float: left; width: 100%">
                                                <asp:GridView ID="grd_fee" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;" ShowFooter="True" OnRowDataBound="grd_fee_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Fees Head">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_feetype" runat="server" Text='<%#Bind("feetype") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_content1" runat="server" Font-Bold="true" ForeColor="Maroon">Total</asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fees Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_payable" runat="server" Text='<%#Bind("payable","{0:n}") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_totalfeeamount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Discount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_disc_amount" runat="server" Text='<%#Bind("disc_amount","{0:n}") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_totaldiscount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Paid Perviously">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_perviously_paid" runat="server" Text='<%#Bind("paid","{0:n}") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_totalpreviously" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Payable">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_dues" runat="server" Text='<%#Bind("net_payable","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_totalpaybale" Font-Bold="true" ForeColor="Maroon" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                </asp:GridView>

                                                <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; display: none">
                                                    <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                                        <tr>
                                                            <th colspan="3">Adjust Amount from Money Receipt</th>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px">Enter Unique Receipt No.
                                                            </td>
                                                            <td style="padding: 5px">
                                                                <asp:TextBox ID="txt_Uniqueno" Font-Bold="true" runat="server" Style="width: 100%;"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btn_adjustamount" runat="server" Text="Adjust Amount" OnClick="btn_adjustamount_Click" CssClass="btn btn-primary" Style="width: 117px;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:GridView ID="grid_adjustamount" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="grid_adjustamount_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;" ShowFooter="True">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Pay Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("slipdate") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_idate" runat="server" Text='<%#Bind("slipIdate") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Paymentmode" runat="server" Text='<%#Bind("Paymentmode") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Payment_id" runat="server" Text='<%#Bind("Payment_id") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unique Receipt No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Unique_id" runat="server" Text='<%#Bind("Unique_id") %>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <FooterTemplate><b>Total</b></FooterTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbl_totaldiscount" runat="server" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                    <RowStyle ForeColor="#000066" />
                                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="adm-box-wprs2">
                                            <asp:Label ID="Label5" runat="server" class="mb-0 text-uppercase" Style="font-weight: 500; font-size: 1rem;" Text="Payment History"></asp:Label>
                                            <div style="overflow: auto; float: left; width: 100%">
                                                <asp:GridView ID="grid_payment_history" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;" ShowFooter="True">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Slip No.">
                                                            <ItemTemplate>
                                                                <a target="_blank" href="slip/Admission_slip.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>">
                                                                    <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                                                </a>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Decription">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description") %>'></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_totaldiscount" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Payment Mode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_paymentmode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>



                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-6" style="padding-left: 0px">
                                        <div class="adm-box-wprs3">
                                            <div class="fnd-box-row-wpr">
                                                <asp:Label ID="Label1" runat="server" class="mb-0 text-uppercase" Style="font-weight: 500; font-size: 1rem;" Text="Payment Deatils"></asp:Label>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label for="validationCustom01" class="form-label lblfnts">Slip Type</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:RadioButton ID="rd_new_bill_no" runat="server" GroupName="a" Text="New Slip No." Checked="true" />
                                                        <asp:RadioButton ID="rd_old_bill" runat="server" GroupName="a" Text="Old Deleted Slip No." />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label for="validationCustom01" class="form-label lblfnts">Slip No</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txt_slip_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label for="validationCustom01" class="form-label lblfnts">Payment Mode</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_paymentmode_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem>Cash</asp:ListItem>
                                                            <asp:ListItem>Pos</asp:ListItem>
                                                            <asp:ListItem>Netbanking</asp:ListItem>
                                                            <asp:ListItem>Deposited In Bank</asp:ListItem>
                                                            <asp:ListItem>Sbdebit</asp:ListItem>
                                                            <asp:ListItem>Cheque</asp:ListItem>
                                                            <asp:ListItem>NEFT</asp:ListItem>
                                                            <asp:ListItem>Debitcard</asp:ListItem>
                                                            <asp:ListItem>Creditcard</asp:ListItem>
                                                            <asp:ListItem>Otherdcard</asp:ListItem>
                                                            <asp:ListItem>Demand Draft(DD)</asp:ListItem>
                                                            <asp:ListItem>UPI</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr" id="bank_dts_amd" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label8" class="form-label lblfnts" runat="server" Text="Bank Name"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="ddl_bank" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label6" class="form-label lblfnts" runat="server" Text="Date"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txt_bank_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        <script>
                                                            $(function () {
                                                                $("#<%=txt_bank_date.ClientID %>").datepicker({
                                                                    dateFormat: "dd/mm/yy",
                                                                    changeMonth: true,
                                                                    changeYear: true,
                                                                    yearRange: "2021:2030",

                                                                    maxDate: '0',
                                                                }).attr("readonly", "true");
                                                            });
                                                        </script>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr" id="pnl_mode_t_n_dv" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="lbl_mode_trns_no_adm" class="form-label lblfnts" runat="server" Text="Transaction No."></asp:Label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txt_trans_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label for="validationCustom01" class="form-label lblfnts">Payment Date</label>
                                                    </div>
                                                    <div class="col-md-3" runat="server" id="admpayDateDV">
                                                        <div style="margin: 0px; padding: 0px; float: left; width: 100%; position: relative" runat="server" id="paydateDVS">
                                                            <asp:TextBox ID="txt_date" runat="server" CssClass="calender-icon form-control find-dv-txtbx"></asp:TextBox>
                                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true" style="font-size: 10px; right: 3px"></i>
                                                            <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
                                                            <script src="../Autocomplete/jquery-ui.js"></script>
                                                            <script>
                                                                $(function () {
                                                                    $("#<%=txt_date.ClientID %>").datepicker({
                                                                        dateFormat: "dd/mm/yy",
                                                                        changeMonth: true,
                                                                        changeYear: true,
                                                                        yearRange: "2021:2030",
                                                                        maxDate: '0',
                                                                    }).attr("readonly", "true");
                                                                });
                                                            </script>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <label for="validationCustom01" class="form-label lblfnts">Payable Amount  </label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="lbl_adjustamount" runat="server" class="form-control find-dv-txtbx"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-md-6" style="display: none">
                                                        <asp:Label ID="lbl_paybaleamount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                                        <label for="validationCustom01" class="form-label lblfnts">Payable Amount  </label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label for="validationCustom01" class="form-label lblfnts">Paid Amount  </label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txt_paid_amount" Font-Bold="true" class="form-control find-dv-txtbx" runat="server" OnTextChanged="txt_paid_amount_TextChanged" AutoPostBack="true" Style="width: 100%;"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <label for="validationCustom01" class="form-label lblfnts">Total Dues   </label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="lbl_totaldues" runat="server" class=" lblfnts form-control find-dv-txtbx" Style="height: 24px;"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label for="validationCustom01" class="form-label  lblfnts">Remarks</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txt_remrks" Font-Bold="true" class="form-control find-dv-txtbx" runat="server" TextMode="MultiLine" Style="width: 100%; height: 50px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-md-3"></div>
                                                    <div class="col-9">
                                                        <div style="overflow: hidden; height: 1px;">
                                                            <asp:Button ID="btn_Submit" runat="server" Text="Pay" OnClick="btn_Submit_Click" CssClass="btn btn-primary" Style="width: 1px; height: 1px; margin: 0px 0px 0px 0px;" />
                                                        </div>
                                                        <a onclick="save_data()" class="button-37">Pay Now</a>
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
            </div> 
        </div>
        <!--end row-->
    </div>


    <div id="mdlAlertMsgs" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 10px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Important Message</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body" style="padding: 5px 5px;">
                    <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                        <div class="disc-tbl-wprs">
                            <asp:Label ID="lbl_alert_msgs" runat="server" class="alertmsgChk" Text="The previous payment for this child by cheque is still pending. Therefore, you cannot make another payment until the previous cheque has cleared."></asp:Label>


                            <div style="width: 100%; float: left; overflow: auto;">
                                <table style="width: 100%; margin: 0px;" class="table table-hover table-striped table-bordered">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Admission No.</th>
                                            <th>Slip No.</th>
                                            <th>Cheque No.</th>
                                            <th>Bank Name</th>
                                            <th>Cheque Date</th>
                                            <th>Cheque Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RPChkDetails" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_admission_no" runat="server" Font-Names="Arial" Text='<%#Bind("Admission_no") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_monthly_slip_no" runat="server" Font-Names="Arial" Text='<%#Bind("Monthly_slip_no") %>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Text='<%#Bind("Cheque_no") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Text='<%#Bind("Bank_name") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Text='<%#Bind("Cheque_date") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Font-Names="Arial" Text='<%#Bind("Cheque_amount") %>'></asp:Label>
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


    <asp:HiddenField ID="hd_totalamount" runat="server" />
    <asp:HiddenField ID="hd_total_discount" runat="server" />
    <asp:HiddenField ID="hd_user_Type" runat="server" />


     
</asp:Content>
