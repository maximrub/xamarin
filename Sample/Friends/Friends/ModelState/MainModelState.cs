using Friends.Domain.Peoples.Entities;

namespace Friends.ModelState
{
    public class MainModelState : Exrin.Framework.ModelState, IMainModelState
    {
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
    }
}