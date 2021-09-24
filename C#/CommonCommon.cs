public class CommonCommon
{
    public class OT_WORK_STATUS
    {
        public static string Name = "WORK_STATUS";
        public static string TERMINATE = "TERMINATE";
        public static decimal TERMINATE_ID = 257;
    }

    public enum TABLE_NAME
    {
        OT_OTHER_LIST = 1,
        OT_OTHER_LIST_GROUP = 2,
        OT_OTHER_LIST_TYPE = 3
    }
}

public enum SystemCodes : int
{
    Profile = 1,
    Attendance = 2,
    Payroll = 3,
    Recruitment = 16,
    Training = 17,
    Meal = 20
}
