// <copyright file="DoubleSumTests.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net.Tests
{
    using System;
    using Xunit;

    public class DoubleSumTests
    {
        [Fact]
        public void ExplicitCastSuccessTest()
        {
            SumType<int, string> sum = "foo";
            Assert.Equal("foo", (string)sum);

            sum = 1;
            Assert.Equal(1, (int)sum);
        }

        [Fact]
        public void ExplicitCastFailsTest()
        {
            SumType<int, string> sum = "foo";
            Assert.Throws<InvalidCastException>(() => (int)sum);

            sum = 1;
            Assert.Throws<InvalidCastException>(() => (string)sum);
        }

        [Fact]
        public void ExplicitCastDowncastUpcastSucceedsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string> upperSum = lowerSum;

            Assert.Equal("foo", (string)upperSum);
        }

        [Fact]
        public void ExplicitCastDowncastUpcastFailsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool> upperSum = lowerSum;

            Assert.Throws<InvalidCastException>(() => (bool)upperSum);
        }

        [Fact]
        public void SumIsTrueTest()
        {
            SumType<int, string> sum = "foo";
            Assert.True(sum.Is<string>());

            sum = 1;
            Assert.True(sum.Is<int>());
        }

        [Fact]
        public void SumIsFalseTest()
        {
            SumType<int, string> sum = 1;
            Assert.False(sum.Is<string>());

            sum = "foo";
            Assert.False(sum.Is<int>());
        }

        [Fact]
        public void SumIsTrueDowncastUpcastTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string> upperSum = lowerSum;
            Assert.True(upperSum.Is<string>());
        }

        [Fact]
        public void SumIsFalseDowncastUpcastTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool> upperSum = lowerSum;
            Assert.False(upperSum.Is<string>());
            Assert.False(upperSum.Is<bool>());
        }

        [Fact]
        public void SumTryGetSucceedsTest()
        {
            SumType<int, string> sum = "foo";
            Assert.True(sum.TryGet<string>(out var stringValue));
            Assert.Equal("foo", stringValue);

            sum = 1;
            Assert.True(sum.TryGet<int>(out var intValue));
            Assert.Equal(1, intValue);
        }

        [Fact]
        public void SumTryGetFailsTest()
        {
            SumType<int, string> sum = 1;
            Assert.False(sum.TryGet<string>(out var stringValue));
            Assert.Equal(default, stringValue);

            sum = "foo";
            Assert.False(sum.TryGet<int>(out var intValue));
            Assert.Equal(default, intValue);
        }

        [Fact]
        public void SumTryGetDowncastUpcastSucceedsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string> upperSum = lowerSum;
            Assert.True(upperSum.TryGet<string>(out var stringValue));
            Assert.Equal("foo", stringValue);
        }

        [Fact]
        public void SumTryGetDowncastUpcastFailsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool> upperSum = lowerSum;
            Assert.False(upperSum.TryGet<string>(out var stringValue));
            Assert.Equal(default, stringValue);

            Assert.False(upperSum.TryGet<bool>(out var boolValue));
            Assert.Equal(default, stringValue);
        }
    }
}
