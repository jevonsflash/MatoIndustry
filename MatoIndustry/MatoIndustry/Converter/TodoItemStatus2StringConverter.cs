using System;
using System.Globalization;
using MatoIndustry.Model;
using Xamarin.Forms;

namespace MatoIndustry.Converter
{
    class TodoItemStatus2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            var currentParam = parameter as string;
            if (string.IsNullOrEmpty(currentParam) || currentParam.Split('|').Length != 3)
            {
                parameter = "正在购买|已完成购买|已经移除";
            }

            var strList = (parameter as string).Split('|');

            var currentStatus = (TodoItemStatus)value;

            var result = string.Empty;
            switch (currentStatus)
            {
                case TodoItemStatus.Deleted:
                    result = strList[2];

                    break;
                case TodoItemStatus.Completed:
                    result = strList[1];

                    break;
                case TodoItemStatus.Pending:
                    result = strList[0];
                    break;

            }
            return result;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
