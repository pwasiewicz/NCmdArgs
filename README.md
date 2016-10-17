# NCmdArgs
[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/pwasiewicz/Was.CommandLineArgs/master/LICENSE)

Simple parser for application arguments. Port of Was.CommandLineArgs.

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

## Positions arguments

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
