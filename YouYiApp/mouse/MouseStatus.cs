using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouYiApp.mouse
{

    public class MouseStatus
    {

        public static MouseStatus mouseStatus = null;

        private static object singleton_Lock = new object(); //锁同步

        public MouseStatus() {
            leftDown = false;
            rightDown = false;
        }

        public bool leftDown
        {
            get;
            set;
        }

        public bool rightDown
        {
            get;
            set;
        }

        public bool openScope
        {
            get;
            set;
        }

        public static MouseStatus GetMouseStatus()
        {
            lock (singleton_Lock)
            {
                if (null == mouseStatus)
                {
                    mouseStatus = new MouseStatus();
                }
            }
            return mouseStatus;
        }

    }
}
