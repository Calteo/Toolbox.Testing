using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Testing.Tests
{
	internal class TestObject
	{
		public event EventHandler? SimpleEvent;

		public void RaiseSimpleEvent(EventArgs args)
		{
			SimpleEvent?.Invoke(this, args);
		}
	}
}
