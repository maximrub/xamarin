using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.Framework;
using Friends.Locator;
using Friends.Model;

namespace Friends.ViewModel
{
    public class LoginOperation : ISingleOperation
    {
        private readonly ILoginModel r_AuthModel;

        public LoginOperation(ILoginModel i_AuthModel)
        {
            r_AuthModel = i_AuthModel;
            ChainedRollback = false;
        }

        public bool ChainedRollback { get; private set; }

        public Func<Task> Rollback
        {
            get
            {
                return null;
            }
        }


        public Func<object, CancellationToken, Task<IList<IResult>>> Function
        {
            get
            {
                return async (i_Parameter, i_Token) =>
                {
                    Result result = null;

                    if(await r_AuthModel.Login())
                    {
                        result = new Result()
                                     {
                                         ResultAction = ResultType.Navigation,
                                         Arguments =
                                             new NavigationArgs() { Key = Views.eMain.Main, StackType = eStack.Main }
                                     };
                    }
                    else
                    {
                        result = new Result()
                                     {
                                         ResultAction = ResultType.Display,
                                         Arguments = new DisplayArgs() { Message = "Login was unsuccessful" }
                                     };
                    }

                    return new List<IResult>() { result };
                };
            }
        }
    }
}
