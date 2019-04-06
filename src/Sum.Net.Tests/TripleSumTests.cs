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
            Assert.True(sum.Is<int>());

            sum = "foo";
            Assert.True(sum.Is<string>());

            sum = true;
            Assert.True(sum.Is<bool>());
        }

        [Fact]
        public void SumIsFalseTest()
        {
            SumType<int, string, bool> sum = 1;
            Assert.False(sum.Is<string>());
            Assert.False(sum.Is<bool>());

            sum = "foo";
            Assert.False(sum.Is<int>());
            Assert.False(sum.Is<bool>());

            sum = true;
            Assert.False(sum.Is<int>());
            Assert.False(sum.Is<string>());
        }

        [Fact]
        public void SumIsTrueDowncastUpcastTest()
        {
            SumType<int, string, bool> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string, bool> upperSum = lowerSum;

            Assert.True(upperSum.Is<string>());

            sum = true;
            SumType<int, string> middleSum = sum;
            upperSum = middleSum;

            Assert.True(upperSum.Is<bool>());
        }

        [Fact]
        public void SumIsFalseDowncastUpcastTest()
        {
            SumType<int, string, bool> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool, string> upperSum = lowerSum;
            Assert.False(upperSum.Is<string>());
            Assert.False(upperSum.Is<bool>());

            sum = true;
            SumType<int, string> middleSum = sum;
            SumType<int, string, float> upperSum2 = middleSum;

            Assert.False(upperSum.Is<string>());
            Assert.False(upperSum.Is<float>());
        }

        [Fact]
        public void SumTryGetSucceedsTest()
        {
            SumType<int, string, bool> sum = 1;
            Assert.True(sum.TryGet<int>(out var intValue));
            Assert.Equal(1, intValue);

            sum = "foo";
            Assert.True(sum.TryGet<string>(out var stringValue));
            Assert.Equal("foo", stringValue);

            sum = true;
            Assert.True(sum.TryGet<bool>(out var boolValue));
            Assert.True(boolValue);
        }

        [Fact]
        public void SumTryGetFailsTest()
        {
            bool boolValue;
            string stringValue;
            int intValue;

            SumType<int, string, bool> sum = 1;
            Assert.False(sum.TryGet<string>(out stringValue));
            Assert.Equal(default, stringValue);
            Assert.False(sum.TryGet<bool>(out boolValue));
            Assert.Equal(default, boolValue);

            sum = "foo";
            Assert.False(sum.TryGet<int>(out intValue));
            Assert.Equal(default, intValue);
            Assert.False(sum.TryGet<bool>(out boolValue));
            Assert.Equal(default, boolValue);

            sum = true;
            Assert.False(sum.TryGet<int>(out intValue));
            Assert.Equal(default, intValue);
            Assert.False(sum.TryGet<string>(out stringValue));
            Assert.Equal(default, stringValue);
        }

        [Fact]
        public void SumTryGetDowncastUpcastSucceedsTest()
        {
            SumType<int, string, bool> sum = true;
            SumType<int> lowerSum = sum;
            SumType<int, string, bool> upperSum = lowerSum;
            Assert.True(upperSum.TryGet<bool>(out var boolValue));
            Assert.True(boolValue);

            sum = true;
            SumType<int, string> middleSum = sum;
            upperSum = middleSum;
            Assert.True(upperSum.TryGet<bool>(out boolValue));
            Assert.True(boolValue);
        }

        [Fact]
        public void SumTryGetDowncastUpcastFailsTest()
        {
            SumType<int, string, bool> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool, string> upperSum = lowerSum;
            Assert.False(upperSum.TryGet<string>(out var stringValue));
            Assert.Equal(default, stringValue);
            Assert.False(upperSum.TryGet<bool>(out var boolValue));
            Assert.Equal(default, stringValue);

            sum = true;
            SumType<int, string> middleSum = sum;
            SumType<int, string, float> upperSum2 = lowerSum;
            Assert.False(upperSum.TryGet<float>(out var floatValue));
            Assert.Equal(default, floatValue);
            Assert.False(upperSum.TryGet<bool>(out boolValue));
            Assert.Equal(default, stringValue);
        }
    }
}
