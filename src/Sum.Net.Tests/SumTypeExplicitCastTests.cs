// <copyright file="SumTypeExplicitCastTests.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net.Tests
{
    using System;
    using Xunit;

    public class SumTypeExplicitCastTests
    {
        [Fact]
        public void SumOneExplicitCastSuccessTest()
        {
            SumType<int> sum = 1;
            Assert.Equal(1, (int)sum);
        }

        [Fact]
        public void SumOneExplicitCastFailsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;

            Assert.Throws<InvalidCastException>(() => (int)lowerSum);
        }

        [Fact]
        public void SumTwoExplicitCastSuccessTest()
        {
            SumType<int, string> sum = "foo";
            Assert.Equal("foo", (string)sum);

            sum = 1;
            Assert.Equal(1, (int)sum);
        }

        [Fact]
        public void SumTwoExplicitCastFailsTest()
        {
            SumType<int, string> sum = "foo";
            Assert.Throws<InvalidCastException>(() => (int)sum);

            sum = 1;
            Assert.Throws<InvalidCastException>(() => (string)sum);
        }

        [Fact]
        public void SumTwoExplicitCastDowncastUpcastSucceedsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string> upperSum = lowerSum;

            Assert.Equal("foo", (string)upperSum);
        }

        [Fact]
        public void SumTwoExplicitCastDowncastUpcastFailsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool> upperSum = lowerSum;

            Assert.Throws<InvalidCastException>(() => (bool)upperSum);
        }
    }
}
