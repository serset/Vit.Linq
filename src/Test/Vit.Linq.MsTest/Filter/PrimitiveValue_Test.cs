﻿using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vit.Core.Module.Serialization;
using Vit.Linq.Filter;
using Vit.Extensions.Linq_Extensions;
using Newtonsoft.Json.Linq;
using Vit.Linq.ComponentModel;

namespace Vit.Linq.MsTest.Filter
{

    [TestClass]
    public class PrimitiveValue_Test
    {
        [TestMethod]
        public void Test_PrimitiveValue()
        {
            {
                var service = new FilterService();
                service.GetPrimitiveValue = (object? valueInRule, IFilterRule rule, Type valueType) =>
                {
                    // to deal with null value
                    if (valueInRule is JValue jv) return (true, jv.Value);
                    if (valueInRule is string str && rule.@operator?.Contains("in", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        return (true, str.Split(',').ToList());
                    }
                    return (false, null);
                };

                var query = DataSource.GetQueryable();

                var strRule = "{'field':'name',  'operator': 'in',  'value':'name3,name4' }".Replace("'", "\"");
                var rule = Json.Deserialize<FilterRule>(strRule);
                var result = query.Where(rule, service).ToList();
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("name3", result[0].name);
                Assert.AreEqual("name4", result[1].name);
            }
        }
    }
}
