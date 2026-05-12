using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace school_web.MyCode
{
    public class Device
    {
        public static bool isDeviceConnected = false;
        public static DataTable dv_dt;
        public static ZkemClient objZkeeper;
        public static bool Connect(string ip, string port = "4370")
        {
             
            try
            {
                string ipAddress = ip; 
                if (ipAddress == string.Empty || port == string.Empty)
                    throw new Exception("The Device IP Address and Port is mandatory !!");
                if (!UniversalStatic.PingTheDevice(ipAddress))
                {
                    //MessageBox.Show("Device ip not pinging");
                    return false;
                }
                int portNumber = port.ToInt();
                if (!int.TryParse(port, out portNumber))
                    throw new Exception("Not a valid port number");

                bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
                if (!isValidIpA)
                    throw new Exception("The Device IP is invalid !!");
                isValidIpA = UniversalStatic.PingTheDevice(ipAddress);

                if (!isValidIpA)
                    throw new Exception("The device at " + ipAddress + ":" + port + " did not respond!!"); 
                objZkeeper = new ZkemClient(RaiseDeviceEvent);
                IsDeviceConnected = objZkeeper.Connect_Net(ipAddress, portNumber);
                if (IsDeviceConnected)
                {
                    manipulator = new DeviceManipulator();
                }
                return isDeviceConnected; 
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            // win.Cursor = Cursors.Arrow;
            return false;
        }
        public static List<AttendanceLog> GetAttendanceLog(int machineNumber=1)
        {
            string enrollmentId = "";
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int minute = 0;
            int second = 0;
            int dwWorkCode = 0;

            var AtndsLogList = new List<AttendanceLog>();

            objZkeeper.ReadAllGLogData(machineNumber);

            while (objZkeeper.SSR_GetGeneralLogData(machineNumber, out enrollmentId, out _, out _, out year, out month, out day, out hour, out minute, out second, ref dwWorkCode))
            {
                AtndsLogList.Add(new AttendanceLog() { Date = new DateTime(year, month, day, hour, minute, second), Id = enrollmentId });
            }

            return AtndsLogList;
        }
        public static void SaveAttendanceLogToCsv(List<AttendanceLog> lst, string filePath)
        {
             filePath = Path.Combine(filePath, path2: $"{PayrollMy.Now.ToString("yyyy")}", path3: $"{PayrollMy.Now.ToString("MMMM")}");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(filePath, $"atn_log_{PayrollMy.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt")}.csv");
            using (var writer = new StreamWriter(filePath))
            {
                // Write header row
                writer.WriteLine("EMP_Id,Date");

                // Write data rows
                foreach (var a in lst)
                {
                    writer.WriteLine($"{a.Id},{a.Date}");
                }
            }
        }
        public static bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set
            {
                isDeviceConnected = value;
                if (isDeviceConnected)
                {
                    // ShowStatusBar("The device is connected !!", true);
                    // btnConnect.Content = "Disconnect";
                    // ToggleControls(true);
                }
                else
                {
                    //  ShowStatusBar("The device is diconnected !!", true);
                    objZkeeper.Disconnect();
                    //   btnConnect.Content = "Connect";
                    //ToggleControls(false);
                }
            }
        }
        public static DeviceManipulator manipulator;
        public static void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        //ShowStatusBar("The device is switched off", true);
                        //  DisplayEmpty();
                        //   btnConnect.Content = "Connect";
                        //   ToggleControls(false);
                        break;
                    }

                default:
                    break;
            }
        }

        internal static bool pushUser(string name, string id)
        {
          return   manipulator.PushUserDataToDevice(objZkeeper, 1, name, id);
        }
    }
}