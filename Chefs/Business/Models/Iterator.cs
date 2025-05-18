namespace Chefs.Business.Models;

// WORKAROUND
// This is a workaround until the following issue is resolved: https://github.com/unoplatform/uno.extensions/issues/2383
// Once the issue is resolved, we should be able to use a single Iterator<T> class for both RemediationStep and int types.

public record StepIterator(IImmutableList<RemediationStep> Items)
{
	public int CurrentIndex { get; init; } = 0;
	public RemediationStep? CurrentItem => CurrentIndex >= 0 && CurrentIndex < Items.Count
		? Items[CurrentIndex]
		: null;

	public int Count => Items.Count;
	public bool CanMoveNext => CurrentIndex < Items.Count - 1;
	public bool CanMovePrevious => CurrentIndex > 0;

	public StepIterator MoveNext()
		=> CanMoveNext
			? this with { CurrentIndex = CurrentIndex + 1 }
			: this;

	public StepIterator MovePrevious()
		=> CanMovePrevious
			? this with { CurrentIndex = CurrentIndex - 1 }
			: this;
}

public record IntIterator(IImmutableList<int> Items)
{
	public int CurrentIndex { get; init; } = 0;
	public int CurrentItem => Items[CurrentIndex];
	public int Count => Items.Count;
	public bool CanMoveNext => CurrentIndex < Items.Count - 1;
	public bool CanMovePrevious => CurrentIndex > 0;
}
