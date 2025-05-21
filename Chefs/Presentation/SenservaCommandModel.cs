
namespace Simeserva.Presentation;

public partial record SenservaCommandModel
{
	private readonly ICommandsService _service;
	public SenservaCommand Command { get; }

	public SenservaCommandModel(
		SenservaCommand command,
		ICommandsService service)
	{
		Command = command;
		_service = service;
	}


	public IListFeed<SenservaCommand> Commands => ListFeed.Async(async ct => await _service.GetAll(ct));
}
