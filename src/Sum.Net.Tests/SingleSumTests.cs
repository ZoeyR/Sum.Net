// <copyright file="SingleSumTests.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class SingleSumTests
    {
        [Fact]
        public void ExplicitCastSuccessTest()
        {
            SumType<int> sum = 1;
            Assert.Equal(1, (int)sum);
        }

        [Fact]
        public void ExplicitCastFailsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;

            Assert.Throws<InvalidCastException>(() => (int)lowerSum);
        }

        [Fact]
        public void SumIsTrueTest()
        {
            SumType<int> sum = 1;
            Assert.True(sum.Value is int);
        }

        [Fact]
        public void SumIsFalseTest()
        {
            SumType<int> sum = 1;
            Assert.False(sum.Value is string);
        }

        [Fact]
        public void SumIsFalseDowncastTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            Assert.True(lowerSum.Value is string);
        }
    }
}
