using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using static LanguageExt.Prelude;
using static System.Console;

namespace Language_Ext_Poc
{
    internal class Program
    {
        private static void Main()
        {
            var optional = Some(10);

            var value = optional.Match(
               Some: v => v,
               None: () => 0
            );

            var value2 = match(optional, v => v, () => 0);

            WriteLine(value);

            optional = Option<int>.None;

            optional.Match(
                WriteLine,
                () => WriteLine(0)
            );

            var person = new Person("Greg", None, Some("12312"));

            WriteLine(person.ToString());

            person.ParseCode().Match(
                Left: ex => WriteLine(":("),
                Right: WriteLine
                );


            var one = Some(1);
            var two = Some(2);
            var three = Some(3);

            var result = match(
                from x in one
                from y in two
                from z in three
                select x + y + z,
                v => v,
                () => 0);


            WriteLine(result);

            Empty();

            var numbes = new List<Option<int>>()
            {
                Some(1),
                Some(100),
                None,
                None,
                None,
                None,
                Some(20),
                Some(213213)
            };

            var newNumbers = numbes.Map(
                 x => x.Match(
                     v => v * 2,
                     () => 0
                 )
             ).ToList();

            newNumbers.ForEach(WriteLine);

            var name = Tuple("Greg", "Felicio");
            var res = map(name, (first, last) => $"{first} {last}");
            WriteLine(res);

            Get().Match(WriteLine, () => WriteLine(":("));

        }

        private static Unit Empty()
        {
            WriteLine("Unit :)");
            return unit;
        }

        private static Option<Person> Get() => Some(new Person("greg", None, None));
    }

    internal class Person
    {
        public Person(string name, Option<string> lastName, Option<string> code)
        {
            Name = name;
            LastName = lastName;
            Code = code;
        }

        public string Name { get; }
        public Option<string> LastName { get; }
        public Option<string> Code { get; }

        public override string ToString() =>
            $"{Name} {LastName.Match(v => v, () => string.Empty)}";

        public Either<Exception, int> ParseCode()
        {
            try
            {
                var code = Code.Match(v => v, () => "1");
                return int.Parse(code);
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

    }
}
