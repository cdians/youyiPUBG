using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using YouYiApp.model;

namespace YouYiApp.common
{
    public class TextBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LogHelper.ShowLog("value: {0}", value);
            GlobalParam globalParam = GlobalParam.GetGlobalParam();
            if (System.Convert.ToInt32(value) == globalParam.DataIndex)
            {
                Color clr = Color.FromRgb(0, 246, 134);
                value =  new SolidColorBrush(clr);
            } else
            {
                Color clr = Color.FromRgb(254, 69, 67);
                value = new SolidColorBrush(clr);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LogHelper.ShowLog("value: {0}", value);
            GlobalParam globalParam = GlobalParam.GetGlobalParam();
            if (System.Convert.ToInt32(value) == globalParam.DataIndex)
            {
                Color clr = Color.FromRgb(0, 246, 134);
                value = new SolidColorBrush(clr);
            }
            else
            {
                Color clr = Color.FromRgb(254, 69, 67);
                value = new SolidColorBrush(clr);
            }
            return value;
        }
    }
}
