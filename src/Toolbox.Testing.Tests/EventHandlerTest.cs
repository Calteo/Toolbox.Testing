namespace Toolbox.Testing.Tests
{
	[TestClass]
	public class EventHandlerTest
	{
		[TestMethod]
		public void CheckEventHandlerCalled()
		{
			var cut = new TestObject();

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler>(nameof(cut.SimpleEvent))
				.Build();

			cut.RaiseSimpleEvent(EventArgs.Empty);

			monitor.Assert();

			Assert.AreEqual(1, monitor.Calls.Count);
		}

		[TestMethod]
		public void CheckEventHandlerExcactCount()
		{
			var cut = new TestObject();

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler>(nameof(cut.SimpleEvent))
				.Exactly(2)
				.Build();

			cut.RaiseSimpleEvent(EventArgs.Empty);
			cut.RaiseSimpleEvent(EventArgs.Empty);

			monitor.Assert();
		}

		[TestMethod]
		[ExpectedException(typeof(AssertFailedException))]
		public void AssertEventHandlerExcactCount()
		{
			var cut = new TestObject();

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler>(nameof(cut.SimpleEvent))
				.Exactly(2)
				.Build();

			cut.RaiseSimpleEvent(EventArgs.Empty);

			monitor.Assert();
		}

		[TestMethod]
		public void CheckEventHandlerMinimumCount()
		{
			var cut = new TestObject();

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler>(nameof(cut.SimpleEvent))
				.Minimum(2)
				.Build();

			cut.RaiseSimpleEvent(EventArgs.Empty);
			cut.RaiseSimpleEvent(EventArgs.Empty);

			monitor.Assert();
		}

		[TestMethod]
		[ExpectedException(typeof(AssertFailedException))]
		public void AssertEventHandlerMinimumCount()
		{
			var cut = new TestObject();

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler>(nameof(cut.SimpleEvent))
				.Minimum(2)
				.Build();

			cut.RaiseSimpleEvent(EventArgs.Empty);

			monitor.Assert();
		}


		[TestMethod]
		public void CheckEventHandlerMaximumCount()
		{
			var cut = new TestObject();

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler>(nameof(cut.SimpleEvent))
				.Maximum(2)
				.Build();

			cut.RaiseSimpleEvent(EventArgs.Empty);
			cut.RaiseSimpleEvent(EventArgs.Empty);

			monitor.Assert();
		}

		[TestMethod]
		[ExpectedException(typeof(AssertFailedException))]
		public void AssertEventHandlerMaximumCount()
		{
			var cut = new TestObject();

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler>(nameof(cut.SimpleEvent))
				.Maximum(2)
				.Build();

			cut.RaiseSimpleEvent(EventArgs.Empty);
			cut.RaiseSimpleEvent(EventArgs.Empty);
			cut.RaiseSimpleEvent(EventArgs.Empty);

			monitor.Assert();			
		}

		[TestMethod]
		public void CheckRaisedCalled()
		{
			var cut = new TestObject();

			var raisedCalled = false;

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler>(nameof(cut.SimpleEvent))
				.Raised((s,e) => raisedCalled = true)
				.Build();

			cut.RaiseSimpleEvent(EventArgs.Empty);

			Assert.IsTrue(raisedCalled);
		}

		[TestMethod]
		[ExpectedException(typeof(AssertFailedException))]
		public void AssertEventHandlerType()
		{
			var cut = new TestObject();

			var monitor = cut.GetMonitor()
				.Subscribe<EventHandler<EventArgs>>(nameof(cut.SimpleEvent))
				.Build();

			monitor.Assert();
		}

	}
}