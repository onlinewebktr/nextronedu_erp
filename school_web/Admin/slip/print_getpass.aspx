<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print_getpass.aspx.cs" Inherits="school_web.Admin.slip.print_getpass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Getpass</title>
      <script src="../../js/jquery-1.10.2.min.js"></script>
    <link href="css/get_outpass.css" rel="stylesheet" />
    
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/get_outpass.css" rel="stylesheet" type="text/css" />');
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
       <div class="main">
              <div class="prnt-btn-sec" runat="server" id="printBtns">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                              <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                           
                        </div> 
                        <div class="noPrint" style="float: right">
                           <asp:Button ID="Button1" CssClass="back-btn" runat="server" OnClick="Button1_Click" />
                        </div>


                           <div id="tblPrintIQ" runat="server">
                <div class="mainautot" id="officecopY">
                    <div class="certificate-wpr1">
                        <asp:Image ID="Image2" runat="server" class="watermarklogos" />
                        <div class="certificate-wpr2">
                            <div class="mainwith">
                                <div class="top" style="display: none;">
                                    <div class="topcell_left">
                                        Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="topcell_right">
                                        School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="heading" id="textheader" runat="server" style="position:relative">
                                    <div class="leftlogoheading">
                                        <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                                    </div>
                                    <div class="righttextheading">
                                        <h1 class="firm-name-h">
                                            <asp:Label ID="lbl_heading" runat="server" Text="School Name"></asp:Label>
                                        </h1>

                                        <div class="addres-firm">
                                            <asp:Label ID="lbl_address" runat="server" ></asp:Label>
                                        </div>
                                        <div class="email-firm">
                                            Email Id. :<asp:Label ID="lbl_emaiid" runat="server" ></asp:Label>

                                          

                         
                            <asp:Label ID="lbl_website" runat="server" Text="www.website.com" style="display:none"></asp:Label>
                                        </div>
                                        <div runat="server" id="contact_no" visible="false" class="contact-frim">
                                            Contact No. :<asp:Label ID="lbl_contact_details" runat="server" ></asp:Label>
                                        </div>
                                        <h1  class="firm-name-h">Hostel Get Pass Application Form</h1>
                                    </div>


                                  
                                     
                                          <img  id="studentphoto" runat="server" class="report-card-rght-dv" style="    margin: 1px 0px 0px 23px;
    position: absolute;
    left: 705px;
    height: 108px;" />
                                             <%--   <p>STUDENT PHOTO</p>--%>
                                            </div>
                                      <div class="wdth33" style="     width: 22%;
    position: absolute;
    left: 690px;
    border: 2px dashed #000;
    top: 117px;
    height: 32px;">
                                              
                                                    
                                                
                                            </div>




                              
                                <div class="heading" id="printheader" runat="server" style="text-align: center">
                                    <asp:Image ID="img_header" runat="server" />
                                </div>



                                <div class="slipno" style="    padding: 43px 0px 4px 0px;">
                                   
                                </div>

                                  <div class="studentdetails"  >
                                       Sir/Madam,
                                       
                                      <div class="student_left-p-info" style="width: 85%;
    margin: -12px 0px 3px 82px;
    padding: 0px;font-size: 16px;">
                                           <br />
                                          
                                        The student details are.
                                   
                                          </div>
                                      </div>

                                <div class="studentdetails"  >

                                   
                                   
                                   

                                    <div class="student_left-p-info">
                                        <p>Student Name</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_studentname" runat="server" ></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Father Name.</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_fathername" runat="server" ></asp:Label>
                                      
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Room No.</p>
                                        <i>:</i>
                                         <asp:Label ID="lbl_rom_no" runat="server" ></asp:Label>
                                       
                                    </div>
                                </div>
                                <div class="studentdetails">
                                    <div class="student_left-p-info"  runat="server" id="fNamDV">
                                        <p>Bed No</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_bead_no" runat="server" ></asp:Label>
                                         
                                        <asp:Label ID="lbl_aadmissionno" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Hostel Name</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_hostel_name" runat="server" ></asp:Label>
                                    </div>
                                    <div class="student_left-p-info"  >
                                        <p>Class</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                       
                                    </div>
                                </div>
                                  <div class="studentdetails">
                                       <div class="student_left-p-info"   >
                                        <p>Section</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_section" runat="server" ></asp:Label>
                                         
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Roll No.</p>
                                        <i>:</i>
                                      <asp:Label ID="lbl_rollno" runat="server" ></asp:Label>
                                    </div>
                                    <div class="student_left-p-info" runat="server" id="Div2">
                                       
                                    </div>
                                </div>
                  
                                 <div class="studentdetails">
                                       <div class="student_left-p-info"     style="width: 100%;">
                                        <p style="width: 21%;">Reason for Leave & days</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_leave_remarks" runat="server"></asp:Label>
                                         
                                    </div>
                                   
                                </div>

                                  <div class="studentdetails">
                                    <div class="student_left-p-info"   style="width: 100%;">
                                        <p style="width: 21%;">Date & Time For Departure</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_departure" runat="server"></asp:Label>
                                         
                                    </div>
                                    <div class="student_left-p-info" style="width: 100%;">
                                        <p style="width: 21%;">Date & Time For Arrival</p>
                                        <i>:</i>
                                      <asp:Label ID="lbl_Arrival" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info" runat="server" id="Div5">
                                       
                                    </div>
                                </div>
                             
                                  <div class="studentdetails">

                                      <div class="form-wprs-contnt-row">
                                            <div class="report-card-rght-dv" style="margin: 1px 0px 0px 23px;">
                                                <%--<p>FATHER PHOTO</p>--%>
                                                <img id="fatherphot" runat="server" style="height:100%; width:100%"  />


                                            </div>
                                            <div class="report-card-rght-dv" style="margin: 0px 0px 18px 232px;">
                                                <%--<p>MOTHER PHOTO</p>--%>
                                                 <img id="motherphot" runat="server" style="height:100%; width:100%"  />
                                            </div>
                                            <div class="report-card-rght-dv" style="margin: 0px 0px 18px 215px;">
                                                <p>
                                               <%--    GURDUEN PHOTO--%>
                                                     <img id="gradianphoto" runat="server" style="height:100%; width:100%"  />
                                                </p>
                                            </div>
                                        </div>
                                   </div>
                          

                      <div class="studentdetails">
                          <div class="form-wprs-contnt-row">


                                            <div class="wdth33" style="width: 25%;">
                                                <p class="form-wprs-contnt-p" style="margin: -11px 0px 0px 10px;">
                                                    ..................................................
                                                <br>
                                                    <b>Signature of Father</b>
                                                </p>
                                            </div>
                                            <div class="wdth33" style="width: 25%;">
                                                <p class="form-wprs-contnt-p" style="margin: -11px 0px 0px 141px;">
                                                    ..................................................
                                                <br>
                                                    <b>Signature of Mother</b>
                                                </p>
                                            </div>
                                            <div class="wdth33" style="width: 25%;">
                                                <p class="form-wprs-contnt-p" style="    margin: -11px 0px 0px 264px;
    width: 84%;">
                                                    ......................................................
                                                <br>
                                                    <b>Signature of Guardian</b>
                                                </p>
                                            </div>
                                            
                                        </div>


                          </div>

                                   <div class="studentdetails">
                                       <p style="margin: 0px;
    padding: 38px 34px 21px 1px;
    float: right;
    font-size: 18px;
    font-weight: bold">   Hostel Manager Signature</p>

                                     

                                       </div>

                                <div  class="studentdetails" style="    text-align: center;
    font-size: 18px;
    font-weight: 800;    margin: 4px 0px 23px 0px;">
                                    ❖ After leaving the hostel, parents/guardian will be responsible for the student.
                                </div>

                                   <div class="studentdetails" style="margin: 4px 0px 20px 0px;">
                                        Signature of Parents/Guardian Signature of Student



                                       </div>


                                <div class="studentdetails">
                                    
                                        <div class="student_left-p-info" style="    width: 45%;">
                                        <p style="width: 15%;">Name</p>
                                        <i>:</i>
                                        <span style="border-bottom:1px solid #000;  width: 81%;    margin: 17px 0px 0px 0px;"></span>
                                    </div>
                                    <div class="student_left-p-info" style="    width: 24%;">
                                        <p style=" width:28%;"> Relation</p>
                                        <i>:</i>
                                    
                                        <span style="border-bottom:1px solid #000;  width: 63%;    margin: 17px 0px 0px 0px;"></span>
                                    </div>
                                    <div class="student_left-p-info" style="width: 31%;">
                                        <p style=" width: 42%;">Contact No +91</p>
                                        <i>:</i>
                                      
                                        <span style="border-bottom:1px solid #000;  width: 45%;    margin: 17px 0px 0px 0px;"></span>
                                       
                                    </div>
                                </div>

                                  <div class="studentdetails">
                                           <div class="student_left-p-info" style="    width: 100%;">
                                        <p style="width: 7%;">Address</p>
                                        <i>:</i>
                                      <span style="border-bottom:1px solid #000;  width: 89%;    margin: 17px 0px 0px 0px;"></span>
                                    </div>
                                      </div>


                                 <div class="studentdetails" style="margin: 20px 0px 20px 0px;">
                                       Warden Details 



                                       </div>

                                <div class="studentdetails">
                                    
                                        <div class="student_left-p-info" style="    width: 45%;">
                                        <p style="width: 15%;">Name</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_wardenn_name" runat="server"  style="    width: 81%;
    "></asp:Label>
                                    </div>
                                    <div class="student_left-p-info" style=" width: 39.3%;">
                                        <p style=" width: 21%;"> EMP Code</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_warden_emp_code" runat="server"   ></asp:Label>
                                      
                                    </div>
                                   
                                </div>

                                 <div class="studentdetails" style="margin: 20px 0px 20px 0px;">
                                    
                                        <div class="student_left-p-info" style="    width: 100%;">
                                        <p style="width:21%;">Date & Time For Departure</p>
                                        <i>:</i>
                                       <span style="border-bottom: 1px solid #000;
    width: 75%;
    margin: 16px 0px 0px 0px;"></span>
                                    </div>
                                   
                                   
                                </div>
                                     <div class="studentdetails">
                                       <p style="margin: 0px;
    padding: 0px 34px 21px 1px;
    float: right;
    font-size: 18px;
    font-weight: bold"> Warden Signature</p>

                                     

                                       </div>

                                   <div class="studentdetails" style="margin: 4px 0px 0px 0px;">
                                      Security Officer Details



                                       </div>


                                  <div class="studentdetails">
                                    
                                        <div class="student_left-p-info" style="    width: 45%;">
                                        <p style="width: 15%;">Name</p>
                                        <i>:</i>
                                       

                                             <span style="border-bottom:1px solid #000;  width: 81%;    margin: 17px 0px 0px 0px;"></span>
                                    </div>
                                    <div class="student_left-p-info" style="    width: 32.3%;">
                                        <p style="width: 16%;">ID. No</p>
                                        <i>:</i>
                               <span style="border-bottom:1px solid #000;    margin: 17px 0px 0px 0px;"></span>
                                      
                                    </div>
                                   
                                </div>

                                 <div class="studentdetails" style="margin: 20px 0px 20px 0px;">
                                    
                                          <div class="student_left-p-info" style="    width: 100%;">
                                        <p style="width:21%;">Date & Time For Departure</p>
                                        <i>:</i>
                                       <span style="border-bottom: 1px solid #000;
    width: 75%;
    margin: 16px 0px 0px 0px;"></span>
                                    </div>
                                   
                                   
                                </div>


                                 <div class="studentdetails" style="margin: 0px 0px 0px 0px;">

                                        <div class="studentdetails" style="width:55%; float:left">
                                       <p style="margin: 0px;
    padding: 38px 34px 21px 1px;
    float: right;
    font-size: 18px;
    font-weight: bold"> Signature of Parents/Guardian Signature of Student</p>

                                     

                                       </div>



                                          <div class="studentdetails"  style="width:45%; float:left">
                                       <p style="margin: 0px;
    padding: 38px 34px 21px 1px;
    float: right;
    font-size: 18px;
    font-weight: bold"> Security signature</p>

                                     

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
    </form>
</body>
</html>
