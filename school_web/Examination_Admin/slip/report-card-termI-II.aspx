<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report-card-termI-II.aspx.cs" Inherits="school_web.Examination_Admin.slip.report_card_termI_II" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /> 
    <link href="assets/css/report-card.css" rel="stylesheet" />


    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="assets/css/report-card.css" rel="stylesheet" type="text/css" />');
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
            <div class="prnt-btn-sec">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="btn_back" CssClass="back-btn" runat="server" />
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
                        <div class="report-card-wpr">
                            <div class="report-card-head">
                                <div class="report-card-left-dv">
                                    <asp:Image ID="Image1" runat="server" />
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" Text="" class="report-card-schl-cont"></asp:Label>
                                    <p class="report-card-schl-emil">
                                        Email : 
                                        <asp:Label ID="lbl_email" runat="server"></asp:Label>
                                    </p>
                                    <h2 class="report-card-ac-sson">ACADEMIC SESSION: 2022-2023</h2>
                                    <h2 class="report-card-rprt-crd">REPORT CARD FOR CLASS – X</h2>
                                </div>
                                <div class="report-card-rght-dv">
                                    <img src="http://dpsntpcfarakka.com/Master_Img/Student/StudentImg20220513103531.png" />
                                </div>
                            </div>

                            <div class="report-card-std-info-dv">
                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">Student's Name</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label1" runat="server" Text="LIZZA KHATUN"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">DATE OF BIRTH</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label2" runat="server" Text="26/04/2004"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">MOTHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label3" runat="server" Text="JELI BIBI"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">MOTHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label4" runat="server" Text="TOFAIL SK."></asp:Label>
                                    </p>
                                </div>

                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label5" runat="server" Text="5805"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label6" runat="server" Text="A"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label7" runat="server" Text="25"></asp:Label>
                                    </p>
                                </div>
                            </div>

                            <div class="subs-mrks-area-dv-trmII">
                                <table>
                                    <tr>
                                        <th>A</th>
                                        <th>SCHOLASTIC AREA</th>
                                        <th colspan="6" class="txt-center">TERM-I</th>
                                        <th colspan="6" class="txt-center">TERM-II</th>
                                        <th colspan="2" class="txt-center">OVERALL</th>
                                    </tr>
                                    <tr>
                                        <th>SN</th>
                                        <th>SUBJECTS</th>
                                        <th>PT (10)</th>
                                        <th>NB (5)</th>
                                        <th>SE (5)</th>
                                        <th>HY (80)</th>
                                        <th>TOTAL (A)(100)</th>
                                        <th>GRADE</th>
                                        <th>PT (10)</th>
                                        <th>NB (5)</th>
                                        <th>SE (5)</th>
                                        <th>AN (80)</th>
                                        <th>TOTAL (B)(100)</th>
                                        <th>GRADE</th>
                                        <th>AVERAGE (A+B/2) (100)</th>
                                        <th>GRADE</th>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>ENGLISH COMMUNICATIVE</td>
                                        <td>9.7</td>
                                        <td>5</td>
                                        <td>4.5</td>
                                        <td>76</td>
                                        <td>95.2</td>
                                        <td>A1</td>
                                        <td>8.8</td>
                                        <td>5</td>
                                        <td>5</td>
                                        <td>75</td>
                                        <td>93.8</td>
                                        <td>A1</td>
                                        <td>94.5</td>
                                        <td>A1</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txt-center">ATTENDANCE</td>
                                        <td colspan="6" class="txt-center">97 OUT OF 102 DAYS</td>
                                        <td colspan="6" class="txt-center">78 OUT OF 83 DAYS</td>
                                        <td class="txt-center">92.2 %</td>
                                        <td class="txt-center">A1</td>
                                    </tr>
                                </table>
                            </div>

                            <div class="subs-mrks-area-lft-dv-trmII">
                                <div class="subs-mrks-area-b-dv-trmII">
                                    <table>
                                        <tr>
                                            <th>B</th>
                                            <th colspan="3">CO-SCHOLASTIC AREAS [3 - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th class="th-bg-rmov">SN</th>
                                            <th class="th-bg-rmov">ACTIVITIES</th>
                                            <th class="th-bg-rmov">TERM-I</th>
                                            <th class="th-bg-rmov">TERM-II</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>ACTIVITY  _____________________</td>
                                            <td>A</td>
                                            <td>A</td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>ART EDUCATION</td>
                                            <td>C</td>
                                            <td>A</td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>HEALTH & PHYSICAL EDUCATION</td>
                                            <td>A</td>
                                            <td>A</td>
                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td>MORAL SCIENCE</td>
                                            <td>A</td>
                                            <td>A</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="subs-mrks-area-lft-dv-trmII">
                                <div class="subs-mrks-area-b-dv-trmII">
                                    <table>
                                        <tr>
                                            <th>C</th>
                                            <th colspan="3">DISCIPLINE [3-POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th class="th-bg-rmov">SN</th>
                                            <th class="th-bg-rmov">ACTIVITIES</th>
                                            <th class="th-bg-rmov">TERM-I</th>
                                            <th class="th-bg-rmov">TERM-II</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>DISCIPLINE</td>
                                            <td>A</td>
                                            <td>A</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="rmrksppp">REMARKS :</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">NEXT SESSION BEGINS ON :	3rd April, 2023</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="sig-dv">
                                <div class="sig-left">
                                    <p class="sig-ps">CLASS TEACHER</p>
                                </div>
                                <div class="sig-left">
                                    <p class="sig-ps">EXAMINATION INCHARGE</p>
                                </div>
                                <div class="sig-left">
                                    <p class="sig-ps">PRINCIPAL</p>
                                </div>
                            </div>

                            <div class="instruction-dv">
                                <div class="instruction-50">
                                    <div class="instruction-tbls">
                                        <table>
                                            <tr>
                                                <th colspan="2" class="txt-center">NSTRUCTIONS</th>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="txt-center">(Scholastic Areas 8-point grading scale)</td>
                                            </tr>
                                            <tr>
                                                <td>MARKS RANGE</td>
                                                <td>GRADE</td>
                                            </tr>
                                            <tr>
                                                <td>91 - 100</td>
                                                <td>A1</td>
                                            </tr>
                                            <tr>
                                                <td>81 - 90</td>
                                                <td>A2</td>
                                            </tr>
                                            <tr>
                                                <td>71 - 80</td>
                                                <td>B1</td>
                                            </tr>
                                            <tr>
                                                <td>61 - 70</td>
                                                <td>B2</td>
                                            </tr>
                                            <tr>
                                                <td>51 - 60</td>
                                                <td>C1</td>
                                            </tr>
                                            <tr>
                                                <td>41 - 50</td>
                                                <td>C2</td>
                                            </tr>
                                            <tr>
                                                <td>33 - 40</td>
                                                <td>D</td>
                                            </tr>
                                            <tr>
                                                <td>32 & Below</td>
                                                <td>E (Failed)</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="instruction-50">
                                    <div class="instruction-tbls floatrght">
                                        <table>
                                            <tr>
                                                <th colspan="2" class="txt-center">NSTRUCTIONS</th>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="txt-center">(Co-Scholastic Areas 3-point grading scale)</td>
                                            </tr>
                                            <tr>
                                                <td>GRADE</td>
                                                <td>REMARKS</td>
                                            </tr>
                                            <tr>
                                                <td>A</td>
                                                <td>Outstanding</td>
                                            </tr>
                                            <tr>
                                                <td>B</td>
                                                <td>Very Good</td>
                                            </tr>

                                            <tr>
                                                <td>C</td>
                                                <td>Fair</td>
                                            </tr>

                                            <tr>
                                                <td class="na-td-inst-trmII" colspan="2">ABBREVIATION :-
                                                    <br />
                                                    <span>PT = PERIODIC TEST
                                                        <br />
                                                        NB = NOTE BOOK<br />
                                                        SE = SUBJECT ENRICHMENT
                                                        <br />
                                                        HY = HALF YEARLY
                                                        <br />
                                                        AN = ANNUAL</span>
                                                </td>
                                            </tr>
                                        </table>
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
