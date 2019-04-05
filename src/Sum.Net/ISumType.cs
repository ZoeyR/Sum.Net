// <copyright file="ISumType.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net
{
    /// <summary>
    /// Abstracts over the idea of a "sum type". Sum types are types that can contain one value of various types.
    /// This abstraction is guaranteed to be typesafe, meaning you cannot access the underlying value without knowing
    /// its specific type.
    /// </summary>
    public interface ISumType
    {
        /// <summary>
        /// Checks if this sum type instance contains the specified type.
        /// </summary>
        /// <typeparam name="T">The type to check for.</typeparam>
        /// <returns>True if this sum type contains a <typeparamref name="T"/>, false otherwise.</returns>
        bool Is<T>();

        /// <summary>
        /// Tries to retrieve a <typeparamref name="T"/> from this instance of ISumType.
        /// </summary>
        /// <typeparam name="T">The type to try and retrieve.</typeparam>
        /// <param name="value">This is set to the underlying <typeparamref name="T"/> value if this instance contains a <typeparamref name="T"/>.</param>
        /// <returns>True if this sum type contains a <typeparamref name="T"/>, false otherwise.</returns>
        bool TryGet<T>(out T value);
    }
}
