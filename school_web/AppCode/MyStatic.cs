using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace school_web.AppCode
{
    public static class MyStatic
    {
        internal static JsonResult Error(this HttpResponseBase r, string message = "Some thing goes wrong")
        {
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                error = true,
                message = message
            }); ;
            var json = new JsonResult() { Data = resp, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return json;
        }
        internal static JsonResult Success(this HttpResponseBase r, string message = "", object data = null)
        {
            var dic = new Dictionary<string, object>();
            dic["error"] = false;
            dic["message"] = message;
            if (data != null)
            {
                if (data.GetType() == typeof(DataTable))
                {
                    dic["data"] = (data as DataTable).toJsonObject();
                }
                else if (data.GetType() == typeof(DataSet))
                {
                    var ds = data as DataSet;
                    var i = 0;
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (i == 0)
                        {
                            dic["data"] = dt.toJsonObject();
                        }
                        else
                        {
                            dic["data_" + i] = dt.toJsonObject();
                        }
                        i++;
                    }
                }
                else if (data.GetType() == typeof(Dictionary<string, object>))
                {
                    var _d = data as Dictionary<string, object>;
                    foreach (var k in _d.Keys)
                    {
                        dic[k] = _d[k];
                    }
                }
                else
                {
                    dic["data"] = data;
                }
            }

            var resp = (new JavaScriptSerializer()).Serialize(dic);

            //return new CompressedJsonResult { Data = resp };
            var json = new JsonResult() { Data = resp, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return json;
        }





        public static string getUserData(this IIdentity idntity, string key)
        {
            if (idntity.IsAuthenticated)
            {
                FormsIdentity identity = (FormsIdentity)idntity;
                FormsAuthenticationTicket t = identity.Ticket;
                string data = t.UserData;
                var user_data = (new JavaScriptSerializer()).Deserialize<dynamic>(data);
                if (user_data == null || !user_data.ContainsKey(key))
                { 
                        return null; 
                }
                else if (user_data.ContainsKey(key))
                { 
                    return user_data[key];
                }
                else
                {
                    return null;
                }
                // Parse and use the userData as needed
            }
            return null;
        }
    }
}