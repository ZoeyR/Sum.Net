// <copyright file="TripleSumTests.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net.Tests
{
    using System;
    using Xunit;

    public class TripleSumTests
    {
        [Fact]
        public void ExplicitCastSuccessTest()
        {
            SumType<int, string, bool> sum = 1;
            Assert.Equal(1, (int)sum);

            sum = "foo";
            Assert.Equal("foo", (string)sum);

            sum = true;
            Assert.True((bool)sum);
        }

        [Fact]
        public void ExplicitCastFailsTest()
        {
            SumType<int, string, bool> sum = 1;
            Assert.Throws<InvalidCastException>(() => (string)sum);
            Assert.Throws<InvalidCastException>(() => (bool)sum);

            sum = "foo";
            Assert.Throws<InvalidCastException>(() => (int)sum);
            Assert.Throws<InvalidCastException>(() => (bool)sum);

            sum = true;
            Assert.Throws<InvalidCastException>(() => (int)sum);
            Assert.Throws<InvalidCastException>(() => (string)sum);
        }

        [Fact]
        public void ExplicitCastDowncastUpcastSucceedsTest()
        {
            SumType<int, string, bool> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string, bool> upperSum = lowerSum;

            Assert.Equal("foo", (string)upperSum);

            sum = true;
            SumType<int, string> middleSum = sum;
            upperSum = middleSum;

            Assert.True((bool)upperSum);
        }

        [Fact]
        public void ExplicitCastDowncastUpcastFailsTest()
        {
            SumType<int, string, bool> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool, string> upperSum = lowerSum;

            Assert.Throws<InvalidCastException>(() => (bool)upperSum);
            Assert.Throws<InvalidCastException>(() => (string)upperSum);

            sum = true;
            SumType<int, string> middleSum = sum;
            SumType<int, string, float> upperSum2 = middleSum;

            Assert.Throws<InvalidCastException>(() => (bool)upperSum);
            Assert.Throws<InvalidCastException>(() => (float)upperSum);
        }

        [Fact]
        public void SumIsTrueTest()
        {
            SumType<int, string, bool> sum = 1;
            Assert.True(sum.Value is int);

            sum = "foo";
            Assert.True(sum.Value is string);

            sum = true;
            Assert.True(sum.Value is bool);
        }

        [Fact]
        public void SumIsFalseTest()
        {
            SumType<int, string, bool> sum = 1;
            Assert.False(sum.Value is string);
            Assert.False(sum.Value is bool);

            sum = "foo";
            Assert.False(sum.Value is int);
            Assert.False(sum.Value is bool);

            sum = true;
            Assert.False(sum.Value is int);
            Assert.False(sum.Value is string);
        }

        [Fact]
        public void SumIsTrueDowncastUpcastTest()
        {
            SumType<int, string, bool> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string, bool> upperSum = lowerSum;

            Assert.True(upperSum.Value is string);

            sum = true;
            SumType<int, string> middleSum = sum;
            upperSum = middleSum;

            Assert.True(upperSum.Value is bool);
        }

        [Fact]
        public void SumIsFalseDowncastUpcastTest()
        {
            SumType<int, string, bool> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool, string> upperSum = lowerSum;
            Assert.True(upperSum.Value is string);
            Assert.False(upperSum.Value is bool);

            sum = true;
            SumType<int, string> middleSum = sum;
            SumType<int, string, float> upperSum2 = middleSum;

            Assert.False(upperSum2.Value is string);
            Assert.False(upperSum2.Value is float);
        }
    }
}
