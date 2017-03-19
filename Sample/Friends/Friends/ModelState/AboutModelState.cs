using Friends.Domain.Peoples.Entities;

namespace Friends.ModelState
{
    public class AboutModelState : Exrin.Framework.ModelState, IAboutModelState
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