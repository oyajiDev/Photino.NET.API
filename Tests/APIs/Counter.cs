
using System;
using System.Collections.Generic;


namespace Photino.NET.API.Tests.APIs {
    public class Counter {
        private Int32 count = 0;

        public Dictionary<String, dynamic> CountUp() {
            this.count += 1;

            return new Dictionary<String, dynamic>(){ { "count", this.count } };
        }

        public Dictionary<String, dynamic> CountDown() {
            if (this.count > 0) {
                this.count -= 1;
            }

            return new Dictionary<String, dynamic>(){ { "count", this.count } };
        }
    }
}
