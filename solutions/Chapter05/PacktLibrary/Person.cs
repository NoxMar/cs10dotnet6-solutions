using System;
using System.Collections.Generic;
using static System.Console;

namespace Packt.Shared
{
    public class Person
    {
        public string Name;
        public DateTime DateOfBirth;

        public WondersOfTheAncientWorld FavoriteAncientWonder;

        public WondersOfTheAncientWorld BucketList;

        public List<Person> Children = new List<Person>();
    }
}