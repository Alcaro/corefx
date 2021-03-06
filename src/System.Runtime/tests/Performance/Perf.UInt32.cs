﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Xunit.Performance;

namespace System.Runtime.Tests
{
    public class Perf_UInt32
    {
        [Benchmark]
        public void ToString_()
        {
            UInt32 i = new UInt32();
            foreach (var iteration in Benchmark.Iterations)
                using (iteration.StartMeasurement())
                {
                    i.ToString(); i.ToString(); i.ToString();
                    i.ToString(); i.ToString(); i.ToString();
                    i.ToString(); i.ToString(); i.ToString();
                }
        }
    }
}
