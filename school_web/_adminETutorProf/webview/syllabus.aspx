<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="syllabus.aspx.cs" Inherits="school_web._adminETutorProf.webview.syllabus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">syllabus
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
         <style>
    body {
      font-family: 'Segoe UI', sans-serif;
      background: #f4f6f8;
      margin: 0;
         padding: 0px 6px 0px 6px;
    }

    .container {
      max-width: 500px;
      margin: auto;
      padding-right: 10px !important;
    padding-left: 10px !important;
    }

    h2 {
      text-align: center;
      color: #333;
    }

    .form-group {
      margin-bottom: 15px;
    }

    label {
      font-weight: bold;
      display: block;
      margin-bottom: 5px;
      color: #555;
    }

    select, input[type="date"] {
      width: 100%;
      padding: 6px;
      border: 1px solid #ccc;
      border-radius: 8px;
    }

    .button-find {
      background-color: #d63384;
      color: white;
      border: none;
      padding: 10px 20px;
      width: 100%;
      border-radius: 8px;
      font-weight: bold;
      cursor: pointer;
      margin-top: 10px;
    }

    .activity-card {
      background: white;
      border-radius: 12px;
      padding: 15px 20px;
      margin-top: 20px;
      box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    }

    .activity-card p {
      margin: 8px 0;
      color: #444;
    }

    .actions {
      margin-top: 15px;
      display: flex;
      justify-content: flex-start;
      gap: 10px;
    }

    .btn1 {
      padding: 8px 14px;
      border: none;
     border-radius: .4285rem;
      cursor: pointer;
      font-weight: bold;
      font-size: 14px;
    }
    .btn-delete {
  background-color: #e74c3c;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
  text-decoration: none;
}

.btn-delete:hover {
  background-color: #c0392b; /* Darker red on hover */
  color: white;
  font-weight: bold;
  font-size: 14px;
  text-decoration: none;
}
    
   .btn-download {
  background-color: #3498db;
  color: white;
  text-decoration: none;
  display: inline-block;
  padding: 8px 16px;
  border-radius: 4px;
  transition: background-color 0.3s;
}

.btn-download:hover {
  background-color: #2980b9; /* Slightly darker blue */
  color: white;
  text-decoration: none;
}

 

    .icon {
      margin-right: 6px;
    }
     .my-btn {
            color: #fff;
            background-color: #fdb351 !important;
            border-color: #fdb351 !important;
            font-size: 15px !important;
            line-height: 23px !important;
            font-weight: 400 !important;
            /*-webkit-transition: all 0.9s;
            -o-transition: all 0.9s;
            -moz-transition: all 0.9s;
            transition: all 0.9s;*/
        }
     .my-btn:hover {
                color: #fff;
                background-color: #fdb351 !important;
            }
         .form-control {
    border-color: rgba(29, 37, 59, .5);
    border-radius: .4285rem;
    font-size: 12px;
    transition: color .3s ease-in-out, border-color .3s ease-in-out, background-color .3s ease-in-out;
     height: 36px!important;
         font-weight: normal!important;
}
            .clndr-icon {
            font-size: 11px !important;
    color: #ff2956;
    position: absolute;
    top: 33px;
    right: 30px;
    left: auto;
    
   
    
        }
        .btn-download:hover a {
    color: #fff !important;
    font-weight: 300;
    text-decoration: none!important;
}
        .Buttoncss {
    background-color: #2563eb !important;
    color: #fff;
    padding: 10px 16px;
    border: none;
    border-radius: 6px;
    cursor: pointer;
    font-size: 0.9rem;
    transition: background-color 0.3s ease, transform 0.2s ease;
    width: 91%;
    font-size:14px!important;
}

    .Buttoncss:hover {
        background-color: #1d4ed8;
    }

    .Buttoncss:active {
        transform: scale(0.98);
         color: #fff;
    }
  </style>
    <script src="../Content/js/my.js"></script>
     <script src="../Content/js/sweetalert2@11.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
    
    <div class="activity-card container">
  <div class="row g-3">
    <!-- Class -->
      <div style="width: 41%;
    float: left;
    margin-left: 18px;">
           <div class="col-6 form-group">
      <label>Class</label>
      <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true" />
    </div>
      </div>
       <div style="width: 47%;
    float: left;
    margin-left: 9px;">
            <div class="col-6 form-group">
      <label>Section</label>
      <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control"   />
    </div>
      </div>
       <div style="width:100%; float: left; text-align:center;margin-top: 10px;">
       <asp:Button ID="btn_submit" runat="server" Text="Find" class="Buttoncss" OnClick="btn_submit_Click" style="margin: 0px 0px 10px 0px;"/>
           </div>
  </div>

  <!-- Submit Button -->
  
</div>

          <div class="card-container" style="margin:0px;padding:0px;width:100%">
              <div id="msg12" runat="server" style="margin-top:20px; border: 1px solid #dee2e6; border-radius: 12px; padding: 20px; text-align: center; color: #6c757d; box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05); background: #fff;">
  <h5 style="margin-bottom: 10px; font-size: 15px; font-weight: 600;">
  The class syllabus could not be found.
  </h5>
  <p style="margin: 0; font-size: 14px;">
    Please try again with a different class.
  </p>
                  </div>
    <asp:Repeater ID="RPDetails" runat="server"  >
        <ItemTemplate>
             <div class="activity-card">
       <p><strong>Class:</strong> <%#Eval("Course_Name") %></p>
      <p><strong>Section:</strong> <%#Eval("Section") %></p>
      <p><strong>Syllabus info:</strong> <%#Eval("Syllabus_info") %></p>
      <p><strong>Date:</strong> <%#Eval("Created_date") %></p>
      <div class="actions" style="display: block;text-align: center;">
          <asp:Label ID="lbl_attachment" Visible="false" runat="server" Text='<%#Bind("Syllabus_filepath") %>'></asp:Label>
                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
           <a id="a1" runat="server" href='<%#Eval("Syllabus_filepath") %>' class="btn1 btn-download" download target="_blank">
                            <i class="fa fa-download" aria-hidden="true"></i> Download
                        </a>
           
        
      </div>
    </div>
            </ItemTemplate>
        </asp:Repeater>
              </div>

          </div>
</asp:Content>
