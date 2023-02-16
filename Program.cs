using System.Security.Cryptography;

Console.WriteLine("============= Basic: Intersect ==============");
List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };
List<int> list2 = new List<int> { 3, 4, 5, 6, 7 };

var list1list2Common = list1.Intersect(list2);

foreach (var item in list1list2Common)
{
    Console.WriteLine(item);
}

var list3 = new List<Person> 
{ 
    new Person { Name = "Alice", Age = 25 }, 
    new Person { Name = "Bob", Age = 30 }, 
    new Person { Name = "Charlie", Age = 35 },
    new Person { Name = "Eve", Age = 50 }
};

var list4 = new List<Person> 
{ 
    new Person { Name = "Bob", Age = 30 }, 
    new Person { Name = "David", Age = 35 }, 
    new Person { Name = "Eve", Age = 40 }
};

Console.WriteLine("============= Complex : Using IntersectBy single property (After .net 5) ==============");

// this is compare name and age both properties
var list3list4CommonSingleProperty = list3.IntersectBy(list4.Select(o => o.Name), x =>x.Name);

foreach (var item in list3list4CommonSingleProperty)
{
    Console.WriteLine(item.Name);
}



Console.WriteLine("============= Complex : Using IntersectBy multiple properties (After .net 5) ==============");

// this is compare name and age both properties
var list3list4CommonMultipleProperties = list3.IntersectBy(list4.Select(o => new { name = o.Name, age = o.Age}), x => new {name = x.Name, age= x.Age});

foreach (var item in list3list4CommonMultipleProperties)
{
    Console.WriteLine(item.Name);
}


Console.WriteLine("============= Complex : Using Comparer ==============");

var personComparer = new PersonComparer();
var commonPeople = list3.Intersect(list4, personComparer);

foreach (var item in commonPeople)
{
    Console.WriteLine(item.Name);
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class PersonComparer : IEqualityComparer<Person>
{
    public bool Equals(Person x, Person y)
    {
        if (x == null || y == null) return false;
        return (x.Name == y.Name && x.Age == y.Age);
    }

    public int GetHashCode(Person obj)
    {
        if (obj == null) return 0;
        return obj.Name.GetHashCode() ^ obj.Age.GetHashCode();
    }
}