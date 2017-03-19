using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Friends.Domain.Validation.Interfaces;

namespace Friends.Domain.Peoples.Entities
{
    public class Person : INotifyPropertyChanged, IValidatable
    {
        private string m_Address;
        private string m_Email;
        private string m_Phone;

        public Guid Id { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get
            {
                return m_Email;
            }

            set
            {
                if (m_Email != value)
                {
                    m_Email = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone
        {
            get
            {
                return m_Phone;
            }

            set
            {
                if(m_Phone != value)
                {
                    m_Phone = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [StringLength(50)]
        public string Address
        {
            get
            {
                return m_Address;
            }

            set
            {
                if (m_Address != value)
                {
                    m_Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime RegistrationDate { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object i_Obj)
        {
            bool equals;

            Person otherPerson = i_Obj as Person;
            if (otherPerson == null)
            {
                equals = false;
            }
            else
            {
                equals = this.Id.Equals(otherPerson.Id);
            }

            return equals;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string i_PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(i_PropertyName));
        }
    }
}
