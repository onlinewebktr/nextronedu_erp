<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee_Form_Apply.aspx.cs" Inherits="school_web.Payroll.Employee_Form_Apply" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Employee Details</title>
    <link href="css/reg.css" rel="stylesheet" />
    <script src="assets/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID%>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/reg.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Open+Sans:100,200,300,400,500,600,700" rel="stylesheet" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>

    <style>
        span {
            color: maroon !important;
            font-weight: bold;
        }

        span {
            color: #040404 !important;
            font-weight: bold;
        }

        .auto-style1 {
            height: 33px;
        }

        table tr td {
            padding: 7px 10px;
            border: 1px solid #988989;
            font-size: 13px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="section">
            <div class="print_form">
                <div class="prnt-btn-sec">
                    <div class="prnt-btn-sec-cntr">
                        <div class="print-btn-sec">
                            <div class="noPrint" style="float: left">
                                <asp:Button ID="btn_back" runat="server" Visible="false" ToolTip="Back" CssClass="back-btn" OnClick="btn_back_Click" />
                            </div>
                            <div class="noPrint" style="float: right">
                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" CssClass="print-btn"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="print-pg-sec" id="tblPrintIQ" runat="server">
                    <div class="print_form-inr">
                        <div class="t-crtifcate-hdr-sec">
                            <div class="t-crtifcate-logo-sec">
                                <asp:Image ID="Image1" runat="server" />
                            </div>
                            <div class="t-crtifcate-hdr-contnt-sec">
                                <asp:Label ID="lbl_school_name" class="t-certificate-comp-name-h" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_address" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                <asp:Label ID="lbl_contact_no" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                <p class="t-certificate-comp-mail-p">
                                    Email : 
                                        <asp:Label ID="lbl_email_school" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="certificate-type-name-t-sec">
                            <h1 class="t-certificate-type-name-h">EMPLOYEE INFORMATION -<asp:Label ID="lbl_session1" runat="server" Text=""></asp:Label></h1>
                        </div>


                        <div class="print_form-dtls">

                            <table class="print_table">
                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Job Information </td>
                                </tr>
                                <tr>
                                    <td>Application ID/Emp Code :</td>
                                    <td>
                                        <asp:Label ID="lbl_registrationid" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                    </td>


                                    <td rowspan="5" style="width: 130px; padding: 1px 1px;">
                                        <asp:Image ID="img_s_image" runat="server" class="studnt-img" Style="height: 120px;" />
                                        <asp:Image ID="img_s_sig" runat="server" class="studnt-sig" Style="border-top: 1px solid #000; padding: 5px 0px 0px 0px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Post Applied For :</td>
                                    <td>
                                        <asp:Label ID="lbl_applyfor" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lbl_subject_name" Visible="false" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="font-weight: 600;">Personal Information </td>
                                </tr>
                                <tr>

                                    <td class="auto-style1">Applicant's Name :</td>
                                    <td class="auto-style1">
                                        <asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="auto-style1">Email id :</td>
                                    <td class="auto-style1">
                                        <asp:Label ID="lbl_Emailid" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nationality :</td>
                                    <td>
                                        <asp:Label ID="lbl_Nationality" runat="server" Text="">INDIAN</asp:Label>
                                    </td>
                                    <td>Date of Birth :</td>
                                    <td>
                                        <asp:Label ID="lbl_Date_birthday" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">Gender :</td>
                                    <td class="auto-style1">
                                        <asp:Label ID="lbl_Gender" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="auto-style1">Place Of Birth :</td>
                                    <td class="auto-style1">
                                        <asp:Label ID="lbl_Place_Of_Birth" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>





                                <tr>
                                    <td>Birth State :</td>
                                    <td>

                                        <asp:Label ID="lbl_Birth_State" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Religion :</td>
                                    <td>

                                        <asp:Label ID="lbl_Religion" runat="server" Text=""></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>Marital Status :</td>
                                    <td>

                                        <asp:Label ID="lbl_marital_status" runat="server" Text=""></asp:Label></td>
                                    <td></td>
                                    <td></td>

                                </tr>



                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Communication Address </td>
                                </tr>

                                <tr>
                                    <td>Address</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_Address_ca" runat="server"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>City
                                    </td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_City_ca" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>State</td>
                                    <td>
                                        <asp:Label ID="lbl_State_ca" runat="server"></asp:Label>
                                    </td>
                                    <td>Pin Code
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_pincode_ca" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Mobile No</td>
                                    <td>
                                        <asp:Label ID="lbl_mobile_no_ca" runat="server"></asp:Label>
                                    </td>
                                    <td>Residence Telephone No.
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_res_tel_no" runat="server"></asp:Label>
                                    </td>
                                </tr>



                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Permanent Address</td>
                                </tr>
                                <tr>
                                    <td>Address :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_Address_pa" runat="server" Text=""></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>City :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_City_pa" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>State :</td>
                                    <td>
                                        <asp:Label ID="lbl_State_pa" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Pin Code :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_pincode_pa" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>




                                <%-- ============================ --%>

                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Family Information (Children Information)</td>
                                </tr>
                                <tr>

                                    <td>Child 1 :</td>
                                    <td>
                                        <asp:Label ID="lbl_Child1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Gender  :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Gender1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Age :</td>
                                    <td>
                                        <asp:Label ID="lbl_age1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>Child 2 :</td>
                                    <td>
                                        <asp:Label ID="lbl_Child2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Gender :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Gender2" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Age :</td>
                                    <td>
                                        <asp:Label ID="lbl_age2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td colspan="2"></td>
                                </tr>


                                <tr>
                                    <td>Child 3 :</td>
                                    <td>
                                        <asp:Label ID="lbl_Child3" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Gender :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Gender3" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Age :</td>
                                    <td>
                                        <asp:Label ID="lbl_age3" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td colspan="2"></td>
                                </tr>

                                <tr>
                                    <td>Father's Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_fathername" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Occupation :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_father_Occupation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mother's Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_mothername" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Occupation :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_mother_Occupation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Spouse's Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_Spouses_name" runat="server"></asp:Label>
                                    </td>
                                    <td>Spouse's Job is Transferable :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Spouses_job" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Spouse's Qualification :</td>
                                    <td>
                                        <asp:Label ID="lbl_Spousequlification" runat="server"></asp:Label>
                                    </td>
                                    <td>Spouse's Profession :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Spouses_Profession" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Spouse's Organization :</td>
                                    <td>
                                        <asp:Label ID="lbl_Spouses_Organization" runat="server"></asp:Label>
                                    </td>
                                    <td>Spouse's Designation :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Spouses_Designation" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <%-- ============================ --%>

                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Academic/Professional Qualification (Please mention 'NA' if not applicable)</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="grid_Academic" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" ShowFooter="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qualification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Qualification" Style="word-break: break-all; text-transform: uppercase" runat="server" Text='<%#Bind("Qualification")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Main Subjects">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Main_Subjects" runat="server" Text='<%#Bind("Main_Subjects")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="School/College">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_School_College" runat="server" Text='<%#Bind("School_College")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Board/University">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Board_University" runat="server" Text='<%#Bind("Board_University")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Year of Passing">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Year_of_Passing" runat="server" Text='<%#Bind("Year_of_Passing")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Percentage of Marks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Percentage_of_Marks" runat="server" Text='<%#Bind("Percentage_of_Marks")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Division">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_other_responsible" runat="server" Text='<%#Bind("Division")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Work Experience (From Latest To Oldest) 
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="I 'm Fresher " /></td>
                                </tr>

                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="grid_work_experiance" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_work_experiance_RowDataBound" ShowFooter="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Organization">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Organization" Style="word-break: break-all" runat="server" Text='<%#Bind("Organization")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_From_Date" runat="server" Text='<%#Bind("From_Date")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_To_Date" runat="server" Text='<%#Bind("To_Date")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate><b>Days</b></FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Days">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Total_Days" runat="server" Text='<%#Bind("Total_Days")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl_Total_Days_row" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specifications">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Specifications" runat="server" Text='<%#Bind("Specifications")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Other">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Cass_teacher" runat="server" Text='<%#Bind("Cass_teacher")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Responsible">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_other_responsible" runat="server" Text='<%#Bind("Other_responsible")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Total Experience</td>
                                </tr>
                                <tr>
                                    <td>In Completed Years :</td>
                                    <td>
                                        <asp:Label ID="lbl_Completed_Years" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Teaching :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Teaching_years" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Administration :</td>
                                    <td>
                                        <asp:Label ID="lbl_Administration_yers" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Any Other (Please Specify) :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_any_other_yers" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Current Job Information</td>
                                </tr>

                                <tr>
                                    <td>Name Of Institution :</td>
                                    <td>
                                        <asp:Label ID="lbl_name_Institution" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Address :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Address_institution" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Contact Number :</td>
                                    <td>
                                        <asp:Label ID="lbl_Contact_institution" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Present/Previous Designation :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Present_previous_designation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td>Date of Joining :</td>
                                    <td>
                                        <asp:Label ID="lbl_date_of_joining" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Place of Posting :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Place_of_Posting" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Total Present Salary :</td>
                                    <td>
                                        <asp:Label ID="lbl_present_salery" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Basic Salery :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Basic_salery" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Allowance :</td>
                                    <td>
                                        <asp:Label ID="lbl_Allowance" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Other Benefits :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Other_Benefits" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Are You Under Service Bond? :</td>
                                    <td>
                                        <asp:Label ID="lbl_are_you_uder_service" runat="server" Text=""></asp:Label>

                                    </td>
                                    <td>Expected Salsry :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Expected_Salsry" runat="server" Text=""></asp:Label></td>
                                </tr>




                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Proficiency In Languages</td>
                                </tr>

                                <tr>
                                    <td colspan="5" style="font-weight: 600;">
                                        <asp:GridView ID="grid_english" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Lanuage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Organization" Style="word-break: break-all" runat="server" Text="ENGLISH"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Read">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_English_read" runat="server" Text='<%#Bind("English_read")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Write">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_English_write" runat="server" Text='<%#Bind("English_write")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Speak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_English_Speak" runat="server" Text='<%#Bind("English_Speak")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                        <asp:GridView ID="grid_hindi" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Lanuage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_hindi" Style="word-break: break-all" runat="server" Text="HINDI"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Read">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Hindi_read" runat="server" Text='<%#Bind("Hindi_read")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Write">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Hindi_write" runat="server" Text='<%#Bind("Hindi_write")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Speak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Hindi_speak" runat="server" Text='<%#Bind("Hindi_speak")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>







                                            </Columns>
                                        </asp:GridView>

                                        <asp:GridView ID="grid_bangali" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Lanuage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_hindi" Style="word-break: break-all" runat="server" Text="BANGALI"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Read">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Bangla_read" runat="server" Text='<%#Bind("Bangla_read")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Write">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Bangla_write" runat="server" Text='<%#Bind("Bangla_write")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Speak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Bangla_speak" runat="server" Text='<%#Bind("Bangla_speak")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>







                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>


                                <tr>
                                    <td>Any Other Language Know :</td>
                                    <td>
                                        <asp:Label ID="lbl_any_other_langwage" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Proficiency In Computer :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Proficiency_computer" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">Uploaded Documents :</td>

                                </tr>
                                <tr>
                                    <td colspan="5" >
                                        <table style="width:100%">
                                            <tr>
                                                <th>Document Type</th>
                                                <th>Document</th>
                                            </tr>
                                            <asp:Repeater ID="rp_doc" runat="server" OnItemDataBound="rp_doc_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <asp:Label ID="lbl_doc_path" runat="server" Visible="false" Text='<%#Bind("file_path")%>'></asp:Label>
                                                        <td>  <asp:Label ID="lbl_doc_type" runat="server" Text='<%#Bind("Doc_Name")%>'></asp:Label></td>
                                                        <td>  <asp:Literal ID="lbl_doc" runat="server"  ></asp:Literal>
 
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </table>
                                    </td>
                                </tr>


                            </table>







                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
