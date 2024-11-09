# Plato.Geometry

**Plato.Geometry** is a C# geometry library with a multi-language geometry kernel at it's core. In
other words it is a math and geometry library that is appropriate for usage in 
3D Computer Aided Design (CAD), Computer Aided Engineering (CAE), and Digital Content Creation (DCC) software. 

Plato.Geometry is currently delivered as a cross-platform C# library, but the roadmap includes C++ and JavaScript support. 
All algorithms and data structures are written in a high-level programming language called Plato
which makes porting to different languages easier. 

# Status

This library is a refactoring and redesign of the following libraries:

* [Ara3D.Geometry](https://github.com/ara3d/geometry)
* [Ara3D.Mathematics](https://github.com/ara3d/mathematics)
* [Vim.Math3D](https://github.com/vimaec/math3d)
* [Ara3D.Geometry-Toolkit](https://github.com/ara3d/geometry-toolkit)

The goal is to create a single library that is more efficient, maintainable, and portable 
that supports both single and double precision floating point math. 

## Compiling and Building Sources

This repository is provided as a convenience, it is intended to used as sub-module of the [Ara3D 
mono-repository](https://github.com/ara3d/ara3d).

The C# source files in this repository are generated from the Plato source files in the `plato-src` directory.
Do not edit the C# source files with the extension ".g.cs" directly. 

The tool for building C# source files from Plato source files is in a separate repository (https://github.com/cdiggins/plato), and is 
called `PlatoWinFormsEditor`. 

## About the C# Library 

Plato.Geometry is a self-contained C# library with no 3rd party dependencies, and very little dependency on the System libraries. 
It is designed to make it easy to define simple, efficient, and easy to read mathematical and geometric routines and data-structures.  

All classes in Plato are immutable, meaning that they can't be changed. However every class comes with 
helper functions for transforming them implicitly into tuples, or classes which are structurally similar.
Helper functions are also provided that allow them to be easily transformed into new values by changing one of their fields,
which are named `With__FIELDNAME__()`.   

## About Code Generation

High quality mathematical libraries in different languages commonly resort to using code generation, to cover the 
large amounts of repetition and boilerplate that can arise. They also do this to minimize introducing abstraction
penalties that can arise by using virtual function table lookups.  

For example: 

* Different dimensionality of vectors
* Implicit/explicit conversions
* Single / double precision 
* String formatting and parsing
* Optimizations such as loop rolling, function inlining  

Some examples of C# mathematical libraries that use code generation techniques are:

* Unity.Mathematics
* VIM.Math3D
* System.Numerics

We took this idea to another level, by creating a domain specific language (Plato) for expressing concrete data types, abstrsact data types, and algorithms
in a way that could easily yield high-performance code, not just in C#, but in other languages.  

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

## Concepts - Low Cost Abstractions 

If a function is defined with a concept as its first parameter, an implementation is automatically 
generated for each type that implements that concept. 
This is similar to how [C++ templates work](https://en.cppreference.com/w/cpp/language/templates).

This is more efficient than interfaces or abstract functions, because what would normally require 
a virtual table lookup in many cases can be avoided altogether. The trade-off is increased code size. 

## Object Syntax but not an OOPL

Plato syntax looks like an Object Oriented Programming Language (OOPL), but there are no _methods_ and 
no implicit `this` object. 

All functions in libraries are implicitly can be called using _dot syntax_ with the first parameter on 
the left of the dot. This is similar to how [extension methods work in C#](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods).

There are also no _properties_. A method with only one argument can be invoked without including the parameters. 

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
This project just takes the idea to a logical next step. 

