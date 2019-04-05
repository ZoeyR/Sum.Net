# Sum.Net (Sum/Union types for C#)

[![Build Status](https://dev.azure.com/dgriffen/Sum.Net/_apis/build/status/dgriffen.Sum.Net?branchName=master)](https://dev.azure.com/dgriffen/Sum.Net/_build/latest?definitionId=6&branchName=master)
[![codecov](https://codecov.io/gh/dgriffen/Sum.Net/branch/master/graph/badge.svg)](https://codecov.io/gh/dgriffen/Sum.Net)

## Features

This repo provides union types inspired by the union type system in TypeScript. This means you get the following features:

- `SumType<A>` can be implicitly upcast to `SumType<A, B>`.
- `SumType<A, B>` can be implicitly downcast to `SumType<A>` (without losing information).
- Any type can be implicitly cast to a `SumType` containing that type.
- You cannot retrieve an instance from `SumType` that is not advertised in its type parameters.
