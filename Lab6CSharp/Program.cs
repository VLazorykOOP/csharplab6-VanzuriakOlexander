using System;
using System.Collections;


public interface IUser
{
    string Name { get; set; }
    string Address { get; set; }
    string PhoneNumber { get; set; }
    void ShowUserInfo();
}

public interface IDotNet
{
    DateTime CreatedAt { get; }
}

// Клас Person, що реалізує інтерфейс користувача
public class Person : IUser
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }

    public Person()
    {
        Console.WriteLine("Person Constructor without parameters called.");
    }

    public Person(string name, string address, string phoneNumber)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Console.WriteLine("Person Constructor with all parameters called.");
    }

    public void ShowUserInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
    }
}

public class Employee : Person, IUser, IDotNet
{
    public string Position { get; set; }
    public double Salary { get; set; }
    public DateTime CreatedAt { get; }

    public Employee(string name, string address, string phoneNumber, string position, double salary)
        : base(name, address, phoneNumber)
    {
        Position = position;
        Salary = salary;
        CreatedAt = DateTime.Now;
        Console.WriteLine("Employee Constructor with all parameters called.");
    }

    public new void ShowUserInfo()
    {
        base.ShowUserInfo();
        Console.WriteLine($"Position: {Position}");
        Console.WriteLine($"Salary: {Salary}");
    }
}

public class Worker : Person, IUser
{
    public string WorkSchedule { get; set; }
    public DateTime HireDate { get; set; }

    public Worker(string name, string address, string phoneNumber, string workSchedule, DateTime hireDate)
        : base(name, address, phoneNumber)
    {
        WorkSchedule = workSchedule;
        HireDate = hireDate;
        Console.WriteLine("Worker Constructor with all parameters called.");
    }

    public new void ShowUserInfo()
    {
        base.ShowUserInfo();
        Console.WriteLine($"Work Schedule: {WorkSchedule}");
        Console.WriteLine($"Hire Date: {HireDate}");
    }
}

public class Engineer : Worker, IUser, IDotNet
{
    public string Specialization { get; set; }
    public string QualificationLevel { get; set; }
    public DateTime CreatedAt { get; }

    public Engineer(string name, string address, string phoneNumber, string workSchedule, int v, DateTime hireDate,
                    string specialization, string qualificationLevel)
        : base(name, address, phoneNumber, workSchedule, hireDate)
    {
        Specialization = specialization;
        QualificationLevel = qualificationLevel;
        CreatedAt = DateTime.Now;
        Console.WriteLine("Engineer Constructor with all parameters called.");
    }

    public new void ShowUserInfo()
    {
        base.ShowUserInfo();
        Console.WriteLine($"Specialization: {Specialization}");
        Console.WriteLine($"Qualification Level: {QualificationLevel}");
    }
}

public interface IFunction : IDotNet
{
    double Calculate(double x);
}

public abstract class FunctionBase : IFunction
{
    public DateTime CreatedAt { get; } = DateTime.Now;

    public abstract double Calculate(double x);
}

public class Line : FunctionBase
{
    private double a;
    private double b;

    public Line(double a, double b)
    {
        this.a = a;
        this.b = b;
    }

    public override double Calculate(double x)
    {
        return a * x + b;
    }
}

public class Kub : FunctionBase
{
    private double a;
    private double b;
    private double c;

    public Kub(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public override double Calculate(double x)
    {
        return a * x * x + b * x + c;
    }
}

public class Hyperbola : FunctionBase
{
    public override double Calculate(double x)
    {
        return 1.0 / x;
    }
}

public class Triangle : IEnumerable
{
    private int a, b, c;
    private string color;

    public Triangle(int f, int s, int t)
    {
        a = f;
        b = s;
        c = t;
        color = "none";
    }

    public Triangle(int f, int s, int t, string col)
    {
        a = f;
        b = s;
        c = t;
        color = col;
    }

    public void Print()
    {
        Console.WriteLine($"Triangle lines: a = {a}, b = {b}, c = {c}, color = {color}");
    }

    public int First
    {
        get { return a; }
        set { a = value; }
    }

    public int Second
    {
        get { return b; }
        set { b = value; }
    }

    public int Third
    {
        get { return c; }
        set { c = value; }
    }

    public string Color
    {
        get { return color; }
    }

    public int Perimeter()
    {
        return a + b + c;
    }

    public double Area()
    {
        float halfperimeter = (a + b + c) / 2;
        return Math.Sqrt(halfperimeter * (halfperimeter - a) * (halfperimeter - b) * (halfperimeter - c));
    }

    public int this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return a;
                case 1: return b;
                case 2: return c;
                default: throw new IndexOutOfRangeException("Index out of range.");
            }
        }
        set
        {
            switch (index)
            {
                case 0: a = value; break;
                case 1: b = value; break;
                case 2: c = value; break;
                case 3: color = value.ToString(); break;
                default: throw new IndexOutOfRangeException("Index out of range.");
            }
        }
    }

    public static Triangle operator ++(Triangle triangle)
    {
        triangle.a++;
        triangle.b++;
        triangle.c++;
        return triangle;
    }

    public static Triangle operator --(Triangle triangle)
    {
        triangle.a--;
        triangle.b--;
        triangle.c--;
        return triangle;
    }

    public static bool operator true(Triangle triangle)
    {
        return triangle.a + triangle.b > triangle.c && triangle.a + triangle.c > triangle.b && triangle.b + triangle.c > triangle.a;
    }

    public static bool operator false(Triangle triangle)
    {
        return triangle.a + triangle.b <= triangle.c ||
               triangle.a + triangle.c <= triangle.b ||
               triangle.b + triangle.c <= triangle.a;
    }

    public static Triangle operator *(Triangle triangle, int scalar)
    {
        triangle.a *= scalar;
        triangle.b *= scalar;
        triangle.c *= scalar;
        return triangle;
    }

    public static implicit operator string(Triangle triangle)
    {
        return $"Triangle lines: a = {triangle.a}, b = {triangle.b}, c = {triangle.c}, color = {triangle.color}";
    }
    public IEnumerator GetEnumerator()
    {
        return new TriangleEnumerator(this);
    }

    private class TriangleEnumerator : IEnumerator
    {
        private int position = -1;
        private Triangle triangle;

        public TriangleEnumerator(Triangle triangle)
        {
            this.triangle = triangle;
        }

        public bool MoveNext()
        {
            position++;
            return position < 3;
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current
        {
            get
            {
                switch (position)
                {
                    case 0: return triangle.First;
                    case 1: return triangle.Second;
                    case 2: return triangle.Third;
                    default: throw new InvalidOperationException();
                }
            }
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the task");
        string? str = Console.ReadLine();
        int n = 0;
        if (str != null) n = int.Parse(str);
        if (n == 1)
        {
            Person person = new Person("John Doe", "123 Main St", "555-1234");
            person.ShowUserInfo();
            Console.WriteLine();

            Employee employee = new Employee("Jane Smith", "456 Elm St", "555-5678", "Manager", 50000);
            employee.ShowUserInfo();
            Console.WriteLine();

            Worker worker = new Worker("Alice Johnson", "789 Oak St", "555-91011", "9-5", DateTime.Now);
            worker.ShowUserInfo();
            Console.WriteLine();
        }
        else if (n == 2)
        {
            IFunction[] functions = new IFunction[]
            {
            new Line(2, 3),
            new Kub(1, -2, 1),
            new Hyperbola()
            };

            double x = 2.5;

            foreach (var function in functions)
            {
                Console.WriteLine($"Function : {function}");
                Console.WriteLine($"Value of the function at x = {x}: {function.Calculate(x)}");
                Console.WriteLine();
            }
        }
        else if (n == 3)
        {
            Triangle triangle = new Triangle(3, 4, 5);

            foreach (int side in triangle)
            {
                Console.WriteLine(side);
            }
        }
    }
}
