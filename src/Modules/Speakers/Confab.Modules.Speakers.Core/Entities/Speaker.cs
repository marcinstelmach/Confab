namespace Confab.Modules.Speakers.Core.Entities
{
    using System;

    public class Speaker
    {
        public Speaker(string name, int age)
        {
            Id = Guid.NewGuid();
            Name = name;
            Age = age;
        }

        private Speaker()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Age { get; private set; }
    }
}