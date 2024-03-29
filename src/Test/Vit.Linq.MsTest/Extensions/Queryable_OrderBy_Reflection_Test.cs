﻿using Vit.Extensions.Linq_Extensions;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vit.Linq.ComponentModel;

namespace Vit.Linq.MsTest.Extensions
{
    [TestClass]
    public class Queryable_OrderBy_Reflection_Test
    {

        #region Test
        [TestMethod]
        public void Test()
        {
            var query = DataSource.GetQueryable();

            #region #1
            {
                var result = query
                    .OrderBy_Reflection(new[] {
                        new OrderField { field = "job.departmentId", asc = false },
                        new OrderField { field = "id", asc = true }
                    })
                    .Page(new PageInfo { pageSize = 10, pageIndex = 1 })
                    .ToList();
                Assert.AreEqual(result.Count, 10);
                Assert.AreEqual(result[0].id, 990);
            }
            #endregion


            #region #2
            {
                var result = query
                    .OrderBy_Reflection("id", false)
                    .Page(pageSize: 10, pageIndex: 2)
                    .ToList();
                Assert.AreEqual(result.Count, 10);
                Assert.AreEqual(result[0].id, 989);
            }
            #endregion


            #region #3
            {
                var result = query
                    .OrderBy_Reflection(new[] {
                        new OrderField { field = "job.departmentId", asc = false },
                        new OrderField { field = "id", asc = true }
                    })
                    .ToPageData(new PageInfo { pageSize = 10, pageIndex = 1 });

                Assert.AreEqual(result.totalCount, 1000);
                Assert.AreEqual(result.items.Count, 10);
                Assert.AreEqual(result.items[0].id, 990);
            }
            #endregion


        }
        #endregion

    }
}
