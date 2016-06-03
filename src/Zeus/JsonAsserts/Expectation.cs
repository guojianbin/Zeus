﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.JsonAsserts
{
    public class Expectation
    {
        public string Query { get; private set; }

        public string Assertion { get; private set; }

        public string ExpectedValue { get; private set; }

        public Expectation(string query, string assertion, string expectedValue)
        {
            Query = query;
            Assertion = assertion;
            ExpectedValue = expectedValue;
        }
        public static Expectation Parse(string expectation)
        {
            var parts = expectation.Split(':');
            var query = parts[0];
            var assertion = parts[1];
            var expectedValue = parts[2];
            return new Expectation(query, assertion, expectedValue);
        }

        public Queue<string> QueryParts { get { return new Queue<string>(Query.Split('.')); } }
    }
}