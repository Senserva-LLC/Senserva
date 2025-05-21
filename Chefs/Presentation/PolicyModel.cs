
namespace Simeserva.Presentation;

public partial record PolicyModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;
	private readonly ICookbookService _cookbookService;
	private readonly IMessenger _messenger;
	public Policy Policy { get; }

	public PolicyModel(
		Policy policy,
		INavigator navigator,
		ITechniqueService recipeService,
		ICookbookService cookbookService,
		IMessenger messenger)
	{
		 Policy = policy;
		_navigator = navigator;
		_techniqueService = recipeService;
		_cookbookService = cookbookService;
		_messenger = messenger;
	}
}
