using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siemserva.Business.Models;

namespace Siemserva.Services.Targets;

public interface ITargetService
{
	ValueTask<IImmutableList<Target>> GetAll(CancellationToken ct);
}

