- Put this inside Program.cs to log DI interfaces and its implementations:

```cs
foreach (var s in builder.Services)
        {
            Console.WriteLine("s.ServiceType: " + s.ServiceType + ", s.ImplementationType: " + s.ImplementationType);
        }
```
