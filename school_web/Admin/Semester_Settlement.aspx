<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Semester_Settlement.aspx.cs" Inherits="school_web.Admin.Semester_Settlement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Semester Settlement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
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

        .fnd-box-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            border: 1px solid #ddd;
            border-radius: 2px;
        }

      
    </style>
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
                            <li class="breadcrumb-item active" aria-current="page">Semester Settlement</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row" id="fullinfo" runat="server" visible="false">
                <div class="col-xl-6">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=""></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation">
                                    <asp:Label ID="Label3" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Admission Fees Deatils"></asp:Label>
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


                                            <asp:TemplateField HeaderText="Paybale">
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
                                <hr />
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


                                <hr />
                                <asp:Label ID="Label1" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Payment History"></asp:Label>
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
                                                <a target="_blank" href="slip/annual-slip.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>">
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
                </div>
                <div class="col-xl-6">
                    <asp:Label ID="Label2" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="background-color: #17ffda;">
                                <div class="row g-3 needs-validation" novalidate="" style="margin-top: 20px">
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Admission No</label>
                                    </div>

                                    <div class="col-md-6">

                                        <asp:Label ID="lbl_admissionno" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Student Name</label>


                                    </div>
                                    <div class="col-md-6">

                                        <asp:Label ID="lbl_studentname" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

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
                                        <label for="validationCustom01" class="form-label">Slip No</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_slip_no" runat="server" class="form-control" Style="height: 27px; border-radius: 2px;"></asp:TextBox>
                                    </div>



                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Payment Mode</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddl_paymentmode" runat="server" Style="width: 100%; height: 28px;" OnSelectedIndexChanged="ddl_paymentmode_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Netbanking</asp:ListItem>
                                            <asp:ListItem>Deposited In Bank</asp:ListItem>
                                            <asp:ListItem>Sbdebit</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                            <asp:ListItem>NEFT</asp:ListItem>
                                            <asp:ListItem>Debitcard</asp:ListItem>
                                            <asp:ListItem>Creditcard</asp:ListItem>
                                            <asp:ListItem>Otherdcard</asp:ListItem>
                                            <asp:ListItem>UPI</asp:ListItem>

                                            <%--<asp:ListItem>Branch</asp:ListItem>--%>
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
                                                        yearRange: "1900:2100",
                                                    }).attr("readonly", "true");
                                                });
                                            </script>
                                        </div>


                                    </div>


                                    <div class="col-md-6" id="privius_head" runat="server" visible="false">
                                        <label for="validationCustom01" class="form-label">Previous Year Dues </label>
                                    </div>
                                    <div class="col-md-6" id="privius_value" runat="server" visible="false">
                                        <asp:Label ID="lbl_previous_dues" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Paybale Amount </label>
                                    </div>
                                    <div class="col-md-6">

                                        <asp:Label ID="lbl_adjustamount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                    </div>


                                    <div class="col-md-6" style="display: none">
                                        <asp:Label ID="lbl_paybaleamount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                        <label for="validationCustom01" class="form-label">Paybale Amount  </label>
                                    </div>




                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Paid Amount  </label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_paid_amount" Font-Bold="true" runat="server" OnTextChanged="txt_paid_amount_TextChanged" AutoPostBack="true" Style="width: 100%;"></asp:TextBox>


                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Settlement Amount  </label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_Settlementamount" Font-Bold="true" runat="server" OnTextChanged="txt_Settlementamount_TextChanged" AutoPostBack="true" Style="width: 100%;"></asp:TextBox>


                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Total Dues   </label>


                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="lbl_totaldues" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>


                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Remarks</label>


                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_remrks" Font-Bold="true" runat="server" TextMode="MultiLine" Style="width: 100%; height: 50px"></asp:TextBox>


                                    </div>



                                    <div class="col-12" style="text-align: center">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Pay" OnClientClick="return confirm('Are you sure you want to pay ?');" OnClick="btn_Submit_Click" CssClass="btn btn-primary" Style="width: 137px; margin: 0px 0px 0px 149px;" />

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
    <asp:HiddenField ID="hd_totalamount" runat="server" />
    <asp:HiddenField ID="hd_total_discount" runat="server" />

</asp:Content>
