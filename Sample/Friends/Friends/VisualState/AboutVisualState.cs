using Exrin.Abstraction;
using Exrin.Framework;
using Friends.Domain.Peoples.Entities;

namespace Friends.VisualState
{
    public class AboutVisualState : Exrin.Framework.VisualState
    {
        public AboutVisualState(IBaseModel i_Model) : base(i_Model)
        {
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
    }
}
