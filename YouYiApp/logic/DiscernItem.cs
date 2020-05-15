using System.Threading.Tasks;
using YouYiApp.common;
using YouYiApp.model;

namespace YouYiApp.logic
{
    public class DiscernItem
    {

        public static bool ValiColor(DiscernPoint point)
        {
            System.Drawing.Point p = new System.Drawing.Point(point.x, point.y);
            int color = SelectColor.GetColorInt(p);
            return color == point.color;
        }

        /** 
         *  三坐标颜色验证
         * */
        public static bool ValiThreePoint(DiscernPoint point0, DiscernPoint point1, DiscernPoint point2)
        {
            bool res = true;
            var t1 = Task.Factory.StartNew<bool>(() =>
            {
                return ValiColor(point0);
            });
            var t2 = Task.Factory.StartNew<bool>(() =>
            {
                return ValiColor(point1);
            });
            var t3 = Task.Factory.StartNew<bool>(() =>
            {
                return ValiColor(point2);
            });
            Task.WaitAll(t1, t2, t3);
            if (!t1.Result || !t2.Result || !t3.Result)
            {
                res = false;
            }
            //Console.WriteLine("Task Finished! The result is {0}", res);
            return res;
        }

    }
}
