using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;

namespace RazorFromDatabase.Services
{
    public class DatabaseViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            
            throw new NotImplementedException();
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            throw new NotImplementedException();
        }
    }
}
