using System;

namespace school_web.MyCode 
{
    public class MachineInfo
    {
        public int MachineNumber { get; set; }
        public String Staff_ID { get; set; }

        public DateTime Attendance_time { get; set; }
        public string DateTimeRecord { get; set; } 
    }

    public class AttendanceLog
    {
        public String Id { get; set; }
        public DateTime Date { get; set; } 
    }
}
