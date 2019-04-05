// <copyright file="SumType.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net
{
    using System;

    /// <summary>
    /// Struct that may contain an <typeparamref name="A"/>.
    /// </summary>
    /// <typeparam name="A">The type this struct is designed to contain.</typeparam>
    /// <remarks>
    /// In some cases this struct may not contain an A, this can only happen if a more expansive sum type was upcast into this type.
    /// For example an instance of <see cref="SumType{Int32}"/> may not contain an int if it was upcast from a <see cref="SumType{Int32, String}"/>
    /// that contained a string instance.
    /// </remarks>
    public struct SumType<A> : ISumType
    {
        internal int Index;
        internal object Value;

        internal SumType(int index, object value)
        {
            this.Index = index;
            this.Value = value;
        }

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="A"/> with a <see cref="SumType{A}"/>.
        /// </summary>
        /// <param name="a">Value to be wrap.</param>
        public static implicit operator SumType<A>(A a) => new SumType<A>(0, a);

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A}"/> to an instance of <typeparamref name="A"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A}"/> does not contain an instance of <typeparamref name="A"/>.</exception>
        /// <param name="sum">Instance to be unwrapped.</param>
        public static explicit operator A(SumType<A> sum) => sum.Index == 0 && sum.Value is A aVal ? aVal : throw new InvalidCastException();

        /// <inheritdoc/>
        public bool Is<T>()
        {
            return this.TryGet<T>(out var _);
        }

        /// <inheritdoc/>
        public bool TryGet<T>(out T value)
        {
            return ((SumType<A, Empty>)this).TryGet<T>(out value);
        }
    }

    /// <summary>
    /// Struct that may contain an <typeparamref name="A"/> or a <typeparamref name="B"/>.
    /// </summary>
    /// <typeparam name="A">The first type this struct is designed to contain.</typeparam>
    /// <typeparam name="B">The second type this struct is designed to contain.</typeparam>
    public struct SumType<A, B> : ISumType
    {
        internal int Index;
        internal object Value;

        internal SumType(int index, object value)
        {
            this.Index = index;
            this.Value = value;
        }

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="A"/> with a <see cref="SumType{A, B}"/>.
        /// </summary>
        /// <param name="a">Value to be wrap.</param>
        public static implicit operator SumType<A, B>(A a) => new SumType<A, B>(0, a);

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="B"/> with a <see cref="SumType{A, B}"/>.
        /// </summary>
        /// <param name="b">Value to be wrap.</param>
        public static implicit operator SumType<A, B>(B b) => new SumType<A, B>(1, b);

        /// <summary>
        /// Implicitly wraps an instance of <see cref="SumType{A}"/> with a <see cref="SumType{A, B}"/>.
        /// </summary>
        /// <param name="sum">Sum instance to wrap.</param>
        public static implicit operator SumType<A, B>(SumType<A> sum) => new SumType<A, B>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly downcasts a <see cref="SumType{A, B}"/> into a <see cref="SumType{A}"/>.
        /// </summary>
        /// <remarks>The resulting <see cref="SumType{A}"/> can later be cast back into a <see cref="SumType{A, B}"/> without any information loss.</remarks>
        /// <param name="sum">Sum instance to downcast.</param>
        public static implicit operator SumType<A>(SumType<A, B> sum) => new SumType<A>(sum.Index, sum.Value);

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B}"/> to an instance of <typeparamref name="A"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B}"/> does not contain an instance of <typeparamref name="A"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator A(SumType<A, B> sum) => sum.Index == 0 && sum.Value is A aVal ? aVal : throw new InvalidCastException();

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B}"/> to an instance of <typeparamref name="B"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B}"/> does not contain an instance of <typeparamref name="B"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator B(SumType<A, B> sum) => sum.Index == 1 && sum.Value is B aVal ? aVal : throw new InvalidCastException();

        /// <inheritdoc/>
        public bool Is<T>()
        {
            return this.TryGet<T>(out var _);
        }

        /// <inheritdoc/>
        public bool TryGet<T>(out T value)
        {
            if (this.Index == 0 && this.Value is A && this.Value is T tValA)
            {
                value = tValA;
                return true;
            }

            if (this.Index == 1 && this.Value is B && this.Value is T tValB)
            {
                value = tValB;
                return true;
            }

            value = default;
            return false;
        }
    }

    internal struct Empty
    {
    }
}
