# Sum.Net (Sum/Union types for C#)

## Features

This repo provides union types inspired by the union type system in TypeScript. This means you get the following features:

- `SumType<A>` can be implicitly upcast to `SumType<A, B>`.
- `SumType<A, B>` can be implicitly downcast to `SumType<A>` (without losing information).
- Any type can be implicitly cast to a `SumType` containing that type.
- You cannot retrieve an instance from `SumType` that is not advertised in its type parameters.
