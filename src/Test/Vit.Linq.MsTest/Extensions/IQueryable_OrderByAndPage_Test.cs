﻿using Vit.Extensions.Linq_Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vit.Linq.ComponentModel;


namespace Vit.Linq.MsTest.Extensions
{
    [TestClass]
    public class IQueryable_OrderByAndPage_Test
    {

        #region Test
        [TestMethod]
        public void Test()
        {
            var query = DataSource.GetIQueryable();

            #region #1
            {
                var result = query
                    .IQueryable_OrderBy(new[] {
                        new OrderField { field = "job.departmentId", asc = false },
                        new OrderField { field = "id", asc = true }
                    })
                    .IQueryable_Page(new PageInfo { pageSize = 10, pageIndex = 1  })
                    .IQueryable_ToList<Person>();
                Assert.AreEqual(result.Count, 10);
                Assert.AreEqual(result[0].id, 990);
            }
            #endregion


            #region #2
            {
                var result = query
                    .IQueryable_OrderBy("id", false)
                    .IQueryable_Page(pageSize: 10, pageIndex: 2)
                    .IQueryable_ToList<Person>();
                Assert.AreEqual(result.Count, 10);
                Assert.AreEqual(result[0].id, 989);
            }
            #endregion


            #region #3
            {
                var result = query
                    .IQueryable_OrderBy(new[] {
                        new OrderField { field = "job.departmentId", asc = false },
                        new OrderField { field = "id", asc = true }
                    })
                    .IQueryable_ToPageData<Person>(new PageInfo { pageSize = 10, pageIndex = 1 });

                Assert.AreEqual(result.totalCount, 1000);
                Assert.AreEqual(result.items.Count, 10);
                Assert.AreEqual(result.items[0].id, 990);
            }
            #endregion

        }
        #endregion


    }
}
