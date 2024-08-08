//-----------------------------------------------------------------------
// <copyright file="FilteringOperations.cs" company="Onoo">
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
    public class FilteringOperations : IFilteringOperations
    {
        public IEnumerable<object> GetWhere(IEnumerable<object> data, IEnumerable<IFilter> filters)
        {
            if(data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if(filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            IEnumerable<object> filteredData = data;

            foreach(var filter in filters)
            {
                filteredData = filteredData.Where(entity => filter.Apply(entity));
            }

            return filteredData;
        }
    }
}
