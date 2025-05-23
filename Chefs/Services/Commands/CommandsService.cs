﻿
namespace Simeserva.Services.Commands;

public class CommandsService(
	IUserService userService,
	IWritableOptions<SearchHistory> searchOptions,
	IMessenger messenger)
	: ICommandsService
{

	public async Task<IImmutableList<SenservaCommand>> GetAll(CancellationToken ct)
	{
		return new List<SenservaCommand>() { new SenservaCommand(), new SenservaCommand() }.ToImmutableList();
	}
}
