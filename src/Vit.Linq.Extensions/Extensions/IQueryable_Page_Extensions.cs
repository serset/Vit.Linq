﻿using System.Linq;
using System.Runtime.CompilerServices;

using Vit.Core.Util.ComponentModel.Data;

namespace Vit.Extensions.Linq_Extensions
{

    public static partial class IQueryable_Page_Extensions
    {


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable IQueryable_Page(this IQueryable query, PageInfo page)
        {
            if (query == null || page == null) return query;

            return query.IQueryable_Page(page.pageIndex, page.pageSize);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex">start from 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable IQueryable_Page(this IQueryable query, int pageIndex, int pageSize)
        {
            return query.IQueryable_Skip((pageIndex - 1) * pageSize).IQueryable_Take(pageSize);
        }
    }
}