
namespace Simeserva.Presentation;

/// <summary>
/// TODO define this more
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="Data"></param>
public partial record LiveDataModel(Guid Id, string Name, string Description, string Data)
	: ISenservaEntity
{
	public LiveDataModel() : this(Guid.NewGuid(), "Hello!", string.Empty, string.Empty)
	{
	}
}
