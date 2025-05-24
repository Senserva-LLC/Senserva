using LiveChartsCore;
using LiveChartsCore.ConditionalDraw;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.UI;
using SkiaSharp;

namespace Simeserva.Views.Controls;
public sealed partial class ChartControl : UserControl
{
	private Technique? _technique;


	public SolidColorBrush UserRiskBrush
	{
		get { return (SolidColorBrush)GetValue(UserRiskBrushProperty); }
		set { SetValue(UserRiskBrushProperty, value); }
	}

	public static readonly DependencyProperty UserRiskBrushProperty =
		DependencyProperty.Register("UserRiskBrush", typeof(SolidColorBrush), typeof(ChartControl), new PropertyMetadata(new SolidColorBrush(Colors.Black), OnBrushChanged));

	public SolidColorBrush DataRiskBrush
	{
		get { return (SolidColorBrush)GetValue(DataRiskBrushProperty); }
		set { SetValue(DataRiskBrushProperty, value); }
	}

	public static readonly DependencyProperty DataRiskBrushProperty =
		DependencyProperty.Register("DataRiskBrush", typeof(SolidColorBrush), typeof(ChartControl), new PropertyMetadata(new SolidColorBrush(Colors.Black), OnBrushChanged));

	public SolidColorBrush DeviceRiskBrush
	{
		get { return (SolidColorBrush)GetValue(DeviceRiskBrushProperty); }
		set { SetValue(DeviceRiskBrushProperty, value); }
	}

	public static readonly DependencyProperty DeviceRiskBrushProperty =
		DependencyProperty.Register("DeviceRiskBrush", typeof(SolidColorBrush), typeof(ChartControl), new PropertyMetadata(new SolidColorBrush(Colors.Black), OnBrushChanged));



	public SolidColorBrush DataLabelBrush
	{
		get { return (SolidColorBrush)GetValue(DataLabelBrushProperty); }
		set { SetValue(DataLabelBrushProperty, value); }
	}

	public static readonly DependencyProperty DataLabelBrushProperty =
		DependencyProperty.Register("DataLabelBrush", typeof(SolidColorBrush), typeof(ChartControl), new PropertyMetadata(new SolidColorBrush(Colors.Black), OnBrushChanged));



	public SolidColorBrush TrackBackgroundBrush
	{
		get { return (SolidColorBrush)GetValue(TrackBackgroundBrushProperty); }
		set { SetValue(TrackBackgroundBrushProperty, value); }
	}

	public static readonly DependencyProperty TrackBackgroundBrushProperty =
		DependencyProperty.Register("TrackBackgroundBrush", typeof(SolidColorBrush), typeof(ChartControl), new PropertyMetadata(new SolidColorBrush(Colors.Black), OnBrushChanged));

	private static void OnBrushChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
	{
		if (dependencyObject is not ChartControl chartControl) return;

		if (chartControl._technique != null)
		{
			chartControl.BuildColumnChart();
			chartControl.BuildDoughnutChart();
		}
	}

	public ChartControl()
	{
		this.InitializeComponent();

		_technique = DataContext as Technique;
		if (_technique != null)
		{
			BuildColumnChart();
			BuildDoughnutChart();
		}

		DataContextChanged += OnDataContextChanged;
	}

	private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
	{
		_technique = args.NewValue as Technique;

		if (_technique != null)
		{
			BuildColumnChart();
			BuildDoughnutChart();
		}
	}

	private void BuildColumnChart()
	{
		//Build column chart
		var _chartdata = new RiskChartItem[]
		 {
			new(nameof(Risks.DataRisk),_technique?.Risks.DataRisk,_technique?.Risks.DataRiskBase,GetColorPaint(nameof(Risks.DataRisk))),
			new(nameof(Risks.UserRisk),_technique?.Risks.UserRisk,_technique?.Risks.UserRiskBase,GetColorPaint(nameof(Risks.UserRisk))),
			new(nameof(Risks.DeviceRisk),_technique?.Risks.DeviceRisk,_technique?.Risks.DeviceRiskBase, GetColorPaint(nameof(Risks.DeviceRisk)))
		 };

		var rowSeries = new RowSeries<RiskChartItem>
		{
			Values = _chartdata,
			DataLabelsPaint = new SolidColorPaint(GetSKColorFromResource(DataLabelBrush)),
			DataLabelsPosition = DataLabelsPosition.Right,
			DataLabelsFormatter = point => $"{point.Model!.Name} {point.Model!.ChartProgressVal}/{point.Model!.MaxValueRef}g",
			DataLabelsSize = 13,
			IgnoresBarPosition = true,
			MaxBarWidth = 22,
			Padding = 1,

			IsVisibleAtLegend = true
		}.OnPointMeasured(point =>
		{
			if (point.Visual is null) return;
			point.Visual.Fill = point.Model!.ColumnColor;
		});
		//End

		//Build column background
		var chartlimit = new RiskChartItem[]
		{
			new(),
			new(),
			new()
		};

		var rowSeriesLimit = new RowSeries<RiskChartItem>
		{
			Values = chartlimit,
			IgnoresBarPosition = true,
			MaxBarWidth = 22,
			Padding = 1,
			Fill = new SolidColorPaint(GetSKColorFromResource(TrackBackgroundBrush))
		};
		//End

		cartesianChart.Series = new[] { rowSeriesLimit, rowSeries };
		cartesianChart.XAxes = new[] { new Axis { IsVisible = false, MaxLimit = 1000 } };
		cartesianChart.YAxes = new[] { new Axis { IsVisible = false } };
	}

	private void BuildDoughnutChart()
	{
		var c = new ISeries[]
		{
			new PieSeries<int>
			{
				Values = new []{ 5 },
				Fill = GetColorPaint(nameof(Risks.DeviceRisk)),
				InnerRadius = 60,
			},
			new PieSeries<int>
			{
				Values = new []{ 5 },
				Fill = GetColorPaint(nameof(Risks.UserRisk)),
				InnerRadius = 60,
			},
			new PieSeries<int>
			{
				Values = new []{ 5 },
				Fill = GetColorPaint(nameof(Risks.DataRisk)),
				InnerRadius = 60,
			}
		};

		pieChart.Series = c;
	}

	private SolidColorPaint GetColorPaint(string name)
	{
		return name switch
		{
			nameof(Risks.UserRisk) => new SolidColorPaint(GetSKColorFromResource(UserRiskBrush)),
			nameof(Risks.DataRisk) => new SolidColorPaint(GetSKColorFromResource(DataRiskBrush)),
			nameof(Risks.DeviceRisk) => new SolidColorPaint(GetSKColorFromResource(DeviceRiskBrush)),
			_ => new SolidColorPaint(SKColors.Yellow),
		};
	}

	private SKColor GetSKColorFromResource(SolidColorBrush brush)
	{
		var color = brush.Color;
		return new SKColor(color.R, color.G, color.B, color.A);
	}
}
