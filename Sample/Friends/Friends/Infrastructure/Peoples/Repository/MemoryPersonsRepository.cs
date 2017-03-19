using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Friends.Core;
using Friends.Domain.Peoples.Entities;
using Friends.Domain.Peoples.Interfaces;
using Friends.Domain.Validation.Interfaces;
using Microsoft.Extensions.Logging;
using ValidationException = Friends.Domain.Validation.Exceptions.ValidationException;

namespace Friends.Infrastructure.Peoples.Repository
{
    public class MemoryPersonsRepository : IPersonsRepository
    {
        private readonly ILogger<MemoryPersonsRepository> r_Logger;
        private readonly IValidator r_Validator;
        private readonly LinkedList<Person> r_Persons;

        public MemoryPersonsRepository(ILogger<MemoryPersonsRepository> i_Logger, IValidator i_Validator)
        {
            r_Logger = i_Logger;
            r_Validator = i_Validator;
            r_Persons = new LinkedList<Person>();
            initData();
        }

        public void Add(Person i_Person)
        {
            ICollection<ValidationResult> validationResults;
            if(r_Validator.Validate(i_Person, out validationResults))
            {
                i_Person.Id = Guid.NewGuid();
                i_Person.RegistrationDate = DateTime.UtcNow;
                r_Persons.AddLast(i_Person);
            }
            else
            {
                string[] errorMessages =
                    validationResults.Select(i_ValidationResult => i_ValidationResult.ErrorMessage).ToArray();
                ValidationException exception = new ValidationException(errorMessages);
                r_Logger.LogError(LoggingEvents.k_InsertItem, exception, "Error adding person");
                throw exception;
            }
        }

        public bool Remove(Guid i_Id)
        {
            return r_Persons.Remove(r_Persons.First(i_Person => i_Person.Id == i_Id));
        }

        public void Update(Guid i_Id, Person i_Value)
        {
            ICollection<ValidationResult> validationResults;
            if(r_Validator.Validate(i_Value, out validationResults))
            {
                Person person = r_Persons.First(i_Person => i_Person.Id == i_Id);
                person.Address = i_Value.Address;
                person.Email = i_Value.Email;
                person.Phone = i_Value.Phone;
            }
            else
            {
                string[] errorMessages =
                    validationResults.Select(i_ValidationResult => i_ValidationResult.ErrorMessage).ToArray();
                ValidationException exception = new ValidationException(errorMessages);
                r_Logger.LogError(LoggingEvents.k_UpdateItem, exception, "Error updating person");
                throw exception;
            }
        }

        public Person Get(Guid i_Id)
        {
            return r_Persons.First(i_Person => i_Person.Id == i_Id);
        }

        public IEnumerable<Person> Get()
        {
            return r_Persons;
        }

        private void initData()
        {
            Add(
                new Person()
                    {
                        FirstName = "Joey",
                        LastName = "Tribbiani",
                        Email = "joey@friends.com",
                        Phone = "052-1111111",
                        Address = "90 Bedford St. Apt. 19, New York, NY 10014"
                    });
            Add(
                new Person()
                    {
                        FirstName = "Ross",
                        LastName = "Geller",
                        Email = "ross@friends.com",
                        Phone = "052-2222222",
                        Address = "90 Bedford St. Apt. 19, New York, NY 10014"
                    });
            Add(
                new Person()
                    {
                        FirstName = "Chandler",
                        LastName = "Bing",
                        Email = "chandler@friends.com",
                        Phone = "052-3333333",
                        Address = "90 Bedford St. Apt. 20, New York, NY 10014"
                    });
            Add(
                new Person()
                    {
                        FirstName = "Monica",
                        LastName = "Geller",
                        Email = "monica@friends.com",
                        Phone = "052-4444444",
                        Address = "90 Bedford St. Apt. 20, New York, NY 10014"
                    });
            Add(
                new Person()
                    {
                        FirstName = "Rachel",
                        LastName = "Green",
                        Email = "rachel@friends.com",
                        Phone = "052-5555555",
                        Address = "90 Bedford St. Apt. 20, New York, NY 10014"
                    });
            Add(
                new Person()
                    {
                        FirstName = "Phoebe",
                        LastName = "Buffay",
                        Email = "phoebe@friends.com",
                        Phone = "052-6666666",
                        Address = "5 Morton St. Apt. 14, New York, NY 10014"
                    });
        }
    }
}