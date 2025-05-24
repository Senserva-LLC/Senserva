namespace Simeserva.Presentation;

public partial record ReportModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _service;
	private readonly IMessenger _messenger;
	public Technique Technique { get; }

	public ReportModel(
		Technique technique,
		INavigator navigator,
		ITechniqueService recipeService,
		IMessenger messenger)
	{
		Technique = technique;
		_navigator = navigator;
		_service = recipeService;
		_messenger = messenger;
	}

}
