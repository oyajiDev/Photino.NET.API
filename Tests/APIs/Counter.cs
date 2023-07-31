
using System;
using System.Collections.Generic;
using PhotinoNET.Responses;


namespace Photino.NET.API.Tests.APIs {
    public class Counter {
        private Int32 count = 0;

        public JsonResponse CountUp() {
            this.count += 1;

            return new JsonResponse{ { "count", this.count } };
        }

        public JsonResponse CountDown() {
            if (this.count > 0) {
                this.count -= 1;
            }

            return new JsonResponse{ { "count", this.count } };
        }
    }
}
