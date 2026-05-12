<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="print_addmission_form.aspx.cs" Inherits="school_web.LMS_VC_Admin.print_addmission_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Print Form
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/print_page.css" rel="stylesheet" />

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=table111.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');

            printWindow.document.write('<html><head><title>Print </title>');
            printWindow.document.write('</head><body style="font-family:arial, sans-serif;font-size: 12px;">');
            printWindow.document.write('<link href="../css/print_page.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write('<link href="../font-awesome-4.0.3/css/font-awesome.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <section>
        <div class="container">
            <div class="printheadse333">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="pimages22">
                            <asp:ImageButton ID="ImageButton_print" CssClass="pimages33" runat="server" ImageUrl="~/images/print/print.png" OnClientClick="return PrintPanel()" />
                            <a id="A1" href="default.aspx" runat="server" class="pimages44" title="Back">
                                <img class="backpring3" src="../images/print/backbtn.png" /></a>
                        </div>



                        <div class="printpage-sec-main" id="table111" runat="server" >
                            <div class="printlogo4455">
                                <img src="../images/print/Header_print.png" class="printlogo" />
                            </div>
                            <h2 class="informatchild22-h2">= Online Registration =</h2>
                            <%-- <p class="informatchild22-pp33">Student Receipt</p>--%>

                            <div class="administration-paragraph">

                                <div class="courses-sec">
                                    <div class="table-responsive">
                                        <table>

                                            <tr>
                                                <td style="width: 230px">आवेदक का नाम (हिंदी में) <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_applicant_name_hn" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                                <td rowspan="3" style="width: 160px;">

                                                    <asp:Image ID="Image1" runat="server" CssClass="studimages-img" ImageUrl="images/print/user33.png" />

                                                    <asp:Image ID="Image2" runat="server" CssClass="studimages-img" Style="margin: 5px 0px 5px 27px; width: 100%; height: 27px!important" />
                                                    <asp:Label ID="lbl_studentapplyid" CssClass="qualificationsec-id" runat="server" Text="Student Id"></asp:Label></td>

                                            </tr>


                                            <tr>
                                                <td style="width: 230px">आवेदक का नाम (अंग्रेजी में)<span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_applicant_name_en" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 230px">पिता / पति का नाम (अंग्रेजी में)  <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_fathername" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 230px">माता का नाम (अंग्रेजी में)<span style="float: right;">: </span></td>
                                                <td>
                                                    <asp:Label ID="lbl_mothername" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                                <td style="width: 230px">जन्म की तिथि <span style="float: right;">: </span></td>
                                                <td colspan="2">
                                                    <asp:Label ID="lbl_dob" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 230px">लिंग <span style="float: right;">: </span></td>
                                                <td>
                                                    <asp:Label ID="lbl_gender" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                                <td style="width: 230px">दिनांक 01/01/2020 को उम्र <span style="float: right;">: </span></td>
                                                <td colspan="2">
                                                    <asp:Label ID="lbl_age" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>


                                            <tr>
                                                <td colspan="2">क्या आप 65वीं सम्मिलित संयुक्त प्रारंभिक परीक्षा में सफल हुए हैं? हाँ/ नहीं <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_65_attend" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">यदि हाँ तो 65वीं सम्मिलित संयुक्त प्रारंभिक परीक्षा का क्रमांक क्या है ।  <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_65_rollno" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>


                                            <tr>
                                                <td colspan="2">अपनी श्रेणी का उल्लेख करें  <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_sharni" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">क्या आप बिहार लोक सेवा आयोग की 64वीं मुख्य परीक्षा में सम्मिलित हुए हैं?(यदि हाँ तो मुख्य परीक्षा का प्रवेश पत्र सलंगन करें) हाँ/ नहीं <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_64_attend_exam" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">मुख्य परीक्षा का ऐच्छिक विषय । <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_main_exam_subject" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">दूरभाष संख्या (एसo टीo डीo कोड सहित) । <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_tel_no" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 230px">व्हाट्सएप्प संख्या <span style="float: right;">: </span></td>
                                                <td>
                                                    <asp:Label ID="lbl_wataasp_no" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                                <td style="width: 150px">ईमेल  <span style="float: right;">: </span></td>
                                                <td colspan="2">
                                                    <asp:Label ID="lbl_emailid" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="5" style="font-weight: 600; background: #e9e9e9;">पत्राचार का पता</td>
                                            </tr>



                                            <tr>
                                                <td style="width: 230px">पता <span style="float: right;">: </span></td>
                                                <td>
                                                    <asp:Label ID="lbl_full_address_let" CssClass="qualificationsec-lablett" Style="word-break: break-all" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                                <td style="width: 150px">जिला   <span style="float: right;">: </span></td>
                                                <td colspan="2">
                                                    <asp:Label ID="lbl_district_let" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 230px">राज्य <span style="float: right;">: </span></td>
                                                <td>
                                                    <asp:Label ID="lbl_state_let" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                                <td style="width: 150px">पिन कोड  <span style="float: right;">: </span></td>
                                                <td colspan="2">
                                                    <asp:Label ID="lbl_pin_let" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="5" style="font-weight: 600; background: #e9e9e9;">स्थाई का पता</td>
                                            </tr>

                                            <tr>
                                                <td style="width: 230px">पता<span style="float: right;">: </span></td>
                                                <td>
                                                    <asp:Label ID="lbl_address_per" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                                <td style="width: 150px">जिला   <span style="float: right;">: </span></td>
                                                <td colspan="2">
                                                    <asp:Label ID="lbl_diistrict_per" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 230px">राज्य <span style="float: right;">: </span></td>
                                                <td>
                                                    <asp:Label ID="lbl_state_per" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                                <td style="width: 150px">पिन कोड  <span style="float: right;">: </span></td>
                                                <td colspan="2">
                                                    <asp:Label ID="lbl_pin_per" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>


                                            <tr>
                                                <td colspan="5" style="font-weight: 600; background: #e9e9e9;">शैक्षणिक योगता</td>
                                            </tr>

                                            <tr>
                                                <td colspan="5">

                                                    <asp:GridView ID="grid_education" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" BackColor="White" CssClass="table table-bordered">
                                                        <RowStyle />
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="उत्तीर्ण परीक्षा का नाम">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Exam_name" runat="server" Text='<%#Bind("Exam_name") %>'> </asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="संकाय एवं विषय ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Subject" runat="server" Text='<%#Bind("Subject") %>'> </asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="उत्तीर्ण होने का वर्ष">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Year" runat="server" Text='<%#Bind("Year") %>'> </asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="संस्थान">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_School_collage" runat="server" Text='<%#Bind("School_collage") %>'> </asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="बोर्ड/विश्वविद्यालय">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Board" runat="server" Text='<%#Bind("Board") %>'> </asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="प्राप्तांक पूर्णांक">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Marks" runat="server" Text='<%#Bind("Marks") %>'> </asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="प्रतिशत">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Percentage" runat="server" Text='<%#Bind("Percentage") %>'> </asp:Label>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="5">क्या आप हज भवन कोचिंग एवं मार्गदर्शन कोषांग द्वारा पूर्व में आयोजित कोचिंग कार्यक्रम में सम्मलित थे? पूर्ण विवरण दें ।</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:Label ID="lbl_haj_cochin_previous" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>


                                            <tr>
                                                <td colspan="2">प्रतियोगिता परीक्षा में आपका परीक्षा का माध्यम-हिंदी/अंग्रेजी <span style="float: right;">: </span></td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbl_type_of_language" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="5">बिहार लोक सेवा आयोग की परीक्षा के अतिरिक्त आप और किस प्रतियोगता परीक्षा में सम्मिलित होने के इक्छुक है। </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:Label ID="lbl_other_exam" CssClass="qualificationsec-lablett" runat="server" Text="xxxxxxxxxxxxxxx"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="5" style="font-weight: 600; background: #e9e9e9;">अनुलंगन पत्र</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">

                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" BackColor="White" CssClass="table table-bordered" OnRowDataBound="GridView1_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="File Name" ItemStyle-Width="178px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_filename" runat="server" Text='<%#Bind("Doc_name") %>'> </asp:Label>
                                                                    <asp:Label ID="lbl_File_id" runat="server" Text='<%#Bind("Doc_id") %>' Visible="false"> </asp:Label>
                                                                    <asp:Label ID="lbl_Doc_type" runat="server" Text='<%#Bind("Doc_type") %>' Visible="false"> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField ItemStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <a id="a1" runat="server" download title="Click here download">
                                                                        <img src="../images/downloadicons.png" style="height: 25px; width: 24px;" /></a>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td colspan="5">
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="मै" Enabled="false" Checked="true" />
                                                    एतद् द्वारा घोषणा करता/करती हूँ कि इस आवेदन में ऊपर दी गई सूचनाएँ सत्य एवं सही हैं । मैंने किसी प्रकार की जानकारी नही छुपाया है । फिर भी यदि ऊपर में दी गई कोइ जानकारी गलत सिद्ध होती है तो उसके लिए मैं जिम्मेवार होऊँगा/होऊँगी । मैं हज भवन कोचिंग एव मार्गदर्शन कोषांग के सभी नियमो का पालन करने के लिए तैयार हूँ ।</td>
                                            </tr>




                                        </table>
                                    </div>
                                </div>


                                <div class="qualimain-sec">


                                    <div class="dateplace">
                                        <p class="txt-date">तिथि :</p>
                                        <asp:Label CssClass="txt-date-label" ID="lbl_date" runat="server" Text="xxxxxxxxxxxxxx"></asp:Label>
                                    </div>

                                    <div class="dateplace">
                                        <p class="txt-date">स्थान  :</p>
                                        <asp:Label CssClass="txt-date-label" ID="lbl_place" runat="server" Text="xxxxxxxxxxxxxxx"></asp:Label>
                                    </div>
                                </div>



                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
