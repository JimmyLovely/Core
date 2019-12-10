using System;
using Microsoft.AspNetCore.Mvc;

using NetCore.Models;
using NetCore.Interface;

namespace NetCore.Models
{

    public class DIPerson : IDIPerson
    {

        private IDIPhone dIPhone;

        public DIPerson(IDIPhone diPhone)
        {
            this.dIPhone = diPhone;
            this.name = "Jimmy";
            this.age = 18;
        }

        public int age;
        public string name;




        public string Read()
        {
            return dIPhone.Read(this.name);
        }

        public string Play()
        {
            return dIPhone.Play(this.name);
        }

        public string money()
        {
            return dIPhone.money(this.name);
        }
    }
}
