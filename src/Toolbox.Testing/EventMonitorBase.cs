namespace Toolbox.Testing
{
	public class EventMonitorBase
	{
		protected internal object Object { get; }
		protected List<EventHookBase> Hooks { get; } = [];

		public List<EventCall> Calls { get; } = [];

		protected EventMonitorBase(object obj) 
		{ 
			Object = obj;
		}
	}
}
