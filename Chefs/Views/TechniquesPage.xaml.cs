using LiveChartsCore;

namespace Simeserva.Views;

public sealed partial class TechniquesPage : Page
{
	public TechniquesPage()
	{
		this.InitializeComponent();

		LiveCharts.Configure(config =>
			config
				.HasMap<RiskChartItem>((nutritionChartItem, point) =>
				{
					// here we use the index as X, and the nutrition value as Y
					return new(point, nutritionChartItem.Value);
				})
		);
	}
}
