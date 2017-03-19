using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.Framework;
using Friends.Locator;

namespace BeaconApp.ViewModel
{
    public class AboutOperation : ISingleOperation
    {
        public AboutOperation()
        {
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
                        await Task.Delay(0);
                        Result result = new Result()
                                            {
                                                ResultAction = ResultType.Navigation,
                                                Arguments =
                                                    new NavigationArgs()
                                                        {
                                                            Key = Views.eMain.About,
                                                            StackType = eStack.Main,
                                                            Parameter = i_Parameter
                                                        }
                                            };

                        return new List<IResult>() { result };
                    };
            }
        }
    }
}