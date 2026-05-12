<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/webview/Site1.Master" AutoEventWireup="true" CodeBehind="Student_Annual_Payment.aspx.cs" Inherits="school_web.Student_Profile.webview.Student_Annual_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Annual Fee Payment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
               <%--   $('#<%=btn_Submit.ClientID %>').click(); --%>
                Confirm();
                document.getElementById("<%=btn_Submit.ClientID %>").click();

            }
            else {
                alert("Already submitted")
            }

        }
    </script>

    <style>
        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #a29b9b;
            text-align: center;
            padding: 3px 4px 3px 5px;
            font-size: 10px;
        }
    </style>
    <style>
        body {
            background-color: #17ffda;
        }
    </style>

    <style>
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            padding: 14px 0px 0px 0px;
            line-height: 49px;
            font-family: Arial;
            font-size: 23pt;
            border: 5px solid #67CFF5;
            width: 89%;
            height: 254px;
            display: block;
            position: fixed;
            background-color: White;
            z-index: 999;
            left: 50px;
            top: 102px;
        }

        .btn.disabled, .btn[disabled], fieldset[disabled] .btn { 
            opacity: 1;
            background: #cbcbcb;
            border: 1px solid #b7b5b5;
        }
    </style>
    <script type="text/javascript">
        function ShowProgress() {

            // alert('sdsjgdhsdgfsd');
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function ShowProgress_hide() {
            // alert('sdsjgdhsdgfsd');

            document.getElementsByClassName('loading').style.visibility = 'hidden';

        }
        $('form').on("submit", function () {
            // alert('sdsjgdhsdgfsd');
            ShowProgress();
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="height: 1px; overflow: hidden">
            <asp:Button ID="btnSubmit" runat="server" Text="Pay"
                OnClientClick="retun ShowProgress();" />
        </div>
        <div class="loading" align="center" id="a1" runat="server">
            Please wait.We have checking your payment status Processing. Please not close and Back app. When till payment process not done.
        <br />
            <br />
            <img src="Get_Epay/loader.gif" />

        </div>
    </div>
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

        <div class="clearfix"></div>

        <div class="fnd-box-wpr-inr" style="padding: 12px 24px 0px 25px; background-color: #17ffda;">



            <div class="card">
                <div class="card-body">
                    <div class="p-4 border rounded">
                        <%--                        <div class="row g-3 needs-validation">--%>
                        <asp:Label ID="Label3" runat="server" Style="font-weight: 500; font-size: 1rem; font-size: 15px;" class="mb-0 text-uppercase" Text="Fees Deatils"></asp:Label>
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
                                <asp:TemplateField HeaderText="Paid">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_perviously_paid" runat="server" Text='<%#Bind("paid","{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totalpreviously" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues">
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
                    </div>
                </div>
            </div>


            <div class="card">
                <div class="card-body">
                    <div class="p-4 border rounded" style="background-color: #17ffda;">
                        <div class="row" style="margin-top: 20px">
                            <asp:Panel ID="Panel1" runat="server" Visible="false">

                                <div class="col-md-6 col-xs-6">
                                    <label for="validationCustom01" class="form-label">Admission No</label>
                                </div>

                                <div class="col-md-6 col-xs-6">

                                    <asp:Label ID="lbl_admissionno" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

                                </div>
                                <div class="col-md-6 col-xs-6">
                                    <label for="validationCustom01" class="form-label">Student Name</label>


                                </div>



                                <div class="col-md-6">

                                    <asp:Label ID="lbl_studentname" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

                                </div>
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Student Type</label>


                                </div>
                                <div class="col-md-6">

                                    <asp:Label ID="lbl_student_type" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

                                </div>
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Session</label>


                                </div>
                                <div class="col-md-6">

                                    <asp:Label ID="lbl_session" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

                                </div>




                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Class</label>


                                </div>
                                <div class="col-md-6">

                                    <asp:Label ID="lbl_class" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

                                </div>
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Deleted Slip No.</label>
                                </div>
                                <div class="col-md-6">


                                    <asp:RadioButton ID="rd_new_bill_no" runat="server" GroupName="a" Text="New Slip No." Checked="true" />
                                    <asp:RadioButton ID="rd_old_bill" runat="server" GroupName="a" Text="Old Deleted Slip No." />
                                </div>

                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Slip No</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txt_slip_no" runat="server" class="form-control" Style="height: 27px; border-radius: 2px;"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Payment Mode</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddl_paymentmode" runat="server" Style="width: 100%; height: 28px;">
                                        <asp:ListItem>Online</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6" id="pnl_mode_t_nS" runat="server" visible="false">
                                    <asp:Label ID="lbl_mode_trns_no" class="form-label" runat="server" Text="Transaction No."></asp:Label>
                                </div>
                                <div class="col-md-6" id="pnl_mode_t_nSS" runat="server" visible="false">
                                    <asp:TextBox ID="txt_trans_no" runat="server" class="form-control" Style="height: 27px; border-radius: 2px;"></asp:TextBox>
                                </div>


                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Date  </label>


                                </div>
                                <div class="col-md-6">
                                    <div style="margin: 0px; padding: 0px; float: left; width: 100%; position: relative">
                                        <asp:TextBox ID="txt_date" runat="server" CssClass="calender-icon" Style="width: 100%;"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />

                                        <script src="../Autocomplete/jquery-ui.js"></script>
                                        <script>
                                            $(function () {
                                                $("#<%=txt_date.ClientID %>").datepicker({
                                                    dateFormat: "dd/mm/yy",
                                                    changeMonth: true,
                                                    changeYear: true,
                                                    yearRange: "2021:2023",

                                                    maxDate: '0',
                                                }).attr("readonly", "true");
                                            });
                                        </script>
                                    </div>


                                </div>

                            </asp:Panel>


                            <div class="col-md-6 col-xs-6" id="privius_head" runat="server" visible="false">
                                <label for="validationCustom01" class="form-label">Previous Year Dues </label>
                            </div>
                            <div class="col-md-6 col-xs-6" id="privius_value" runat="server" visible="false">
                                <asp:Label ID="lbl_previous_dues" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                            </div>

                            <div class="col-md-6 col-xs-6"  id="tPayableDV1" runat="server">
                                <label for="validationCustom01" class="form-label">Payable Amount</label>
                            </div>
                            <div class="col-md-6 col-xs-6" id="tPayableDV2" runat="server">
                                <asp:Label ID="lbl_adjustamount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                            </div>



                            <div class="col-md-6 col-xs-6" style="display: none">
                                <asp:Label ID="lbl_paybaleamount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                <label for="validationCustom01" class="form-label">Payable Amount  </label>
                            </div>

                            <div class="col-md-6 col-xs-6">
                                <label for="validationCustom01" class="form-label" style="padding: 9px 0px 0px 0px; float: left; margin: 0px 0px 6px 0px;">
                                    Payment Total
                                </label>
                            </div>
                            <div class="col-md-6 col-xs-6">
                                <asp:TextBox ID="txt_paid_amount" Font-Bold="true" runat="server" OnTextChanged="txt_paid_amount_TextChanged" AutoPostBack="true" Style="font-weight: bold; width: 100%; margin: 10px 0px 6px 0px;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" id="ttlduesDV" runat="server">
                            <div class="col-md-6 col-xs-6">
                                <label for="validationCustom01" class="form-label">Total Dues</label>
                            </div>

                            <div class="col-md-6 col-xs-6">
                                <asp:Label ID="lbl_totaldues" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 col-xs-6" style="display: none">
                                <label for="validationCustom01" class="form-label">Remarks</label>
                            </div>
                            <div class="col-md-6 col-xs-6" style="display: none">
                                <asp:TextBox ID="txt_remrks" Font-Bold="true" runat="server" TextMode="MultiLine" Style="width: 100%; height: 50px">Online Fee</asp:TextBox>
                            </div>



                            <div class="col-12" style="text-align: center">
                                <asp:Button ID="btn_Submit" runat="server" Text="Pay" OnClientClick="return confirm('Are you sure you want to pay ?');" OnClick="btn_Submit_Click" CssClass="btn btn-primary" Style="width: 137px; margin: 6px 0px 0px 167px;" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="p-4 border rounded" style="background-color: #17ffda;">
                        <div class="row">
                            <div class="col-md-12 col-xs-12">
                                <asp:Label ID="Label1" runat="server" Style="font-weight: 500; font-size: 1rem; font-size: 15px;" class="mb-0 text-uppercase" Text="Payment History"></asp:Label>
                                <asp:GridView ID="grid_payment_history" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;" ShowFooter="True" OnRowDataBound="grid_payment_history_RowDataBound">
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
                                                <%--   <a target="_blank" href="slip/annual-slip.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>">
                                            <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                        </a>--%>

                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Decription" Visible="false">
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

                                        <asp:TemplateField HeaderText="Payment Mode" Visible="false">
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
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hd_totalamount" runat="server" />
    <asp:HiddenField ID="hd_total_discount" runat="server" />
</asp:Content>
