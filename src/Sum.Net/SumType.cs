// <copyright file="SumType.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Struct that may contain an <typeparamref name="A"/>.
    /// </summary>
    /// <typeparam name="A">The type this struct is designed to contain.</typeparam>
    /// <remarks>
    /// In some cases this struct may not contain an A, this can only happen if a more expansive sum type was upcast into this type.
    /// For example an instance of <see cref="SumType{Int32}"/> may not contain an int if it was upcast from a <see cref="SumType{Int32, String}"/>
    /// that contained a string instance.
    /// </remarks>
    [JsonConverter(typeof(SumConverter))]
    public struct SumType<A> : ISumType
    {
        internal int Index;

        internal SumType(int index, object value)
        {
            this.Index = index;
            this.Value = value;
        }

        /// <inheritdoc/>
        public object Value { get; }

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
    }

    /// <summary>
    /// Struct that may contain an <typeparamref name="A"/> or a <typeparamref name="B"/>.
    /// </summary>
    /// <typeparam name="A">The first type this struct is designed to contain.</typeparam>
    /// <typeparam name="B">The second type this struct is designed to contain.</typeparam>
    [JsonConverter(typeof(SumConverter))]
    public struct SumType<A, B> : ISumType
    {
        internal int Index;

        internal SumType(int index, object value)
        {
            this.Index = index;
            this.Value = value;
        }

        /// <inheritdoc/>
        public object Value { get; }

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
    }

    /// <summary>
    /// Struct that may contain an <typeparamref name="A"/>, a <typeparamref name="B"/>, or a <typeparamref name="C"/>.
    /// </summary>
    /// <typeparam name="A">The first type this struct is designed to contain.</typeparam>
    /// <typeparam name="B">The second type this struct is designed to contain.</typeparam>
    /// <typeparam name="C">The third type this struct is designed to contain.</typeparam>
    [JsonConverter(typeof(SumConverter))]
    public struct SumType<A, B, C> : ISumType
    {
        internal int Index;

        internal SumType(int index, object value)
        {
            this.Index = index;
            this.Value = value;
        }

        /// <inheritdoc/>
        public object Value { get; }

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="A"/> with a <see cref="SumType{A, B, C}"/>.
        /// </summary>
        /// <param name="a">Value to be wrap.</param>
        public static implicit operator SumType<A, B, C>(A a) => new SumType<A, B, C>(0, a);

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="B"/> with a <see cref="SumType{A, B, C}"/>.
        /// </summary>
        /// <param name="b">Value to be wrap.</param>
        public static implicit operator SumType<A, B, C>(B b) => new SumType<A, B, C>(1, b);

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="C"/> with a <see cref="SumType{A, B, C}"/>.
        /// </summary>
        /// <param name="c">Value to be wrap.</param>
        public static implicit operator SumType<A, B, C>(C c) => new SumType<A, B, C>(2, c);

        /// <summary>
        /// Implicitly wraps an instance of <see cref="SumType{A}"/> with a <see cref="SumType{A, B, C}"/>.
        /// </summary>
        /// <param name="sum">Sum instance to wrap.</param>
        public static implicit operator SumType<A, B, C>(SumType<A> sum) => new SumType<A, B, C>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly wraps an instance of <see cref="SumType{A, B}"/> with a <see cref="SumType{A, B, C}"/>.
        /// </summary>
        /// <param name="sum">Sum instance to wrap.</param>
        public static implicit operator SumType<A, B, C>(SumType<A, B> sum) => new SumType<A, B, C>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly downcasts a <see cref="SumType{A, B, C}"/> into a <see cref="SumType{A}"/>.
        /// </summary>
        /// <remarks>The resulting <see cref="SumType{A}"/> can later be cast back into a <see cref="SumType{A, B, C}"/> without any information loss.</remarks>
        /// <param name="sum">Sum instance to downcast.</param>
        public static implicit operator SumType<A>(SumType<A, B, C> sum) => new SumType<A>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly downcasts a <see cref="SumType{A, B, C}"/> into a <see cref="SumType{A, B}"/>.
        /// </summary>
        /// <remarks>The resulting <see cref="SumType{A, B}"/> can later be cast back into a <see cref="SumType{A, B, C}"/> without any information loss.</remarks>
        /// <param name="sum">Sum instance to downcast.</param>
        public static implicit operator SumType<A, B>(SumType<A, B, C> sum) => new SumType<A, B>(sum.Index, sum.Value);

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B, C}"/> to an instance of <typeparamref name="A"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B, C}"/> does not contain an instance of <typeparamref name="A"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator A(SumType<A, B, C> sum) => sum.Index == 0 && sum.Value is A aVal ? aVal : throw new InvalidCastException();

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B}"/> to an instance of <typeparamref name="B"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B, C}"/> does not contain an instance of <typeparamref name="B"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator B(SumType<A, B, C> sum) => sum.Index == 1 && sum.Value is B aVal ? aVal : throw new InvalidCastException();

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B, C}"/> to an instance of <typeparamref name="C"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B, C}"/> does not contain an instance of <typeparamref name="B"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator C(SumType<A, B, C> sum) => sum.Index == 2 && sum.Value is C aVal ? aVal : throw new InvalidCastException();
    }

    /// <summary>
    /// Struct that may contain an <typeparamref name="A"/>, a <typeparamref name="B"/>, a <typeparamref name="C"/>, or a <typeparamref name="D"/>.
    /// </summary>
    /// <typeparam name="A">The first type this struct is designed to contain.</typeparam>
    /// <typeparam name="B">The second type this struct is designed to contain.</typeparam>
    /// <typeparam name="C">The third type this struct is designed to contain.</typeparam>
    /// <typeparam name="D">The fourth type this struct is designed to contain.</typeparam>
    [JsonConverter(typeof(SumConverter))]
    public struct SumType<A, B, C, D> : ISumType
    {
        internal int Index;

        internal SumType(int index, object value)
        {
            this.Index = index;
            this.Value = value;
        }

        /// <inheritdoc/>
        public object Value { get; }

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="A"/> with a <see cref="SumType{A, B, C, D}"/>.
        /// </summary>
        /// <param name="a">Value to be wrap.</param>
        public static implicit operator SumType<A, B, C, D>(A a) => new SumType<A, B, C, D>(0, a);

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="B"/> with a <see cref="SumType{A, B, C, D}"/>.
        /// </summary>
        /// <param name="b">Value to be wrap.</param>
        public static implicit operator SumType<A, B, C, D>(B b) => new SumType<A, B, C, D>(1, b);

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="C"/> with a <see cref="SumType{A, B, C, D}"/>.
        /// </summary>
        /// <param name="c">Value to be wrap.</param>
        public static implicit operator SumType<A, B, C, D>(C c) => new SumType<A, B, C, D>(2, c);

        /// <summary>
        /// Implicitly wraps a value of type <typeparamref name="D"/> with a <see cref="SumType{A, B, C, D}"/>.
        /// </summary>
        /// <param name="d">Value to be wrap.</param>
        public static implicit operator SumType<A, B, C, D>(D d) => new SumType<A, B, C, D>(3, d);

        /// <summary>
        /// Implicitly wraps an instance of <see cref="SumType{A}"/> with a <see cref="SumType{A, B, C, D}"/>.
        /// </summary>
        /// <param name="sum">Sum instance to wrap.</param>
        public static implicit operator SumType<A, B, C, D>(SumType<A> sum) => new SumType<A, B, C, D>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly wraps an instance of <see cref="SumType{A, B}"/> with a <see cref="SumType{A, B, C, D}"/>.
        /// </summary>
        /// <param name="sum">Sum instance to wrap.</param>
        public static implicit operator SumType<A, B, C, D>(SumType<A, B> sum) => new SumType<A, B, C, D>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly wraps an instance of <see cref="SumType{A, B, C}"/> with a <see cref="SumType{A, B, C, D}"/>.
        /// </summary>
        /// <param name="sum">Sum instance to wrap.</param>
        public static implicit operator SumType<A, B, C, D>(SumType<A, B, C> sum) => new SumType<A, B, C, D>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly downcasts a <see cref="SumType{A, B, C, D}"/> into a <see cref="SumType{A}"/>.
        /// </summary>
        /// <remarks>The resulting <see cref="SumType{A}"/> can later be cast back into a <see cref="SumType{A, B, C, D}"/> without any information loss.</remarks>
        /// <param name="sum">Sum instance to downcast.</param>
        public static implicit operator SumType<A>(SumType<A, B, C, D> sum) => new SumType<A>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly downcasts a <see cref="SumType{A, B, C, D}"/> into a <see cref="SumType{A, B}"/>.
        /// </summary>
        /// <remarks>The resulting <see cref="SumType{A, B}"/> can later be cast back into a <see cref="SumType{A, B, C, D}"/> without any information loss.</remarks>
        /// <param name="sum">Sum instance to downcast.</param>
        public static implicit operator SumType<A, B>(SumType<A, B, C, D> sum) => new SumType<A, B>(sum.Index, sum.Value);

        /// <summary>
        /// Implicitly downcasts a <see cref="SumType{A, B, C, D}"/> into a <see cref="SumType{A, B, C}"/>.
        /// </summary>
        /// <remarks>The resulting <see cref="SumType{A, B, C}"/> can later be cast back into a <see cref="SumType{A, B, C, D}"/> without any information loss.</remarks>
        /// <param name="sum">Sum instance to downcast.</param>
        public static implicit operator SumType<A, B, C>(SumType<A, B, C, D> sum) => new SumType<A, B, C>(sum.Index, sum.Value);

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B, C, D}"/> to an instance of <typeparamref name="A"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B, C, D}"/> does not contain an instance of <typeparamref name="A"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator A(SumType<A, B, C, D> sum) => sum.Index == 0 && sum.Value is A aVal ? aVal : throw new InvalidCastException();

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B, C, D}"/> to an instance of <typeparamref name="B"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B, C, D}"/> does not contain an instance of <typeparamref name="B"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator B(SumType<A, B, C, D> sum) => sum.Index == 1 && sum.Value is B aVal ? aVal : throw new InvalidCastException();

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B, C, D}"/> to an instance of <typeparamref name="C"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B, C, D}"/> does not contain an instance of <typeparamref name="C"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator C(SumType<A, B, C, D> sum) => sum.Index == 2 && sum.Value is C aVal ? aVal : throw new InvalidCastException();

        /// <summary>
        /// Attempts to cast an instance of <see cref="SumType{A, B, C, D}"/> to an instance of <typeparamref name="D"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown if this instance of <see cref="SumType{A, B, C, D}"/> does not contain an instance of <typeparamref name="D"/>.</exception>
        /// <param name="sum">Instance to unwrap.</param>
        public static explicit operator D(SumType<A, B, C, D> sum) => sum.Index == 3 && sum.Value is D aVal ? aVal : throw new InvalidCastException();
    }
}
