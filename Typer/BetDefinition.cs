using System;

namespace Typer
{
    public abstract class BetDefinition
    {
        private DateTime _closeDate;
        
        public DateTime CloseDate
        {
            get { return _closeDate; }
            protected set { _closeDate = value; }
        }

        protected BetDefinition(DateTime closeDate)
        {
            _closeDate = closeDate;
        }
    }
}
