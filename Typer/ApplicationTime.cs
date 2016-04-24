using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typer
{
    public class ApplicationTime
    {
        private static ITimeProvider _currentTimeProvider;

        public static ITimeProvider CurrentTimeProvider
        {
            get { return _currentTimeProvider ?? (_currentTimeProvider = new DefaultTimeProvider()); }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Time provider can not be null");
                }

                _currentTimeProvider = value;
            }
        }

        public static void ResetToDefault()
        {
            _currentTimeProvider = new DefaultTimeProvider();
        }

        public static DateTime GetUtcNow()
        {
            return CurrentTimeProvider.GetUtcNow();
        }

        internal static void ClearLogic()
        {
            _currentTimeProvider = null;
        }
    }
}
