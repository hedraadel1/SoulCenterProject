//-----------------------------------------------------------------------
// <copyright file="SortingOperations.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using SoulCenterProject.DataOperations.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoulCenterProject.DataOperations.Classes
{
    public class SortingOperations : ISortingOperations
    {
        public IEnumerable<object> Sort(IEnumerable<object> data, string sortBy, bool sortDescending)
        {
            if(data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if(string.IsNullOrWhiteSpace(sortBy))
            {
                throw new ArgumentException("SortBy column cannot be null or empty.");
            }

            if(!data.Any())
            {
                return data;
            }

            if(!data.First()
                .GetType()
                .GetProperties()
                .Any(p => p.Name.Equals(sortBy, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"SortBy column '{sortBy}' does not exist in the entity.");
            }

            if(sortDescending)
            {
                return data.OrderByDescending(entity => entity.GetType().GetProperty(sortBy).GetValue(entity, null));
            } else
            {
                return data.OrderBy(entity => entity.GetType().GetProperty(sortBy).GetValue(entity, null));
            }
        }
    }
}
