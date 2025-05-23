using System.Globalization;
using Microsoft.UI.Xaml.Data;

namespace Simeserva.Converters;

public class CookingTimeFormatter : IValueConverter
{
	public object? Convert(object value, Type targetType, object parameter, string language)
	{
		if (value is FixTime time)
		{
			string timeString = (time.ToString() ?? FixTime.Under15min.ToString()).Replace("Under", "");

			return timeString.Insert(2, " ");
		}

		return null;
	}

	public object? ConvertBack(object value, Type targetType, object parameter, string language)
		=> throw new NotSupportedException("Only one-way conversion is supported.");
}
