using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.Framework;
using Friends.Domain.Peoples.Entities;
using Friends.Model;

namespace Friends.VisualState
{
    public class MainVisualState : Exrin.Framework.VisualState
    {
        private readonly IMainModel r_Model;

        public MainVisualState(IMainModel i_Model)
            : base(i_Model)
        {
            r_Model = i_Model;
        }

        [Binding(BindingType.TwoWay)]
        public Person SelectedPerson
        {
            get
            {
                return Get<Person>();
            }

            set
            {
                Set(value);
            }
        }

        public ObservableCollection<Person> Persons
        {
            get
            {
                return Get<ObservableCollection<Person>>();
            }

            set
            {
                Set(value);
            }
        }

        public override void Init()
        {
            Task.Run(async () =>
                {
                    IEnumerable<Person> persons = await r_Model.GetPersons();
                    Persons = new ObservableCollection<Person>(persons);
            });
        }
    }
}