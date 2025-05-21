
namespace Simeserva.Presentation;

public partial record SenservaCommandModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;
	private readonly ICookbookService _cookbookService;
	private readonly IMessenger _messenger;

	public SenservaCommandModel(
		INavigator navigator,
		ITechniqueService recipeService,
		ICookbookService cookbookService,
		IMessenger messenger)
	{
		_navigator = navigator;
		_techniqueService = recipeService;
		_cookbookService = cookbookService;
		_messenger = messenger;
	}
}
