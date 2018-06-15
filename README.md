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
  - InvokeMethod
- Object Extensions
  - [CopyValueTo](#objectcopyvaluetoobject-target-copyvaluemethod-method-bool-ingoreconverttypefailed-stringcomparison-stringcomparison)
# How to use
## Type.FindProperties&lt;TAttribute&gt;()
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
## Type.FindProperty&lt;TAttribute&gt;()
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
## Type.InvokeMethod(string methodName, object obj, params object[] parameters)
Allow to dynamic call method.
```csharp
pulic class User
{
      public void Count()
      {
      }
}

var user = new User();
var type = typeof(User);
type.InvokeMethod(user, null);
```
If you want to use this method via async/await, do it like the folllow code.
```csharp
pulic class User
{
      public void Count()
      {
      }
}

public class Test
{
    public async Task Test()
    {
        var user = new User();
        var type = typeof(User);
        await (Task)type.InvokeMethod(user, null);
    }
}

```
## Object.CopyValueTo(object target, CopyValueMethod method, bool ingoreConvertTypeFailed, StringComparison stringComparison)
Allow to copy value of properties to target object. Only support use property name or alias name.  
```csharp
var obj1 = new Object();
var obj2 = new Object();
obj1.CopyValueTo(obj2);
```
### Parameters
- target: Copy value of properties to this object.
- method: The method how to copy value. Default is CopyValueMethod.UsingPropertyNameOrAlias.
- ingoreConvertTypeFailed: If two properties have different type, it will be thrown when try to set value. If this value is true, it will skip, otherwize throw exception. Default is false.
- stringComparison: Compare name or alias with this value. Default is StringComparison.CurrentCulture.
### Copy value method
```csharp
public enum CopyValueMethod
{
      UsingPropertyNameOrAlias
}
```
### Use Alias
Allow to use AliasAttribute to specify another name. It will be used to compare property name.
```csharp
public class UserModel
{
      [Alias("RealName")]
      public string Name {get; set;}
}

public class UserEntity
{
    public string RealName {get; set;}
}

var obj1 = new UserModel();
var obj2 = new UserEntity();
obj1.CopyValueTo(obj2);
```
