<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/webview/Site1.Master" AutoEventWireup="true" CodeBehind="OnlineResult_view.aspx.cs" Inherits="school_web.Student_Profile.webview.OnlineResult_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Result View
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ111.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/print_report.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }


    </script>
    <script type="text/javascript" async src="https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.1/MathJax.js?config=MML_HTMLorMML-full">
    </script>

    <link href="css/print_report.css" rel="stylesheet" />
    <style>
        @media screen and (max-width: 992px) {
            .hide1 {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">

        <div class="mainautot">
            <div style="padding: 0px 0px 0px 0px; margin: 10px 0px 0px 0px; height: 45px; width: 870px; float: left;">
                <asp:Button ID="btn_back" runat="server" Text="Back" class="noPrint"
                    Style="float: left; background-color: black; color: white; font-size: 16px" OnClick="btn_back_Click" Height="40px" Width="65px" />
                <asp:Button ID="btn_print" runat="server" Text="Printit" class="noPrint hide1" OnClientClick="return PrintPanel()"
                    Style="float: right; background-color: black; color: white; font-size: 16px" Height="40px" Width="65px" />
            </div>
        </div>
        <div class="mainautot">
            <div class="certificate-wpr1">
                <div class="certificate-wpr2">
                    <div class="mainwith">

                        <div id="tblPrintIQ111" runat="server">
                            <div class="top" style="display: none">
                                <div class="topcell_left">
                                    Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="topcell_right">
                                    School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="heading" id="textheader" runat="server">
                                <div class="leftlogoheading">
                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                                </div>
                                <div class="righttextheading" style="text-align: center;">
                                    <h1 class="firm-name-h">
                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                    </h1>

                                    <div class="addres-firm">
                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                    </div>
                                    <div class="email-firm">
                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                    </div>
                                    <div runat="server" id="contact_no" visible="false" class="contact-frim">
                                        Contact No. :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="heading" id="printheader" runat="server" style="text-align: center">

                                <asp:Image ID="img_header" runat="server" />

                            </div>

                            <div class="slipno">
                                <div class="slipno_middle" style="width: 100%; text-align: center; padding-left: 24px;">
                                    <b>Online Test Reuslt</b>
                                </div>
                            </div>
                            <div class="slipno">
                                <div class="slipno_left">
                                    Date :
                        <asp:Label ID="lbl_paymentdate" runat="server" Font-Bold="true"></asp:Label>
                                </div>

                                <div class="slipno_right">
                                </div>
                            </div>
                            <div class="studentdetails">
                                <div class="student_left-p-info">
                                    <p>Name</p>
                                    <i>:</i>
                                    <asp:Label ID="lbl_studentname" runat="server"></asp:Label>
                                </div>
                                <div class="student_left-p-info">
                                    <p>Adm. No.</p>
                                    <i>:</i>
                                    <asp:Label ID="lbl_aadmissionno" runat="server"></asp:Label>
                                </div>
                                <div class="student_left-p-info">
                                    <p>Class</p>
                                    <i>:</i>
                                    <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="studentdetails" style="border-bottom: 2px solid #000;">
                                <div class="student_left-p-info">
                                    <p>Father Name</p>
                                    <i>:</i>
                                    <asp:Label ID="lbl_fathername" runat="server"></asp:Label>
                                </div>
                                <div class="student_left-p-info">
                                    <p>Session</p>
                                    <i>:</i>
                                    <asp:Label ID="lbl_session" runat="server"></asp:Label>
                                </div>
                                <div class="student_left-p-info">
                                    <p>Roll</p>
                                    <i>:</i>
                                    <asp:Label ID="lbl_rollno" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="studentdetails">
                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; font-size: 17px;">
                                    <tr>
                                        <td style="padding: 5px;">Test Name
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_testname" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">Subject Name
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lblsubject_name" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">Number of questions
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_no_of_question" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">Number of correct Answers
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_correct_answer" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 5px;">Number of answered questions
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_tot_answered" runat="server"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">Number of unattempt questions
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_unattempted" runat="server"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">Number of Negative questions
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_no_neagtive_marks" runat="server"></asp:Label>
                                        </td>

                                    </tr>



                                    <tr>
                                        <td style="padding: 5px;">Total Positive Marks
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_p_marks" runat="server"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">Total Negative Marks
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_n_marks" runat="server"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">Total Obtains Marks
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_obtains" runat="server"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px;">
                                            <div style="margin: 0px auto; text-align: center; height: 50px; width: 50px; background: #080808; color: #fff; font-weight: bold; border-radius: 7px; padding: 15px 0px 0px 0px;">
                                                <asp:Label ID="lbl_marks1" runat="server"></asp:Label>/<asp:Label ID="lbl_total_marks" runat="server"></asp:Label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td colspan="2">
                                            <asp:LinkButton ID="lbl_viewresult" runat="server" OnClick="lbl_viewresult_Click" ForeColor="Black">Explanation View</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>


                            <div class="pay_particular">
                                <asp:GridView ID="grd_view" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#333" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="grd_view_RowDataBound" CssClass="grd" Style="width: 100%; margin: 10px 0px 0px 0px; font-size: 16px; text-align: left">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Question with Answer">
                                            <ItemTemplate>
                                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; text-align: left">

                                                    <tr>


                                                        <td style="padding: 1px 0px 1px 3px; text-align: left;" colspan="2">
                                                            <asp:Label ID="lbl_sl" runat="server" ForeColor="Red" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>)&nbsp;  <span style="color: blue"> </span>
                                                            <asp:Label ID="lbl_question" runat="server" Text='<%#Bind("Question_name")%>' Style="word-break: break-all" ForeColor="Blue"></asp:Label>
                                                            <%--<br />--%>
                                                            <span style="color: green; display: none">Question (HN)</span>
                                                            <asp:Label ID="lbl_question_hn" runat="server" Text='<%#Bind("Question_name_HN")%>' CssClass="font_hindi_normal" Style="word-break: break-all; display: none" ForeColor="Green"></asp:Label>
                                                        </td>






                                                    </tr>
                                                    <tr>

                                                        <td style="padding: 1px 0px 1px 3px; text-align: left;" colspan="2">
                                                            <span style="color: Blue">Correct Answer </span><asp:Label ID="lbl_answer" runat="server" Text='<%#Bind("ans")%>' Style="word-break: break-all" ForeColor="Blue"></asp:Label>
                                                            <%--<br />--%>

                                                            <span style="color: Blue; display: none">Answer (HN)</span>
                                                            <asp:Label ID="lbl_answer_hn" runat="server" Text='<%#Bind("ans_HN")%>' CssClass="font_hindi_normal" Style="word-break: break-all; display: none" ForeColor="Green"></asp:Label>
                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 1px 0px 1px 3px; text-align: left;" colspan="2">
                                                            <span style="color: Blue">Your Answer</span>
                                                            <asp:Label ID="lbl_your_asnwer" runat="server" Style="word-break: break-all"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 1px 0px 1px 3px; text-align: left;" colspan="2">
                                                            <i class="fa fa-clock-o" aria-hidden="true" style="font-size: 16px; display: none"></i>
                                                            &nbsp;
                                                <asp:Label ID="lbl_time" runat="server" Text="0:0(mm:ss)" Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lbl_test_code" runat="server" Text='<%#Bind("test_id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_quest_code" runat="server" Text='<%#Bind("questionid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Opetion_id" runat="server" Text='<%#Bind("Opetion_id")%>' Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>


                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <HeaderStyle BackColor="#E4E4E4" Font-Bold="True" ForeColor="Black" />
                                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                    <RowStyle BackColor="White" ForeColor="#333" />
                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>




    </div>
    <asp:HiddenField ID="hd_studentid" runat="server" />
    <asp:HiddenField ID="hd_testid" runat="server" />
    <asp:HiddenField ID="hd_ipaddress" runat="server" />
    <asp:HiddenField ID="hd_idate" runat="server" />
    <asp:HiddenField ID="hd_attempt_id" runat="server" />

    <asp:HiddenField ID="hd_Examcode" runat="server" />
    <asp:HiddenField ID="hd_examtype_code" runat="server" />
    <asp:HiddenField ID="hd_testmode" runat="server" />
    <asp:HiddenField ID="hd_df_language" runat="server" />
    <asp:HiddenField ID="hd_exam_category" runat="server" />

    <asp:HiddenField ID="hd_entry_id" runat="server" />
    <asp:HiddenField ID="hd_package_id" runat="server" />
</asp:Content>
