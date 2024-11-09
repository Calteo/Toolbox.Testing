using System.Reflection;

namespace Toolbox.Testing
{
	public class EventHook<T, TE> : EventHookBase where T : class where TE : Delegate
	{
		internal EventHook(EventMonitor<T> monitor, EventInfo eventInfo)
			: base(monitor, eventInfo)
		{
			Monitor = monitor;			
		}

		private EventMonitor<T> Monitor { get; }
		
		public EventHook<T, TOther> Subscribe<TOther>(string name) where TOther : Delegate
		{
			return Monitor.Subscribe<TOther>(name);
		}

		public EventMonitor<T> Build() => Monitor.Build();

		public EventHook<T, TE> Raised(TE action)
		{
			Actions.Add((EventHookBase h, EventCall c) => action.DynamicInvoke(c.Arguments));
			return this;
		}

		public EventHook<T, TE> Exactly(int count)
		{
			Assertations.Add((EventHookBase h) => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, h.Count, $"{h.EventInfo.Name} exact call count."));
			return this;
		}
		public EventHook<T, TE> Minimum(int count)
		{
			Assertations.Add((EventHookBase h) =>
			{
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(h.Count >= count, $"Expected:<{count}>. Actual:<{h.Count}>. {h.EventInfo.Name} minimum call count not reached.");
			});
			return this;
		}

		public EventHook<T, TE> Maximum(int count)
		{
			Assertations.Add((EventHookBase h) =>
			{
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(h.Count <= count, $"Expected:<{count}>. Actual:<{h.Count}>. {h.EventInfo.Name} maximum call count excceed.");
			});
			return this;
		}

	}
}
