using System;
using System.Collections.Generic;
using Friends.Domain.Peoples.Entities;

namespace Friends.Domain.Peoples.Interfaces
{
    public interface IPersonsRepository
    {
        void Add(Person i_Person);

        bool Remove(Guid i_Id);

        void Update(Guid i_Id, Person i_Person);

        Person Get(Guid i_Id);

        IEnumerable<Person> Get();
    }
}