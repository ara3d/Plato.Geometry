# Plato.Geometry

**Plato.Geometry** is a multi-language geometry kernel. In
other words it is a math and geometry library that is appropriate for usage in 
3D Computer Aided Design (CAD), Computer Aided Engineering (CAE), and 
Digital Content Creation (DCC) software. 

Plato.Geometry is currently delivered as a C# library, but the roadmap includes C++ and JavaScript support. 
All algorithms and data structures are written in a high-level programming language called Plato
which makes porting to different languages easier. 

# About Plato 

[Plato](https://github.com/cdiggins/Plato) is designed specifically for helping to write efficient low-level libraries in a 
straightforward manner, with less boilerplate. 

* All data types are read-only 
* No abstraction penalty
* Operator overloading
* Default implementations for:
  * Converting to strings
  * Equality Comparison
  * Generating hash code 
  * Parsing strings
  * Implicit conversion to/from tuples

The Plato tool-chain is written from the ground-up in C# and parses, analyzes, optimizes, and generates code.  

## Top Level Structures  

Plato has three types of top-level structures that a user can define:

* **Library** - a collection of functions
* **Type** - a concrete data type (e.g., a struct)
* **Concept** - an abstract data type (e.g., an interface or trait)

## Concepts - Zero Cost Abstractions 

If a function is defined with a concept as its first parameter, an implementation is automatically 
generated for each type that implements that concept. 
This is similar to how [C++ templates work](https://en.cppreference.com/w/cpp/language/templates).  

## Object Syntax but not an OOPL

Plato syntax looks like an Object Oriented Programming Language (OOPL), but there are no _methods_ and 
no implicit `this` object. 

All functions in libraries are implicitly can be called using _dot syntax_ with the first parameter on 
the left of the dot. This is similar to how [extension methods work in C#](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods).

There are also no _properties_. A method with only one argument can be invoked without including the parameters. 

## Learning More

At this point, the only thing left to do is to start reading the code. 
We would welcome question submitted as issues so we can improve the documentation.   

# FAQ (Frequently Asked Questions)

## Q: Why? 

**A:** Basic mathematical and geometry algorithms and data structures continue to be written
over and over again. We can find thousands or more versions of the same algorithm 
written with various bugs and limitations, in different styles and languages. 

## Q: Isn't this too Ambitious?

**A:** No. We have experience building math and geometry libraries in different programming languages,
as well as parsing and compilers. When you put it all together, this is actually much easier than 
trying to manually write and maintain libraries by hand in different programming languages. 
We observed that sophisticated math libraries
often resort to code generation techniques simply to work around limitations in the core languages. 
This project just takes the idea to its logical conclusion. 

