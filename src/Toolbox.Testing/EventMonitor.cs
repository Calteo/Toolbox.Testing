namespace Toolbox.Testing
{
	public class EventMonitor<T> : EventMonitorBase where T : class
	{
		public EventMonitor(T obj) : base(obj)
		{ 
		}

		public EventMonitor<T> Build() => this;

		public void Assert()
		{
			foreach (var hook in Hooks)
			{
				hook.Assert();
			}
		}

		public EventHook<T, TE> Subscribe<TE>(string name) where TE : Delegate
		{
			var eventInfo = Object.GetType().GetEvent(name)
				?? throw new MissingFieldException(Object.GetType().FullName, name);

			var hook = new EventHook<T, TE>(this, eventInfo);
			Hooks.Add(hook);

			return hook;
		}
	}
}
