﻿using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Model.Paging
{
    public class PaginatedResult<T>
    {
        public int CurrentItemCount { get; set; }
        public int ItemsPerPage { get; set; }
        public long TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();

        public PaginatedResult(List<T> items, int pageIndex, long totalCount, int pageSize)
        {
            var totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items;
            PageIndex = pageIndex;
            TotalItems = totalCount;
            TotalPages = totalPage;
            ItemsPerPage = pageSize;
            CurrentItemCount = items.Count;
        }

        public static async Task<PaginatedResult<T>> CreatePaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<T>(items, pageIndex, count, pageSize);
        }

        public static async Task<PaginatedResult<T>> CreatePaginatedListMongo(IFindFluent<T, T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountDocumentsAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            return new PaginatedResult<T>(items, pageIndex, count, pageSize);
        }
    }
}