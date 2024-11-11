using System.Diagnostics;
using System.Reflection;

namespace Toolbox.Testing
{
	[DebuggerDisplay("{Event.Name,nq}")]
	public class EventCall(EventInfo @event, object?[] arguments)
	{
		public EventInfo Event { get; } = @event;
		public object?[] Arguments { get; } = arguments;
	}
}
