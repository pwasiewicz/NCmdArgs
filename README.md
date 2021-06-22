# NCmdArgs
[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/pwasiewicz/Was.CommandLineArgs/master/LICENSE)

Simple parser for application arguments. Port of Was.CommandLineArgs.

Available on nuget for .NET, .NET Standard & .NET Core.

## Basic usage

```c#
internal class SimpleStringOptions
{
    [CommandArgument]
    public string Hello { get; set; }
}

var args = new[] {   "--hello", "sampleval" };
var opt = new SimpleStringOptions();

var p = new CommandLineParser();
p.Parse(opt, args);

Assert.AreEqual("sampleval", opt.Hello);
```

You can configure any  argument via passing paremeters to **CommandArgument** attribute:

```c#
[CommandArgument(ShortName = "h", Required = true, Description = "Hello test value")]
public string Hello { get; set; }
```

## Collections 

You can set collection as arguments. Library will put elements after argument name unless it is other argument:

```ps
> ./myapp --id 10 20 44 --other-arg
```

It this case - elements 10, 20, 40 will be inserted into collection.
As for now, the following collection types are supported:
* List
* Array
* Hashset
* Enumerable (creates List instance underhood)

## Verbs

Library supports git-like verbs in arguments:

```C#
public class VerbOptions
{
    [CommandVerb (Description = "My sample verb")]
    public VerbCommand MyVerb { get; set; }

    [CommandArgument(Description =  "Hello world switch.")]
    public string Hello { get; set; }
}

public class VerbCommand
{
    [CommandArgument]
    public string Hello { get; set; }
}

var args = new[] { "myverb", "--hello", "test" };
var opt = new VerbOptions();

var p = new CommandLineParser();
p.Parse(opt, args);


Assert.AreEqual("test", opt.MyVerb.Hello);
```

You can also access verb in other way:
```c#
p.LastVerb //instance of verb parsed
```

## Help

You can write help based on arguments class structure and descriptions in attributes.

```c#
var p = new CommandLineParser();
using (var w = new StringWriter())
{
    p.Usage<RequriedSImpleStringOptions>(w);

    result = w.ToString();
}
```

Sample output:

```PS
Program arguments:
   --Hello, -h  Hello test value
```

## Position of arguments

You can also base on arguments positions:

```c#
[CommandArgument]
[CommandInlineArgument(0)]
public string Hello { get; set; }
```

```ps
> ./myapp hello-arg-value
```

As you can see above - you do not need to pass arugment name (only position in arguments list).


# Release notes

## Version 1.0.4.7
* Fixed Parse - returns false instead of throwing exception for some cases
* Fixed verb parssing - callback for specific verb was called event is parsing arguments failed
* Usage writer prints verbs options also

## Version 1.0.4.4
* Command name can be implicity splitted by specified char. For example, property MyOption can be resolved as my-option

## Version 1.0.4.3
* Default property for property can be set inside options class (f.e in ctor)

## Version 1.0.4.2
* Long switch attribute for argument (f.e to override long name that is fetched from property name)

## Version 1.0.4.1
* Added .NET Framework 4.5.1 & 4.5.2 target

## Version 1.0.4
* `bool` handler is no expecting value anymore (you can just specify flag `-h` instead of `-h true`)
* Fixed invalid verb name in usages

## Previous Version
Not available for now. 