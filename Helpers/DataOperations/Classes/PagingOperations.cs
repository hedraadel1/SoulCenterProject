//-----------------------------------------------------------------------
// <copyright file="PagingOperations.cs" company="Onoo">
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
    public class PagingOperations : IPagingOperations
    {
        public IEnumerable<object> Page(IEnumerable<object> data, int pageNumber, int pageSize)
        {
            if(data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if(pageNumber < 1)
            {
                throw new ArgumentException("Page number cannot be less than 1.");
            }

            if(pageSize < 1)
            {
                throw new ArgumentException("Page size cannot be less than 1.");
            }

            return data.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
