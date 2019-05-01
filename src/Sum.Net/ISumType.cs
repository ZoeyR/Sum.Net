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
        /// Gets the value stored in the SumType. This can be matched against using the "is" operator.
        /// </summary>
        object Value { get; }
    }
}
