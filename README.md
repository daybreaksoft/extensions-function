# Daybreaksoft.Extensions.Functions
Daybreaksoft.Extensions.Functions is an extensions for Asp.Net or Asp.Net Core.
# Installing via NuGet
The easiest way to install Daybreaksoft.Extensions.Functions is via [NuGet](https://www.nuget.org/packages/Daybreaksoft.Extensions.Functions).  
In Visual Studio's [Package Manager Console](https://docs.microsoft.com/zh-cn/nuget/tools/package-manager-console), enter the following command:
```bash
Install-Package Daybreaksoft.Extensions.Functions
```
# Support Frameworks
- net451
- netstandard1.3
- netstandard2.0
# Features
- Type Extensions
  - [FindProperties&lt;TAttribute&gt;](#typefindpropertiestattribute)
  - [FindProperty&lt;TAttribute&gt;](#typefindpropertytattribute)
  - [InvokeMethod](#typeinvokemethod)
- Object Extensions
  - [CopyValueTo](#objectcopyvalueto)
- Exception Extensions
  - [GetRootMessage](#exceptiongetrootmessage)
# How to use
## Type.FindProperties&lt;TAttribute&gt;
Allow to find property vis specified attrubute. It supports return multiple results.
```csharp
pulic class User
{
      [Required]
      public string Name {get; set;}
  
      [Required]
      public string Password {get; set;}
}

var type = typeof(User);
type.FindProperties<RequiredAttribute>();
```
## Type.FindProperty&lt;TAttribute&gt;
Allow to find property vis specified attrubute. Only support return signle results.
```csharp
pulic class User
{
      [Key]
      public string UserId {get; set;}
}

var type = typeof(User);
type.FindProperty<KeyAttribute>();
```
## Type.InvokeMethod
Allow to dynamic call method.
```csharp
pulic class User
{
      public void Count(int number)
      {
      }
}

var user = new User();
var type = typeof(User);
type.InvokeMethod("Count", user, 1);
```
If you want to use this method that implemented by async/await, do it like the folllow code.
```csharp
pulic class User
{
      public async Task CountAsync()
      {
      }
}

public class Test
{
    public async Task TestAsync()
    {
        var user = new User();
        var type = typeof(User);
        await (Task)type.InvokeMethod("CountAsync", user, 1);
    }
}

```
## Object.CopyValueTo
Allow to copy value of properties to target object. Only support use property name or alias name.  
```csharp
var obj1 = new Object();
var obj2 = new Object();
obj1.CopyValueTo(obj2);
```
### Parameters
- target: Copy value of properties to this object.
- ignorePropertyNames: Allow to ignore specified property when copy value. Default is null.
- propertyMap: Allow to change property name to new name as compare value. Default is null.
- ignoreRefType: Allow to ingore ref type when copy value. Default is true.
- forcePropertyNames: Allow to force to copy value even if ignore ref type. Default is null.
- stringComparison: Compare name or alias with this value. Default is StringComparison.CurrentCulture.
## Exception.GetRootMessage
Get root error message if there are inner exception(s).  
```csharp
try
{
    Method();
}
catch (Exception e)
{
    Console.WriteLine(e.GetRootMessage())
}
```
