namespace Simeserva.Presentation;

public partial record ReportModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;
	private readonly ICookbookService _cookbookService;
	private readonly IMessenger _messenger;
	public Report Report { get; }

	public ReportModel(
		Report report,
		INavigator navigator,
		ITechniqueService recipeService,
		ICookbookService cookbookService,
		IMessenger messenger)
	{
		Report = report;
		_navigator = navigator;
		_techniqueService = recipeService;
		_cookbookService = cookbookService;
		_messenger = messenger;
	}
}
