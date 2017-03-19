using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.Framework;
using Friends.Model;

namespace Friends.ViewModel
{
    public class CallOperation : ISingleOperation
    {
        private readonly IAboutModel r_AboutModel;

        public CallOperation(IAboutModel i_AboutModel)
        {
            r_AboutModel = i_AboutModel;
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

                    if(await r_AboutModel.Call())
                    {
                        result = new Result()
                                     {
                                         ResultAction = ResultType.None
                                     };
                    }
                    else
                    {
                        result = new Result()
                                     {
                                         ResultAction = ResultType.Display,
                                         Arguments = new DisplayArgs() { Message = "Call was unsuccessful" }
                                     };
                    }

                    return new List<IResult>() { result };
                };
            }
        }
    }
}
