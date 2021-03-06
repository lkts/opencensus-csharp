﻿// <copyright file="TestClock.cs" company="OpenCensus Authors">
// Copyright 2018, OpenCensus Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace OpenCensus.Testing.Common
{
    using OpenCensus.Common;

    public class TestClock : IClock
    {
        private Timestamp currentTime = Timestamp.Create(1493419949, 223123400);
        private object _lck = new object();

        public static TestClock Create()
        {
            return new TestClock();
        }

        public static TestClock Create(Timestamp time)
        {
            TestClock clock = new TestClock();
            clock.Time = time;
            return clock;
        }

        public Timestamp Time
        {
            get
            {
                lock (_lck) { return currentTime; }

            }

            set
            {
                lock (_lck) { currentTime = value; }
            }

        }

        public void AdvanceTime(Duration duration)
        {
            lock (_lck) { currentTime = currentTime.AddDuration(duration); }
        }

        public Timestamp Now
        {
            get
            {
                lock (_lck) { return currentTime; }
            }
        }

        private TestClock() { }
    }
}
