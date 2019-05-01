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
            Assert.True(sum.Value is string);

            sum = 1;
            Assert.True(sum.Value is int);
        }

        [Fact]
        public void SumIsFalseTest()
        {
            SumType<int, string> sum = 1;
            Assert.False(sum.Value is string);

            sum = "foo";
            Assert.False(sum.Value is int);
        }

        [Fact]
        public void SumIsTrueDowncastUpcastTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string> upperSum = lowerSum;
            Assert.True(upperSum.Value is string);
        }

        [Fact]
        public void SumIsFalseDowncastUpcastTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool> upperSum = lowerSum;
            Assert.True(upperSum.Value is string);
            Assert.False(upperSum.Value is bool);
        }
    }
}
