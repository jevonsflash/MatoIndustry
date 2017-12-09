﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace MatoIndustry.Converter
{
	public class True2FalseConverter:IValueConverter
	{
		public True2FalseConverter ()
		{
			
		}

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !((bool)value);
		}
		public object ConvertBack(object value, Type targetType, object parameter,CultureInfo culture)
		{
			return !((bool)value);
		}
	}
}

