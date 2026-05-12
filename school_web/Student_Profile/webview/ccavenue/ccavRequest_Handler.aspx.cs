using CCA.Util;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.ccavenue
{
    public partial class ccavRequest_Handler : System.Web.UI.Page
    {
        CCACrypto ccaCrypto = new CCACrypto();
        //string workingKey = "E83ACB90DA0B89B8A16C74271DD48474";//put in the 32bit alpha numeric key in the quotes provided here 	
        //string merchant_id = "2348104"; 
        //public string strAccessCode = "AVFQ54KD46BY77QFYB";// put the access key in the quotes provided here.

        string workingKey = My.Working_KEY_CCAV(); //"E83ACB90DA0B89B8A16C74271DD48474";//put in the 32bit alpha numeric key in the quotes provided here 	
        string merchant_id = My.MERCHANT_KEY_CCAV();
        public string strAccessCode = My.strAccessCode();  //"AVFQ54KD46BY77QFYB";// put the access key in the quotes provided here.

        string ccaRequest = "";
        public string strEncRequest = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cc = Session["test"].ToString(); //get parameters values 
                strEncRequest = ccaCrypto.Encrypt(cc, workingKey); // encript and send data  
            }
        }
    }
}