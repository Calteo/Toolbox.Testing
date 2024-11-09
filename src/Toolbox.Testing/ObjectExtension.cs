using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Testing
{
	public static class ObjectExtension
	{
		public static EventMonitor<T> GetMonitor<T>(this T obj) where T : class	=> new(obj);
	}
}
