using System.Collections.Generic;
using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.Base;
using Friends.Domain.Peoples.Entities;
using Friends.Domain.Peoples.Interfaces;
using Friends.ModelState;
using Microsoft.Extensions.Logging;

namespace Friends.Model
{
    public class MainModel : BaseModel, IMainModel
    {
        private readonly IPersonsRepository r_PersonsRepository;
        private readonly ILogger<MainModel> r_Logger;

        public MainModel(IExrinContainer i_ExrinContainer, IPersonsRepository i_PersonsRepository, ILogger<MainModel> i_Logger)
            : base(i_ExrinContainer, new MainModelState())
        {
            r_PersonsRepository = i_PersonsRepository;
            r_Logger = i_Logger;
        }

        public IMainModelState State
        {
            get
            {
                return ModelState as IMainModelState;
            }
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            r_Logger.LogInformation("Getting Freinds list");
            return await Task.Factory.StartNew<IEnumerable<Person>>(() => r_PersonsRepository.Get());
        }
    }
}