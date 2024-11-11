using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Toolbox.Testing
{
	public class EventHookBase
	{
		private EventMonitorBase Monitor { get; }
		protected internal EventInfo EventInfo;
		public int Count { get; private set; }

		protected EventHookBase(EventMonitorBase monitor, EventInfo eventInfo)
		{
			Monitor = monitor;
			EventInfo = eventInfo;

			if (EventInfo.EventHandlerType == null)
				throw new ArgumentException($"Event {eventInfo.DeclaringType?.FullName}.{eventInfo.Name} does not have EventHandlerType.");
			var invoke = EventInfo.EventHandlerType.GetMethod("Invoke")
				?? throw new ArgumentException($"Event {eventInfo.DeclaringType?.FullName}.{eventInfo.Name} does not have EventHandlerType.Invoke().");

			var eventParameters = EventInfo.EventHandlerType?.GetMethod("Invoke")?.GetParameters()
				?? throw new ArgumentException($"Event {EventInfo.DeclaringType?.FullName}.{EventInfo.Name} parameters of EventHandlerType.Invoke() mssing.");

			var handlerName = $"Handler{eventParameters.Length}";

			var genericHandler = typeof(EventHookBase).GetMethod(handlerName, BindingFlags.Instance | BindingFlags.NonPublic)
				?? throw new MissingMethodException($"No handler found for events with {eventParameters.Length} parameters.");

			var method = genericHandler.MakeGenericMethod(eventParameters.Select(p => p.ParameterType).ToArray());

			var d = Delegate.CreateDelegate(EventInfo.EventHandlerType, this, method);
			eventInfo.AddEventHandler(Monitor.Object, d);		
		}

		protected List<InvokeAction> Actions { get; } = [];
		protected List<InvokeAssertation> Assertations { get; } = [];

		protected delegate void InvokeAction(EventHookBase hook, EventCall call);
		protected delegate void InvokeAssertation(EventHookBase hook);

		[SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection")]
		private void Handler2<T1, T2>(T1 p1, T2 p2)
		{
			Handler(p1, p2);
		}
		private void Handler(params object?[] args)
		{
			Count++;

			var call = new EventCall(EventInfo, args);

			Monitor.Calls.Add(call);

			foreach (var action in Actions)
			{
				action(this, call);
			}
		}

		public void Assert()
		{
			Assertations.ForEach(a => a(this));
		}

	}
}
