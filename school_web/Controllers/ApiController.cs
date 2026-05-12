using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web; 
using System.Web.Mvc;

namespace school_web.Controllers
{
    [RoutePrefix("api")]
    public class ApiController : Controller
    { 
        [Route("get-roll-no-list")]
        public ActionResult get_district_list(Dictionary<string, object> data)
        {
            var mt = My.dataTable($@"DECLARE @MaxRollNumber INT;

SELECT
    @MaxRollNumber = MAX(rollnumber) + 1
FROM
    admission_registor
WHERE
    Session_id = '{data["session_id"]}'
    AND Class_id = '{data["class"]}'
    AND Section = '{data["section"]}'
    AND Status = 1;

            --Step 2: Use the calculated maximum rollnumber in the CTE
WITH Numbers AS(
    SELECT 1 AS Number
    UNION ALL
    SELECT Number + 1
    FROM Numbers
    WHERE Number + 1 <= @MaxRollNumber
)
SELECT
    Number AS MissingRollNumber
FROM
    Numbers
LEFT JOIN
    admission_registor AR
ON
    Numbers.Number = AR.rollnumber
    AND AR.Session_id = '{data["session_id"]}'
    AND AR.Class_id = '{data["class"]}'
    AND AR.Section = '{data["section"]}'
    AND AR.Status = 1
WHERE
    AR.rollnumber IS NULL
ORDER BY
    Number
OPTION(MAXRECURSION 0); ");





            return Response.Success(data: mt);
        }



        [Route("get-h-roll-no-list")]
        public ActionResult get_disting_h_roll_no_list(Dictionary<string, object> data)
        {
            var mt = My.dataTable($@"DECLARE @MaxRollNumber INT;

SELECT
    @MaxRollNumber = MAX(Hostel_roll_no) + 1
FROM
    admission_registor
WHERE
    Session_id = '{data["session_id"]}' 
    AND hosteltaken='Yes'
    AND Status = 1;

            --Step 2: Use the calculated maximum rollnumber in the CTE
WITH Numbers AS(
    SELECT 1 AS Number
    UNION ALL
    SELECT Number + 1
    FROM Numbers
    WHERE Number + 1 <= @MaxRollNumber
)
SELECT
    Number AS MissingRollNumber
FROM
    Numbers
LEFT JOIN
    admission_registor AR
ON
    Numbers.Number = AR.Hostel_roll_no
    AND AR.Session_id = '{data["session_id"]}'
    AND hosteltaken='Yes'
    AND AR.Status = 1
WHERE
    AR.Hostel_roll_no IS NULL
ORDER BY
    Number
OPTION(MAXRECURSION 0);");





            return Response.Success(data: mt);
        }
    }
}