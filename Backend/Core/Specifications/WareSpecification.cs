﻿using Ardalis.Specification;
using Core.DTO;
using Core.DTO.PaginationFilter;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public static class WareSpecification
    {
        internal class GetByTitleAndCreatorId : Specification<Ware>,
                                                ISingleResultSpecification<Ware>
        {
            public GetByTitleAndCreatorId(
                string wareTitle,
                string creatorId)
            {
                Query.Where(w => w.Title == wareTitle &&
                            w.CreatorId == creatorId)
                     .AsNoTracking();
            }
        }

        internal class GetPage : Specification<Ware>
        {
            public GetPage(
                PaginationFilterDTO paginationFilter)
            {
                Query.Where(w => w.AvailableCount > 0)
                     .Include(w => w.Category)
                     .OrderBy(w => w.Title)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize)
                     .AsNoTracking();
            }
        }

        internal class GetAllByCategoryTitle : Specification<Ware>
        {
            public GetAllByCategoryTitle(
                string categoryTitle)
            {
                Query.Where(w => w.Category.Title == categoryTitle)
                     .AsNoTracking();
            }
        }

        internal class GetByCategory : Specification<Ware>
        {
            public GetByCategory(PaginationFilterWareDTO paginationFilter)
            {
                Query.Where(w => w.Category.Title == paginationFilter.CategoryTitle)
                     .Include(c => c.Category)
                     .OrderBy(w => w.Title)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize)
                     .AsNoTracking();
            }
        }

        internal class GetById : Specification<Ware>,
                                 ISingleResultSpecification<Ware>
        {
            public GetById(int id)
            {
                Query.Where(w => w.Id == id)
                     .Include(w => w.Creator)
                     .Include(w => w.Category)
                     .Include(w => w.Characteristics)
                     .AsNoTracking();
            }
        }

        internal class GetByCreatorId : Specification<Ware>,
                                        ISingleResultSpecification<Ware>
        {
            public GetByCreatorId(
                PaginationFilterDTO paginationFilter,
                string creatorId)
            {
                Query.Where(w => w.CreatorId == creatorId)
                     .Include(w => w.Creator)
                     .Include(w => w.Category)
                     .Include(w => w.Characteristics)
                     .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                     .Take(paginationFilter.PageSize)
                     .AsNoTracking();
            }
        }
    }
}
