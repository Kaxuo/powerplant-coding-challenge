using System.Globalization;
using System.IO;
using System;
using Xunit;
using powerplant_coding_challenge.Methods;
using powerplant_coding_challenge.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit.Abstractions;
using System.Linq;

namespace powerplant_coding_challenge.Tests
{
    public class GetPowerTest
    {
        private readonly ITestOutputHelper output;
        public GetPowerTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Theory]
        [InlineData("/example_payloads/payload1.json")]
        [InlineData("/example_payloads/payload2.json")]
        [InlineData("/example_payloads/payload3.json")]
        [InlineData("/example_payloads/payload4.json")]
        [InlineData("/example_payloads/payload5.json")]
        public void TestPayloads(string path)
        {
            List<Payload> payload = new List<Payload>();
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + path))
            {
                string json = r.ReadToEnd();
                payload = JsonConvert.DeserializeObject<List<Payload>>(json);
            }
            var result = GetPower.GetPowerUsage(payload[0].powerplants, payload[0].load, payload[0].fuels.Wind);
            var totalPower = result.Select(powerplant => powerplant.power);
            Assert.Equal(payload[0].load, totalPower.Sum());
            foreach (var powerplant in result)
            {
                Assert.True(powerplant.power <= powerplant.pmax);
                Assert.True(powerplant.power >= powerplant.pmin || powerplant.power == 0);
            }
        }
    }
}