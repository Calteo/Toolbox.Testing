using System.Diagnostics;
using System.Reflection;

namespace Toolbox.Testing
{
	[DebuggerDisplay("{Event.Name,nq}")]
	public class EventCall
    {
		public EventInfo Event { get; }
		public object?[] Arguments { get; }

		public EventCall(EventInfo @event, object?[] arguments)
		{
			Event = @event;
			Arguments = arguments;	
		}
	}
}
