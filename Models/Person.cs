using System;
using Microsoft.AspNetCore.Mvc;

using NetCore.Models;

namespace NetCore.Models
{

    public class Person
    {
        public Person()
        {

        }

        public int age;
        public string name;


        public string Read()
        {
            Phone phone = new Phone();
            return phone.Read(this.name);
        }

        public string Play()
        {
            Phone phone = new Phone();
            return phone.Play(this.name);
        }

        public string money()
        {
            Phone phone = new Phone();
            return phone.money(this.name);
        }
    }
}
