<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admission-form.aspx.cs" Inherits="school_web.Admin.slip.admission_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Form</title>
    <link href="css/reg-form.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/reg-form.css" rel="stylesheet" type="text/css" />');
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="invoice-sec">
            <div class="prnt-btn-sec" runat="server" id="printBtns">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="btn_back" CssClass="back-btn" runat="server" OnClick="btn_back_Click" />
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>


            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec">
                    <div class="invoice-wpr">
                        <asp:Image ID="img_watermark" Visible="false" runat="server" class="wtr-mrk-img" />
                        <div class="report-card-wpr">
                            <div id="textheader" runat="server" style="width: 100%">
                                <div class="report-card-head {{reportCardSubS[0].Hdr_frst}}">
                                    <div class="report-card-left-dv">
                                        <asp:Image ID="Image1" runat="server" />
                                        <asp:Label ID="lbl_estd" runat="server" class="estdTextP"></asp:Label>
                                    </div>
                                    <div class="report-card-cntr-dv">
                                        <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server" Style="font-size: 30px; line-height: 38px;"></asp:Label>

                                        <asp:Label ID="lbl_reg_udise_id" class="report-card-schl-emil" runat="server"></asp:Label>


                                        <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server" Style="display: none"></asp:Label>
                                        <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add" Style="font-size: 17px; line-height: 25px;"></asp:Label>
                                        <asp:Label ID="lbl_contact_no" runat="server" Text="" class="report-card-schl-cont v-false {{reportCardSubS[0].Is_contact_no_show}}"></asp:Label>


                                        <p class="report-card-schl-emil" style="display: none">
                                            Website : 
                                        <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                        </p>
                                        <h2 class="report-card-ac-sson" style="font-size: 17px;">Admission Form (SESSION :
                                        <asp:Label ID="lbl_sessions" runat="server" Text="2026-2027"></asp:Label>)</h2>
                                    </div>


                                    <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server" Style="display: none"></asp:Label>
                                    <div class="report-card-rght-dv" style="margin: 6px 0px 0px 0px;">
                                        <p>Affix passport size photo of the student</p>
                                    </div>
                                </div>
                            </div>

                            <div id="printheader" runat="server" style="width: 100%; text-align: center">
                                <asp:Image ID="img_header" runat="server" />

                                <h2 style="border-top: 1px solid #3c3c3c; margin: 4px 0px -10px 0px; font-size: 22px; padding: 0px 0px 2px 0px;">Re-Admission Form (Session : 2026-2027)</h2>
                                <p style="margin: 8px 0px 3px 0px;">(Students will not be charged any Re-Admission fee.)</p>
                                <p style="margin: 0px 0px 0px 0px;">(Filling out this form is mandatory for all students until 10th May 2026.)</p>
                            </div>




                            <div class="form-wprs-dv">
                                <h2 class="form-wprs-dv-title-h">Details of the student</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="personaldetails" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50" style="width: 60%;">
                                                <p class="form-wprs-contnt-p">
                                                    Admission No. :
                                                    <asp:Label ID="Label1" runat="server" Style="width: 35%; left: 113px;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50" style="width: 40%;">
                                                <p class="form-wprs-contnt-p">
                                                    Admission Date  :
                                                <asp:Label ID="lbl_date" runat="server" Style="width: 62%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>


                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50" style="width: 40%;">
                                                <p class="form-wprs-contnt-p">
                                                    Admission in Class  :
                                                <asp:Label ID="lbl_application_no" runat="server" Style="width: 51%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50" style="width: 30%">
                                                <p class="form-wprs-contnt-p">
                                                    Caste  :
                                                <asp:Label ID="Label2" runat="server" Style="width: 70%; right: 20px;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50" style="width: 30%">
                                                <p class="form-wprs-contnt-p">
                                                    Category  :
                                                <asp:Label ID="Label3" runat="server" Style="width: 66%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    WhatsApp No.  :
                                                <asp:Label ID="Label4" runat="server" Style="width: 67%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Mobile No.  :
                                                <asp:Label ID="Label5" runat="server" Style="width: 79%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Name of the Scholar <i style="text-transform: lowercase; font-style: normal;">(in block letter)</i> : 
                                                    <span id="lbl_candidates_name" style="right: 0px; width: 67%;"></span>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50" style="width: 30%">
                                                <p class="form-wprs-contnt-p">
                                                    Date of Birth  :
                                                <asp:Label ID="Label6" runat="server" Style="width: 53%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50" style="width: 70%">
                                                <p class="form-wprs-contnt-p">
                                                    in word  :
                                                <asp:Label ID="Label7" runat="server" Style="width: 88%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                            <%--<div class="wdth50" style="width: 23%">
                                                <p class="form-wprs-contnt-p">
                                                    Age Proof  :
                                                <asp:Label ID="Label8" runat="server" Style="width: 57%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>--%>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Mother's Name  : 
                                                    <span id="lbl_mth_name" style="right: 0px; width: 85%;"></span>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Father's Name : 
                                                    <span id="lbl_fth_name" style="right: 0px; width: 85%;"></span>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Occupation of Father :
                                                <asp:Label ID="Label9" runat="server" Style="width: 57%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Occupation Of Mother  :
                                                <asp:Label ID="Label10" runat="server" Style="width: 58%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Permanent Address : 
                                                    <span id="lbl_add_name" style="right: 0px; width: 81%;"></span>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p" style="margin: 20px 0px 0px 0px;">
                                                    <span id="lblfgname" style="right: 0px; width: 100%;"></span>
                                                </p>
                                            </div>
                                        </div>



                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Correspondence address : 
                                                    <span id="lbl_addd_name" style="right: 0px; width: 76%;"></span>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p" style="margin: 20px 0px 0px 0px;">
                                                    <span id="lblfggg" style="right: 0px; width: 100%;"></span>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Nationality :
                                                <asp:Label ID="Label11" runat="server" Style="width: 74%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Religion :
                                                <asp:Label ID="Label12" runat="server" Style="width: 83%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Last School Attended :  
                                                    <span id="lbl_df_name" style="right: 0px; width: 78%;"></span>
                                                </p>
                                            </div>
                                        </div>
                                        <%--<div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Bank Details - Father/Mother/Childs Name :  
                                                    <span id="lbl_trame" style="right: 0px; width: 60%;"></span>
                                                </p>
                                            </div>
                                        </div>--%>
                                        <div class="form-wprs-contnt-row">
                                            <%--<div class="wdth50" style="width: 33%">
                                                <p class="form-wprs-contnt-p">
                                                    A/c No.  :
                                                <asp:Label ID="Label13" runat="server" Style="width: 74%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50" style="width: 30%">
                                                <p class="form-wprs-contnt-p">
                                                    IFS Code  :
                                                <asp:Label ID="Label14" runat="server" Style="width: 72%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50" style="width: 37%">
                                                <p class="form-wprs-contnt-p">
                                                    Branch Name  :
                                                <asp:Label ID="Label15" runat="server" Style="width: 65%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>--%>



                                            <div class="form-wprs-contnt-row">
                                                <div class="wdth100">
                                                    <p class="form-wprs-contnt-p">
                                                        Document Attached :   
                                                    </p>
                                                    <p class="form-wprs-contnt-p" style="width: 30%;">
                                                        1. Student’s Photo – 2 pcs 
                                                    </p>
                                                    <p class="form-wprs-contnt-p" style="width: 60%;">
                                                        2. Student’s Aadhaar 
                                                    </p>
                                                    <%--<p class="form-wprs-contnt-p" style="width: 30%;">
                                                        3.  Date of Birth Certificate
                                                    </p>--%>
                                                    <p class="form-wprs-contnt-p" style="width: 30%;">
                                                        3. Father’s Aadhaar 
                                                    </p>
                                                    <p class="form-wprs-contnt-p" style="width: 60%;">
                                                        4.  Mother’s Aadhaar
                                                    </p>
                                                    <%--<p class="form-wprs-contnt-p" style="width: 60%;">
                                                        6. TC is required from Std. I to Std. VIII. 
                                                    </p>--%>
                                                </div>
                                            </div>
                                            <div class="form-wprs-contnt-row">
                                                <div class="wdth100" style="margin: 10px 0px 0px 0px;">
                                                    <p class="form-wprs-contnt-p" style="font-weight: bold">
                                                        Declaration :-  
                                                    </p>
                                                    <p class="form-wprs-contnt-p" style="text-transform: none; font-weight: 500; font-size: 15px; line-height: 25px;">
                                                        I hereby declare that the information given in this admission form is true and correct to the best of my knowledge that if any offence of my ward as disobedience, undiscipline, misbehave with the teacher or friends we will be punished for that. I will accept the punishment to my ward and even to be restricted without any complain.
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-wprs-contnt-row">
                                                <div class="wdth100" style="margin: 40px 0px 0px 0px;">
                                                    <p class="form-wprs-contnt-p" style="width: 50%; text-transform: none; font-size: 15px;">
                                                        Student's Signature   
                                                    </p>
                                                    <p class="form-wprs-contnt-p" style="width: 50%; float: right; text-align: right; text-transform: none; font-size: 15px;">
                                                        Parents or Guardian Signature
                                                    </p>
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
    </form>
</body>
</html>
