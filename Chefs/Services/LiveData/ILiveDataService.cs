using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simeserva.Services.LiveData;

public interface ILiveDataService
{
	ValueTask<IImmutableList<LiveDataModel>> GetAll(CancellationToken ct);
}
