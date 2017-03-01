using System;
using System.Threading;

namespace Xamarin.Extensions.Logging.MobileCenter
{
    public class MobileCenterLogScope
    {
        private static readonly AsyncLocal<MobileCenterLogScope> sr_Value = new AsyncLocal<MobileCenterLogScope>();
        private readonly string r_Name;
        private readonly object r_State;

        internal MobileCenterLogScope(string i_Name, object i_State)
        {
            r_Name = i_Name;
            r_State = i_State;
        }

        public static MobileCenterLogScope Current
        {
            get
            {
                return sr_Value.Value;
            }

            set
            {
                sr_Value.Value = value;
            }
        }

        public MobileCenterLogScope Parent { get; private set; }

        public static IDisposable Push(string i_Name, object i_State)
        {
            MobileCenterLogScope newParentScope = Current;
            Current = new MobileCenterLogScope(i_Name, i_State) { Parent = newParentScope };

            return new DisposableScope();
        }

        public override string ToString()
        {
            return r_State != null ? r_State.ToString() : null;
        }

        private class DisposableScope : IDisposable
        {
            public void Dispose()
            {
                MobileCenterLogScope.Current = MobileCenterLogScope.Current.Parent;
            }
        }
    }
}
