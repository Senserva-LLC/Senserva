using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simeserva.Views.Flyouts;
using Uno.Extensions.Navigation;

namespace Simeserva.Presentation.Extensions;

public static class INavigatorExtensions
{
	public static Task<NavigationResponse?> ShowDialog(this INavigator navigator, object sender, DialogInfo dialogInfo, CancellationToken ct)
	{
		return navigator.NavigateDataAsync(sender, new DialogInfo(dialogInfo.Title, dialogInfo.Content), cancellation: ct);
	}
}
