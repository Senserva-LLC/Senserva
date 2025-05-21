
namespace Simeserva.Services.Techniques;

/// <summary>
/// Implements recipe related methods
/// </summary>
public interface ICommandsService
{
	/// <summary>
	/// Techniques method
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// GetReportsAsync each recipe from api
	/// </returns>
	public Task<IImmutableList<SenservaCommand>> GetAll(CancellationToken ct);
}
