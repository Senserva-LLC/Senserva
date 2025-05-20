
namespace Simeserva.Presentation;

public partial record PoliciesModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;
	private readonly ICookbookService _cookbookService;
	private readonly IMessenger _messenger;

	public PoliciesModel(
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
