using AttendanceDAL;
using Framework.Data;
using LinqKit;

 namespace AttendanceBusiness.BackgroundProcess
{
    public class AttendanceBusinessBackgroundProcess
    {
      private int _interval = 60000; // 1min
      public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
            }
          }
        public void Start()
        {
            timer = new System.Timers.Timer(_interval); // 1min
            timer.Start();
        }

        private void Timer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
        }
    }
}

       

